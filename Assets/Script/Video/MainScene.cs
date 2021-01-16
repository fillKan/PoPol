using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    [SerializeField] private Animator _Animator;

    public void AnimatorDisable()
    {
        _Animator.enabled = false;
    }
}
