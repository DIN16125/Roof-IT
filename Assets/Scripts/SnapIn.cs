using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnapIn : MonoBehaviour {

    

    private void OnTriggerEnter(Collider other)
    {
        snapIn(other.gameObject);
    }

    public void snapIn(GameObject brick)
    {
        brick.transform.position = this.transform.position;
        brick.transform.rotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
