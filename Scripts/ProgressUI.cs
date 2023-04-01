using System.Threading;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour{
    [SerializeField] CuttingCounter cuttingCounter;
    [SerializeField] Image progressBar;
    private void Start() {
        cuttingCounter.OnProgressChange += CuttingCounter_OnprogessChange;

        progressBar.fillAmount = 0f;
        Hide();
    }
    private void CuttingCounter_OnprogessChange(object sender, CuttingCounter.OnProgressChangeEventArgs e){
        progressBar.fillAmount = e.progressNormalized;

        if(progressBar.fillAmount == 0 || progressBar.fillAmount == 1){
            Hide();
        }else{
            Show();
        }
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}