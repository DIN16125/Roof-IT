using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour {

    public bool IsSnappable {get; private set; }
    public GameObject CollidingObject { get; private set; }
    public bool isSnapped { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        CollidingObject = other.gameObject;

        if (other.gameObject.tag == "NotBrick")
        {

            IsSnappable = true;
            //Debug.Log(other.GetComponent<Text>().text);
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NotBrick")
        {
            IsSnappable = false;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}