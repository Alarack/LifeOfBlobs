using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // I dont think you need any comments here, wont be changing these in here.
    public float panSpeed = 20f;
    float panSpeedMax = 50f, panSpeedMin = 20f;
    public float panBorder = 10f;
    public float scrollSpeed = 20f;
    public AnimationCurve tilt;
    public Vector2 panLimits;
    public float minY = 10f, maxY = 100f;
    float rotY, rotX;
    void Update()
    {
        Vector3 pos = transform.localPosition;

        Quaternion rot = transform.rotation;

        if(Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * 50f * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * 50f * Time.deltaTime;
            rotY += mouseX;
            rotX -= mouseY;
        }
        
        transform.rotation = Quaternion.Euler(rotX, rotY, rot.z);   

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            panSpeed = panSpeedMax;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            panSpeed = panSpeedMin;
        }
        if(Input.GetKey(KeyCode.W)) 
        {
            pos += transform.forward * panSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S)) 
        {
            pos -= transform.forward * panSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A)) 
        {
            pos -= transform.right * panSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D)) 
        {
            pos += transform.right * panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimits.x, panLimits.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimits.y, panLimits.y);

        transform.position = pos;
    }
}
