using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class TileBehavior : MonoBehaviour
{

    [SerializeField]
    private GameObject railPrefab;
    [SerializeField]
    private GameObject railTurnPrefab;
    Vector3 additionnalHeight = new Vector3(0f, 1f, 0f);

    public void CreateRail(Quaternion rotation)
    {
        GameObject rail = Instantiate(railPrefab, transform.position + additionnalHeight, rotation);
        

    }

    public void CreateTurnRail(Quaternion rotation)
    {
        Instantiate(railTurnPrefab, transform.position + additionnalHeight, rotation);
    }


    public void CheckPath(Vector2 currentPos, Vector2 nextPos, Vector2 lastPos)
    {

        Vector2 turnDir = nextPos - currentPos;
        Quaternion rotation = Quaternion.identity;

            
                if (turnDir == new Vector2(1, 0))
                {
                    rotation = Quaternion.Euler(0f, -90f, 0f);
                    CreateTurnRail(rotation);
                    return;
                }
                else if (turnDir == new Vector2(0, 1))
                {
                    rotation = Quaternion.Euler(0f, 90f, 0f);
                    CreateTurnRail(rotation);
                    return;
                 }
                else if (turnDir == new Vector2(-1, 1) || turnDir == new Vector2(1, -1))
                {
                    CreateTurnRail(rotation);
                     return;

                }
            

            if (currentPos.x < nextPos.x)
            {
                // Direction "bas"
                rotation = Quaternion.Euler(0f, 0f, 0f); // 
                CreateRail(rotation);

            }
            else if (currentPos.x > nextPos.x)
            {
                // Direction "haut"
                rotation = Quaternion.Euler(0f, 0f, 0f); // Pas de rotation
                CreateRail(rotation);

            }
            else if (currentPos.y < nextPos.y)
            {
                // Direction "droite"
                rotation = Quaternion.Euler(0f, 90f, 0f); // Rotation 90° autour de l'axe Y
                CreateRail(rotation);

            }
            else if (currentPos.y > nextPos.y)
            {
                // Direction "gauche"
                CreateRail(rotation);

            }





        }
    }
    //var monrail

    //connaitre son rail
    //on m'ajoute une direction
    //on m'enlève une direction

    //update le rail


