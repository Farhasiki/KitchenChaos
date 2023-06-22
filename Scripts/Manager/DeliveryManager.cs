using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour{
    public static DeliveryManager Instance {get; private set;}

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSuccess;

    [SerializeField] RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 6f;
    private int spawnrecipeAmount; 
    private int spawnrecipeAmountMax = 10; 
    private int successFulRecipesAmount = 0;
    private int showrecipeAmountMax = 5;
    private int showrecipeAmount = 0;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
     
    private void Update() {
        if(!GameManager.Instance.IsGamePlaying())return; 
        spawnRecipeTimer -= Time.deltaTime; // 减去上一帧到这一帧所用的时间，更新生成食谱的计时器
        if(spawnRecipeTimer <= 0){ // 当计时器达到0时，生成一个新的食谱
            spawnRecipeTimer = spawnRecipeTimerMax; // 重置计时器
            if(showrecipeAmount < showrecipeAmountMax){ // 如果当前生成的食谱数量小于最大数量
                showrecipeAmount ++ ; // 增加当前生成的食谱数量

                // 随机获取一个待做食谱
                RecipeSO waitingRecipeSO = recipeListSO.RecipeSOList[UnityEngine.Random.Range(0,recipeListSO.RecipeSOList.Count)]; 
                waitingRecipeSOList.Add(waitingRecipeSO); // 将待做食谱添加到待做食谱列表中
                OnRecipeSpawned?.Invoke(this,EventArgs.Empty); // 触发食谱生成事件
            }       
        }
    }


    public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
        // 根据传递的盘子厨房物体，生成一个临时的配方
        RecipeSO plateRecipe = ScriptableObject.CreateInstance<RecipeSO>();
        plateRecipe.kitchenObjectSOList = plateKitchenObject.GetKitchenObjectSOList();
        
        // 遍历待做菜单列表
        for(int i = 0; i < waitingRecipeSOList.Count; ++i){
            // 检查该配方与待做菜单列表中的配方是否匹配
            if(CheckRecipe(waitingRecipeSOList[i],plateRecipe)){//相同的hash值
                // 若匹配，则删除待做菜单列表中对应的配方
                waitingRecipeSOList.RemoveAt(i);
                successFulRecipesAmount ++;
                showrecipeAmount --;
                
                // 触发OnRecipeCompleted事件
                OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                
                OnRecipeSuccess?.Invoke(this,EventArgs.Empty);

                // 返回
                return ;
            }
        }
        OnRecipeFailed?.Invoke(this,EventArgs.Empty);
    }

    private bool CheckRecipe(RecipeSO recipeSO1,RecipeSO recipeSO2){
        return recipeSO1.GetHashCode() == recipeSO2.GetHashCode();
    }

    public List<RecipeSO> GetWaitingRecipeSOList(){
        return waitingRecipeSOList;
    }
    
    public int GetSuccessfulRcipesAmount(){
        return successFulRecipesAmount;
    }
}