using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance {get; set;}
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private float volume = 1f;
    private void Awake() {
        // 将该脚本对象赋值给 Instance 静态变量
        Instance = this;
        // 从 PlayerPrefs 中获取存储的音效音量值，如果没有存储过，则使用默认值 1f
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start() {
        // 订阅事件，当菜单配送管理器触发菜谱失败事件时，调用DeliveryManager_OnRecipeFailed方法
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        // 订阅事件，当菜单配送管理器触发菜谱成功事件时，调用DeliveryManager_OnRecipeSuccess方法
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        // 订阅事件，当切菜计数器触发任意切割事件时，调用CuttingCounter_OnAnyCut方法
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        // 订阅事件，当玩家实例触发拾取物品事件时，调用Player_OnPickupSomething方法
        Player.Instance.OnPickupSomething += Player_OnPickupSomething;
        // 订阅事件，当物品放置区域触发任意物品放置事件时，调用BaseCounter_OnAnyObjectPlacedHere方法
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        // 订阅事件，当垃圾桶触发任意物品丢弃事件时，调用TrashCounter_OnAnyObjectTrashed方法
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed; 
    }

    // 当任何物体被扔进垃圾桶时触发
    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e){
        // 将sender转换为TrashCounter类型的变量
        TrashCounter trashCounter = sender as TrashCounter;
        // 播放声音并指定音频片段和位置
        PlaySound(audioClipRefsSO.trash,trashCounter.transform.position);
    }


    // 当在计数器上放置物品时
    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e){
        // 转换为计数器类型
        BaseCounter baseCounter = sender as BaseCounter;
        // 播放声音，传入对应的音频剪辑和位置
        PlaySound(audioClipRefsSO.objectDrop ,baseCounter.transform.position);
    }


    // 当玩家捡起了某个物品时触发的事件处理函数
    private void Player_OnPickupSomething(object sender, EventArgs e){
        // 获取当前场景中唯一的玩家对象
        Player player = Player.Instance;
        // 播放物品捡起的声音效果
        PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
    }


    // 它会在 "CuttingCounter" 的任何一个切割事件被触发时被调用
    private void CuttingCounter_OnAnyCut(object sender, EventArgs e){
        // 将发送者（即 "CuttingCounter" 实例）转换为 "CuttingCounter" 类型
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        // 播放切割音效
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }


    // 当一个食谱无法被成功配送时被调用
    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e){
        // 获取 "DeliveryCounter" 的实例
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        // 播放配送失败的音效
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    // 当一个食谱成功被配送时被调用
    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e){
        // 获取 "DeliveryCounter" 的实例
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        // 播放配送成功的音效
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    // 这个方法用于播放音效，传入一个音效剪辑数组、位置和音量参数
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1){
        // 从音效剪辑数组中随机选择一个音效剪辑
        AudioClip clip = audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)];
        // 调用重载的 PlaySound 方法，传入选定的音效剪辑、位置和音量参数
        PlaySound(clip, position, volume);
    }

    // 这个方法用于在指定的位置播放音效
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1){
        // 调用 AudioSource 类的静态方法 PlayClipAtPoint，在指定位置播放音效剪辑
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    
    // 这个方法用于在指定位置播放脚步声
    public void PlayFootstepsSound(Vector3 position, float volumeMultipliter = 1){
        // 调用 PlaySound 方法，传入脚步声音效剪辑以及指定位置和音量
        PlaySound(audioClipRefsSO.footstep, position, volume * volumeMultipliter);
    }
    
    // 用于调整音效音量
    public void ChangeValume(){
        // 每次调用方法，将音效音量增加 0.1
        volume += .1f;
        // 检查音效音量是否已达到最大值（1.09）
        if(volume > 1.09){
            // 若已达到最大值，则将音效音量重置为 0
            volume = 0f;
        }
        // 使用 PlayerPrefs 将更新后的音效音量保存到本地
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,volume);
        PlayerPrefs.Save();
    }

    public float GetVolume(){
        return volume;
    }
}