using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteNeedDeleteAnimationCache
{

    private Dictionary<GameObject, HashSet<Type>> _deleteAnimationCache = new Dictionary<GameObject, HashSet<Type>>();

    public void Add(GameObject obj, System.Type animationType)
    {
        HashSet<Type> typeList = null;
        if (!_deleteAnimationCache.TryGetValue(obj, out typeList))
        {
            typeList = new HashSet<Type>();
            _deleteAnimationCache.Add(obj, typeList);
        }

        typeList.Add(animationType);
    }

    public void Remove(GameObject obj, System.Type animationType)
    {
        HashSet<Type> typeList = null;
        if (_deleteAnimationCache.TryGetValue(obj, out typeList))
        {
            if(typeList.Contains(animationType))
            {
                typeList.Remove(animationType);
            }
        }
    }

    public void ForEachItem(Action<GameObject, Type> callBack)
    {
        foreach(var pair in _deleteAnimationCache)
        {
            var obj = pair.Key; 
            foreach(var type in pair.Value)
            {
                callBack(obj, type);
            }
        }
    }
}
