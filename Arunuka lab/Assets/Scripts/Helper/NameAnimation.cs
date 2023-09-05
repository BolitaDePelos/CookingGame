using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void PlayAnimationByName(string animationName)
    {
        // Play anim by name
        animator.Play(animationName);
    }
}
