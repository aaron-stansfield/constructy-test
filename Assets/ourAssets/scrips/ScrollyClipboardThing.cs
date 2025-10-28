using System.Xml.Schema;
using UnityEngine;

public class ScrollyClipboardThing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScrollbarUpdated(float pos)
    {
        var tr = GetComponent<RectTransform>();
        float topPosition = tr.rect.yMax;
        float bottomPosition = tr.rect.yMin;

        //while(tr.rect.yMax <= -1000)
        //{
        //    Mathf.Lerp()
        //}
    }
}
