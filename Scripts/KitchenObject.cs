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
        transform.rotation = Quaternion.identity;
    }
    public IKitchenObjectParent GetKitchenObjectParent(){
        return this.kitchenObjectParent;
    }

    public void DestroySelf(){
        GetKitchenObjectParent().ClearKitchenObject();

        Destroy(gameObject);
    }
    
    /// <summary>
    /// 静态方法
    /// 物品类生成物品到指定的位置非实体
    /// </summary>
    /// <param name="kitchenObjectSO">要生成的物品信息</param>
    /// <param name="kitchenObjectParent">生成位置</param>
    /// <returns>生成的物体</returns>
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent){
        Transform kitchenObjectransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectransform.GetComponent<KitchenObject>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);//生成物品到物体载体上

        return kitchenObject;
    }

}