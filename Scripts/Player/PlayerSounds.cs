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

    private void Update(){
        // 增加播放声音的计时器
        playSoundTimer += Time.deltaTime;
        
        // 如果计时器达到最大值
        if(playSoundTimer >= playSoundTimerMax){
            // 重置计时器
            playSoundTimer = 0;

            // 如果玩家正在行走
            if(player.IsWalking()){
                // 播放脚步声音
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(transform.position, volume);
            }
        }
    }

}