using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;


public class Levelgenerator : MonoBehaviour
{
    public GameObject[,] tilesMap;

    [SerializeField]
    private GameObject blockPrefab, big_forest_prefab, moutain_prefab;

    [SerializeField]
    private int dim_x_block, dim_z_block;


    [SerializeField]
    private Vector2[] list_big_forest, list_mountains;


    [SerializeField]
    private int floor_Y = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilesMap = new GameObject[dim_x_block, dim_z_block];

        for (int a = 0; a < dim_x_block; a++)
        {
            for (int b = 0; b < dim_z_block; b++)
            {
                GameObject gbj = Instantiate(blockPrefab);
                gbj.transform.position = new Vector3(a, floor_Y, b);
                tilesMap[a, b] = gbj;
            }
        }

        foreach (Vector2 pos_forest in list_big_forest)
        {
            GameObject big_forest = Instantiate(big_forest_prefab);
            big_forest.transform.position = new Vector3(pos_forest.x, floor_Y + 1, pos_forest.y);
        }

        foreach (Vector2 pos_mountain in list_mountains)
        {
            GameObject mountain = Instantiate(moutain_prefab);
            mountain.transform.position = new Vector3(pos_mountain.x, floor_Y + 1, pos_mountain.y);
        }

        //tilesMap[x1, z1].createRail(param);
        List<Vector2> listVectors = new List<Vector2> { new Vector2(1, 1), new Vector2(1, 2),
            new Vector2(1, 3), new Vector2(2, 3), new Vector2(3, 3),
            new Vector2(3, 4), new Vector2(3, 5), new Vector2(3, 6), new Vector2(4, 6),
            new Vector2(4, 5), new Vector2(3, 5), new Vector2(2,5), new Vector2(2, 6)

        };
        CheckCurrent(listVectors);
        DeleteCurrent(listVectors);
    }

    public void destroyCurrentRail(){
        
    }


    public void CheckCurrent(List<Vector2> railway)
    {

        for (int i = 0; i < railway.Count - 1; i++)
        {
            Vector2 currentPos = railway[i];
            Vector2 nextPos = railway[i + 1];
            Quaternion rotation = Quaternion.identity;

            if (i > 0)
            {
                Vector2 previousPos = railway[i - 1];
                tilesMap[(int)currentPos.x, (int)currentPos.y].GetComponent<TileBehavior>().CheckPath(currentPos, nextPos, previousPos);

            }

        }


    }

    public void DeleteCurrent(List<Vector2> railway)
    {

        for (int i = 0; i < railway.Count - 1; i++)
        {
            Vector2 currentPos = railway[i];
            Vector2 nextPos = railway[i + 1];
            Quaternion rotation = Quaternion.identity;

            if (i > 0)
            {
                Vector2 previousPos = railway[i - 1];
                tilesMap[(int)currentPos.x, (int)currentPos.y].GetComponent<TileBehavior>().DeleteRail();

            }

        }

   
    }

}



