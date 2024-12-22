using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MenuManager : MonoBehaviour
{

    [SerializeField]
    GameObject creditSprite, settingsGameobject;

    [SerializeField]
    Slider slideSound, slideSoundtrack;
    [SerializeField] Toggle toggleFullscreen;

    [SerializeField] private float DefaultValueSoundtrackSLider=1f, DefaultValueSoundsSLider=1f;

    [SerializeField] private float cooldownPopUpSound=1f, trackedTime = -1f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            float valueToSounds = PlayerPrefs.GetFloat("VolumeSounds");
            slideSound.SetValueWithoutNotify(valueToSounds);

            float valueToSoundtrack = PlayerPrefs.GetFloat("VolumeSoundtrack");
            slideSound.SetValueWithoutNotify(valueToSoundtrack);
            
            SoundManager.Instance.setVolumeSoundtrackAndSounds();


            slideSoundtrack.SetValueWithoutNotify(DefaultValueSoundtrackSLider);
            activateSprite(creditSprite,false);
            activateSprite(settingsGameobject, false);
            SoundManager.Instance.playMenuMusic();
            if (!PlayerPrefs.HasKey("isFullscreen")){
                PlayerPrefs.SetInt("isFullscreen",0);
            }
            actualizeFullScreenState();

    }

    // Update is called once per frame
    void Update()
    {
        if(trackedTime>0 && Time.time - trackedTime > cooldownPopUpSound){
            trackedTime=-1f;
            SoundManager.Instance.setVolumeSoundtrackAndSounds();
            SoundManager.Instance.PlayPopUpBaby();

        }
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
    SoundManager.Instance.stopMenuMusic();

    SceneManager.LoadScene("Scenes/Level_Scene");
}


public void OnQuitSettingsButtonEnter(){
    activateSprite(settingsGameobject, false);
}

public void OnSliderSoundtrackValueChange(float value){
        PlayerPrefs.SetFloat("VolumeSoundtrack", slideSoundtrack.value);
        SoundManager.Instance.setVolumeSoundtrackAndSounds();
}

public void OnSliderSoundsValueChange(float value){
        PlayerPrefs.SetFloat("VolumeSounds", slideSound.value);
        trackedTime = Time.time;
}

public void OnFullScreenCheckaseMarked(){
   
   int correspondinfInt =0;
   if (toggleFullscreen.enabled) {
        correspondinfInt = 1;
   }
   
   PlayerPrefs.SetInt("isFullscreen", correspondinfInt);
   actualizeFullScreenState();
}

private void actualizeFullScreenState(){
   if( PlayerPrefs.GetInt("isFullscreen")==1) {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }else {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
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
