using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BabyManager : MonoBehaviour
{

    [SerializeField]
    private int baby_despawn_timer = 5, babyLostCounter = 0,nbBabyDeadTillLost= 5;

    [SerializeField]
    private Vector2[] list_bloqued_baby_spawns;

    [SerializeField]
    private float baby_spawn_rate_probability = 0.2f, time_wait_spawn_baby = 0.5f;

    [SerializeField]
    private GameObject baby_prefab;

    [SerializeField]
    private int range_max_x=20, range_max_z = 20;


[SerializeField]
    private float increaseDifficultySpawnerRate = 0.0f, increaseDifficultySpawnRateIncrementor = 0.007f;

    [SerializeField]
    private int floorY = 0;

        [SerializeField]
    private gameState gStateInstance;

    [SerializeField]
    private string[] list_String_unspawnable_baby;

    [SerializeField]
    private Levelgenerator levelgenerator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        launchLoopBaby();
    }

    public void launchLoopBaby(){
        spawn_baby_random_place();
        StartCoroutine(FirstSpawnBaby());
    }

    Vector2 getRandomVector2(){
        int rand_x = Random.Range(0, range_max_x);
        int rand_z = Random.Range(0, range_max_z);
        return new Vector2(rand_x, rand_z);
    }

    bool isVecInForbiddenList(Vector2 tested_vec){
        foreach(Vector2 vec in list_bloqued_baby_spawns){
            if(vec.Equals(tested_vec)){
                return true;
            }
        }
        return false;
    }

    bool ZoneNotSpawnable(Vector2 tested_vec){

        int[] id_zone_non_spawnable = new int[]{1,2,3,4};

        foreach (int id_zone in id_zone_non_spawnable){
            if(levelgenerator.getStateFromTile((int)tested_vec.x, (int)tested_vec.y)==id_zone){
                return true;
            }
        }
        return false;
        /*transform.position = new Vector3(tested_vec.x, floorY+3, tested_vec.y);
        Ray ray_test = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits =  Physics.RaycastAll(ray_test, 2f);
            if (hits.Length > 0)
            {
                foreach(RaycastHit hit in hits){
                                    Debug.Log("Spotted "+ hit.transform.gameObject.tag);

               foreach(string tag in list_String_unspawnable_baby){

                if(tag == hit.transform.gameObject.tag){
                     Debug.Log("Spotted a structure at baby speculation point !");

                    return true;
                }
               }
               }
        
            }else {
            }
            
            return false;*/

    }

    void spawn_baby_random_place(){
        Vector2 randVec = getRandomVector2();
        while(isVecInForbiddenList(randVec) || ZoneNotSpawnable(randVec)){
             randVec = getRandomVector2();
             Debug.Log("Il y a quelques chose a cet endroit !");
        }

        gStateInstance.OnBabySpawn();
        increaseDifficultySpawnerRate+=increaseDifficultySpawnRateIncrementor;
        
        GameObject newBaby = Instantiate(baby_prefab);
        newBaby.transform.position = new Vector3(randVec.x, floorY+1, randVec.y);
        levelgenerator.setStateFromTile((int)randVec.x,(int)randVec.y,3);
        newBaby.GetComponent<Human>().Initialize(baby_despawn_timer, this);
        //newBaby.GetComponent<Human>().OnBabyDespawn.AddListener( babyLost) ;

        //StartCoroutine(waitTillBabyExplode( newBaby,baby_despawn_timer));
    }

    public void babyLost(Human baby){
        Destroy(baby.gameObject);
        gStateInstance.newBabyDead();
    }




   /* IEnumerator waitTillBabyExplode(GameObject baby,int duration){
            yield return new WaitForSeconds(duration);
            if(!gameState.gamePause){
             
            babyLost(baby);
            }
    }*/

      IEnumerator FirstSpawnBaby(){
            yield return new WaitForSeconds(3);
            if(!gStateInstance.isGamePaused()){
  
            if(Random.Range(0f,1f)< baby_spawn_rate_probability+increaseDifficultySpawnerRate){
                spawn_baby_random_place();
            }
            StartCoroutine(LoopSpawnBaby());
            }
    }

     IEnumerator LoopSpawnBaby(){
            yield return new WaitForSeconds(time_wait_spawn_baby);
            if(!gStateInstance.isGamePaused()){
            float current_proba = baby_spawn_rate_probability+increaseDifficultySpawnerRate;
            while(current_proba>1){
                spawn_baby_random_place();
                current_proba-=1;
            }
            if(Random.Range(0f,1f)< current_proba ){
                spawn_baby_random_place();
            }
            
            StartCoroutine(LoopSpawnBaby());
            }
    }
}
