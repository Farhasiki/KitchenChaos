using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter: BaseCounter{
    [SerializeField] private KitchenObjectSO cuttingKitchenObject;//当前柜台储存货物的信息
    public override void Interact(Player player){
        // (Same method)意义同上方
        if(this.HaskitchenObject() ^ player.HaskitchenObject()){// (One of Player and Counter has something) 玩家和柜台有一个上有物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
                // (Put kitchenObject on counter)将物品放到柜台上
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }else{// (There is a KitchenObject here)柜台上有物品
                // (Put kitchenObject on player)将物品放到玩家手中
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }else{//(Both had something or not) 都有物品或者都没有
            // Do nothing
        }  
    }
    public override void InteractAlternate(Player player){
        // (Same method)意义同上方
        if(this.HaskitchenObject() ^ player.HaskitchenObject()){// (One of Player and Counter has something) 玩家和柜台有一个上有物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
                // (Put kitchenObject on counter)将物品放到柜台上
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }else{// (There is a KitchenObject here)柜台上有物品
                // (Cut the KitchenObject) 切物品
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(cuttingKitchenObject,this);
            }
        }
    }
}