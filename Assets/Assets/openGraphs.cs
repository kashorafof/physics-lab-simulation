using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openGraphs : MonoBehaviour
{
    public GameObject graphsContainer ;
    public GameObject mainUI ;
    void enableGraphsPage()
    {
        if (checkingCollide.disPoints.Count >0  && checkingCollide.disPoints[checkingCollide.disPoints.Count-1].y !=0)
        {
            graphsContainer.SetActive(true);
            checkingCollide.playing = false;
            Time.timeScale = 0f;
            windowGraph.Static_generateGraphs();
        }

    }

    public void disableGraphsPage()
    {
        if (windowGraph.createdObjects.Count > 0)
        {
            for (int i = windowGraph.createdObjects.Count - 1; i >= 0; i--)
            {
                Destroy(windowGraph.createdObjects[i]);
                Debug.Log("destroyed " + windowGraph.createdObjects[i].name);
                windowGraph.createdObjects.RemoveAt(i);
            }
        }
        graphsContainer.SetActive(false);
        checkingCollide.playing = true;
    }

}
