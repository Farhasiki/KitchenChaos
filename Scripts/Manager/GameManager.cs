using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour{

    public static GameManager Instance {get; private set;}
    public event EventHandler OnStateChange;
    public event EventHandler<OnTogglePauseGameEventArgs> OnTogglePauseGame;
    public class OnTogglePauseGameEventArgs : EventArgs{
        public int timeScale;
    }

    enum State{
        WaitingToStart,
        CountdownToSart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float countdownToStartTimer = 5f;
    private float gamePlayingTimer = 120f;
    private float gamePlayingTimerMax = 120f;

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }
    private void GameInput_OnPauseAction(object sender, EventArgs e){
        TogglePauseGame();
    }
    private void GameInput_OnInteractAction(object sender, EventArgs e){
        if(state == State.WaitingToStart){
            state = State.CountdownToSart;
            OnStateChange?.Invoke(this,EventArgs.Empty);
        }
    }
    private void Update() {
        switch (state){
            case State.WaitingToStart:
                break;
            case State.CountdownToSart:
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer <= 0){
                    state = State.GamePlaying;
                    OnStateChange?.Invoke(this,EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer <= 0){
                    state = State.GameOver;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChange?.Invoke(this,EventArgs.Empty);
                }
                break;
            case State.GameOver:
                    OnStateChange?.Invoke(this,EventArgs.Empty);
                break;
        }
    }

    public bool IsGamePlaying(){
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive(){
        return state == State.CountdownToSart;
    }

    public float GetCountdownToStartTimer(){
        return countdownToStartTimer;
    }
    public bool IsGameOver(){
        return state == State.GameOver;  
    }
    public float GetGamePlayingTimerNormalized(){
        return 1 - gamePlayingTimer / gamePlayingTimerMax;
    }

    
    public void TogglePauseGame(){
        Time.timeScale = (int)(Time.timeScale) ^ 1;
        OnTogglePauseGame?.Invoke(this,new OnTogglePauseGameEventArgs{
            timeScale = (int)Time.timeScale
        });
    }
}