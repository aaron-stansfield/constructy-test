using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class clipboardScrip : MonoBehaviour
{
    [SerializeField] GameObject ticketPrefab;

    [SerializeField] Transform ticketTransform;

    private bool gripInputReady = true;

    [SerializeField] private GameObject clipBoard;

    [SerializeField] XRInputValueReader<float> m_LeftGripInput = new XRInputValueReader<float>("Grip");

    [SerializeField] public bool buttonPressed;



    [SerializeField] GameObject page1;

    [SerializeField] GameObject page2;


    [SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");

    private float lastGripState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_LeftGripInput.ReadValue() > 0.5f && gripInputReady && lastGripState != m_LeftGripInput.ReadValue())
        {
            lastGripState = m_LeftGripInput.ReadValue();
            //invert state
            clipBoard.SetActive(!clipBoard.gameObject.activeInHierarchy);
            gripInputReady = false;
            StartCoroutine(inputTimer(1));
        }

        if (m_RightGripInput.ReadValue() < 0.75f && ticketTransform.childCount != 0)
        {
            ticketTransform.GetChild(0).transform.SetParent(null);
        }
    }


    //timer so that inputs have a delay before they are read again
    private IEnumerator inputTimer(float time)
    {
        yield return new WaitForSeconds(time);
        gripInputReady = true;
    }



    // these functions are called by buttons and then they call the main ticket spawn func with their respective names
    
    public void spawnPPETicket()
    {

        spawnTicket("no ppeoeppee","ppe");
    }

    public void spawnOtherTicket()
    {
        spawnTicket("other","other");
    }


    private void spawnTicket(string ticketName,string tag)
    {
        if (ticketTransform.childCount != 0 || m_RightGripInput.ReadValue() < 0.95f)
            return;

        GameObject dude = Instantiate(ticketPrefab);
        dude.transform.SetParent(ticketTransform);
        dude.tag = tag;
        dude.transform.localPosition = Vector3.zero;
        dude.transform.localEulerAngles = new Vector3(0,180,0);

        dude.transform.GetComponentInChildren<TMP_Text>().text = ticketName;
    }


    public void changePage()
    {
        if (page1.activeInHierarchy)
        {
            page1.SetActive(false);

            page2.SetActive(true);
        }

        else
        {

            page1.SetActive(true);

            page2.SetActive(false);
        }


    }


}
