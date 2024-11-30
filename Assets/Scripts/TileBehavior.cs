using UnityEngine;
using UnityEngine.UIElements;


public class TileBehavior : MonoBehaviour
{

    [SerializeField]
    private GameObject railPrefab;

    public void CreateRail()
    {

        Vector3 additionnalHeight = new Vector3(0f, 1f, 0f);
        GameObject rail = Instantiate(railPrefab, transform.position + additionnalHeight, Quaternion.identity);
        


    }

    //var monrail

    //connaitre son rail
    //on m'ajoute une direction
    //on m'enlève une direction

    //update le rail

}
