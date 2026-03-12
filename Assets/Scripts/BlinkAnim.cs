using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class BlinkAnim : MonoBehaviour
{
    [SpineAnimation]
    public string blink;
    public string dress_idle;

    SkeletonAnimation skeletonAnimation;
    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        // Play the walk animation on track 0.
        skeletonAnimation.AnimationState.SetAnimation(0, dress_idle, true);

        // Repeatedly play the gungrab and gunkeep animation on track 1.
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.9f, 9f));
            skeletonAnimation.AnimationState.SetAnimation(1, blink, false);

        }
       
    }
}
