using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {

    private bool firstRow = true;
    public int Row;
    public int Column;
    public GameObject brick;
    public GameObject notBrick;
    private ArrayList notBrickList = new ArrayList();
    public ArrayList NotBrickList {
        get
        {
            return notBrickList;
        }
    }

    

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
        for (int y = 0; y <= Row; y++)
        {
            for (int x = 0; x < Column; x++)
            {
                notBrick.GetComponent<Transform>().transform.position = new Vector3((float)posX, (float)posY, (float)posZ);
                if (!firstRow)
                {
                    notBrick.SetActive(false);
                }
                Instantiate(notBrick);
                notBrickList.Add(notBrick);
                
                posX += 0.5f;
            }
            firstRow = false;
            posX = 0f;
            posY += 0.179f;
        }
    }
}
