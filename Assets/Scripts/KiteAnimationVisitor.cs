using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KiteAnimationVisitor {

    public static AnimationType AddKiteAnimation<AnimationType>(this GameObject obj) where AnimationType : KiteAnimationBase
    {
        return KiteAnimationContainer.Instance.AddAnimation<AnimationType>(obj);
    }
}
