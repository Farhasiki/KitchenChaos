using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;//生成物品位置
    private KitchenObject kitchenObject;//当前柜台上放的物品
    
    public virtual void Interact(Player player){}

    public Transform GetKitchenObjectFollowTransform(){
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
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
