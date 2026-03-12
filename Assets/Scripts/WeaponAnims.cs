using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class WeaponAnims : MonoBehaviour
{
    public AnimationReferenceAsset one_hand_sword;
    public AnimationReferenceAsset one_hand_sword_and_shield;
    public AnimationReferenceAsset two_hand_sword;
    public AnimationReferenceAsset dual_wield;
    public AnimationReferenceAsset idle;

    SkeletonAnimation skeletonAnimation;
    // Start is called before the first frame update

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    public void OneHandSword()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        skeletonAnimation.AnimationState.SetAnimation(2, one_hand_sword, true);
    }

    public void OneHandAndShield()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        skeletonAnimation.AnimationState.SetAnimation(2, one_hand_sword_and_shield, true);
    }

    public void TwoHandSword()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        skeletonAnimation.AnimationState.SetAnimation(2, two_hand_sword, true);
    }

    public void DualWield()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        skeletonAnimation.AnimationState.SetAnimation(2, dual_wield, true);
    }

    public void Idle()
    {
        skeletonAnimation.AnimationState.SetAnimation(2, idle, true);
    }
}