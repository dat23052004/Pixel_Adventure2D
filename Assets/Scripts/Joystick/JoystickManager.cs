using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    PlayerController PlayerControllerComponent;
    public GameObject TouchPointIcon;
    Collider2D Collider2DComponent;

    private void Awake()
    {
        PlayerControllerComponent = FindObjectOfType<PlayerController>();
        Collider2DComponent = GetComponent<Collider2D>();
    }
    public void OnJoystickTouching(Vector2 TouchPosition)
    {
        MoveTouchPointIcon(TouchPosition);
        Vector2 JoystickDirection = Direction2Points2D(TouchPosition, transform.position);

        if (JoystickDirection.x > 0) //move Player to Right
        {
            PlayerControllerComponent.isPressedButtonRight = true;
            PlayerControllerComponent.isPressedButtonLeft = false;
        }
        else if (JoystickDirection.x < 0) //move Player to Left
        {
            PlayerControllerComponent.isPressedButtonRight = false;
            PlayerControllerComponent.isPressedButtonLeft = true;
        }
        else // =0 : Player stop movement
        {
            PlayerControllerComponent.isPressedButtonRight = false;
            PlayerControllerComponent.isPressedButtonLeft = false;
        }
    }

    Vector2 Direction2Points2D(Vector2 Point1, Vector2 Point2) // Start from Point1
    {
        var Direction2D = Point1 - Point2;
        return Direction2D.normalized;
    }

    int Angle360Between2Points2D(Vector2 Point1, Vector2 Point2) // Point1 is Center Position
    {
        var AngleValue = Mathf.RoundToInt((float)((Mathf.Atan2(Point2.y - Point1.y, Point2.x - Point1.x) * (180 / 3.14)) % 360));

        if (AngleValue < 0)
        {
            return Mathf.RoundToInt(360 - Mathf.Abs(AngleValue));
        }
        else
        {
            return AngleValue;
        }
    }

    void MoveTouchPointIcon(Vector2 TouchPosition)
    {
        var ClosestPosition = Collider2DComponent.ClosestPoint(TouchPosition);
        TouchPointIcon.transform.position = ClosestPosition;
    }


}
