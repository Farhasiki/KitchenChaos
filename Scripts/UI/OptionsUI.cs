using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour{
    [SerializeField] private Button soundEffectsButton; 
    [SerializeField] private Button musicButton; 
    [SerializeField] private Button closeButton; 
    [SerializeField] private TextMeshProUGUI soundEffectsText; 
    [SerializeField] private TextMeshProUGUI musicText; 

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
            Hide();
        });
    }

    private void Start() {
        GameManager.Instance.OnTogglePauseGame += GameManager_OnTogglePauseGame;
        UpdateVisual();
        Hide();
    }

    private void GameManager_OnTogglePauseGame(object sender, EventArgs e){
        Hide(); 
    }

    private void UpdateVisual(){
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    } 
    public void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}