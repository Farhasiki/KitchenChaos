using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeliveryResultUI : MonoBehaviour{
    private const string POPUP = "Popup";
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        gameObject.SetActive(false);
    }
    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e){
        // 激活弹出框
        gameObject.SetActive(true);
        // 播放弹出动画
        animator.SetTrigger(POPUP);
        // 修改背景颜色
        backgroundImage.color = successColor;
        // 修改图标
        iconImage.sprite = successSprite;
        // 修改提示信息
        messageText.text = "DELIVERY\nSUCCESS";
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e){
        //激活当前对象
        gameObject.SetActive(true);
        //设置动画触发器
        animator.SetTrigger(POPUP);
        //更改背景图片颜色
        backgroundImage.color = failedColor;
        //更改icon图片
        iconImage.sprite = failedSprite;
        //更改文本
        messageText.text = "DELIVERY\nFAILED";
    }

}