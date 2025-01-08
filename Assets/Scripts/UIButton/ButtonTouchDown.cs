using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTouchDown : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerController controller;
    public void OnPointerClick(PointerEventData eventData)
    {
       if(gameObject.name == "Button Jump")
        {
            controller.PlayerJump();
        }
       if(gameObject.name == "Button Restart")
        {
            controller.GameRestart();
        }
    }
}
