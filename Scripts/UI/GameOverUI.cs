using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOverUI : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI RecipeDeliveredText;
    private void Start() {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChange(object sender, EventArgs e){
        if(GameManager.Instance.IsGameOver()){
            Time.timeScale = 0;
            RecipeDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRcipesAmount().ToString();
            Show();
        }else{
            Hide();
        }
    }

    private void Update() {
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    } 
}