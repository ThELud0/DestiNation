using System.IO;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEditor;

public class TrainStation : MonoBehaviour
{
    private bool onPathMaking;    
    [SerializeField] private GameObject rail;
    void Start()
    {
        onPathMaking = false;
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && onPathMaking) // Bouton gauche de la souris
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 clickPosition = hit.point;
                PathCreation(transform.position, clickPosition);
                Debug.Log($"Clic détecté à : {clickPosition}");
            }
        }
    }

    void OnMouseDown(){
        Invoke("PathMake",1);
    }


    void PathMake(){
        onPathMaking = true;
    }

    private void PathCreation(Vector3 start, Vector3 end){
        bool OnX = false;
        bool OnY = false;
        float StartOnAxe;
        float EndOnAxe;

        Vector3 difference = end - start;
        if (difference.x < difference.y){
            StartOnAxe = start.x;
            EndOnAxe = end.x;
            OnX = true;
            Debug.Log("OnX");
        }else{
            StartOnAxe = start.y;
            EndOnAxe = end.y;
            OnY = true;
            Debug.Log("OnY");
        }

        Vector3 railPosition = start;
        float railPositionOnAxe;
        if(OnX){
            railPositionOnAxe = railPosition.x;
        }else{
            railPositionOnAxe = railPosition.z;
        }

        while (EndOnAxe - railPositionOnAxe > 1){
            Debug.Log(railPosition);
            Instantiate(rail, railPosition, Quaternion.identity);
            if(OnX){
                railPosition += new Vector3(1,0,0);
            }else if(OnY){
                railPosition += new Vector3(0,0,1);
            }
            if(OnX){
                railPositionOnAxe = railPosition.x;
            }else{
                railPositionOnAxe = railPosition.z;
            }
        }


    }

    
}
