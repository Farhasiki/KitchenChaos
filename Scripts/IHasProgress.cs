using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHasProgress{
    // 定义 OnProgressChange 事件
    public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;
    // 定义一个内部类，用于传递进度值
    public class OnProgressChangeEventArgs : EventArgs{
        public float progressNormalized;
    }
}
