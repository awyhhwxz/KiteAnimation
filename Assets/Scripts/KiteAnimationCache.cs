
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteAnimationCache {
    
    private Dictionary<GameObject, List<IKiteAnimation>> _animationCache = new Dictionary<GameObject, List<IKiteAnimation>>();

    public IKiteAnimation Add(GameObject obj, System.Type animationType)
    {
        List<IKiteAnimation> animationList = null;
        if (!_animationCache.TryGetValue(obj, out animationList))
        {
            animationList = new List<IKiteAnimation>();
            _animationCache.Add(obj, animationList);
        }

        var targetAnimation = animationList.Find(animation =>
        {
            return animation.GetType().Equals(animationType);
        });

        if (targetAnimation == null)
        {
            targetAnimation = System.Activator.CreateInstance(animationType, new object[] { obj }) as IKiteAnimation;
            animationList.Add(targetAnimation);
        }

        return targetAnimation;
    }

    public IKiteAnimation Get(GameObject obj, System.Type animationType)
    {
        List<IKiteAnimation> animationList = null;
        if (_animationCache.TryGetValue(obj, out animationList))
        {
            var foundAnimation = animationList.Find(animation => animation.GetType().Equals(typeof(IKiteAnimation)));
            if (foundAnimation != null)
            {
                return foundAnimation as IKiteAnimation;
            }
        }

        return null;
    }

    public void Stop(GameObject obj, System.Type animationType)
    {
        List<IKiteAnimation> animationList = null;
        if (_animationCache.TryGetValue(obj, out animationList))
        {
            var foundAnimation = animationList.Find(animation => animation.GetType().Equals(animationType));
            if (foundAnimation != null)
            {
                var animationBase = foundAnimation as KiteAnimationBase;
                animationBase.Enable = false;
                animationBase.OnAnimationRemoved();
            }
        }
    }

    public void Remove(GameObject obj, System.Type animationType)
    {

        List<IKiteAnimation> animationList = null;
        if (_animationCache.TryGetValue(obj, out animationList))
        {
            var foundAnimationIndex = animationList.FindIndex(animation => animation.GetType().Equals(animationType));
            if (foundAnimationIndex >= 0)
            {
                animationList.RemoveAt(foundAnimationIndex);
            }

            if (animationList.Count == 0)
            {
                _animationCache.Remove(obj);
            }
        }
    }

    public void ForEachItem(System.Action<GameObject, IKiteAnimation> callBack)
    {
        foreach (var pair in _animationCache)
        {
            var obj = pair.Key;
            foreach (var animation in pair.Value)
            {
                if(animation.Enable)
                {
                    callBack(obj, animation);
                }
            }
        }
    }
}
