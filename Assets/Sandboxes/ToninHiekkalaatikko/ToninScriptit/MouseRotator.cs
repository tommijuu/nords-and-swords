using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotator : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 100f;
    [SerializeField]
    private EnchantRotator rotateScript;

    bool dragging = false;

    public Rigidbody rb;

    public int enchantCost;
    public string enchantStats;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Eka left");
        if (other.gameObject.CompareTag("CheckPointLeft") && rotateScript.firstCheckpointCleared)
        {
            rotateScript.checkPointToMove = 2;

        }
        else if (other.gameObject.CompareTag("CheckPointLeft") && rotateScript.firstCheckpointCleared == false)
        {
            rotateScript.checkPointToMove = 0;
            rotateScript.firstCheckpointCleared = true;
            Debug.Log("Eka left");

        }
        else if (other.gameObject.CompareTag("CheckPointRight") && rotateScript.firstCheckpointCleared == false)
        {
            rotateScript.checkPointToMove = 1;
            rotateScript.firstCheckpointCleared = true;
            Debug.Log("Eka Right");

        }
        else if (other.gameObject.CompareTag("CheckPointRight") && rotateScript.firstCheckpointCleared)
        {
            rotateScript.checkPointToMove = 2;

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
        if (!dragging)
        {
            transform.Rotate(0, 1, 0, Space.World);
        }

    }
}
