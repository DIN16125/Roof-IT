using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public SteamVR_TrackedController controller;
    Vector3 lastPos;
    bool grabbed;
    void Start()
    {
        controller = this.GetComponent<SteamVR_TrackedController>();
        lastPos = transform.position;
    }
    void Update()
    {
        if (!grabbed) lastPos = transform.position;
        if (controller.padPressed)
        {
            var offset = transform.position - lastPos;
            offset.y = 0;
            transform.parent.position += offset * -1;
            lastPos = transform.position;
            grabbed = true;
        }
        else { grabbed = false; }
    }
}
