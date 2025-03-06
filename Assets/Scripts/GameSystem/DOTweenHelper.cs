using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using TMPro;

public static class DOTweenHelper //非同期処理用の汎用関数
{
    public static async UniTask LerpAsync<T>(T start, T end, float duration, Ease ease, Action<T> updateValue)
    {
        if (typeof(T) == typeof(float))
        {
            await DOTween.To(() => (float)(object)start, 
                             x => updateValue((T)(object)x), 
                             (float)(object)end, duration)
                        .SetEase(ease)
                        .ToUniTask();
        }
        else if (typeof(T) == typeof(Vector3))
        {
            await DOTween.To(() => (Vector3)(object)start, 
                             x => updateValue((T)(object)x), 
                             (Vector3)(object)end, duration)
                        .SetEase(ease)
                        .ToUniTask();
        }
        else if (typeof(T) == typeof(Quaternion))
        {
            await DOTween.To(() => ((Quaternion)(object)start).eulerAngles, 
                             x => updateValue((T)(object)Quaternion.Euler(x)), 
                             ((Quaternion)(object)end).eulerAngles, duration)
                        .SetEase(ease)
                        .ToUniTask();
        }
        else if (typeof(T) == typeof(Vector2))
        {
            await DOTween.To(() => (Vector2)(object)start, 
                             x => updateValue((T)(object)x), 
                             (Vector2)(object)end, duration)
                        .SetEase(ease)
                        .ToUniTask();
        }
    }
}
