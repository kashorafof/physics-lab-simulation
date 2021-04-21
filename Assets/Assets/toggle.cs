using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle : MonoBehaviour
{
    private static toggle instance ;
    Toggle editchecker;
    private void Awake()
    {
        instance = this;

        editchecker = gameObject.GetComponent<Toggle>();
        changeEditAblety();
    }
    

    static public bool editAblingAngle;
    public void changeEditAblety()
    {

        editAblingAngle = editchecker.isOn;
    }
    
}
