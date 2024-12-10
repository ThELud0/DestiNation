using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UiLevel : MonoBehaviour
{

    [SerializeField]
    TMP_Text scoreText, babyCounterText, finalScoreText;

    [SerializeField] 
    GameObject panelLost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start(){
        panelLost.SetActive(false);
    }
    public void updateText(int score, int babyDead, int maxBabyDead){
        scoreText.text = "Score :"+score.ToString();
        babyCounterText.text = babyDead.ToString()+"/"+maxBabyDead.ToString();
    }

    private void updateTextFinalScore(int scoreFinal){
        finalScoreText.text = "Score :"+scoreFinal.ToString();
    }

    public void displayLostPanelWithScore(int scoreFinal){
        panelLost.SetActive(true);
        updateTextFinalScore(scoreFinal);
    }

    public void onRetryButtonPressed(){
        Debug.Log("Retry !");
        SceneManager.LoadScene("Scenes/Level_Scene");
    }

    public void OnMenuButtonPressed(){
            SceneManager.LoadScene("Scenes/Menu");
    }
}
