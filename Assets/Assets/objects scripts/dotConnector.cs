using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotConnector : MonoBehaviour
{
    [SerializeField] public  Vector2 start, end;
    [SerializeField] public GameObject startGameObject, endGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(graphManager.Static_getConnectorWidth(), graphManager.Static_getConnectorHeight()); 

    }
    
}
