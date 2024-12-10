using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


public class GameTools : MonoBehaviour
{
    private static float floor_Y=1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public static Vector2 get2Dfrom3DVector(Vector3 vec){
        return new Vector2(vec.x, vec.z);
   }

   
   public static Vector3 get3Dfrom2DVector(Vector2 vec){
        return new Vector3(vec.x,floor_Y, vec.y);
   }


   public List<GameObject> copyList(List<GameObject> l){
     List<GameObject> l2 = new ();
     foreach(GameObject i in l){
          l2.Add(i);
     }
     return l2;
   }

   public static List<Vector2> copyListVector2(List<Vector2> l){
     List<Vector2> l2 = new ();
     foreach(Vector2 i in l){
          l2.Add(i);
     }
     return l2;
   }
}