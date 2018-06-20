using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGrabObject : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
        
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    public GameObject getObjectInHand()
    {
        return objectInHand;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            if (objectInHand.tag == "Brick" && objectInHand.GetComponent<Brick>().IsSnappable && !objectInHand.GetComponent<Brick>().isSnapped)
            {
                GameObject other = objectInHand.GetComponent<Brick>().CollidingObject;

                objectInHand.transform.position = other.transform.position;
                objectInHand.transform.rotation = other.transform.rotation;
                objectInHand.GetComponent<Brick>().isSnapped = true;
                objectInHand.GetComponent<Brick>().enabled = false;
                objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>());
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.SetActive(false);

                //acticvate block over placed block
                GameObject plane = GameObject.FindGameObjectWithTag("Floor");

                int BlocktoActivateID = int.Parse(other.GetComponent<Text>().text) + plane.GetComponent<Build>().Column;
                GameObject s = GameObject.Find("NotBrick" + BlocktoActivateID.ToString());
                s.GetComponent<BoxCollider>().isTrigger = true;
                s.GetComponent<Collider>().enabled = true;
                s.SetActive(true);
            }
            else
            {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>());
                objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
                objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
            }
        }
        objectInHand = null;
    }
}
