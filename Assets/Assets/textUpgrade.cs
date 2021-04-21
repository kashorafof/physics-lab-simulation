using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textUpgrade : MonoBehaviour
{
    public Text angleText;
    public Transform Main;
    private void Start()
    {
        angleText = gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        
        angleText.text = EditAngle.Angle + "°";
    }
}
