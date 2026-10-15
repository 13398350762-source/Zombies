using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraAnimator : MonoBehaviour
{
    private Animator animator;

    private UnityAction overAction;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TurnLeft(UnityAction action)
    {
        animator.SetTrigger("Left");
        overAction = action;
    }

    public void TurnRight(UnityAction action)
    {
        animator.SetTrigger("Right");
        overAction = action;
    }

    public void PlayOver()
    {
        overAction?.Invoke();
        overAction = null;
    }
}
