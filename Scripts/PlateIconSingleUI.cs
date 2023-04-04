using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour{
    [SerializeField] Image image;

    // 定义一个名为 SetKitchenObjectSO 的公共方法，该方法用于设置厨房物品对象的 KitchenObjectSO 属性
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO){
        // 将厨房物品对象的图片 sprite 属性设置为参数 kitchenObjectSO 的 sprite 属性
        image.sprite = kitchenObjectSO.sprite;
    }

}