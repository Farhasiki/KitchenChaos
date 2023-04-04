using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour{
    enum Mode{
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }
    [SerializeField] Mode mode;
    private void LateUpdate() {
        // 根据 mode 值的不同，控制物体的朝向
        switch (mode){
            // 如果 mode 的值为 Mode.LookAt，则设置物体的朝向面向相机
            case Mode.LookAt: 
                transform.LookAt(Camera.main.transform);
                break;
            // 如果 mode 的值为 Mode.LookAtInverted，则设置物体的朝向背向相机
            case Mode.LookAtInverted: 
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(dirFromCamera + transform.position);
                break;
            // 如果 mode 的值为 Mode.CameraForward，则设置物体的朝向与相机的前向向量相同
            case Mode.CameraForward: 
                transform.forward = Camera.main.transform.forward;
                break;
            // 如果 mode 的值为 Mode.CameraForwardInverted，则设置物体的朝向与相机的前向向量相反
            case Mode.CameraForwardInverted: 
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}   