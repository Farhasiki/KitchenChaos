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

    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e){
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.IsFired() && (e.progressNormalized >= burnShowProgressAmount);

        animator.SetBool(IS_FLASHING,show);
    }
}