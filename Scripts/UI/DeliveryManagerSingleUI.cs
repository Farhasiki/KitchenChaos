using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManagerSingleUI : MonoBehaviour{
    [SerializeField] TextMeshProUGUI recipeNameText;
    [SerializeField] Transform iconContainer;
    [SerializeField] Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }
    
    // 用 RecipeSO 对象设置 UI 界面中的食谱名称文本框
    public void SetRecipeSO(RecipeSO recipeSO){
        recipeNameText.text = recipeSO.recipeName;
        // 对于每一个在 RecipeSO 对象中的厨房物品
        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList){
            // 实例化一个图标模板，并将其放置在图标容器中
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            // 激活该图标模板的游戏对象
            iconTransform.gameObject.SetActive(true);
            // 设置该图标模板的 Image 组件的精灵为厨房物品的精灵
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }

}
