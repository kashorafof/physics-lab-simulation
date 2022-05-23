using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    static public void restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
