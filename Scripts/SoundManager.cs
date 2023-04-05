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
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickupSomething += Player_OnPickupSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }
    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e){
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash,trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e){
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop ,baseCounter.transform.position);
    }

    private void Player_OnPickupSomething(object sender, EventArgs e){
        Player player = Player.Instance;
        PlaySound(audioClipRefsSO.objectPickup,player.transform.position);
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