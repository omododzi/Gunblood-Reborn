using UnityEngine;

public class AnimatorController
{
    public Animator animator;

    public void Headshot()
    {
        animator.SetTrigger("Head");
    }

    public void Bodyshot()
    {
        animator.SetTrigger("Body");
    }
}
