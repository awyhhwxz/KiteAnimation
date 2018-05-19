using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteCommonAnimation : KiteAnimationBase
{
    public KiteAnimationKeyInfo StartKey;
    public KiteAnimationKeyInfo EndKey;

    public KiteCommonAnimation(GameObject rootObj)
        : base(rootObj)
    {

    }

    protected override void PlayImpl()
    {
        base.PlayImpl();
    }

    public override void ToTheEnd()
    {
        CompleteAnimation();
    }

    public override void Update()
    {
        var percent = _currentTime / Duration;
        if(Mathf.Equals(percent, 1.0f))
        {
            CompleteAnimation();
        }
        else
        {
            LerpAnimation(percent);
            if(PerFrameCallBack!=null)
            {
                PerFrameCallBack.Invoke(_rootObj, percent);
            }
            _currentTime += Time.deltaTime;
            _currentTime = Mathf.Min(_currentTime, Duration);
        }
    }

    protected void LerpAnimation(float percent)
    {
        _rootObj.transform.localPosition = Vector3.Lerp(StartKey.Position, EndKey.Position, percent);
        if(NeedLinearLerpEuler)
        {
            _rootObj.transform.localEulerAngles = Vector3.Lerp(StartKey.Rotation, EndKey.Rotation, percent);
        }
        else
        {
            _rootObj.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(StartKey.Rotation), Quaternion.Euler(EndKey.Rotation), percent);
        }
    }

    protected void CompleteAnimation()
    {
        _rootObj.transform.localPosition = EndKey.Position;
        _rootObj.transform.localEulerAngles = EndKey.Rotation;

        if (PerFrameCallBack != null)
        {
            PerFrameCallBack.Invoke(_rootObj, 1.0f);
        }

        if(CompleteCallBack != null)
        {
            CompleteCallBack.Invoke(KiteAnimationCallBackType.Complete, _rootObj, 1.0f);
        }

        OnAnimationComplete();
    }
}
