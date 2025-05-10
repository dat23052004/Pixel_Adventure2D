using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 0.3f;
    private float destroyDelay = 2f;
    private float fallSpeed = 25f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem particleSystem;
    private string currentAnim = "";
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FeetPlayer"))
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        ChangeAnim("Off");
        particleSystem.Stop();
        yield return new WaitForSeconds(.7f);

        StartCoroutine(FallDown());

        Destroy(gameObject, destroyDelay);
    }

    private IEnumerator FallDown()
    {
        while(true)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            yield return null;
        }
    }

    void ChangeAnim(string anim)
    {

        if (currentAnim == anim) return; 

        currentAnim = anim;
        animator.Play(currentAnim);
    }
}
