using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Image bgJoyStick, stick;
    private Vector3 inputVector;
    public bool isRaw;

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgJoyStick.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = pos.x / bgJoyStick.rectTransform.sizeDelta.x;
            pos.y = pos.y / bgJoyStick.rectTransform.sizeDelta.y;

            inputVector = new Vector3(pos.x * 2 - 1, 0, pos.y * 2 - 1);

            if (inputVector.magnitude > 1)
            {
                inputVector = inputVector.normalized;
            }

            float sizeBg = bgJoyStick.rectTransform.sizeDelta.x / 3;

            stick.rectTransform.anchoredPosition = new Vector3(inputVector.x * sizeBg, inputVector.z * sizeBg, 0);
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        stick.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (isRaw == true)
        {
            if (inputVector.x > 0.01f)
            {
                inputVector.x = 1;
            }
            else if (inputVector.x < -0.01f)
            {
                inputVector.x = -1;
            }
            else
            {
                inputVector.x = 0;
            }
        }

        return inputVector.x;
    }

    public float Vertical()
    {
        if (isRaw == true)
        {
            if (inputVector.z > 0.01f)
            {
                inputVector.z = 1;
            }
            else if (inputVector.z < -0.01f)
            {
                inputVector.z = -1;
            }
            else
            {
                inputVector.z = 0;
            }
        }

        return inputVector.z;
    }
}
