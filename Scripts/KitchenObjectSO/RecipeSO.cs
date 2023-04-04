using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class RecipeSO : ScriptableObject{
    public List<KitchenObjectSO> kitchenObjectSOList;//食谱
    public string recipeName;

    public override int GetHashCode(){
        int hashcode = 0;
        foreach(KitchenObjectSO kitchenObjectSO in kitchenObjectSOList){
            hashcode |= (1 << kitchenObjectSO.number);
        }
        return hashcode;
    }
}