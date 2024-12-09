using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PathFinderTool : MonoBehaviour
{

    private Vector2 precedent_pos_mouse_pressed, current_pos_mouse_pressed;

    private Trainstation current_trainstation = null;

    public UnityEvent<List<Vector2>> OnPathFound = new ();

    public UnityEvent<List<Vector2>> OnBeginPathSet = new ();

    private List<Vector2> list_tuiles_path = new ();

    private Levelgenerator tuile_manager;

    public LayerMask unspawnableZone;

    public GameObject visualitor;

    public string[] list_String_unspawnable_baby;

    public Levelgenerator levelGeneratorInstance;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach(Vector2 step in list_tuiles_path)
        {
            Gizmos.DrawSphere(new Vector3(step.x, 3f, step.y), 0.2f);
        }
    }

void Update(){
    if (Mouse.current.leftButton.wasPressedThisFrame)
        {
      //mouse_clicked_on_something();
            
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnClickOnTile();
        }
}


   /*  void mouse_clicked_on_something()
   {

        Debug.Log("mouuuuse");
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100, unspawnableZone);
        if (hits.Length > 0) 
        {
            foreach(RaycastHit hit in hits)
             {
                Debug.Log("the tag is "+hit.transform.gameObject.tag);
                if(hit.transform.gameObject.tag == "Trainstation"){
                    trainChanged(hit.transform.gameObject.GetComponent<Trainstation>());
                    Debug.Log("trainnnn");
                    return;
                }

                if(hit.transform.gameObject.tag == "Human"){
                    AddToPathOfTuile(hit.transform.position, true);
                    Debug.Log("baaby");

                    return;
                }

                if(hit.transform.gameObject.tag == "Structure"){
                    resetpath();
                    Debug.Log("on annulle");

                    return;
                }

                if(precedent_pos_mouse_pressed==Vector2.zero){
                    return;
                }
            }

        }*/
       /* RaycastHit hit2;
        if(Physics.Raycast(ray, out hit2, 100)){
            Debug.Log("begin Path");

            AddToPathOfTuile(hit2.transform.position, false);
        }else{
            Debug.Log("not detected");
        }*/
   //}


   void OnClickOnTile()
   {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100, unspawnableZone);

        print("hit number : " + hits.Length);

        if (hits.Length > 0) 
        {
            foreach(RaycastHit hit in hits)
             {
                Debug.Log("the tag is "+hit.transform.gameObject.tag);
                if(hit.transform.gameObject.tag == "Trainstation"){
                    trainChanged(hit.transform.gameObject.GetComponent<Trainstation>());
                    Debug.Log("trainnnn");
                    return;
                }

                if(hit.transform.gameObject.tag == "Human"){
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
        }
   }

   void AddTuileToPathWithoutPathFinding(Vector2 direction, int depth)
   { 
        for (int i = 1; i <= depth; i++)
        {
            Vector2 position = precedent_pos_mouse_pressed + direction ;
            if (!isTileFreeForRail(position)) break;
            precedent_pos_mouse_pressed = position;
            list_tuiles_path.Add(position);
            if(isAHuman(position)) 
            {
                pathCompleted();
                return;
            }
        }
   }


   bool isAHuman(Vector2 tested_vec)
     {
        Vector3 origin = new Vector3(tested_vec.x, 3, tested_vec.y);
        Ray ray_test = new Ray(origin, Vector3.down);
        RaycastHit[] hits =  Physics.RaycastAll(ray_test, 100f, unspawnableZone);

        Debug.DrawLine(origin, origin + Vector3.down * 100, Color.red, 10);
        print(hits.Length);
        if (hits.Length > 0)
        {
            foreach(RaycastHit hit in hits)
            {
                Debug.Log("Ssssspotted "+ hit.transform.gameObject.tag);

                

                    if(hit.transform.gameObject.TryGetComponent<Human>(out Human h))
                    {
                        Debug.Log("Structure block");
                        return true;
                    }
                
            }
    
        }
        return false;
    }
   


     bool isTileFreeForRail(Vector2 tested_vec)
     {
        Vector3 origin = new Vector3(tested_vec.x, 3, tested_vec.y);
        Ray ray_test = new Ray(origin, Vector3.down);
        RaycastHit[] hits =  Physics.RaycastAll(ray_test, 100f, unspawnableZone);

        Debug.DrawLine(origin, origin + Vector3.down * 100, Color.red, 10);
        print(hits.Length);
        if (hits.Length > 0)
        {
            foreach(RaycastHit hit in hits)
            {
                Debug.Log("Ssssspotted "+ hit.transform.gameObject.tag);

                foreach(string tag in list_String_unspawnable_baby)
                {

                    if(tag == hit.transform.gameObject.tag)
                    {
                        Debug.Log("Structure block");
                        return false;
                    }
                }
            }
    
        }
        foreach(Vector2 vec in list_tuiles_path){
            if(vec==tested_vec){
                return false;
            }
        }
        return true;
    }
   

   private float dir(float a, float b){
        return (b-a)/Mathf.Abs(b-a);
   }

   private void AddToPathOfTuile(Vector3 posMouse, bool isEnd = true){

    
                if(precedent_pos_mouse_pressed==Vector2.zero){
                    return;
                }
        current_pos_mouse_pressed=new Vector2(posMouse.x, posMouse.z);
        TestAddTuileToPath(new Vector2(precedent_pos_mouse_pressed.x, precedent_pos_mouse_pressed.y), new Vector2(current_pos_mouse_pressed.x, current_pos_mouse_pressed.y));
        if (isEnd){
            pathCompleted();
            return;
        }else{
            precedent_pos_mouse_pressed=current_pos_mouse_pressed;
        }
        return;

   }

  /* private void AddToPathOfTuile(Vector3 posMouse){
    current_pos_mouse_pressed=posMouse;
    for(int treshold_x=precedent_pos_mouse_pressed.x;treshold_x != current_pos_mouse_pressed.x; treshold_x = treshold_x + 1* dir(precedent_pos_mouse_pressed.x, current_pos_mouse_pressed.x) ){
        for(int treshold_z=precedent_pos_mouse_pressed.z;treshold_z != current_pos_mouse_pressed.z; treshold_z = treshold_z + 1* dir(precedent_pos_mouse_pressed.z, current_pos_mouse_pressed.z) ){
         if (tuile_manager.railCanBePlacedHere(treshold_x, treshold_z)){

        }
    }}}*/
   

   private void TestAddTuileToPath(Vector2 start, Vector2 end){
    Debug.Log("ok on va de "+start+" a "+end);
    Vector2 newStart = new Vector2(start.x, start.y);
        if (newStart.x != end.x){
                newStart.x = newStart.x + dir(newStart.x, end.x);
                if (isTileFreeForRail(newStart)){
                        list_tuiles_path.Add(newStart);
                        Visualize(newStart);

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
                        Visualize(newStart);
                        TestAddTuileToPath(newStart, end);
                        return;
                }
        }

        if (newStart == end){
            return;
        }
        resetpath();
   }

   private void resetpath(){
        list_tuiles_path.Clear();
        precedent_pos_mouse_pressed = Vector2.zero;
        current_pos_mouse_pressed = Vector2.zero;
        OnBeginPathSet.Invoke(null);
   }

   private void colorpath(){
        OnBeginPathSet.Invoke(list_tuiles_path);
   }

   private void trainChanged(Trainstation newTrainStation){
        resetpath();
        OnPathFound.RemoveAllListeners();
        current_trainstation = newTrainStation;
        precedent_pos_mouse_pressed = new Vector2(newTrainStation.transform.position.x, newTrainStation.transform.position.z);
        OnPathFound.AddListener(current_trainstation.spawnTrain);
   }


    public void printPath(){
        foreach(Vector2 vec in list_tuiles_path){
            Debug.Log(vec);
        }
    }

    private void Visualize(Vector2 pos){
            Instantiate(visualitor, new Vector3(pos.x, 2, pos.y), Quaternion.identity);
    }

    private void pathCompleted(){
        Debug.Log("path completed"+ list_tuiles_path);
        printPath();
        precedent_pos_mouse_pressed = Vector2.zero;
        //current_trainstation.GetComponent<Trainstation>().onTrainHasArrived.AddListener(levelGeneratorInstance.DeleteCurrent);
        OnPathFound.Invoke(list_tuiles_path);
        //levelGeneratorInstance.CheckCurrent(list_tuiles_path);
        
   }

}
