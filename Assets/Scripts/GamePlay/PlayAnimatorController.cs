using UnityEngine;

public class PlayAnimatorController : MonoBehaviour
{


    public  AnimationClip walkingAnim;
    public  AnimationClip idleAnim;
    public  AnimationClip attackAnim;


    /*<summary>
     * This method plays an animation for a given animator component
     * </summary>
     * <params>
     * -clip: an animation clip to be played
     * -animator: an animator component that contains the clip
     * </param>
     * <returns>
     * void
     * </returns>
     */
    public void PlayAnimation(AnimationClip clip, Animator animator)
    {
        animator.Play(clip.name);
    }

    public static void SetBoolForAnimator(Animator animator, bool value, string flag)
    {
        animator.SetBool(flag, value);
    }


}
