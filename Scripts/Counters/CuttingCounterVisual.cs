using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        cuttingCounter.OnPlayerCuttingObject += CuttingCounter_OnPlayerCuttingObject;
    }
    private void CuttingCounter_OnPlayerCuttingObject(object sender,EventArgs e){
        animator.SetTrigger(CUT);
    }
}