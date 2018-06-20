using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour {

    private bool firstRow = true;
    public int Row;
    public int Column;
    int id = 0;
    public GameObject brick;
    public GameObject notBrick;
    Collider m_Collider;


    // Use this for initialization
    void Start () {
        firstWall();
        pseudoWall1();  
	}

    void firstWall()
    {
        float posX = 0f;
        float posY = 0.08980004f;
        float posZ = 0f;
        for (int y = 1; y <= Row; y++)
        {
            for (int x = 1; x <= Column; x++)
            {
                brick.GetComponent<Transform>().transform.position = new Vector3((float)posX, (float)posY, (float)posZ);
                Instantiate(brick);
                posX += 0.5f;
            }
            posX = 0f;
            posY += 0.179f;
        }
    }

    void pseudoWall1()
    {
        float posX = 0f;
        float posY = 0.08980004f;
        float posZ = -2f;
        for (int y = 0; y < Row; y++)
        {
            for (int x = 0; x < Column; x++)
            {

                notBrick.GetComponent<Collider>().enabled = true;
                notBrick.GetComponent<BoxCollider>().isTrigger = true;
                notBrick.GetComponent<MeshRenderer>().enabled = true;

                GameObject nb = notBrick;
                nb.GetComponent<Transform>().transform.position = new Vector3((float)posX, (float)posY, (float)posZ);

                if (!firstRow)
                {
                    m_Collider = nb.GetComponent<Collider>();
                    m_Collider.enabled = false;
                    nb.GetComponent<MeshRenderer>().enabled = false;
                }

                nb.GetComponent<Text>().text = id.ToString();
                Instantiate(nb).name = "NotBrick"+id;

                posX += 0.5f;
                id++;
                nb = null;
            }
            firstRow = false;
            posX = 0f;
            posY += 0.179f;
        }
    }
}
