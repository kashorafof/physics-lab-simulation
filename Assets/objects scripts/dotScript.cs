using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dotScript : MonoBehaviour ,IPointerEnterHandler ,IPointerExitHandler
{
    [SerializeField]public Vector2 value, postition;
    Vector2 mousePoistion;
    Vector2 PrevMousePoistion;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        windowGraph.ShowTooltip_Static(value.ToString("0.00"));
    }

   public void OnPointerExit(PointerEventData eventData)
    {
        windowGraph.HideTooltip_Static();
    }

    
}
