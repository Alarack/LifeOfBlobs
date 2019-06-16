using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAnimation : MonoBehaviour
{
    public Animator anim;
    float speed;
    Vector3 lastPosition = Vector3.zero;
    private void Update() {
        Vector3 curMove = transform.position - lastPosition;
        speed = curMove.magnitude / Time.deltaTime;
        lastPosition = transform.position;
        anim.SetFloat("speed", speed / 10);
    }
}
