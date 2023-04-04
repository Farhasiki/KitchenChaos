using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour{
    public static DeliveryManager Instance {get; private set;}

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    [SerializeField] RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 6f;
    private int spawnrecipeAmount; 
    private int spawnrecipeAmountMax = 5; 

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
     
    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0){
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(spawnrecipeAmount < spawnrecipeAmountMax){
                spawnrecipeAmount ++ ;

                RecipeSO waitingRecipeSO = recipeListSO.RecipeSOList[UnityEngine.Random.Range(0,recipeListSO.RecipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
        RecipeSO plateRecipe = ScriptableObject.CreateInstance<RecipeSO>();
        plateRecipe.kitchenObjectSOList = plateKitchenObject.GetKitchenObjectSOList();
        for(int i = 0; i < waitingRecipeSOList.Count; ++i){
            if(CheckRecipe(waitingRecipeSOList[i],plateRecipe)){//相同的hash值
                Debug.Log("Player delivered the correct recipe!");
                waitingRecipeSOList.RemoveAt(i);

                OnRecipeCompleted?.Invoke(this,EventArgs.Empty);

                return ;
            }
        }
        Debug.Log("Player did not delivered the correct recipe!");
    }

    private bool CheckRecipe(RecipeSO recipeSO1,RecipeSO recipeSO2){
        return recipeSO1.GetHashCode() == recipeSO2.GetHashCode();
    }

    public List<RecipeSO> GetWaitingRecipeSOList(){
        return waitingRecipeSOList;
    }
}