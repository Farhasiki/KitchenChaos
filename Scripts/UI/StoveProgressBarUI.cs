using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveProgressBarUI : MonoBehaviour{
    [SerializeField]private StoveCounter stoveCounter;
    private const string IS_FLASHING = "IsFlashing";

    private Animator animator;  
    private void Start() {
        animator = GetComponent<Animator>();
        stoveCounter.OnProgressChange += StoveCounter_OnProgressChange;
        animator.SetBool(IS_FLASHING,false);
    }

    // StoveCounter_OnProgressChange方法会在炉灶计数器进度变化时被调用
    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e){
        // 需要烧焦时的阈值
        float burnShowProgressAmount = .5f;
        // 当炉灶被点燃并且当前进度超过阈值时，显示闪烁效果
        bool show = stoveCounter.IsFired() && (e.progressNormalized >= burnShowProgressAmount);
        // 设置闪烁效果的动画布尔参数
        animator.SetBool(IS_FLASHING,show);
    }
}