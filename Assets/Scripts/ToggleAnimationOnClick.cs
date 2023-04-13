using UnityEngine;

public class ToggleAnimationOnClick : MonoBehaviour
{
    public Animator[] partAnimators;
    public string animationName;

    private bool isAnimationForward = true;

    private void OnMouseDown()
    {
        foreach (Animator animator in partAnimators)
        {
            if (isAnimationForward)
            {
                animator.Play(animationName, 0, 0f);
                animator.speed = 1;
            }
            else
            {
                animator.Play(animationName, 0, 1f);
                animator.speed = -1;
            }
        }

        isAnimationForward = !isAnimationForward;
    }
}