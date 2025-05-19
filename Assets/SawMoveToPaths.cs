using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMoveToPaths : MonoBehaviour
{
    [SerializeField] List<Transform> paths;         
    public float moveSpeed = 2f;          
    public float waitTime = 1f;           

    private int currentIndex = 0;         
    private bool isWaiting = false;

    private void Update()
    {
        if (paths.Count == 0 || isWaiting) return;

        
        Transform target = paths[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAndMoveNext());
        }
    }

    IEnumerator WaitAndMoveNext()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        
        currentIndex = (currentIndex + 1) % paths.Count;
        isWaiting = false;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i] != null)
            {
                Gizmos.DrawWireSphere(paths[i].position, 0.1f);
                if (i < paths.Count - 1)
                    Gizmos.DrawLine(paths[i].position, paths[i + 1].position);
                else
                    Gizmos.DrawLine(paths[i].position, paths[0].position); // Vòng lại điểm đầu
            }
        }
    }

}
