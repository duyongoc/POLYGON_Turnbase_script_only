﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationDataType
{
    ChangeAnimationByState,
    ChangeAnimationByClip,
}

public class AnimationData : ScriptableObject
{

    [Space]
    public AnimationDataType type;

    [Tooltip("0 = No animation")]
    [StringShowConditional(conditionFieldName: "type", conditionValue: "ChangeAnimationByState")]
    public int animationActionState = 0;


    [StringShowConditional(conditionFieldName: "type", conditionValue: "ChangeAnimationByState")]
    public float animationDuration = 0;

    [StringShowConditional(conditionFieldName: "type", conditionValue: "ChangeAnimationByClip")]
    public AnimationClip animationClip;


    public float AnimationDuration
    {
        get
        {
            switch (type)
            {
                case AnimationDataType.ChangeAnimationByState:
                    return animationDuration;

                case AnimationDataType.ChangeAnimationByClip:
                    return animationClip == null ? 0f : animationClip.length;
            }
            return 0f;
        }
    }
}

public static class AnimationDataExtension
{
    public static int GetAnimationActionState(this AnimationData animation)
    {
        return animation == null ? 0 : animation.animationActionState;
    }

    public static float GetAnimationDuration(this AnimationData animation)
    {
        return animation == null ? 0f : animation.AnimationDuration;
    }

    public static AnimationClip GetAnimationClip(this AnimationData animation)
    {
        return animation == null ? null : animation.animationClip;
    }
}