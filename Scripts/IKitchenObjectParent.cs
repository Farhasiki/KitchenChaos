using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent {
    /// <summary>
    /// 获取柜台上的物品生成点
    /// </summary>
    /// <returns>counterTopPoint</returns>
    public Transform GetKitchenObjectFollowTransform();

    /// <summary>
    /// 设置柜台上物品
    /// </summary>
    /// <param name="kitchenObject">放置的物品</param>
    public void SetKitchenObject(KitchenObject kitchenObject);

    /// <summary>
    /// 获取柜台物品
    /// </summary>
    /// <returns>kitchenObject</returns>
    public KitchenObject GetKitchenObject();

    /// <summary>
    /// 清空柜台
    /// </summary>
    public void ClearKitchenObject();

    /// <summary>
    /// 判断柜台上是否有物体
    /// </summary>
    /// <returns></returns>
    public bool HaskitchenObject();
}
