using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : BaseUI{
    [SerializeField] private Button soundEffectsButton; 
    [SerializeField] private Button musicButton; 
    [SerializeField] private Button closeButton; 
    [SerializeField] private Button moveUpButton; 
    [SerializeField] private Button moveDownButton; 
    [SerializeField] private Button moveLeftButton; 
    [SerializeField] private Button moveRightButton; 
    [SerializeField] private Button interactButton; 
    [SerializeField] private Button interactAltButton; 
    [SerializeField] private Button pauseButton;  
    [SerializeField] private TextMeshProUGUI soundEffectsText; 
    [SerializeField] private TextMeshProUGUI musicText; 
    [SerializeField] private TextMeshProUGUI moveUpText; 
    [SerializeField] private TextMeshProUGUI moveDownText; 
    [SerializeField] private TextMeshProUGUI moveLeftText; 
    [SerializeField] private TextMeshProUGUI moveRightText; 
    [SerializeField] private TextMeshProUGUI interactText; 
    [SerializeField] private TextMeshProUGUI interactAltText; 
    [SerializeField] private TextMeshProUGUI pauseText; 
    [SerializeField] private Transform ReBindingUI; 
    private Action onCloseButtonAction;

    private void Awake() {
        soundEffectsButton.onClick.AddListener( () => {
            SoundManager.Instance.ChangeValume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeValume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => {
            onCloseButtonAction?.Invoke();
            Hide();
        });
        moveUpButton.onClick.AddListener(() => {ReBinding(GameInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() => {ReBinding(GameInput.Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(() => {ReBinding(GameInput.Binding.Move_Left);});
        moveRightButton.onClick.AddListener(() => {ReBinding(GameInput.Binding.Move_Right);});
        interactButton.onClick.AddListener(() => {ReBinding(GameInput.Binding.Interact);});
        interactAltButton.onClick.AddListener(() => {ReBinding(GameInput.Binding.InteractAlternate);});
        //pauseButton.onClick.AddListener(() => {ReBinding(GameInput.Binding.Pause);});
    }

    private void Start() {
        GameManager.Instance.OnTogglePauseGame += GameManager_OnTogglePauseGame;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void GameManager_OnTogglePauseGame(object sender, EventArgs e){
        Hide(); 
    }

    private void UpdateVisual(){
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    } 
    public void Show(Action onCloseButtonAction){
        base.Show();
        soundEffectsButton.Select();
        this.onCloseButtonAction = onCloseButtonAction;
    }

    private void ShowPressToRebindKey(){
        ReBindingUI.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey(){
        ReBindingUI.gameObject.SetActive(false);
    }
    private void ReBinding(GameInput.Binding binding){
        //重新绑定输入
        GameInput.Instance.Rebinding(binding,() => {
            //隐藏重新绑定提示
            HidePressToRebindKey();
            //更新输入可视化
            UpdateVisual();
        });
        //显示重新绑定提示
        ShowPressToRebindKey();
    }

}