using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour{

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public static MusicManager Instance {get; private set;} 
    private AudioSource audioSource;
    private float volume = .3f;

    private void Awake() {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME,.3f);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }
    public void ChangeValume(){
        volume += .1f; //增加音量
        if(volume > 1.09){ //如果超过最大音量，归零
            volume = 0f;
        }
        audioSource.volume = volume; //将音量值应用到 AudioSource 上
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME,volume); //将音量值存储到 PlayerPrefs 中
        PlayerPrefs.Save(); //保存 PlayerPrefs 中的数据
    }

    public float GetVolume(){
        return volume;
    }
}
