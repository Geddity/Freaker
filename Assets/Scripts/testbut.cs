using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.AttachmentTools;

public class testbut : MonoBehaviour
{
    public void Combine ()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        Skeleton skeleton = skeletonAnimation.skeleton;

        skeleton.SetSkin("armor/light/1/lth_armr1_boots");
        skeleton.SetSlotsToSetupPose();
        skeletonAnimation.Update(0);
    }
}
