using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 
public class Player : MonoBehaviour, IKitchenObjectParent{
    public static Player Instance {get; private set; }

    public event EventHandler OnPickupSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs{
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;//接收物品位置

    private const float EPS = 1e-3f;//微小偏移量
    private Vector3 lastInteractDir;// 上一次交互方向
    private bool isWalking = false;//正在移动
    private BaseCounter selectedCounter; // 选中的柜台
    private KitchenObject kitchenObject;//手里的物品

    private void Awake() {
        if(Instance != null){
            Debug.LogError("多个实例");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;//注册
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;// 注册
    }
    
    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e){
        if(!GameManager.Instance.IsGamePlaying())return ;
        if(selectedCounter != null){
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e){
        if(!GameManager.Instance.IsGamePlaying())return ;
        if(selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }


    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking (){
        return isWalking;
    }

    /// <summary>
    /// 处理交互
    /// </summary>
    private void HandleInteractions(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x,0,inputVector.y);

        if(moveDir != Vector3.zero){
             lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position,lastInteractDir, out RaycastHit raycastHit,interactDistance,countersLayerMask)){
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                if(baseCounter != selectedCounter){
                    SetSelectedCounter(baseCounter);
                }
            }else {
                SetSelectedCounter(null);
            }
        }else{
            SetSelectedCounter(null);
        }
    }
    /// <summary>
    /// 处理移动
    /// </summary>
    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();//创建输入的二维向量，在逻辑上与移动向量分离
        //单独创建移动向量
        Vector3 moveDir = new Vector3(inputVector.x,0,inputVector.y);

        float moveDistance = Time.deltaTime * moveSpeed;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,playerRadius,moveDir,moveDistance);
        if(Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,playerRadius,moveDir,out RaycastHit raycastHit,moveDistance)){
            Debug.Log(raycastHit.collider.name);
            Debug.Log(canMove);
            Debug.Log(moveDir);
        }
        
        //处理移动方向
        if(!canMove){ // 目标方向不能移动
            //分解移动向量 尝试向X方向移动
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized;
            canMove = (moveDirX.x > .5f || moveDirX.x < -.5f)  && !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,playerRadius,moveDirX,moveDistance);
            if(canMove){ //可以在X方向移动
                moveDir = moveDirX;
            }else{ //X 方向不能行走
                //尝试向Z轴运动
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized;
                canMove = (moveDirZ.z > .5f || moveDirZ.z < -.5f) && !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,playerRadius,moveDirZ,moveDistance);
                if(canMove){ //可以向Z方向移动
                    moveDir = moveDirZ;
                }else{ //两个方向都不能移动
                }
            }
        }

        if(canMove)this.transform.position += moveDir * moveDistance;
        
        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 20f;
        this.transform.forward = Vector3.Lerp(this.transform.forward,moveDir + new Vector3(EPS,0,EPS),Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter){
        this.selectedCounter = selectedCounter;
        
        OnSelectCounterChanged?.Invoke(this,new OnSelectedCounterChangedEventArgs{
            selectedCounter = selectedCounter
        });
    }


    public Transform GetKitchenObjectFollowTransform(){
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null){
            OnPickupSomething?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject(){
        return this.kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HaskitchenObject(){
        return kitchenObject != null;
    }
}
