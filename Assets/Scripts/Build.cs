using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour {

    private bool firstRow = true;
    public int Row;
    public int Column;
    int id = 0;
    public GameObject tile;
    public GameObject notBrick;
    Collider m_Collider;
    public int available = 0;


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
                tile.GetComponent<Transform>().transform.position = new Vector3((float)posX, (float)posY, (float)posZ);
                Instantiate(tile);
                posX += 0.5f;
            }
            posX = 0f;
            posY += 0.179f;
        }
    }

    void pseudoWall1()
    {

        float posX = -0.19f;
        float posY = 2.55f;
        float posZ = -3.885f;
        for (int y = 0; y < Row; y++)
        {
            for (int x = 0; x < Column; x++)
            {

                notBrick.GetComponent<Collider>().enabled = true;
                notBrick.GetComponent<Collider>().isTrigger = true;
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
                Instantiate(nb).name = "NotBrick" + id;

                GameObject.Find("NotBrick" + id).transform.SetParent(GameObject.Find("PseudoWall").transform);

                posX += 0.18f;
                id++;
                nb = null;
                available++;
            }
           

            firstRow = false;
            posX = 0f;
            posY += 0.013f;
            posZ += 0.16f;
        }
        GameObject.Find("PseudoWall").transform.Rotate(new Vector3(-45f, 180f, 0f));
        GameObject.Find("PseudoWall").transform.position = new Vector3(-0.369f, 2.164f, -6.976f);
        GameObject spawner = GameObject.FindGameObjectWithTag("Tile_Spawner");
        spawner.GetComponent<TileSpawner>().counter = available;
    }
}
