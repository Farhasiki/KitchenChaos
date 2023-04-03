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

    private void PlateCounter_OnPlateSpawned(object sender, EventArgs e){
        Transform plateVisualTransform = Instantiate(plateVisualPrefab,counterTopPoint);

        float plateOffset = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0,plateOffset * plateVisualGamejectList.Count,0);

        plateVisualGamejectList.Add(plateVisualTransform.gameObject);
    }
    private void PlateCounter_OnPlateDestroy(object sender, EventArgs e){
        GameObject plateVisualObject = plateVisualGamejectList[plateVisualGamejectList.Count - 1];
        
        Destroy(plateVisualObject);
        plateVisualGamejectList.Remove(plateVisualObject);
    }
}

