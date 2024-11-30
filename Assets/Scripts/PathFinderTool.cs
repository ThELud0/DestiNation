using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PathFinderTool : MonoBehaviour
{

    private Vector2 precedent_pos_mouse_pressed = null, current_pos_mouse_pressed = null;

    private Trainstation current_trainstation = null;

    public UnityEvent OnPathFound = new ();

    public UnityEvent<List<Vector2>> OnBeginPathSet = new ();

    private List<Vector2> list_tuiles_path = new ();

    private Levelgenerator tuile_manager;

    public string[] list_String_unspawnable_baby;
    

     void OnMouseDown()
   {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, out hit, 100);
        if (hits.Length > 0) {
            foreach(RaycastHit hit in hits){
                if(hit.transform.gameObject.tag == "TrainStation"){
                    trainChanged(hit.transform.gameObject.GetComponent<TrainStation>());
                    return;
                }

                if(hit.transform.gameObject.tag == "Human"){
                    pathCompleted();
                    return;
                }

                if(hit.transform.gameObject.tag == "Structure"){
                    resetpath();
                    return;
                }

                if(precedent_pos_mouse_pressed==null){
                    return;
                }
            }

            AddToPathOfTuile(hit.transform.position);
        }
   }


     bool isTileFreeForRail(Vector2 tested_vec){
        transform.position = new Vector3(tested_vec.x, floorY+3, tested_vec.y);
        Ray ray_test = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits =  Physics.RaycastAll(ray_test, 2f);
            if (hits.Length > 0)
            {
                foreach(RaycastHit hit in hits){
                                    Debug.Log("Spotted "+ hit.transform.gameObject.tag);

               foreach(string tag in list_String_unspawnable_baby){

                if(tag == hit.transform.gameObject.tag){
                     Debug.Log("Spotted a structure at baby speculation point !");

                    return false;
                }
               }
               }
        
            }else {
            }
            
            foreach(Vector2 vec in list_tuiles_path){
                if(vec==tested_vec){
                    return false;
                }
            }
            return true;
    }
   

   private int dir(float a, float b){
        return (b-a)/Mathf.abs(b-a);
   }

   private void AddToPathOfTuile(Vector3 posMouse){
        current_pos_mouse_pressed=posMouse;
        TestAddTuileToPath(new Vector2(precedent_pos_mouse_pressed.x, precedent_pos_mouse_pressed.y), new Vector2(current_pos_mouse_pressed.x, current_pos_mouse_pressed.y));

   }

  /* private void AddToPathOfTuile(Vector3 posMouse){
    current_pos_mouse_pressed=posMouse;
    for(int treshold_x=precedent_pos_mouse_pressed.x;treshold_x != current_pos_mouse_pressed.x; treshold_x = treshold_x + 1* dir(precedent_pos_mouse_pressed.x, current_pos_mouse_pressed.x) ){
        for(int treshold_z=precedent_pos_mouse_pressed.z;treshold_z != current_pos_mouse_pressed.z; treshold_z = treshold_z + 1* dir(precedent_pos_mouse_pressed.z, current_pos_mouse_pressed.z) ){
         if (tuile_manager.railCanBePlacedHere(treshold_x, treshold_z)){

        }
    }}}*/
   

   private void TestAddTuileToPath(Vector2 start, Vector2 end){
    Vector2 newStart = new Vector2(start.x, start.y);
        if (newStart.x != end.x){
                newStart.x = newStart.x + dir(newStart.x, end.x);
                if (isTileFreeForRail(newStart)){
                        list_tuiles_path.Add(newStart);
                        TestAddTuileToPath(newStart, end);
                        return;
                }
        }

        if(newStart.y != end.y)
        {
            newStart = new Vector2(start.x, start.y);
            newStart.y = newStart.y + dir(newStart.y, end.y);
                if (isTileFreeForRail(newStart)){
                        list_tuiles_path.Add(newStart);
                        TestAddTuileToPath(newStart, end);
                        return;
                }
        }

        if (newStart == end){
            pathCompleted();
            return;
        }
        resetpath();
   }

   private void resetpath(){
        list_tuiles_path.Clear();
        OnBeginPathSet.Invoke(null);
   }

   private void colorpath(){
        OnBeginPathSet.Invoke(list_tuiles_path);
   }

   private void trainChanged(GameObject newTrainStation){
        resetpath();
        OnPathFound.RemoveAllListeners();
        current_trainstation = newTrainStation;
        //OnPathFound.AddListener(current_trainstation.);
   }

    private void pathCompleted(){
        OnPathFound.Invoke();
   }

}
