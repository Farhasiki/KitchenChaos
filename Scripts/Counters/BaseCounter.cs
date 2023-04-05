using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;//生成物品位置
    private KitchenObject kitchenObject;//当前柜台上放的物品

    public static event EventHandler OnAnyObjectPlacedHere;
    
    public virtual void Interact(Player player){
        Debug.Log("BaseCounter.Interact ");
    }
    public virtual void InteractAlternate(Player player){
        Debug.Log("BaseCounter.InteractAlternate ");
    }

    public Transform GetKitchenObjectFollowTransform(){
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null){
            OnAnyObjectPlacedHere?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject(){
        return this.kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HaskitchenObject(){
        return kitchenObject != null;
    }
}
