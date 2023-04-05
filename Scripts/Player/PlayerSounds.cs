 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour{
    private Player player;

    private float playSoundTimer;
    private float playSoundTimerMax = .1f;
    private void Start() {
        player = GetComponent<Player>();
    }

    private void Update() {
        playSoundTimer += Time.deltaTime;
        if(playSoundTimer >= playSoundTimerMax){
            playSoundTimer = 0;

            if(player.IsWalking()){
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(transform.position,volume);
            }
        }
    }
}