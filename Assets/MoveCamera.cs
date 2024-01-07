using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SmoothMouseLook : MonoBehaviour
{
    bool MiddleDown;
    float sensitivity = 0.1f;
    Vector3 PreviousPosition;
    float minfov;
    void Start()
    {
        PreviousPosition = Input.mousePosition;
        minfov = Camera.main.fieldOfView;
    }

    void Update()
    {
        //When middle click, move the maincamera view
        if (Input.GetMouseButtonDown(2))
            MiddleDown = true;
        if (Input.GetMouseButtonUp(2))
            MiddleDown = false;

        if (MiddleDown)
            transform.LookAt(new Vector3(transform.position.x + sensitivity * (Input.mousePosition.x - PreviousPosition.x), transform.position.y + sensitivity * (Input.mousePosition.y - PreviousPosition.y)));
        else
            PreviousPosition = Input.mousePosition;
        if (Input.GetMouseButton(1))
            Debug.Log(Camera.main.fieldOfView);
        if (Camera.main.fieldOfView + Input.mouseScrollDelta.y * -1 <= minfov && Camera.main.fieldOfView + Input.mouseScrollDelta.y * -1 >= 30)
            Camera.main.fieldOfView += Input.mouseScrollDelta.y * -1;
    }
}