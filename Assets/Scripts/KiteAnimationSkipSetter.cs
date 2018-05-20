using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteAnimationSkipSetter : System.IDisposable
{
    private bool _valueCache = false;
	
    public KiteAnimationSkipSetter()
    {
        _valueCache = KiteAnimationContainer.Instance.NeedSkipAnimation;
        KiteAnimationContainer.Instance.NeedSkipAnimation = true;
    }

    public void Dispose()
    {
        KiteAnimationContainer.Instance.NeedSkipAnimation = _valueCache;
    }
}
