using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour{
    private PlayerInputActions playerInputActions; 
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += interact_performed;//注册互动事件
        playerInputActions.Player.IntrtactAlternate.performed += interactAlternate_performed;
    }

    //(Input Keycode.F to trigger) 玩家输入f触发交互事件
    private void interactAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAlternateAction?.Invoke(this,EventArgs.Empty);
    }
    
    //(Input Keycode.E to trigger) 玩家输入e触发交互事件
    private void interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();//新输入系统获取方向向量

        inputVector = inputVector.normalized;
        return inputVector;
    }
}