using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ContainerCounter : BaseCounter{
    [SerializeField] private KitchenObjectSO kitchenObjectOS;//当前柜台储存货物的信息
    public event EventHandler OnPlayerGrabbedObject;//玩家抓取物品
    /// <summary>
    /// 交互
    /// </summary>
    public override void Interact(Player player){
        if(player.HaskitchenObject())return ;// (Player is carrying something) 玩家正拿着东西
        Transform kitchenObjectransform = Instantiate(kitchenObjectOS.prefab);
        kitchenObjectransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);//生成物品
        OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
    }
    
}