using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class checkingCollide : MonoBehaviour
{
    float timer = 0f;
    static public bool Timing;
    private float positionX;
    private float distance;
    public GameObject plane;

    static public bool playing;
    static Rigidbody rb;
    public static List<Vector2> disPoints = new List<Vector2>();
    public static List<Vector2> VelocityPoints = new List<Vector2>();

    static Vector2 highestPoint;
    public Text text;
    

    // Start is called before the first frame update
    void Start()
    {
        Timing = false;
           plane = GameObject.FindGameObjectWithTag("Plane");
        playing = true;
        transform.position = new Vector3(0, 0, 0);
        transform.localPosition =new Vector3(- (plane.transform.localScale.x / 2 - 0.5f) ,plane.transform.localScale.y,0);
        rb = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        distance = (plane.transform.localScale.x / 2) - transform.localScale.x/2;
        positionX = transform.localPosition.x +distance ;
        if (Timing)
        {
            if (positionX < plane.transform.localScale.x - 1)
            {


                disPoints.Add(new Vector2(timer, positionX));
                VelocityPoints.Add(new Vector2(timer, rb.velocity.magnitude));
                float textXValue = positionX;
                Debug.Log("t =" + scientificText(timer) + " , x=  " + scientificText(textXValue) + " , |v|= " + rb.velocity.magnitude );
                timer += Time.deltaTime;
            }
        }
    }
    
    string scientificText(float value)
    {
        int E=0;
        if(value >=10)
        {
            for (E=0; value >= 10; value /= 10)
            {
                E++;
            }
            for (E = 0; value < 1; value *= 10)
            {
                E--;
            }
        }
        if (E != 0)
        {
            return value.ToString("0.00") + "E" + E;
        }
        else
        {
            return value.ToString("0.00");
        }
    }

    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Plane")
        {
            Timing = false;
        }
        
    }
}
