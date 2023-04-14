using UnityEngine;

public class ToggleAnimationOnClick : MonoBehaviour
{
    public Animator[] partAnimators;
    public string animationName;
    public string reverseAnimationName;

    private bool isAnimationForward = true;

    private void OnMouseDown()
    {
        foreach (Animator animator in partAnimators)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                animator.Play(reverseAnimationName);
            }
            else
            {
                animator.Play(animationName);
            }
        }
    }
}