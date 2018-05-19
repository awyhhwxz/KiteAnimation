using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KiteAnimationCallBackType
{
    Break,
    Complete,
    Perframe,
}

public abstract class KiteAnimationBase : IKiteAnimation
{
    protected GameObject _rootObj;
    protected float _currentTime;

    public KiteAnimationBase(GameObject rootObj)
    {
        _rootObj = rootObj;
    }

    public void Play()
    {
        PlayImpl();
    }

    protected virtual void PlayImpl()
    {
        Enable = true;
        if (!Mathf.Equals(_currentTime, 0.0f))
        {
            InvokeBreakCallBack();
        }
    }

    protected void OnAnimationComplete()
    {
        Enable = false;
        _currentTime = 0.0f;
    }

    public abstract void ToTheEnd();

    public abstract void Update();
    
    public bool Enable { get; set; }

    public float Duration { get; set; }

    public bool NeedLinearLerpEuler = false;


    public Action<GameObject, float> PerFrameCallBack;

    private Action<KiteAnimationCallBackType, GameObject, float> _completeCallBack;

    public Action<KiteAnimationCallBackType, GameObject, float> CompleteCallBack
    {
        get { return _completeCallBack; }
        set
        {
            if (value == null)
            {
                InvokeBreakCallBack();
            }

            _completeCallBack = value;
        }
    }

    protected void InvokeBreakCallBack()
    {
        if (_completeCallBack != null)
        {
            _completeCallBack.Invoke(KiteAnimationCallBackType.Break, _rootObj, _currentTime / Duration);
        }

        _currentTime = 0.0f;
    }
}
