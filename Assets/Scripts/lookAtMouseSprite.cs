using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtMouseSprite : MonoBehaviour
{
    public GameObject MouseLook;
    void Update()
    {
        Vector3 position = new Vector3(MouseLook.transform.position.x, MouseLook.transform.position.y, 0F);

        Vector3 targetMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
        float angle = (Mathf.Atan2(targetMouse.y, targetMouse.x) * Mathf.Rad2Deg) + 90F;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
