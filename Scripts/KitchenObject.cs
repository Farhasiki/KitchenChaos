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

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject){
        if(this is PlateKitchenObject){
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }else{
            plateKitchenObject = null;
            return false;
        }
    }

    /// <summary>
    /// 设置物体的位置
    /// </summary>
    /// <param name="kitchenObjectParent"></param>
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent){
        if(this.kitchenObjectParent != null){//检查是否已有父物体
            this.kitchenObjectParent.ClearKitchenObject();//清空原父物体
        }
        //修改逻辑位置
        this.kitchenObjectParent = kitchenObjectParent;//设置新父物体
        if(kitchenObjectParent.HaskitchenObject()){
            Debug.LogError("IKitchenObjectParent has object");//如果新父物体已有对象，输出错误信息
        }
        kitchenObjectParent.SetKitchenObject(this);//设置自身为新父物体的对象
        //修改物理位置
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();//更改父物体
        transform.localPosition = Vector3.zero;//将自身坐标重置为0
        transform.rotation = Quaternion.identity;//将自身旋转重置为默认值
    }

    public IKitchenObjectParent GetKitchenObjectParent(){
        return this.kitchenObjectParent;//返回父物体
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
        // 实例化一个预制件
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        // 获取生成的物品的 KitchenObject 组件
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        // 设置物品的物体载体
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}