using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamHolder : MonoBehaviour
{
    public float speed = 10f, rotSpeed = 10;
    public GameObject target;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, rotSpeed * Time.deltaTime);
    }
}
