using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter{

    public static DeliveryCounter Instance {get; private set;}

    private void Awake() {
        Instance = this;
    }
    
    public override void Interact(Player player){
        // 如果玩家没有拿着任何厨房物品，直接返回
        if(player.GetKitchenObject() == null)
            return;

        // 尝试获取玩家拿着的物品是否为盘子
        if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
            // 如果是盘子，将盘子交给 "DeliveryManager" 来进行配送，然后销毁玩家手上的物品
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            player.GetKitchenObject().DestroySelf();
        }
    }

}