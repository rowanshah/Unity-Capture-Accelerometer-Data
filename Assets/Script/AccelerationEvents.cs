using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationEvents : MonoBehaviour
{ 
    void Update()
    {
        GetAccelerometerValue();
    }

    Vector3 GetAccelerometerValue()
    {
        Vector3 acc = Vector3.zero;
        float period = 0.0f;

        foreach(AccelerationEvent evnt in Input.accelerationEvents)
        {
            acc += evnt.acceleration * evnt.deltaTime;
            period += evnt.deltaTime;
        }
        if (period > 0)
        {
            acc *= 1.0f / period;
        }
        return acc;
    }
}

