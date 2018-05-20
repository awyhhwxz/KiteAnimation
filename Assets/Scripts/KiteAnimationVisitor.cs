using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KiteAnimationVisitor {

    public static AnimationType AddKiteAnimation<AnimationType>(this GameObject obj) where AnimationType : KiteAnimationBase
    {
        return KiteAnimationContainer.Instance.AddAnimation<AnimationType>(obj);
    }

    public static AnimationType GetKiteAnimation<AnimationType>(this GameObject obj) where AnimationType : KiteAnimationBase
    {
        return KiteAnimationContainer.Instance.GetAnimation<AnimationType>(obj);
    }

    public static void RemoveKiteAnimation<AnimationType>(this GameObject obj) where AnimationType : KiteAnimationBase
    {
        KiteAnimationContainer.Instance.RemoveAnimation<AnimationType>(obj);
    }
}
