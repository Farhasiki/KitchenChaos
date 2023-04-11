using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour{
    [SerializeField]private StoveCounter stoveCounter;
    private void Start() {
        stoveCounter.OnProgressChange += StoveCounter_OnProgressChange;
        Hide();
    }

    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e){
        float burnShowProgressAmount = .5f;  // 灶台开始显示火焰的进度阈值
        
        bool show = stoveCounter.IsFired() && (e.progressNormalized >= burnShowProgressAmount);  // 是否需要显示火焰

        if(show){
            Show();  // 显示火焰
        }else{
            Hide();  // 隐藏火焰
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}