using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHasProgress{
    public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;
    public class OnProgressChangeEventArgs : EventArgs{
        public float progressNormalized;
    }
}