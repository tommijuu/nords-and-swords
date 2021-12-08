using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 100f;

    bool dragging = false;

    public Rigidbody rb;

    private void OnMouseDrag()
    {
        dragging = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

    }

    private void FixedUpdate()
    {
        if (dragging)
        {
            float x = Input.GetAxis("Mouse X") * rotateSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse Y") * rotateSpeed * Time.fixedDeltaTime;

            rb.AddTorque(Vector3.down * x);
            rb.AddTorque(Vector3.right * y);
        }

    }
}
