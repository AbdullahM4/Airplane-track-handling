using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpin : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        if (speed > 0f)
        {
            transform.Rotate(0, 0, speed * Time.fixedDeltaTime, Space.Self);
        }
    }
}
