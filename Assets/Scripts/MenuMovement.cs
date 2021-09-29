using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    private float mousePositionX ;
    private float mousePositionY ;
    // Update is called once per frame
    void Update()
    {
        mousePositionX = Input.mousePosition.y;
        mousePositionX = Input.mousePosition.x;
        GetComponent<RectTransform>().position = new Vector2(
            -(mousePositionX / Screen.width) * 20 + (Screen.width / 2),
            (mousePositionY / Screen.height) * 20 + (Screen.height /2)
        );
    }
}
