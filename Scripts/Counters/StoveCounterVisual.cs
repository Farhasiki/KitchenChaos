using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start() {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }
    // 定义一个名为 StoveCounter_OnStateChange 的私有方法，该方法用于根据火炉状态的变化，控制火炉的可视化物体和粒子特效是否显示
    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e){
        // 如果火炉的状态是 Fried 或 Frying，则显示火炉的可视化物体和粒子特效，否则隐藏它们
        bool showVisual = (e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying);
        // 设置火炉的可视化物体是否显示
        stoveOnGameObject.SetActive(showVisual);
        // 设置粒子特效是否显示
        particlesGameObject.SetActive(showVisual);
    }

}