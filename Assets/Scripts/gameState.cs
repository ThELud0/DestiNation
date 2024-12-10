using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gameState : MonoBehaviour
{

    [SerializeField] private UiLevel UiInstance;

    private int score = 0, nbBabyDead = 0, maxBabyDead = 5, scoreDefaulTrainSucceed = 100;

    private bool gamePause=false;

    public List<Train> allRunningTrains = new ();

    public BabyManager babyManagerInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
           updateTextInInstance(); 
           setGamePaused(false);
    }

    public bool isGamePaused(){
        return gamePause;
    }

    public void setGamePaused(bool state){
        gamePause=state;
        if(state){
            relaunchProcessAfterUnpaus();
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

    public void addToScore(int add){
        score+=add;
        updateTextInInstance();
    }

    public void newBabyDead(){
        nbBabyDead++;
        updateTextInInstance();
        if(nbBabyDead>=maxBabyDead){
            gameIsLost();
        }
    }

    private void gameIsLost(){
        Debug.LogWarning("Game lost");
        UiInstance.displayLostPanelWithScore(score);
    }

    void updateTextInInstance(){
        UiInstance.updateText(score,nbBabyDead, maxBabyDead);
    }
}
