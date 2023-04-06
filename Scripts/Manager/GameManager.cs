using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour{

    public static GameManager Instance {get; private set;}
    public event EventHandler OnStateChange;

    enum State{
        WaitingToStart,
        CountdownToSart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 2f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 10f;
    private float gamePlayingTimerMax = 10f;

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update() {
        switch (state){
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer <= 0){
                    state = State.CountdownToSart;
                    OnStateChange?.Invoke(this,EventArgs.Empty);
                }
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
        Debug.Log(state);
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
}