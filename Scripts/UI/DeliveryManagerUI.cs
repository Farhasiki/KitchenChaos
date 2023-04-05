using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawned +=  Delivery_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted +=  Delivery_OnRecipeCompleted;

        UpdateVisual();
    }

    private void Delivery_OnRecipeSpawned(object sender, EventArgs e){
        UpdateVisual();
    }
    private void Delivery_OnRecipeCompleted(object sender, EventArgs e){
        UpdateVisual();
    }

    private void UpdateVisual(){
        // 销毁除模板以外的UI元素
        foreach( Transform child in container){
            if(child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        
        // 生成等待中的食谱UI元素
        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()){
            // 实例化食谱UI模板并添加到父容器
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            // 激活该UI元素
            recipeTransform.gameObject.SetActive(true);
            // 设置食谱数据
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }

}