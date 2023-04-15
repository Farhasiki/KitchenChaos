using System.Globalization;
using System;
using UnityEngine;
using TMPro;
public class GameStartCountdownUI : BaseUI{
    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;
    private int lastCountdownNumber;
    private const String NUMBER_POPUP = "Number_Popup";
    private void Start() {
        animator = GetComponent<Animator>();
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        gameObject.SetActive(false);
    }

    // 当 "GameManager" 对象的状态改变时，调用此方法
    private void GameManager_OnStateChange(object sender, EventArgs e){
        // 检查是否处于倒计时状态
        if(GameManager.Instance.IsCountdownToStartActive()){
            // 如果处于倒计时状态，显示该 UI 面板
            Show();
        }else{
            // 如果不处于倒计时状态，隐藏该 UI 面板
            Hide();
        }
    }

    private void Update(){
        // 获取到倒计时计时器，向上取整并将结果转换为字符串，并将其赋值给 "countdownText" 文本框的文本
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();
        if(lastCountdownNumber != countdownNumber){
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
        lastCountdownNumber = countdownNumber;
    }
}