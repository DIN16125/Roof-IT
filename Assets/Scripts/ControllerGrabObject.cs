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
        if(collidingObject.tag == "Tile_Spawner")
        {
            GameObject spawner = GameObject.FindWithTag("Tile_Spawner");
            if (spawner.GetComponent<TileSpawner>().counter > 0)
            {
                GameObject tile = GameObject.FindWithTag("Tile_Spawner").GetComponent<TileSpawner>().toSpawn;
                spawner.GetComponent<TileSpawner>().counter -= 1;

                tile.transform.position = collidingObject.transform.position;
                GameObject clone = Instantiate(tile);

                objectInHand = clone;
                var joint = AddFixedJoint();
                joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
                
            }
            if (spawner.GetComponent<TileSpawner>().counter == 0)
            {
                spawner.SetActive(false);
            }

        }
        else
        {
            objectInHand = collidingObject;
            collidingObject = null;
            var joint = AddFixedJoint();
            joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        }   
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
            if (objectInHand.tag == "Tile" && objectInHand.GetComponent<Brick>().IsSnappable && !objectInHand.GetComponent<Brick>().isSnapped)
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
                GameObject s = GameObject.Find("NotBrick" + BlocktoActivateID);
                if (s != null)
                {
                    s.GetComponent<Collider>().isTrigger = true;
                    s.GetComponent<Collider>().enabled = true;
                    s.SetActive(true);
                }

                int look = GameObject.Find("Plane").GetComponent<Build>().available;
                //Tiles you have to place
                plane.GetComponent<Build>().available -= 1;
                Debug.Log(look);
                if (GameObject.Find("Plane").GetComponent<Build>().available == 0)
                {
                    GameObject.Find("Fertig").GetComponent<Text>().text = "FERTIG";
                    GameObject.Find("Camera UI").GetComponent<GameManager>().StopAllCoroutines();
                }

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
