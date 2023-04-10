using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour{
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource audioSource;
    private bool playtWarningSound = false;
    private float warningSoundTimer;

    private void Awake() { 
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Start() {
        stoveCounter.OnProgressChange += StoveCounter_OnProgressChange;
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }
    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e){
        float burnShowProgressAmount = .5f;
        playtWarningSound = stoveCounter.IsFired() && (e.progressNormalized >= burnShowProgressAmount);
    }
    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e){
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if(playSound){
            audioSource.Play();
        }else{
            audioSource.Pause();
        }
    }

    private void Update() {
        if(playtWarningSound){
            warningSoundTimer -= Time.deltaTime;
            if(warningSoundTimer <= 0){
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayStoveBurnWaningSound(stoveCounter.transform.position);
            }
        }
    }
}