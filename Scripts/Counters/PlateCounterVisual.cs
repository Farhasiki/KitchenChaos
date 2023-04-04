using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCouter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGamejectList;
    private void Awake() {
        plateVisualGamejectList = new List<GameObject>();
    }
    private void Start() {
        plateCouter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCouter.OnPlateRemove += PlateCounter_OnPlateDestroy;
    }

    // 定义一个名为 PlateCounter_OnPlateSpawned 的私有方法，该方法用于在柜台顶部创建一个盘子可视化物体
    private void PlateCounter_OnPlateSpawned(object sender, EventArgs e){
        // 在柜台顶部创建一个盘子可视化物体，保存其 Transform 组件
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        // 定义盘子之间的偏移量
        float plateOffset = 0.1f;
        // 计算新创建盘子的位置，使其垂直向上偏移一定距离
        plateVisualTransform.localPosition = new Vector3(0, plateOffset * plateVisualGamejectList.Count, 0);
        // 将新创建的盘子可视化物体添加到列表中
        plateVisualGamejectList.Add(plateVisualTransform.gameObject);
    }

    // 定义一个名为 PlateCounter_OnPlateDestroy 的私有方法，该方法用于在销毁盘子时，移除盘子可视化物体并销毁其 GameObject
    private void PlateCounter_OnPlateDestroy(object sender, EventArgs e){
        // 获取列表中最后一个盘子可视化物体
        GameObject plateVisualObject = plateVisualGamejectList[plateVisualGamejectList.Count - 1];
        // 销毁盘子可视化物体的 GameObject
        Destroy(plateVisualObject);
        // 从列表中移除该盘子可视化物体
        plateVisualGamejectList.Remove(plateVisualObject);
    }

}

