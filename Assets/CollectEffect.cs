using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEffect : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    // Update is called once per frame


    IEnumerator PlayAnimation()
    {
        Debug.Log("toiu da");
        var currentAnimationInfo = animator.GetCurrentAnimatorStateInfo(0);
        var animationDuration = currentAnimationInfo.length;
        yield return new WaitForSeconds(animationDuration);
        Destroy(this.gameObject);
    }
}
