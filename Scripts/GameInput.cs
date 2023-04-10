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
        playerInputActions.Player.Interact.performed -= interact_performed;
        playerInputActions.Player.IntrtactAlternate.performed -= interactAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_Performed;
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
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();//新输入系统获取方向向量

        inputVector = inputVector.normalized;
        return inputVector;
    }


    public string GetBindingText(Binding binding){
        switch (binding){
            default:
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputActions.Player.IntrtactAlternate.bindings[0].ToDisplayString();
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
        }
    }
    public void Rebinding(Binding binding, Action onActionRebound){
        
        InputAction inputAction;
        int bindingIdx;
        switch (binding){
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIdx = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIdx = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIdx = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIdx = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIdx = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.Player.IntrtactAlternate;
                bindingIdx = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIdx = 0;
                break;
        }
        playerInputActions.Player.Disable();
        inputAction.PerformInteractiveRebinding(bindingIdx) 
        .OnComplete(callback => {
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS,playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            OnBindingRebind?.Invoke(this,EventArgs.Empty);
        })
        .Start();
    }
}