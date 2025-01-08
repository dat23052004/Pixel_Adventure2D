using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(PrepareNonLoopAnaimation());
    }
    IEnumerator PrepareNonLoopAnaimation()
    {
        var currentAnimationInfo = animator.GetCurrentAnimatorStateInfo(0);
        var animationDutarion = currentAnimationInfo.length;
        yield return new WaitForSeconds(animationDutarion);
        Destroy(this.gameObject);
    }
}
