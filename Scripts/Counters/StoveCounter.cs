using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter, IHasProgress{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;// 煎炸的食谱
    [SerializeField] private BurningRecipeSO [] burningRecipeArray;// 烧焦的食谱

    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public class OnStateChangeEventArgs : EventArgs{
        public State state;
    }
    public enum State{ //煎锅当前状态
        Idle,
        Frying,
        Fried,
        Burned,
    }
    private FryingRecipeSO fryingRecipeSO = null;//油炸配方
    private BurningRecipeSO  burningRecipeSO = null;//油炸配方
    private State state;
    private float fryingTimer = 0;//油炸计时器
    private float burningTimer = 0;//烧焦计时器

    private void Start() {
        state = State.Idle;
    }
    private void Update() {
        if(HaskitchenObject()){ // 如果当前厨具上有食材或食物
            switch (state){
                case State.Idle: // 空闲状态
                    break;
                case State.Frying://烹饪状态
                    fryingTimer += Time.deltaTime; // 烹饪计时器自增
                    OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs{
                        progressNormalized = fryingTimer / fryingRecipeSO.maxFryingTimer
                    });

                    if(fryingTimer >= fryingRecipeSO.maxFryingTimer){ // 如果烹饪时间超过最大烹饪时间
                        GetKitchenObject().DestroySelf(); // 销毁当前厨具上的食材或食物

                        // 根据烹饪食谱生成烹饪完成的食物
                        KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(fryingRecipeSO.output,this);

                        state = State.Fried; // 切换到炒熟状态
                        OnStateChange?.Invoke(this, new StoveCounter.OnStateChangeEventArgs{
                            state = state
                        });
                        burningTimer = 0; // 重置烧焦计时器
                        burningRecipeSO = GetBurningRecipeSOFromInput(kitchenObject.GetKitchenObjectSO()); // 获取下一个食物烧焦所需的食谱
                    }
                    break;
                case State.Fried: // 炒熟状态
                    burningTimer += Time.deltaTime; // 烧焦计时器自增
                    OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs{
                        progressNormalized = burningTimer / burningRecipeSO.maxBurningTimer
                    });

                    if(burningTimer >= burningRecipeSO.maxBurningTimer){ // 如果烧焦时间超过最大烧焦时间
                        GetKitchenObject().DestroySelf(); // 销毁当前厨具上的食物

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output,this); // 根据食谱生成烧焦的食物
                        
                        state = State.Burned; // 切换到烧焦状态

                        OnStateChange?.Invoke(this, new StoveCounter.OnStateChangeEventArgs{
                            state = state
                        });
                        OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs{
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned: // 烧焦状态
                    break;
            }
        }
    }
    public override void Interact(Player player){
        // (Same method)意义同上方
        if(this.HaskitchenObject() ^ player.HaskitchenObject()){// (One of Player and Counter has something) 玩家和柜台有一个上有物品
            if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){// (KitchenObject has Recipe) 有食谱的放到菜板上
                    // (Put kitchenObject on counter)将物品放到柜台上
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingTimer = 0;
                    state = State.Frying;
                    fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

                    OnStateChange?.Invoke(this, new StoveCounter.OnStateChangeEventArgs{
                        state = state
                    });
                    OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs{
                        progressNormalized = fryingTimer / fryingRecipeSO.maxFryingTimer
                    });
                }
            }else{// (There is a KitchenObject here)柜台上有物品
                // (Put kitchenObject on player)将物品放到玩家手中
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChange?.Invoke(this, new StoveCounter.OnStateChangeEventArgs{
                    state = state
                });
                OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs{
                    progressNormalized = 0f
                });
            }
        }else{//(Both had something or not) 都有物品或者都没有
            // Do nothing
        }  
    }

    /// <summary>
    /// 切菜交互
    /// </summary>
    // public override void InteractAlternate(Player player){
    //     // (Same method)意义同上方
    //     if(this.HaskitchenObject() ^ player.HaskitchenObject()){// (One of Player and Counter has something) 玩家和柜台有一个上有物品
    //         if(player.HaskitchenObject()){// (Player is carrying something)玩家手上有物品
               

    //         }else if(GetKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
    //             // (There is a KitchenObject here and KitchenObject can be cutting)柜台上有物品 并且可以被切
    //             // (Cut the KitchenObject) 切物品

    //             fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
    //             KitchenObjectSO outputKitchenObject = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());

    //             GetKitchenObject().DestroySelf();

    //             KitchenObject.SpawnKitchenObject(outputKitchenObject,this);
    //             OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs{
    //                 progressNormalized = fryingTimer / fryingRecipeSO.maxFryingTimer
    //             });
    //         }
    //     }
    // }
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
    private BurningRecipeSO GetBurningRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(BurningRecipeSO burningRecipeSO in burningRecipeArray){
            if(burningRecipeSO.input == inputKitchenObjectSO){
                return burningRecipeSO;
            }
        }
        return null;
    }
}