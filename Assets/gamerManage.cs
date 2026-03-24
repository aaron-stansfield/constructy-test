
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gamerManage : MonoBehaviour
{

    [SerializeField] TMP_Text reportText;

<<<<<<< Updated upstream:Assets/gamerManage.cs
    [SerializeField] clipboardScrip clipboard;
    [SerializeField] GameObject[] situations;

    public Hashtable reports = new Hashtable();

=======
    public List<GameObject> situations = new List<GameObject>();

    public Hashtable reports = new Hashtable();

    public List<GameObject> situationList = new List<GameObject>();

>>>>>>> Stashed changes:Assets/Assets/Scripts/GameManager.cs
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
        //running total so the player gets a score/x at the end
        int runningTotal = 0;

        int i = 0;
        foreach (GameObject situation in situations)
        {
<<<<<<< Updated upstream:Assets/gamerManage.cs
            reports[i] = situation.GetComponent<situationScrip>().report();
=======
            situationList.Add(situation);
            reports[i] = situation.GetComponent<SituationScript>().report();
>>>>>>> Stashed changes:Assets/Assets/Scripts/GameManager.cs
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
<<<<<<< Updated upstream:Assets/gamerManage.cs
=======

            /*situationList[j].transform.GetChild(0).gameObject.SetActive(false);
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

            }*/
>>>>>>> Stashed changes:Assets/Assets/Scripts/GameManager.cs
        }
        
        //total score out of total number of scenarios
        reportText.text += ($"Final Score = {runningTotal}/{reports.Count * 2}");

        //displays the report
        clipboard.openReportPage();
        

    }

}
