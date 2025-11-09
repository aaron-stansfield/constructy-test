using MEC;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tapeScrip : MonoBehaviour
{
    [SerializeField] GameObject realTicket;

    public GameObject currentTicketSection;

    public bool ongoing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startDispensing(Transform ticketTransform, GameObject ticketObj)
    {
        ongoing = true;
        currentTicketSection = ticketObj.transform.GetChild(0).gameObject;
        StartCoroutine(ticketDispensing(ticketTransform, ticketObj));
    }

    public IEnumerator ticketDispensing(Transform ticketTransform, GameObject ticketObj)
    {
        while (ongoing)
        {

            if (Vector3.Distance(currentTicketSection.transform.position, ticketTransform.position) >
                Vector3.Distance(currentTicketSection.transform.position, currentTicketSection.transform.GetChild(1).position))
            {
                GameObject lastTicket = currentTicketSection;
                currentTicketSection = Instantiate(realTicket,this.transform);
                
                currentTicketSection.transform.localScale = new Vector3(0.69359f, 0.69359f, 0.69359f);

                //forgive me for i have sinned
                currentTicketSection.transform.GetChild(0).GetComponent<Renderer>().material.color = 
                    lastTicket.transform.GetChild(0).GetComponent<Renderer>().material.color;

                currentTicketSection.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text =
                    lastTicket.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text;

                currentTicketSection.transform.position = lastTicket.transform.GetChild(1).transform.position;
                
                currentTicketSection.name = this.transform.childCount.ToString();
                Debug.Log($"{lastTicket.transform.GetChild(1).gameObject.name}");
            }

            currentTicketSection.transform.LookAt(ticketTransform.position);

            yield return new WaitForSeconds(0.01f);
        }

        currentTicketSection = null;

    }

    public void stopDispensing()
    {
        ongoing = false;
    }

}
