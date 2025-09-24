using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class gamerManage : MonoBehaviour
{

    [SerializeField] TMP_Text reportText;

    private int runningTotal;

    [SerializeField] GameObject[] situations;

    public Hashtable reports = new Hashtable();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void finished()
    {
        int i = 0;
        foreach (GameObject situation in situations)
        {
            reports.Add(i, situation.GetComponent<situationScrip>().report());
            i++;
        }

        for (int j = 0; j < reports.Count; j++)
        {
            Debug.Log(reports[j]);
            runningTotal += (int)reports[j];
        }

        reportText.text = ($"{runningTotal}/{reports.Count}");

    }

}
