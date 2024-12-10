using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public class MenuManager : MonoBehaviour
{

    [SerializeField]
    GameObject creditSprite, settingsGameobject;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            activateSprite(creditSprite,false);
            activateSprite(settingsGameobject, false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


 public void OnCreditButtonEnter(){
  //  Debug.Log("test ca marche");
    activateSprite(creditSprite);
    //fadeIntCredit(4f);
}

public void OnQuitCreditsButtonEnter(){
    activateSprite(creditSprite, false);
}


 public void OnSettingsButtonEnter(){
    activateSprite(settingsGameobject);
}

public void OnPlayButtonEnter(){
    SceneManager.LoadScene("Scenes/Level_Scene");
}


public void OnQuitSettingsButtonEnter(){
    activateSprite(settingsGameobject, false);
}


private void activateSprite(GameObject s, bool visible = true)
{
    s.SetActive(visible);
}


IEnumerator fadeIntCredit(float duration)
{

    SpriteRenderer creditRender = creditSprite.GetComponent<SpriteRenderer>();
    float counter = 0;

    Color spriteColor = creditRender.material.color;

    while (counter < duration)
    {
        counter += Time.deltaTime;
        float alpha = Mathf.Lerp(1, 0, counter / duration);
      //  Debug.Log(alpha);
        creditRender.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);

        yield return null;
    }
}

}
