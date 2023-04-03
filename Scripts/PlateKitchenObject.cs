 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

 public class PlateKitchenObject : KitchenObject{

    public event EventHandler<OnIngredientAddEventArgs> OnIngredientAdd;

    public class  OnIngredientAddEventArgs : EventArgs{
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField]private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;
    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO){
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO)){
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO)){
            // kitchenObjectSO = null;
            return false;
        }else{
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdd?.Invoke(this,new PlateKitchenObject.OnIngredientAddEventArgs{
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }
}