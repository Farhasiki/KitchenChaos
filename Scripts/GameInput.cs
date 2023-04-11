using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour{
    public static GameInput Instance {get; private set;}
    private PlayerInputActions playerInputActions; 
    public event EventHandler OnInteractAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnBindingRebind;
    private const string PLAYER_PREFS_BINDINGS = "IOnputBindings";

    public enum Binding{
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
    }
    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += interact_performed;//注册互动事件
        playerInputActions.Player.IntrtactAlternate.performed += interactAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_Performed;
        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)){
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
    } 

    private void OnDestroy() {
        // 取消对 Player.Interact.performed 事件的监听
        playerInputActions.Player.Interact.performed -= interact_performed;
        // 取消对 Player.InteractAlternate.performed 事件的监听
        playerInputActions.Player.IntrtactAlternate.performed -= interactAlternate_performed;
        // 取消对 Player.Pause.performed 事件的监听
        playerInputActions.Player.Pause.performed -= Pause_Performed;
        // 销毁 playerInputActions 对象
        playerInputActions.Dispose();
    }


    //(Input Keycode.F to trigger) 玩家输入f触发交互事件
    private void interactAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAlternateAction?.Invoke(this,EventArgs.Empty);
    }
    
    //(Input Keycode.E to trigger) 玩家输入e触发交互事件
    private void interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }

    private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnPauseAction?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized(){
        // 获取玩家输入的方向向量
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();//新输入系统获取方向向量

        // 将方向向量归一化
        inputVector = inputVector.normalized;
        // 返回归一化后的方向向量
        return inputVector;
    }



    public string GetBindingText(Binding binding){
        // 使用 switch 语句处理不同的绑定类型
        switch (binding){
            // 默认情况下，返回 Pause 动作的第一个绑定
            default:
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            // 返回 Interact 动作的第一个绑定
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            // 返回 IntrtactAlternate 动作的第一个绑定
            case Binding.InteractAlternate:
                return playerInputActions.Player.IntrtactAlternate.bindings[0].ToDisplayString();
            // 返回 Move 动作的第二个绑定
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            // 返回 Move 动作的第三个绑定
            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            // 返回 Move 动作的第四个绑定
            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            // 返回 Move 动作的第五个绑定
            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
        }
    }

    public void Rebinding(Binding binding, Action onActionRebound){
        InputAction inputAction;
        int bindingIdx;
        // 根据不同的绑定类型获取对应的输入动作和绑定索引
        switch (binding){
            // Move_Up 对应 Move 动作的第二个绑定
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIdx = 1;
                break;
            // Move_Down 对应 Move 动作的第三个绑定
            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIdx = 2;
                break;
            // Move_Left 对应 Move 动作的第四个绑定
            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIdx = 3;
                break;
            // Move_Right 对应 Move 动作的第五个绑定
            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIdx = 4;
                break;
            // Interact 对应 Interact 动作的第一个绑定
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIdx = 0;
                break;
            // IntrtactAlternate 对应 IntrtactAlternate 动作的第一个绑定
            case Binding.InteractAlternate:
                inputAction = playerInputActions.Player.IntrtactAlternate;
                bindingIdx = 0;
                break;
            // Pause 对应 Pause 动作的第一个绑定
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIdx = 0;
                break;
        }
        // 禁用玩家输入系统
        playerInputActions.Player.Disable();
        // 执行交互重绑定操作
        inputAction.PerformInteractiveRebinding(bindingIdx) 
        // 绑定完成后执行的回调函数
        .OnComplete(callback => {
            // 释放回调对象
            callback.Dispose();
            // 启用玩家输入系统
            playerInputActions.Player.Enable();
            // 调用重绑定完成后的回调函数
            onActionRebound();
            // 将绑定数据保存到 PlayerPrefs 中
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS,playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            // 触发绑定重绑定完成事件
            OnBindingRebind?.Invoke(this,EventArgs.Empty);
        })
        .Start();
    }

}