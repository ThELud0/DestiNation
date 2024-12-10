using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameState : MonoBehaviour
{

    [SerializeField] private UiLevel UiInstance;

    private int score = 0, nbBabyDead = 0, maxBabyDead = 5, scoreDefaulTrainSucceed = 100;
    //ici jai craqué, jai mis des static
    public static bool gamePause=false;

        public static int compteurTrain = 0;


    public List<Train> allRunningTrains = new ();

    public BabyManager babyManagerInstance;

    public SoundManager soundManagerInstance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
                compteurTrain = 0;
           updateTextInInstance(); 
           setGamePaused(false);
    }

    public bool isGamePaused(){
        return gamePause;
    }

    public static void setGamePaused(bool state){
        gamePause=state;
        if(state){
            //relaunchProcessAfterUnpaus();
        }
    }

    public void addTrainToList(Train t){
        allRunningTrains.Add(t);
    }

    private void relaunchProcessAfterUnpaus(){
        babyManagerInstance.launchLoopBaby();
        foreach(Train t in allRunningTrains){
            t.gamePaused=false;
        }
    }

    public void setScore(int a){
        score = a;
        updateTextInInstance();
    }


     public void addToScore(){
        addToScore(scoreDefaulTrainSucceed);
    }

    public void addNbBabyToScore(int i){
        addToScore(scoreDefaulTrainSucceed*i);
    }

    public void addToScore(int add){
        score+=add;
        soundManagerInstance.PlayArrivedToDestination();
        updateTextInInstance();
    }

    public void newBabyDead(){
        nbBabyDead++;
        soundManagerInstance.PlayBabyEnd();
        updateTextInInstance();
        if(nbBabyDead>=maxBabyDead){
            gameIsLost();
        }
    }

    private void gameIsLost(){
        if(gamePause){
            return;
        }
        gameState.compteurTrain=0;
        Debug.LogWarning("Game lost");
        setGamePaused(true);
        UiInstance.displayLostPanelWithScore(score);
        soundManagerInstance.PlayEndGame();
        
        
    }

    void updateTextInInstance(){
        UiInstance.updateText(score,nbBabyDead, maxBabyDead);
    }

    public void OnRailPlaced(){
            soundManagerInstance.PlayRailPose();
    }

    public void OnBabySpawn(){
            soundManagerInstance.PlayPopUpBaby();
    }

    public void OnCrashTrain(){
        soundManagerInstance.PlayTrainAccident();
    }

 



}
