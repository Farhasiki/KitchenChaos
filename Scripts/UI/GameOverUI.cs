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

    // 当 "GameManager" 对象的状态改变时，调用此方法
    private void GameManager_OnStateChange(object sender, EventArgs e){
        // 检查游戏是否结束
        if(GameManager.Instance.IsGameOver()){
            // 将交付管理器中成功配送的食谱数量设置为文本框的文本
            RecipeDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRcipesAmount().ToString();
            // 显示该 UI 面板
            Show();
        }else{
            // 如果游戏没有结束，隐藏该 UI 面板
            Hide();
        }
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    } 
}