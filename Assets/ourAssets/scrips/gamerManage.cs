using System;
using System.Collections;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using TMPro;
using UnityEngine;

public class gamerManage : MonoBehaviour
{

    [SerializeField] TMP_Text reportText;

    [SerializeField] GameObject[] situations;

    public Hashtable reports = new Hashtable();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finished();
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
            reports[i] = situation.GetComponent<situationScrip>().report();
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
        }
        
        //total score out of total number of scenarios
        reportText.text += ($"Final Score = {runningTotal}/{reports.Count * 2}");



    }

}
