using System.Collections;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

    [SerializeField] LayerMask ticketLayer;
    [SerializeField] LayerMask fixLayer;

    [SerializeField] public bool buttonPressed;

    [SerializeField] GameObject rightController;

    [SerializeField] GameObject page1;

    [SerializeField] GameObject page2;

    [SerializeField] GameObject page3;


    [SerializeField] XRInputValueReader<float> m_LeftGripInput = new XRInputValueReader<float>("Grip");

    [SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");

    private float lastGripState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the grip is pressed at least half way in
        //and some borked code to make it not flicker on and off when you hold the grip down
        
        if (m_LeftGripInput.ReadValue() > 0.5f && gripInputReady && lastGripState != m_LeftGripInput.ReadValue())
        {
            lastGripState = m_LeftGripInput.ReadValue();
            //invert state
            clipBoard.SetActive(!clipBoard.gameObject.activeInHierarchy);
            gripInputReady = false;
            StartCoroutine(inputTimer(1));
        }



        if (m_RightGripInput.ReadValue() < 0.5f && ticketTransform.childCount != 0)
        {
            //this is to make the ticket just float where it was let go assuming it is in range of a situation thingy

            foreach (Collider col in Physics.OverlapSphere(ticketTransform.GetChild(0).position, 0.01f))
            {
                if (col.CompareTag("hazard"))
                {
                    ticketTransform.GetChild(0).transform.SetParent(null);
                    return;

                }
            }

            if (ticketTransform.childCount > 0)
            {
                Destroy(ticketTransform.GetChild(0).gameObject);
            }



        }

        if (m_RightGripInput.ReadValue() > 0.5f && ticketTransform.childCount == 0)
        {


            foreach (Collider col in Physics.OverlapSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius))
            {
                if(col.GetComponent<spawnFixObject>() == null)
                {
                    break;
                }
                GameObject dude = col.GetComponent<spawnFixObject>().SpawnPrefab();

                dude.gameObject.transform.SetParent(ticketTransform);

                dude.transform.localPosition = Vector3.zero;

                //puts the obj in the correct orientation relative to the controller
                dude.transform.localEulerAngles = new Vector3(0, 180, 0);
                return;
            }

            foreach (Collider col in Physics.OverlapSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius, fixLayer))
            {
                col.gameObject.transform.SetParent(ticketTransform);

                col.transform.localPosition = Vector3.zero;

                return;
            }

            foreach (Collider col in Physics.OverlapSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius,ticketLayer))
            {
                col.gameObject.transform.SetParent(ticketTransform);

                col.transform.localPosition = Vector3.zero;

                //puts the ticket in the correct orientation relative to the controller
                col.transform.localEulerAngles = new Vector3(0, 180, 0);
                return;
            }

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

    public void spawnTrippingHazardTicket()
    {
        spawnTicket("tripping hazard", "tripping hazard");
    }

    private void spawnTicket(string ticketName,string tag)
    {
        if (ticketTransform.childCount != 0 || m_RightGripInput.ReadValue() < 0.95f)
            return;

        GameObject dude = Instantiate(ticketPrefab);
        //ticketTransform is a child of the poke object on the right controller
        dude.transform.SetParent(ticketTransform);
        dude.tag = tag;
        dude.transform.localPosition = Vector3.zero;
        
        //puts the ticket in the correct orientation relative to the controller
        dude.transform.localEulerAngles = new Vector3(0,180,0);

        dude.transform.GetComponentInChildren<TMP_Text>().text = ticketName;
    }



    // todo: make all this code not shit
    public void changePage()
    {
        if (page1.activeInHierarchy)
        {
            page1.SetActive(false);
            page3.SetActive(false);

            page2.SetActive(true);
        }

        else
        {

            page1.SetActive(true);

            page3.SetActive(false);
            page2.SetActive(false);
        }


    }

    public void openReportPage()
    {
        if (page3.activeInHierarchy)
        {
            page3.SetActive(false);

            page1.SetActive(true);
        }

        else
        {
            page1.SetActive(false);
            page2.SetActive(false);
            page3.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius);
    }

}
