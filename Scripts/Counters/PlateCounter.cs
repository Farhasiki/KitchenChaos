using UnityEngine;
using System;

public class PlateCounter : BaseCounter{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemove;
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private float spawnPlateTimer = 0f;
    private float spawnPlateTimerMax = 4f;
    private int plateSpawnedAmount = 0;
    private int plateSpawnedAmountMax = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer >= spawnPlateTimerMax){
            spawnPlateTimer = 0;

            if(plateSpawnedAmount < plateSpawnedAmountMax){
                plateSpawnedAmount ++;
                //生成盘子

                OnPlateSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }
 
    public override void Interact(Player player)
    {
        if(!player.HaskitchenObject()){// (Player has no KtichenObject)玩家空手
            if(plateSpawnedAmount > 0){
                KitchenObject.SpawnKitchenObject(kitchenObjectSO,player);
 
                plateSpawnedAmount --;
                OnPlateRemove?.Invoke(this,EventArgs.Empty);
            }
        }else{
            //if(player.GetKitchenObject().TryGetPlate())
        }
    }
}