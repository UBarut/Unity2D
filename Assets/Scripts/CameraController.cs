using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Vector3 offset;
    public float damping;

    CheckPoints checkPoints;

    private void FixedUpdate()
    {
        checkPoints = GameObject.FindObjectOfType<CheckPoints>();
        transform.position = Vector3.Lerp(transform.position, checkPoints.gObject.transform.position, damping) + offset;
    }


}
