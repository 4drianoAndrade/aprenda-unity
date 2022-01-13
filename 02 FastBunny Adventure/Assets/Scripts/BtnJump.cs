using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnJump : MonoBehaviour
{
    public GameObject objectAction;
    //public actions btn;
    public Color[] color;
    private Image btnImg;

    private void Start()
    {
        btnImg = GetComponent<Image>();
        btnImg.color = color[0];
    }

    public void OnPointerDown()
    {
        btnImg.color = color[0];
        //objectAction.SendMessage(btn.ToString(), SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerUp()
    {
        btnImg.color = color[1];
    }
}
