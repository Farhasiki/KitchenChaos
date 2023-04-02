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
                // Do nothing
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
        /*
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
        */  
    }
}