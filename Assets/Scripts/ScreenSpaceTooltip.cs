using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenSpaceTooltip : MonoBehaviour
{
    public static ScreenSpaceTooltip Instance { get; private set; }
    [SerializeField]
    private RectTransform canvasRectTransform;
    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMesh;
    private RectTransform tooltipRectTransform;

    private System.Func<string> getTooltipTextFunc;

 

    private void Awake()
    {
        Instance = this;
        //SetText("Default text");
        backgroundRectTransform = transform.Find("tooltipBackground").GetComponent<RectTransform>();
        textMesh = transform.Find("tooltipText").GetComponent<TextMeshProUGUI>();
        tooltipRectTransform = backgroundRectTransform.parent.GetComponent<RectTransform>();

        HideTooltip();
    }

    private void SetText(string tooltipText)
    {
        textMesh.SetText(tooltipText);
        textMesh.ForceMeshUpdate();
        Vector2 textSize = textMesh.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }


    // Update is called once per frame
    void Update()
    {
        SetText(getTooltipTextFunc());

        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)  //stop tooltip from going outside the screen horizontally
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)  //stop tooltip from going outside the screen vertically
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        tooltipRectTransform.anchoredPosition = anchoredPosition;
    }

    private void ShowTooltip(string tooltipText)
    {
        ShowTooltip(() => tooltipText);
    }
    /* Using a delegate allows dynamically updating tooltips.
     * Example of use: 
     * System.Func<string> getTooltipTextFunc = () => {
     * return "This is an example tooltip" + timer;
     * };
     * Updating "timer" variable in other piece of code automatically reflects in tooltip
     */

    private void ShowTooltip(System.Func<string> getTooltipTextFunc)
    {
        this.getTooltipTextFunc = getTooltipTextFunc;
        gameObject.SetActive(true);
        SetText(getTooltipTextFunc());
    }



    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipText) {
        Instance.ShowTooltip(tooltipText); }

    public static void ShowTooltip_Static(System.Func<string> getTooltipTextFunc)
    {
        Instance.ShowTooltip(getTooltipTextFunc);
    }

    public static void HideTooltip_Static()
    {
        Instance.HideTooltip();
    }
}
