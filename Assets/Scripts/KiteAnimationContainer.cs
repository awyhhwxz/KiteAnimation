using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteAnimationContainer : MonoBehaviour {

    private static KiteAnimationContainer _instance = null;
    public static KiteAnimationContainer Instance {
        get
        {
            if(_instance == null)
            {
                var rootObj = new GameObject("KiteAnimationRootObj");
                _instance = rootObj.AddComponent<KiteAnimationContainer>();
            }
            return _instance;
        }
    }

    private KiteAnimationContainer()
    {
    }

    public bool NeedSkipAnimation = false;

    private KiteAnimationCache _animationCache = new KiteAnimationCache();
    private KiteNeedDeleteAnimationCache _deleteAnimationCache = new KiteNeedDeleteAnimationCache();

    public AnimationType AddAnimation<AnimationType>(GameObject obj) where AnimationType : KiteAnimationBase
    {
        _deleteAnimationCache.Remove(obj, typeof(AnimationType));
        return _animationCache.Add(obj, typeof(AnimationType)) as AnimationType;
    }

    public AnimationType GetAnimation<AnimationType>(GameObject obj) where AnimationType : KiteAnimationBase
    {
        return _animationCache.Get(obj, typeof(AnimationType)) as AnimationType;
    }

    public void RemoveAnimation<AnimationType>(GameObject obj) where AnimationType : KiteAnimationBase
    {
        RemoveAnimation(obj, typeof(AnimationType));
    }

    public void RemoveAnimation(GameObject obj, System.Type animationType)
    {
        _animationCache.Stop(obj, animationType);
        _deleteAnimationCache.Add(obj, animationType);
    }

    // Update is called once per frame
    void Update ()
    {        
        _animationCache.ForEachItem((obj, animation) => animation.Update());
        _deleteAnimationCache.ForEachItem((obj, animationType)=>_animationCache.Remove(obj, animationType));
    }
}
