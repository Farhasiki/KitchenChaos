using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour{

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private OptionsUI optionsUI;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        }); 
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        }); 
        optionsButton.onClick.AddListener(() => {
            Hide();
            optionsUI.Show(Show);
        }); 
    }

    private void Start() {
        GameManager.Instance.OnTogglePauseGame += GameManager_OnToggleGamePause;
        Hide();
    }

    private void GameManager_OnToggleGamePause(object sender,GameManager.OnTogglePauseGameEventArgs e){
        if(e.timeScale == 0){
            Show();
        }else{
            Hide();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
        resumeButton.Select();
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}