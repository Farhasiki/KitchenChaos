 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

 public class PlateKitchenObject : KitchenObject{

    public event EventHandler<OnIngredientAddEventArgs> OnIngredientAdd;//添加食材事件

    public class  OnIngredientAddEventArgs : EventArgs{
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField]private List<KitchenObjectSO> validKitchenObjectSOList;//合法食材
    private List<KitchenObjectSO> kitchenObjectSOList;//已拿的食材
    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

   // 定义一个名为 TryAddIngredient 的公共方法，该方法用于尝试添加一个食材到厨房物品对象中
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO){
        // 判断厨房物品对象是否合法，如果不合法则返回 false
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)){
            return false;
        }
        // 判断厨房物品对象是否已经存在，如果已经存在则返回 false
        if (kitchenObjectSOList.Contains(kitchenObjectSO)){
            // kitchenObjectSO = null;
            return false;
        }else{
            // 将传入的厨房物品对象添加到厨房物品对象列表中
            kitchenObjectSOList.Add(kitchenObjectSO);
            // 触发 OnIngredientAdd 事件，将该厨房物品对象作为参数传递给事件处理器
            OnIngredientAdd?.Invoke(this,new PlateKitchenObject.OnIngredientAddEventArgs{
                kitchenObjectSO = kitchenObjectSO
            });
            // 返回添加成功
            return true;
        }
    }


    public List<KitchenObjectSO> GetKitchenObjectSOList(){
        return kitchenObjectSOList;
    }
}