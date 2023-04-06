using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    public static SoundManager Instance {get; set;}
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake() {
        Instance = this;
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


    private void CuttingCounter_OnAnyCut(object sender, EventArgs e){
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop,cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e){
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail,deliveryCounter.transform.position);
    }
    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e){
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess,deliveryCounter.transform.position);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1){
        PlaySound(audioClipArray[UnityEngine.Random.Range(0,audioClipArray.Length)],position,volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1){
        AudioSource.PlayClipAtPoint(audioClip,position,volume);
    }
    
    public void PlayFootstepsSound(Vector3 position, float volume){
        PlaySound(audioClipRefsSO.footstep,position,volume);
    }
}