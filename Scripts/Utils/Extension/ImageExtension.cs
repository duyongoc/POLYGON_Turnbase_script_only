using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExtension
{


    public static void DoFadeOnce(this Image image, float duration, System.Action callback = null)
    {
        image?.DOFade(1, 0);
        image?.DOFade(0, duration).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }

    public static bool ActiveSelf(this Image image)
    {
        return image.gameObject.activeSelf;
    }

    public static void SetActive(this Image image, bool value)
    {
        if (image is null || image == null)
            return;

        image.gameObject?.SetActive(value);
    }


}
