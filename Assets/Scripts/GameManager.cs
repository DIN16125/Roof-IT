using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {



    IEnumerator time()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            automationtrigger();
        }
    }

    private void automationtrigger()
    {
        
    }
}
