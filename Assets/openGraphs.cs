using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openGraphs : MonoBehaviour
{
    public GameObject graphsContainer ;
    public GameObject mainUI ;
    void enableGraphsPage()
    {
        if (checkingCollide.disPoints.Count >0 )
        {
            graphsContainer.SetActive(true);
            checkingCollide.playing = false;
            Time.timeScale = 0f;
        }

    }

    public void disableGraphsPage()
    {

        graphsContainer.SetActive(false);
        checkingCollide.playing = true;
    }

}
