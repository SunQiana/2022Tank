using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    private GameObject camRoot;
    private Vector3 camV3;
    public float camSpeed = 0.5f;

    private void Awake()
    {
        camRoot = this.gameObject;
    }

    private void Update()
    {
        camV3 = camRoot.transform.position;
        if (Input.GetKey(KeyCode.W))
            camV3.z += 1*camSpeed;

        if (Input.GetKey(KeyCode.S))
            camV3.z -= 1 * camSpeed;

        if (Input.GetKey(KeyCode.A))
            camV3.x -= 1 * camSpeed;

        if (Input.GetKey(KeyCode.D))
             camV3.x += 1 * camSpeed;

        camRoot.transform.position = camV3;
    }
}
