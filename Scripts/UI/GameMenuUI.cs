using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        // 给playButton添加点击事件，跳转到游戏场景
        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
        // 给quitButton添加点击事件，退出应用
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
        // 初始化时间缩放
        Time.timeScale = 1f;
    }

}
