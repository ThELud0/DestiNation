using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


public class BetterPathFinderTool : MonoBehaviour
{

    [SerializeField]
    private Vector2 precedent_pos_mouse_pressed, current_pos_mouse_pressed;

    private Trainstation current_trainstation = null;

    public UnityEvent<List<Vector2>> OnPathFound = new ();

    public UnityEvent<List<Vector2>> OnBeginPathSet = new ();

[SerializeField]
    private List<Vector2> list_tuiles_path_validated = new (), list_tuiles_path_tested=new (),precedent_list_tuiles_path_tested  = new ();

    private Levelgenerator tuile_manager;

    public LayerMask sol_occulter;

    //public GameObject visualitor;

    //public string[] list_String_unspawnable_baby;

    public Levelgenerator levelGeneratorInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
      private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach(Vector2 step in list_tuiles_path_tested)
        {
            Gizmos.DrawSphere(new Vector3(step.x, 3f, step.y), 0.2f);
        }
    }

    void Update(){
           if (Mouse.current.leftButton.isPressed)
        {
            OnClickOnTile();
        }
    }


   void OnClickOnTile()
   {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100, sol_occulter);

        //print("hit number : " + hits.Length);

        if (hits.Length > 0) 
        {
            foreach(RaycastHit hit in hits)
             {
                Debug.Log("the tag is "+hit.transform.gameObject.tag);
                if(hit.transform.gameObject.tag == "Trainstation"){
                    setNewTrainstationStart(hit.transform.gameObject.GetComponent<Trainstation>());
                    //Debug.Log("trainnnn");
                    return;
                }




             }
                print("on passe au path");
                //here, we are on an empty sol, all other cases have returned
                getPathFromPrecedentPointToMouse(hits[0].transform.position);

        }

   

                /*if(hit.transform.gameObject.tag == "Human"){
                    Vector2 pos_Human = new Vector2(hit.transform.position.x, hit.transform.position.z);
                    Vector2 diff = (pos_Human - list_tuiles_path[list_tuiles_path.Count-1]).normalized;
                    if (diff== new Vector2(0,1) || diff==new Vector2(1,0)){
                    pathCompleted();
                    Debug.Log("baaby");
                    }


                    return;
                }

                if(hit.transform.gameObject.tag == "Structure"){
                    resetpath();
                    Debug.Log("on annulle");

                    return;
                }

                // if(precedent_pos_mouse_pressed==Vector2.zero){
                //     return;
                // }
            }
            Debug.Log("on add");

            if (current_trainstation) 
            {
                Vector3 hitPosition = hits[0].transform.position;
                Vector2 hitCoordinates =  new Vector3 (hitPosition.x, hitPosition.z);

                Vector2 direction = (hitCoordinates - precedent_pos_mouse_pressed).normalized;
                

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    direction = new Vector2(direction.x/Mathf.Abs(direction.x), 0);
                }
                else 
                {
                    direction = new Vector2(0, direction.y/Mathf.Abs(direction.y));
                }

                int depth = Mathf.Abs(direction.x != 0 
                ? Mathf.RoundToInt(precedent_pos_mouse_pressed.x - hitCoordinates.x) 
                : Mathf.RoundToInt(precedent_pos_mouse_pressed.y - hitCoordinates.y));

                AddTuileToPathWithoutPathFinding(direction, depth);
            }
        }*/
   }

   private List<Vector2> getPathFromPrecedentPointToMouse(Vector3 pos_clicked){
        current_pos_mouse_pressed = GameTools.get2Dfrom3DVector(pos_clicked);
        list_tuiles_path_tested.Clear();
        recursiveGetPath(precedent_pos_mouse_pressed, current_pos_mouse_pressed);
        return null;
   }

   private bool isTileForRailCanBePlaced(Vector2 pos_tested)
   {
        return levelGeneratorInstance.getStateFromTile((int)pos_tested.x, (int)pos_tested.y)==4;
   }

   private List<Vector2> recursiveGetPath(Vector2 start, Vector2 end)
   {
        if(start.x != end.x){
            float start_next_step = start.x + dir_normalised(start.x, end.x);
            Vector2 pos_tested = new Vector2((int)start_next_step, (int)start.y);
            if (isTileForRailCanBePlaced(pos_tested))
            {
                list_tuiles_path_tested.Add(pos_tested);
                return recursiveGetPath(pos_tested, end);
            }
        }

        if(start.y != end.y){
            float start_next_step = start.y + dir_normalised(start.y, end.y);
            Vector2 pos_tested = new Vector2(start.x, start_next_step);
            if (isTileForRailCanBePlaced(pos_tested))
            {
                list_tuiles_path_tested.Add(pos_tested);
                return recursiveGetPath(pos_tested, end);
            }
        }

        if (start == end){
            return list_tuiles_path_tested;
        }

        return null;
   }

    private float dir_normalised(float start, float end){
        return (end-start)/Mathf.Abs(end-start);
   }


   private void setNewTrainstationStart(Trainstation newTrainStation){
        //resetpath();
        OnPathFound.RemoveAllListeners();
        current_trainstation = newTrainStation;
        precedent_pos_mouse_pressed = GameTools.get2Dfrom3DVector(current_trainstation.getDeparturePosition());
        OnPathFound.AddListener(current_trainstation.spawnTrain);
   }

    

 
}
