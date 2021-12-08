using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FidgetSpinner : MonoBehaviour
{

    public Rigidbody CameraRB;
    public float speed;

    private void Update()
    {
        // Kääntää kameraa hitaasti
        CameraRB.transform.Rotate(0, speed, 0 * Time.deltaTime);
    }

}
