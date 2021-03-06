using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;

public class windowGraph : MonoBehaviour
{
    [SerializeField] Sprite circleSprite;
    [SerializeField] Sprite VerticalBlackDashes;
    [SerializeField] Sprite HorizontalBlackDashes;
    private RectTransform graphContainerXvsT;
    private RectTransform graphContainerVvsT;
    private RectTransform graphContainer;
    RectTransform labelTemplateX;
    RectTransform labelTemplateY;
    RectTransform dashTemplateX;
    RectTransform dashTemplateY;
    private GameObject tooltipGameObject;
    int numberOfGrids;
    List<Vector2> xSquareFunction = new List<Vector2>();
    float jump;
    [SerializeField] private float pixelsPerUnitMultiplier;
    [SerializeField] private Camera uiCamera;
    static Vector2 highestPoint;
    private static windowGraph instance;
    public  float radius = 3f;

    private void Awake()
    {
        instance = this;
        numberOfGrids = 10;
        Debug.Log("awake");
        
        graphContainerXvsT = GameObject.FindGameObjectWithTag("graphContainerXvsT").GetComponent<RectTransform>();
        graphContainerVvsT = GameObject.FindGameObjectWithTag("graphContainerVvsT").GetComponent<RectTransform>();
        generateCircleForListOfVectors(checkingCollide.disPoints, graphContainerXvsT);
        generateCircleForListOfVectors(checkingCollide.VelocityPoints, graphContainerVvsT);
        
        tooltipGameObject = GameObject.Find("tooltip").gameObject;




        HideTooltip();
    }
    static public float Static_getRadius()
    {

        return instance.radius;
    }
    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tooltipGameObject.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        tooltipGameObject.transform.localPosition = localPoint;
    }


    void ShowTooltip(String tooltipText)
    {
        tooltipGameObject.SetActive(true);
        Text tooltipUiText = tooltipGameObject.transform.Find("text").GetComponent<Text>();
        tooltipUiText.text = tooltipText;

        float textPaddingSize = 4f;
        Vector2 backGroundSize = new Vector2(
            tooltipUiText.preferredWidth + textPaddingSize * 2f,
            tooltipUiText.preferredHeight + textPaddingSize * 2f
            );


        tooltipGameObject.transform.Find("background").GetComponent<RectTransform>().sizeDelta = backGroundSize;
        tooltipGameObject.transform.SetAsLastSibling();
    }

    void HideTooltip()
    {
        tooltipGameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(String tooltipText)
    {
        instance.ShowTooltip(tooltipText);
    }
    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }

    Vector2 scientific(float value)
    {
        int E = 0;
        for (E = 0; value >= 10; value /= 10)
        {
            E++;
        }
        for (E = 0; value < 1; value *= 10)
        {
            E--;
        }
        return new Vector2(value, E);
    }

    void generateCircleForListOfVectors(List<Vector2> Points, RectTransform graphContainerTry)
    {
        graphContainer = graphContainerTry;
        GameObject lastGameObject = null;
        float widthDifferance = graphContainer.rect.width / numberOfGrids;


        highestPoint = Points[Points.Count - 1];
        foreach (var point in Points)
        {
            GameObject circleGameObject = createCircle(point);
            if (lastGameObject != null && circleGameObject != null)
            {
                createDotConnection(lastGameObject, circleGameObject);
            }


            lastGameObject = circleGameObject;



        }


        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        Vector2 highest = Points[Points.Count - 1];
        float jump = highest.x / numberOfGrids;
        Vector2 ScientificResult = scientific(jump);
        Debug.Log(jump + " its scientific is " + ScientificResult);
        jump = float.Parse(ScientificResult.x.ToString("0.0"));
        jump *= Mathf.Pow(10, ScientificResult.y);
        jump = Mathf.Round(scientific(jump).x) * Mathf.Pow(10, scientific(jump).y);

        dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();


        Debug.Log(jump);
        for (int i = 1; (jump * i) / highestPoint.x * graphContainer.rect.width + radius <= graphContainer.rect.width * 0.97f; i++)
        {

            float percentPointLabel = (jump * i) / highestPoint.x * graphContainer.rect.width + radius;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.localScale *= 3f;
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(percentPointLabel, -2f);
            labelX.GetComponent<Text>().text = (jump * i).ToString("0.00");

            if (i != 0)
            {
                GameObject gameObject = new GameObject("dashesX", typeof(Image));
                gameObject.transform.SetParent(graphContainer);
                gameObject.GetComponent<Image>().sprite = VerticalBlackDashes;
                gameObject.GetComponent<Image>().type = Image.Type.Tiled;
                gameObject.GetComponent<Image>().pixelsPerUnitMultiplier = pixelsPerUnitMultiplier;
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
                rectTransform.anchorMax = new Vector2(0, 0);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.sizeDelta = new Vector2(0.5f, graphContainer.rect.size.y - 0.6f);
                rectTransform.localScale = new Vector3(1f, 1f, 1f);
                rectTransform.anchoredPosition = new Vector2(percentPointLabel - 0.38f, graphContainer.rect.height / 2);
                rectTransform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }

        labelTemplateY = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        float jumpY = highest.y / numberOfGrids;
        Vector2 ScientificResultY = scientific(jumpY);
        jumpY = float.Parse(ScientificResultY.x.ToString("0.0"));
        jumpY *= Mathf.Pow(10, ScientificResultY.y);
        jumpY = Mathf.Round(scientific(jumpY).x) * Mathf.Pow(10, scientific(jumpY).y);

        dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();

        for (int i = 0; (jumpY * i) / highestPoint.y * graphContainer.rect.height <= graphContainer.rect.height * 0.97f; i++)
        {

            float percentPointLabelY = (jumpY * i) / highestPoint.y * graphContainer.rect.height + radius;

            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.localScale *= 3f;
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            labelY.anchoredPosition = new Vector2(-4f, percentPointLabelY);
            labelY.GetComponent<Text>().text = (jumpY * i).ToString("0.00") + " -";

            if (i != 0)
            {
                GameObject gameObject = new GameObject("dashesY", typeof(Image));
                gameObject.transform.SetParent(graphContainer);
                gameObject.GetComponent<Image>().sprite = HorizontalBlackDashes;
                gameObject.GetComponent<Image>().type = Image.Type.Tiled;
                gameObject.GetComponent<Image>().pixelsPerUnitMultiplier = pixelsPerUnitMultiplier;
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
                RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
                rectTransform.anchorMax = new Vector2(0, 0);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.sizeDelta = new Vector2(graphContainer.rect.size.x - 0.6f, 0.5f);
                rectTransform.localScale = new Vector3(1f, 1f, 1f);
                rectTransform.anchoredPosition = new Vector2(graphContainer.rect.width / 2, percentPointLabelY - 0.38f);
                rectTransform.localEulerAngles = new Vector3(0, 0, 0);
            }



        }
    }

    GameObject createCircle(Vector2 point)
    {
        dotScript pointScript;
        Vector2 pointPosition;
        pointPosition.x = point.x / highestPoint.x * graphContainer.rect.width + radius;
        pointPosition.y = point.y / highestPoint.y * graphContainer.rect.height + radius;

        if (pointPosition.x <= 100f && pointPosition.y <= 100f)
        {
            GameObject CircleGameObject = new GameObject("circle", typeof(Image));
            CircleGameObject.AddComponent<dotScript>();
            pointScript = CircleGameObject.GetComponent<dotScript>();
            CircleGameObject.transform.SetParent(graphContainer, false);
            CircleGameObject.GetComponent<Image>().sprite = circleSprite;
            CircleGameObject.AddComponent<Button_UI>();
            RectTransform rectTransform = CircleGameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = pointPosition;
            rectTransform.sizeDelta = new Vector2(radius, radius);
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchorMax = new Vector2(0f, 0f);

            pointScript.value = point;
            pointScript.postition = pointPosition;

            return CircleGameObject;
        }
        else
        {
            return null;
        }
    }

    void createDotConnection(GameObject startGameObject, GameObject endGameObject)
    {
        Vector2 start = startGameObject.GetComponent<RectTransform>().anchoredPosition;
        Vector2 end = endGameObject.GetComponent<RectTransform>().anchoredPosition;
        GameObject gameObject = new GameObject("dotConnector", typeof(Image));
        gameObject.transform.SetParent(graphContainer);
        gameObject.AddComponent<dotConnector>();
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (end - start).normalized;
        float distance = (end - start).magnitude;
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance * 4f, 1.5f);
        rectTransform.anchoredPosition = start + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

        dotConnector dotConnectorComponent = gameObject.GetComponent<dotConnector>();
        dotConnectorComponent.startGameObject = startGameObject;
        dotConnectorComponent.start = start;
        dotConnectorComponent.endGameObject = endGameObject;
        dotConnectorComponent.end = end;
    }
    



    
}






#region tests
/*
RectTransform dashY = Instantiate(dashTemplateY);
dashY.localScale = Vector3.one;
dashY.SetParent(graphContainer);
dashY.gameObject.SetActive(true);
dashY.sizeDelta = new Vector2(graphContainer.rect.width, 1f);
dashY.anchoredPosition = new Vector2(graphContainer.rect.width/2, percentPointLabelY);


GameObject gameObject = new GameObject("dashesY", typeof(Image));
gameObject.transform.SetParent(graphContainer, false);
gameObject.GetComponent<Image>().sprite = blackDashes;
RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
rectTransform.anchoredPosition = new Vector2(graphContainer.rect.width / 2, percentPointLabelY);
rectTransform.sizeDelta = new Vector2(graphContainer.rect.width, 1f);
rectTransform.anchorMin = new Vector2(0f, 0f);
rectTransform.anchorMax = new Vector2(0f, 0f);
*/
#endregion