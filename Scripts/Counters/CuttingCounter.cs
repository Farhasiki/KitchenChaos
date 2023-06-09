using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter: BaseCounter, IHasProgress{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeArray;// 切菜品的食谱

    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData(){
        OnAnyCut = null;
    }
    public event EventHandler OnPlayerCuttingObject;
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

    private int cuttingProgress = 0;
    public override void Interact(Player player){
        // (Same method)意义同上方
        if(this.HaskitchenObject()){// (Counter has something) 柜台上有物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    // (Player is carrying plate) 玩家拿着盘子
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        //添加到盘子中
                        GetKitchenObject().DestroySelf();
                    }
                }
                
            }else{// (Player is carrying nothing)柜台上有物品
                // (Put kitchenObject on player)将物品放到玩家手中

                GetKitchenObject().SetKitchenObjectParent(player);

                OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs{// 触发事件
                    progressNormalized = 0f
                });
            }
        }else{// (There is nothing here)柜台上无物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){// (KitchenObject has Recipe) 有食谱的放到菜板上
                    // (Put kitchenObject on counter)将物品放到柜台上
                    cuttingProgress = 0;
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }else{// (Player has nothing) 玩家没有物品
            }
        }  
    }

    /// <summary>
    /// 切菜交互
    /// </summary>
    public override void InteractAlternate(Player player){
        if(this.HaskitchenObject() ^ player.HaskitchenObject()){// (One of Player and Counter has something) 玩家和柜台有一个上有物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
               

            }else if(GetKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
                // (There is a KitchenObject here and KitchenObject can be cutting)柜台上有物品 并且可以被切
                // (Cut the KitchenObject) 切物品

                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());//获取切菜食谱
                cuttingProgress ++;
                
                OnAnyCut?.Invoke(this,EventArgs.Empty);
                OnPlayerCuttingObject?.Invoke(this,EventArgs.Empty);
                OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs{// 触发事件
                    progressNormalized = cuttingProgress * 1f / cuttingRecipeSO.maxCuttingProgress
                });

                if(cuttingProgress >= cuttingRecipeSO.maxCuttingProgress){
                    KitchenObjectSO outputKitchenObject = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());

                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(outputKitchenObject,this);
                }
            }
        }
    }
    /// <summary>
    /// 检查放入食物是否有效
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        return GetCuttingRecipeSOFromInput(inputKitchenObjectSO) != null;
    }
    /// <summary>
    /// 根据菜谱返回做出的菜
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(inputKitchenObjectSO);

        return cuttingRecipeSO == null ? null : cuttingRecipeSO.output;
    }

    /// <summary>
    /// 获取当前食物的菜谱
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeArray){
            if(cuttingRecipeSO.input == inputKitchenObjectSO){
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}