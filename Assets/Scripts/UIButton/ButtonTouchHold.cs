using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTouchHold : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    private bool isHold = false;
    private PlayerController PlayerController;

    JoystickManager joystickManager;
    private void Awake()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        joystickManager = FindObjectOfType<JoystickManager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHold = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(touchEnd());
        
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        if(name == "Touch Area Left")
        {
            joystickManager.OnJoystickTouching(eventData.position);
        }
    }

    private void Update()
    {
        if(isHold == true)
        {
            if(name == "Button Right")
            {
                PlayerController.isPressedButtonRight = true;
            }
            if (name == "Button Left")
            {
                PlayerController.isPressedButtonLeft = true;
            }
            if (name == "Button Run")
            {
                PlayerController.PlayerRunOn();
            }
        }
    }

    IEnumerator touchEnd()
    {
        yield return new WaitForSeconds(0.05f);
        isHold = false;
        if (name == "Button Right")
        {
            PlayerController.isPressedButtonRight = false;
        }
        if (name == "Button Left")
        {
            PlayerController.isPressedButtonLeft = false;
        }
        if (name == "Button Run")
        {
            PlayerController.PlayerRunOff();
        }
        if (name == "Touch Area Left")
        {
            joystickManager.OnJoystickTouching(joystickManager.transform.position);
        }
    }

    
}
