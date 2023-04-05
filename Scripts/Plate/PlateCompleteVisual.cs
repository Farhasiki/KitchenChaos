using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour{
    [Serializable]
    public struct KitchenObjectSO_GameObject{
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> KitchenObjectSOGameObjectList;

    private void Start() {
        plateKitchenObject.OnIngredientAdd += PlateKitchenObject_OnIngredientAdd;

        foreach(KitchenObjectSO_GameObject kitchenObjectSOGameObject in KitchenObjectSOGameObjectList){
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    // 定义一个名为 PlateKitchenObject_OnIngredientAdd 的方法，该方法为事件处理器
    // 该事件处理器用于在厨房中添加食材时激活对应的厨房物品对象
    private void PlateKitchenObject_OnIngredientAdd(object sender, PlateKitchenObject.OnIngredientAddEventArgs e){
        // 遍历 KitchenObjectSOGameObjectList 列表中的所有元素
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in KitchenObjectSOGameObjectList){
            // 判断 e.kitchenObjectSO 是否等于当前遍历到的元素的 kitchenObjectSO 属性
            if (e.kitchenObjectSO == kitchenObjectSOGameObject.kitchenObjectSO){
                // 若成立，激活当前遍历到的元素的 gameObject 属性
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}