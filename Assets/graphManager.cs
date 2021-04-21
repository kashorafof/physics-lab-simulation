using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphManager : MonoBehaviour
{
    private static graphManager instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public float radius = 3f;
    public float getRadius()
    {
        return instance.radius;

    }
    static public float Static_getRadius()
    {

        return instance.getRadius();
    }
    


    public float width = 3f;
    static public float Static_getConnectorWidth()
    {

        return instance.getConnectorWidth();
    }
    public float getConnectorWidth()
    {

        return instance.width;
    }

    public float height = 3f;
    static public float Static_getConnectorHeight()
    {

        return instance.getConnectorHeight();
    }
     public float getConnectorHeight()
    {

        return instance.height;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
