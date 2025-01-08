using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public SpriteRenderer spriteRendererFill;
    float sizeHeight;


    private void Awake()
    {
        sizeHeight = spriteRendererFill.size.y;
    }
    private void Start()
    {
        
    }
    public void SetHelth(float currentHealth, float maxHealth)
    {
        spriteRendererFill.size = new Vector2(currentHealth/maxHealth, sizeHeight);
    }
}
