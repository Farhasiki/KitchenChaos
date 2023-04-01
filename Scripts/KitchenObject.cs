using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;//物品信息
    private IKitchenObjectParent kitchenObjectParent;//当前物品放在哪个柜台上
    public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObjectSO;
    }

    /// <summary>
    /// 设置物体的位置
    /// </summary>
    /// <param name="kitchenObjectParent"></param>
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent){
        if(this.kitchenObjectParent != null){//清空原柜台
            this.kitchenObjectParent.ClearKitchenObject();
        }
        //修改逻辑位置
        this.kitchenObjectParent = kitchenObjectParent;
        if(kitchenObjectParent.HaskitchenObject()){
            Debug.LogError("IKitchenObjectParent has object");
        }
        kitchenObjectParent.SetKitchenObject(this);
        //修改物理位置
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();//更改父物体
        transform.localPosition = Vector3.zero;//更改坐标
    }
    public IKitchenObjectParent GetKitchenObjectParent(){
        return this.kitchenObjectParent;
    }

}