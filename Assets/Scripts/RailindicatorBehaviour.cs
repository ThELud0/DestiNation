using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
//using System.Collections.Color;


public class RailindicatorBehaviour : MonoBehaviour
{


[SerializeField]
private GameObject gameobjectTextRailIndicator;

[SerializeField]
private GameObject gameobjectBulle;



private TMP_Text textRailIndicator;
    private int max_rail_number = 99;
    private int current_TileRail_put = 0;


[SerializeField]
    private Color defaulttextColor;

    [SerializeField]
    private Color numberRailExceededErrorColor ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textRailIndicator = gameobjectTextRailIndicator.GetComponent<TMP_Text>();
        Debug.Log(textRailIndicator);
                setupRailRange(8);
            setCurrentRail(150);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateRailIndicator(){
        textRailIndicator.text = getNumberRailFromNumberTiles(current_TileRail_put).ToString()+'/'+max_rail_number.ToString();
        if (getNumberRailFromNumberTiles(current_TileRail_put) < max_rail_number){
                textRailIndicator.color = defaulttextColor;
        }else{
                textRailIndicator.color = numberRailExceededErrorColor;
        }

        gameobjectBulle.transform.position = (new Vector2(GameStateResources.previousX, GameStateResources.previousZ));

        
    }

    void setupRailRange(int max_rail){
         max_rail_number = max_rail;
         current_TileRail_put = 0;
         UpdateRailIndicator();
    }

    void addRail(){
        current_TileRail_put++;
        UpdateRailIndicator();
    }

    void setCurrentRail ( int number_rail){
        current_TileRail_put = number_rail;
        UpdateRailIndicator();
        }


    // return the number of rail placed according to hte number of tiles (*3)
    int getNumberRailFromNumberTiles(int number_tiles_placed){
        return number_tiles_placed * 3;
    }

    int getNumberTilesFromNumberRails(int number_rails_placed){
        return 0;// Mathf.round(number_tiles_placed / 3);
    }


}
