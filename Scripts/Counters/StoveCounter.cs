using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;// 切菜品的食谱

    // public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;
    // public event EventHandler OnPlayerCuttingObject;

    // public class OnProgressChangeEventArgs : EventArgs{
    //     public float progressNormalized;
    // }

    private int fryingProgress = 0;
    public override void Interact(Player player){
        // (Same method)意义同上方
        if(this.HaskitchenObject() ^ player.HaskitchenObject()){// (One of Player and Counter has something) 玩家和柜台有一个上有物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){// (KitchenObject has Recipe) 有食谱的放到菜板上
                    // (Put kitchenObject on counter)将物品放到柜台上
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }else{// (There is a KitchenObject here)柜台上有物品
                // (Put kitchenObject on player)将物品放到玩家手中
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }else{//(Both had something or not) 都有物品或者都没有
            // Do nothing
        }  
    }

    /// <summary>
    /// 切菜交互
    /// </summary>
    public override void InteractAlternate(Player player){
        // (Same method)意义同上方
        if(this.HaskitchenObject() ^ player.HaskitchenObject()){// (One of Player and Counter has something) 玩家和柜台有一个上有物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
               

            }else if(GetKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
                // (There is a KitchenObject here and KitchenObject can be cutting)柜台上有物品 并且可以被切
                // (Cut the KitchenObject) 切物品

                FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                    KitchenObjectSO outputKitchenObject = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());

                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(outputKitchenObject,this);
            }
        }
    }
    /// <summary>
    /// 检查放入食物是否有效
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        return GetFryingRecipeSOFromInput(inputKitchenObjectSO) != null;
    }
    /// <summary>
    /// 根据菜谱返回做出的菜
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOFromInput(inputKitchenObjectSO);

        return fryingRecipeSO == null ? null : fryingRecipeSO.output;
    }

    /// <summary>
    /// 获取当前食物的菜谱
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private FryingRecipeSO GetFryingRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray){
            if(fryingRecipeSO.input == inputKitchenObjectSO){
                return fryingRecipeSO;
            }
        }
        return null;
    }
}