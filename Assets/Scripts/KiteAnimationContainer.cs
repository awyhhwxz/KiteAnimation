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
