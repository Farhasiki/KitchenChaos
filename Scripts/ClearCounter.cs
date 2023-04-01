using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectOS;//当前柜台储存货物的信息

    /// <summary>
    /// 交互
    /// </summary>
    public override void Interact(Player player){
        
    }
}