using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Constant.TAG_PLAYER))
            GameManager.Instance.ReachEndPoint();
    }
}
