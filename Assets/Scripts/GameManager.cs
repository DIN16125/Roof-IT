using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    int sekunden = 0;
    int minute = 0;
    int placeholder = 0;

    public IEnumerator time()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            automationtrigger();
            sekunden++;
        }
    }

    private void Start()
    {
        StartCoroutine(time());
    }

    public void stopCoroutine()
    {
        StopCoroutine(time());
    }




    private void automationtrigger()
    {
        bool placeholding = true;
        if (sekunden == 60)
        {
            minute += 1;
            sekunden = 0;
        }

        if (sekunden >= 10)
        {
            placeholding = false;
        }

        if (placeholding)
        {
            GameObject.Find("Time").GetComponent<Text>().text = minute.ToString() + ":" + placeholder + sekunden.ToString();
        }
        else if (!placeholding)
        {
            GameObject.Find("Time").GetComponent<Text>().text = minute.ToString() + ":" + sekunden.ToString();
        }

    }
}
