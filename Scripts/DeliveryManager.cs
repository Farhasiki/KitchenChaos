using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour{
    public static DeliveryManager Instance {get; private set;}
    [SerializeField] RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 5f;
    private int spawnrecipeAmount; 
    private int spawnrecipeAmountMax = 4; 

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
                RecipeSO waitingRecipeSO = recipeListSO.RecipeSOList[Random.Range(0,recipeListSO.RecipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
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
                return ;
            }
        }
        Debug.Log("Player did not delivered the correct recipe!");
    }

    private bool CheckRecipe(RecipeSO recipeSO1,RecipeSO recipeSO2){
        Debug.Log("recipeSO1" + recipeSO1.GetHashCode());
        Debug.Log("recipeSO2" + recipeSO2.GetHashCode());
        return recipeSO1.GetHashCode() == recipeSO2.GetHashCode();
    }
}