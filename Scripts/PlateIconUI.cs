using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour{
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

    private void UpdateVisual(){
        foreach(Transform child in transform){
            if(child == iconTemplate)continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){
            Transform iconTransform = Instantiate(iconTemplate,transform);
            iconTransform.gameObject.SetActive(true);

            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}