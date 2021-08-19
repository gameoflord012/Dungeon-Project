﻿using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ActorAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMovementParameter(bool isWalk)
    {
        animator.SetBool("isWalk", isWalk);
    }
}