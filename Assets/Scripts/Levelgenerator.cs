using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
    private int floor_Y= 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilesMap = new GameObject[dim_x_block,dim_z_block];

        for(int a = 0; a < dim_x_block; a++){
           for(int b = 0; b < dim_z_block; b++){
               GameObject gbj =Instantiate(blockPrefab);
               gbj.transform.position = new Vector3(a,floor_Y, b);
               tilesMap[a,b] = gbj;
           } 
        }

        foreach(Vector2 pos_forest in list_big_forest){
            GameObject big_forest = Instantiate(big_forest_prefab);
            big_forest.transform.position = new Vector3(pos_forest.x,floor_Y+1, pos_forest.y);
        }

        foreach(Vector2 pos_mountain in list_mountains){
            GameObject mountain = Instantiate(moutain_prefab);
            mountain.transform.position = new Vector3(pos_mountain.x,floor_Y+1, pos_mountain.y);
        }

        //tilesMap[x1, z1].createRail(param);
        List<Vector2> listVectors = new List<Vector2> { new Vector2(1, 1), new Vector2(2, 2) };
        CheckPath(listVectors); 
    }


    public void CheckPath(List<Vector2> railway)
    {

        foreach (Vector2 pos in railway)
        {
            tilesMap[(int)pos.x, (int)pos.y].GetComponent<TileBehavior>().CreateRail();
        }



    }


}
