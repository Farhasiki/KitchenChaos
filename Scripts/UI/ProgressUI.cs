using System.Threading;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour{
    [SerializeField] GameObject hasProgressObject;
    private IHasProgress hasProgress;
    [SerializeField] Image progressBar;
    private void Start() {
        hasProgress = hasProgressObject.GetComponent<IHasProgress>();
        if(hasProgress == null){
            Debug.LogError("The object" + hasProgressObject + "does not have IHasProgress");
        }
        hasProgress.OnProgressChange += CuttingCounter_OnprogessChange;

        progressBar.fillAmount = 0f;
        Hide();
    }
    private void CuttingCounter_OnprogessChange(object sender, IHasProgress.OnProgressChangeEventArgs e){
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