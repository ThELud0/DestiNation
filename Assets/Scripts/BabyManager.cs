using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BabyManager : MonoBehaviour
{

    [SerializeField]
    private int baby_despawn_timer = 5;

    [SerializeField]
    private Vector2[] list_bloqued_baby_spawns;

    [SerializeField]
    private float baby_spawn_rate_probability = 0.2f;

    [SerializeField]
    private GameObject baby_prefab;

    [SerializeField]
    private int range_max_x=20, range_max_z = 20;

    [SerializeField]
    private int floorY = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn_baby_random_place();
        StartCoroutine(LoopSpawnBaby());
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

    void spawn_baby_random_place(){
        Vector2 randVec = getRandomVector2();
        while(isVecInForbiddenList(randVec)){
             randVec = getRandomVector2();
             Debug.Log("Il y a quelques chose a cet endroit !");
        }
        
        GameObject newBaby = Instantiate(baby_prefab);
        newBaby.transform.position = new Vector3(randVec.x, floorY, randVec.y);
        StartCoroutine(waitTillBabyExplode( newBaby,baby_despawn_timer));
    }


    IEnumerator waitTillBabyExplode(GameObject baby,int duration){
            yield return new WaitForSeconds(duration);
            Destroy(baby);
    }

     IEnumerator LoopSpawnBaby(){
            yield return new WaitForSeconds(1);
            if(Random.Range(0f,1f)< baby_spawn_rate_probability){
                spawn_baby_random_place();
            }
            StartCoroutine(LoopSpawnBaby());
    }
}
