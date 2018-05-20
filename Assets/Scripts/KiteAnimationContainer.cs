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

    private Dictionary<Object, List<IKiteAnimation>> _animationCache = new Dictionary<Object, List<IKiteAnimation>>();
	
    public AnimationType AddAnimation<AnimationType>(GameObject obj) where AnimationType : KiteAnimationBase
    {
        var animationType = typeof(AnimationType);
        List<IKiteAnimation> animationList = null;
        if (!_animationCache.TryGetValue(obj, out animationList))
        {
            animationList = new List<IKiteAnimation>();
            _animationCache.Add(obj, animationList);
        }

        var targetAnimation = animationList.Find(animation =>
        {
            return animation.GetType().Equals(animationType);
        }) as AnimationType;

        if(targetAnimation == null)
        {
            targetAnimation = System.Activator.CreateInstance(animationType, new object[] { obj }) as AnimationType;
            animationList.Add(targetAnimation);
        }

        return targetAnimation;
    }

    public AnimationType GetAnimation<AnimationType>(GameObject obj) where AnimationType : KiteAnimationBase
    {
        List<IKiteAnimation> animationList = null;
        if (_animationCache.TryGetValue(obj, out animationList))
        {
            var foundAnimation = animationList.Find(animation => animation.GetType().Equals(typeof(AnimationType)));
            if (foundAnimation != null)
            {
                return foundAnimation as AnimationType;
            }
        }

        return null;
    }

    public void RemoveAnimation<AnimationType>(GameObject obj) where AnimationType : KiteAnimationBase
    {
        RemoveAnimation(obj, typeof(AnimationType));
    }

    public void RemoveAnimation(GameObject obj, System.Type animationType)
    {
        List<IKiteAnimation> animationList = null;
        if (_animationCache.TryGetValue(obj, out animationList))
        {
            var foundAnimation = animationList.Find(animation => animation.GetType().Equals(animationType));
            if(foundAnimation != null)
            {
                var animationBase = foundAnimation as KiteAnimationBase;
                animationBase.OnAnimationRemoved();
                animationList.Remove(foundAnimation);
            }

            if(animationList.Count == 0)
            {
                _animationCache.Remove(obj);
            }
        }
    }

    // Update is called once per frame
    void Update () {

        foreach(var pair in _animationCache)
        {
            var animationList = pair.Value;
            foreach(var animation in animationList)
            {
                if(animation.Enable)
                {
                    animation.Update();
                }
            }
        }
    }
}
