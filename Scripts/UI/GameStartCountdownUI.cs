using System;
using UnityEngine;
using TMPro;
public class GameStartCountdownUI : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start() {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChange(object sender, EventArgs e){
        if(GameManager.Instance.IsCountdownToStartActive()){
            Show();
        }else{
            Hide();
        }
    }

    private void Update() {
        countdownText.text = (Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer())).ToString();
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}