using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    /// <summary>
    /// 交互
    /// </summary>
    public override void Interact(Player player){
        if(HaskitchenObject()){// (There is a KitchenObject here)柜台上有物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    // (Player is carrying plate) 玩家拿着盘子
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        //添加到盘子中
                        GetKitchenObject().DestroySelf();
                    }
                }else{// (Player is carrying other something) 玩家拿着其他东西
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        // (The Plate is on Counter) 桌子上有盘子
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }else{// (Player not carrying anything)玩家手上没有物品
                // (Put kitchenObject on player)将物品放到玩家手中
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }else{// (There is no kitchenObject here)柜台上没有物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
                // (Put kitchenObject on counter)将物品放到柜台上
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }else{// (Player not carrying anything)玩家手上没有物品
                // (Do nothing)什么都不做
            }
        }
    }
}