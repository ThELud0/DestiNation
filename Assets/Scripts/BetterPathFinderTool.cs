using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Linq;


public class BetterPathFinderTool : MonoBehaviour
{

    [SerializeField]
    private Vector2 precedent_pos_mouse_pressed, current_pos_mouse_pressed, start_rail;

    private bool canBuildPath = false;


    private Trainstation current_trainstation = null;

    public UnityEvent<List<Vector2>> OnPathFound = new();

    public UnityEvent<List<Vector2>> OnBeginPathSet = new();

    [SerializeField]
    private List<Vector2> list_tuiles_path_validated = new(), list_tuiles_path_tested = new(), precedent_list_tuiles_path_tested = new();

    private Levelgenerator tuile_manager;

    public LayerMask sol_occulter;

    
    public gameState gameStateInstance;

    //public GameObject visualitor;

    //public string[] list_String_unspawnable_baby;

    public Levelgenerator levelGeneratorInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (Vector2 step in list_tuiles_path_tested)
        {
            Gizmos.DrawSphere(new Vector3(step.x, 3f, step.y), 0.2f);
        }
    }

    void Update()
    {
if(gameStateInstance.isGamePaused()){
    resetPath();
    return;
}

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnClickOnTile();
        }

        if (canBuildPath)
        {
            OnMouseOverOnTile();
        }
    }


    void OnClickOnTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100, sol_occulter);

        //print("hit number : " + hits.Length);

        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                //Debug.Log("the tag is " + hit.transform.gameObject.tag);
                if (hit.transform.gameObject.tag == "Trainstation")
                {
                    setNewTrainstationStart(hit.transform.gameObject.GetComponent<Trainstation>());
                    //Debug.Log("trainnnn");
                    return;
                }

                if (hit.transform.gameObject.tag == "Human")
                {
                    if (canBuildPath)
                    {
                        bool result = getPathFromPrecedentPointToMouse(hits[0].transform.position);
                        if (result)
                        {
                            thePathHaveBeenSet();
                        }
                    }
                    //Debug.Log("trainnnn");
                    return;
                }else if(hit.transform.gameObject.tag == "Structure"){
                    resetPath();
                }




            }

            if (!canBuildPath)
            {
                return;
            }
           // print("precedent pos is now " + hits[0].transform.position);

            addCurrentPathTovalidatedAndContinuePath(hits[0].transform.position);
            //here, we are on an empty sol, all other cases have returned


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

    private void addCurrentPathTovalidatedAndContinuePath(Vector3 pos)
    {
        precedent_pos_mouse_pressed = (list_tuiles_path_tested[^1]);
         list_tuiles_path_validated = getConcatenatePath();
    }


    void OnMouseOverOnTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100, sol_occulter);

        //print("hit number : " + hits.Length);

        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                //Debug.Log(hits[0].transform.position);
                getPathFromPrecedentPointToMouse(hits[0].transform.position);
               // Debug.Log("the tag is " + hit.transform.gameObject.tag + " and " + list_tuiles_path_tested);
                if (hit.transform.gameObject.tag == "Trainstation")
                {
                    levelGeneratorInstance.colorPathToRed(getConcatenatePath());
                    return;
                }
                else if (hit.transform.gameObject.tag == "Human")
                {
                    levelGeneratorInstance.colorPathToGreen(getConcatenatePath());
                    return;
                }else if (hit.transform.gameObject.tag == "Structure")
                {
                    levelGeneratorInstance.colorPathToRed(getConcatenatePath());
                    return;
                }
                else
                {
                    levelGeneratorInstance.colorPathToYellow(getConcatenatePath());
                    
                }





            }

            /*if(!canBuildPath){
               return;
            }
               print("on passe au path");
               //here, we are on an empty sol, all other cases have returned
              getPathFromPrecedentPointToMouse(hits[0].transform.position);*/


        }
    }

    private bool getPathFromPrecedentPointToMouse(Vector3 pos_clicked)
    {
        current_pos_mouse_pressed = GameTools.get2Dfrom3DVector(pos_clicked);
        list_tuiles_path_tested.Clear();
        List<Vector2> list;
        if (list_tuiles_path_validated.Count == 0){
            start_rail =  current_trainstation.getClosestStartRailPosition(current_pos_mouse_pressed);
            list = recursiveGetPath(start_rail, current_pos_mouse_pressed);
        }else{
            //list_tuiles_path_validated.Add(precedent_pos_mouse_pressed);
            list = recursiveGetPath(precedent_pos_mouse_pressed, current_pos_mouse_pressed);
        }
        if (list != null)
        {
            levelGeneratorInstance.colorPathToYellow(list);
            return true;
        }
        //levelGeneratorInstance.colorPathToRed(list);

        return false;
    }

    private bool isTileForRailCanBePlaced(Vector2 pos_tested)
    {
        foreach(Vector2 dir in getConcatenatePath()){
            if(pos_tested == dir){
                return false;
            }
        }

        int[] zone_rail_cannot_be_placed = new int[]{2,3,4};
        foreach (int zone in zone_rail_cannot_be_placed){
               if(levelGeneratorInstance.getStateFromTile((int)pos_tested.x, (int)pos_tested.y) == zone) {
                    return false;
               }
        }
        return true;
    }

    private bool isDestination(Vector2 pos_tested){
        return levelGeneratorInstance.getStateFromTile((int)pos_tested.x, (int)pos_tested.y) == 3;
    }

    private List<Vector2> recursiveGetPath(Vector2 start, Vector2 end)
    {
        //print($"de {start} a {end}");
        if (start.x != end.x)
        {
            float start_next_step = start.x + dir_normalised(start.x, end.x);
            Vector2 pos_tested = new Vector2((int)start_next_step, (int)start.y);
            if(pos_tested==end && isDestination(end)){
                list_tuiles_path_tested.Add(pos_tested);
                return list_tuiles_path_tested;
            }
            if (isTileForRailCanBePlaced(pos_tested))
            {
                list_tuiles_path_tested.Add(pos_tested);
                return recursiveGetPath(pos_tested, end);
            }
            else
            {
               // print($"cant put rail on {pos_tested}");
            }
        }

        if (start.y != end.y)
        {
            float start_next_step = start.y + dir_normalised(start.y, end.y);
            Vector2 pos_tested = new Vector2(start.x, start_next_step);
            if(pos_tested==end && isDestination(end)){
                list_tuiles_path_tested.Add(pos_tested);
                return list_tuiles_path_tested;
            }
            if (isTileForRailCanBePlaced(pos_tested))
            {
                list_tuiles_path_tested.Add(pos_tested);
                return recursiveGetPath(pos_tested, end);
            }
            else
            {
               // print($"cant put rail on {pos_tested}");
            }
        }

        if (start == end)
        {
            //print($"finished");
            return list_tuiles_path_tested;
        }
       // print($"cancelled");
        return null;
    }

    private float dir_normalised(float start, float end)
    {
        return (end - start) / Mathf.Abs(end - start);
    }


    private void setNewTrainstationStart(Trainstation newTrainStation)
    {

        if (current_trainstation!=null){
            //current_trainstation.onTrainHasArrived.RemoveAllListeners();

        }
        //resetpath();
        OnPathFound.RemoveAllListeners();
        /*if(newTrainStation.occupied){
            resetPath();
            return;
        }*/
        current_trainstation = newTrainStation;

        canBuildPath = true;
        precedent_pos_mouse_pressed = GameTools.get2Dfrom3DVector(current_trainstation.getDeparturePosition());
        OnPathFound.AddListener(current_trainstation.spawnTrain);
       // list_tuiles_path_validated.Add(precedent_pos_mouse_pressed);
    }

    private List<Vector2> getConcatenatePath()
    {
        return list_tuiles_path_validated.Union<Vector2>(list_tuiles_path_tested).ToList<Vector2>();

    }

    private void thePathHaveBeenSet()
    {
        Debug.Log("TCHOU THCOOOOOOOU");
        list_tuiles_path_validated.Insert(0, GameTools.get2Dfrom3DVector(current_trainstation.transform.position));
        gameStateInstance.OnRailPlaced();
        list_tuiles_path_validated =  getConcatenatePath();
        List<Vector2> l = new();
        levelGeneratorInstance.colorPath(l, 0);
        //levelGeneratorInstance.setRailInListIndex();
        levelGeneratorInstance.CheckCurrent(list_tuiles_path_validated);
        //current_trainstation.onTrainHasArrived.AddListener(callForDeleteCurrent);
        current_trainstation.setPathFindToolInstance(this);
        OnPathFound.Invoke(list_tuiles_path_validated);
        canBuildPath = false;
        ClearAllPaths();
    }

    public void callForDeleteCurrent(List<Vector2> list_pos){
        //Debug.Log("called ");
        levelGeneratorInstance.DeleteCurrent(list_pos);
    }

    private void ClearAllPaths(){
        list_tuiles_path_tested.Clear();
        list_tuiles_path_validated.Clear();
    }

    private void resetPath(){
        ClearAllPaths();
        current_trainstation = null;
        canBuildPath = false;
    }




}
