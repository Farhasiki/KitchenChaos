using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : BaseUI{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform iconTemplate;
    
    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        plateKitchenObject.OnIngredientAdd += PlateKitchenObject_OnIngredientAdd;
    }
 
    private void PlateKitchenObject_OnIngredientAdd(object sender, EventArgs e){
        UpdateVisual();
    }

    // This method updates the visual display of the kitchen objects on a plate
    /// <summary>
    /// 更新盘子UI界面
    /// </summary>
    private void UpdateVisual(){
        // Loop through all child transforms of the current transform
        foreach(Transform child in transform){
            //跳过模板UI
            // Check if the current child is the icontemplate - if it is, skip it and continue with the next child
            if(child == iconTemplate) continue;
            //清空UI
            // Otherwise, destroy the current child game object
            Destroy(child.gameObject);
        }
        // Loop through all kitchen object scriptable objects in the plate kitchen object list
        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){
            //生成UI
            // Instantiate a new icon transform using the icon template, as a child of the current transform
            Transform iconTransform = Instantiate(iconTemplate, transform);
            // Set the new icon transform to be active in the scene
            iconTransform.gameObject.SetActive(true);
            //设置Icon
            // Get the PlateIconSingleUI component on the new icon transform, and set its kitchen object scriptable object to the current kitchen object
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
    
}