using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public Character plSc;
    public RectTransform joystick;
    public Vector2 result;
    // Start is called before the first frame update
    
    public void OnPointerDown(PointerEventData ped)
    {
        ChangeJoy(ped.position);
    }
    public void OnDrag (PointerEventData ped)
    {
        ChangeJoy(ped.position);
    }
    public void OnPointerUp(PointerEventData ped)
    {
        ResetJoy();
    }

    public void ChangeJoy(Vector2 pedPos)
    {
        Vector2 diff = pedPos - (Vector2)GetComponent<RectTransform>().position;
        Vector2 modifiedDiff = diff * (1f / GetComponentInParent<Canvas>().scaleFactor);
        modifiedDiff /= GetComponent<RectTransform>().sizeDelta * 0.5f;
        result = Vector2.ClampMagnitude(modifiedDiff, 1f);
        modifiedDiff = result * GetComponent<RectTransform>().sizeDelta * 0.5f;
        joystick.localPosition = modifiedDiff;
    }
    public void ResetJoy()
    {
        result = Vector2.zero;
        joystick.localPosition = Vector2.zero;
    }
}
