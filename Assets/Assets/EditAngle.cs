using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EditAngle : MonoBehaviour
{
    static public int Angle;
    public GameObject Main ;
    private int smallRotate = 1;
    private int largeRotate = 5;
    static bool start;

    private void Awake()
    {
        Angle = 0;
         Main = GameObject.FindGameObjectWithTag("Main");
        Time.timeScale = 0f;
    }

    private void Update()
    {
    }

    public GameObject blocker;
    public void resume()
    {
        start = true;
        checkingCollide.Timing = !checkingCollide.Timing;
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        checkingCollide.playing = !checkingCollide.playing;

        blocker.SetActive(true);
    }
    
    public void debugPoints()
    {

        foreach (var point in checkingCollide.disPoints)
        {
            Debug.Log("at : " + point.x.ToString("0.00") + " the object was " + point.y);
        }


    }

    public void restart()
    {

        start = false;
        checkingCollide.disPoints.Clear();
        checkingCollide.VelocityPoints.Clear();
        gameManager.restart();
            Time.timeScale = 0f;
    }

    public void increase()
    {
        if (!start || toggle.editAblingAngle)
        {
            if (0 <= Angle && Angle < 5)
            {
                Main.transform.Rotate(0f, 0f, -smallRotate);
                Angle += smallRotate;
            }

            else if (5 <= Angle && Angle <= 85)
            {
                Main.transform.Rotate(0f, 0f, -largeRotate);
                Angle += largeRotate;
            }
        }
    }

    public void decrease()
    {
        if (!start || toggle.editAblingAngle)
        {
            if (0 < Angle && Angle <= 5)
            {
                Main.transform.Rotate(0f, 0f, smallRotate);
                Angle -= smallRotate;
            }

            else if (5 < Angle && Angle <= 90)
            {

                Main.transform.Rotate(0f, 0f, largeRotate);
                Angle -= largeRotate;
            }
        }
    }

}
