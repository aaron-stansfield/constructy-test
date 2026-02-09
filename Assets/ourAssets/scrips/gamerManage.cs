using System;
using System.Collections;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class gamerManage : MonoBehaviour
{
    public static gamerManage Instance = null;

    [SerializeField] TMP_Text reportText;

    [SerializeField] GameObject[] situations;

    public Hashtable reports = new Hashtable();

    private GameObject[] situationList = new GameObject[12];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            DestroyImmediate(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void finished()
    {
        //running total so the player gets a score/x at the end
        int runningTotal = 0;

        int i = 0;
        foreach (GameObject situation in situations)
        {
            situationList[i] = situation;
            reports[i] = situation.GetComponentInChildren<situationScrip>().report();
            i++;
        }


        //gets a bool[] from each scenario, index 0 being the ticket check and 1 being the fix check 
        reportText.text = "";
        for (int j = 0; j < reports.Count; j++)
        {
            bool[] intermediate = (bool[])reports[j];
            Debug.Log(intermediate[1]);
            
            if (intermediate[0])
            {
                runningTotal += 1;
            }

            if (intermediate[1])
            {
                runningTotal += 1;
            }
            // report for each scenario (just the ticket check for now)
            reportText.text += ($"{situations[j].name}: ID - {(intermediate[0] ? "Identified" : "Missed")}, \n Fixed - {(intermediate[1] ? "Yes" : "No")}\n");

            situationList[j].transform.GetChild(0).gameObject.SetActive(false);
            if (intermediate[0])
            {
                situationList[j].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {

                try
                {
                    if (situationList[j].transform.GetChild(2).transform != null)
                    {

                        situationList[j].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }

                catch
                {
                    situationList[j].transform.GetChild(1).gameObject.SetActive(true);
                }

            }
        }
        
        //total score out of total number of scenarios
        reportText.text += ($"Final Score = {runningTotal}/{reports.Count * 2}");



    }

}
