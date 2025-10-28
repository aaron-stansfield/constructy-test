using MEC;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class GunScrip : MonoBehaviour
{
    [SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");
    [SerializeField] XRInputValueReader<float> gunButton = new XRInputValueReader<float>("primaryButton");

    [SerializeField] GameObject controllerVisual;

    [SerializeField] GameObject rightController;

    [SerializeField] private GameObject gun;
    [SerializeField] private Transform ticketTransform;
    [SerializeField] private GameObject ticketPrefab;
    [SerializeField] private LayerMask ticketLayer;
    [SerializeField] private LayerMask fixLayer;
    [SerializeField] Transform ticketTransformGun;

    private string[] ticketTags = new string[]
    {
        "electrical",
        "trippingHazard",
        "noPPE",
        "other",
        "good",
        "chemical"
    };

    private GameObject currentDude;

    [SerializeField] private TicketSelector ticketSelector;
    [SerializeField] private CriticalOption criticalOption;


    private bool triggerHeld;


    private bool canToggleGun = true;
    private bool gripInputReady = true;
    [SerializeField] GameObject heldItem = null;

    void Start()
    {
        if (gun == null && transform.childCount > 0)
            gun = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        HandleGunToggle();
        HandleTicketSpawn();


        // ticket pointing at gun end
        if ( heldItem != null && gun.activeSelf)
        {
            heldItem.transform.LookAt(ticketTransformGun.position);
            heldItem.transform.transform.localScale = new Vector3(heldItem.transform.localScale.x, heldItem.transform.localScale.y, Vector3.Distance(heldItem.transform.position, gun.transform.position) *  8);
            //if (currentDude.transform.GetChild(0).transform.localScale.z > currentDude.transform.GetChild(1).GetComponent<RectTransform>().)

        }
    }


    // turn gun on / off
    private void HandleGunToggle()
    {
        if (gunButton.ReadValue() > 0.75f && canToggleGun)
        {
            canToggleGun = false;
            controllerVisual.SetActive(!controllerVisual.activeSelf);
            gun.SetActive(!gun.activeSelf);
            Timing.CallDelayed(0.25f, () => canToggleGun = true);
        }
    }

    private void HandleTicketSpawn()
    {
        if (m_RightGripInput.ReadValue() > 0.8f && gripInputReady && heldItem == null && gun.activeSelf)
        {
            gripInputReady = false;

            //taken and slightly changed from clipboard script
            GameObject tempHeldTicket = Instantiate(ticketPrefab);
            tempHeldTicket.transform.SetParent(ticketTransformGun);
            tempHeldTicket.transform.localPosition = Vector3.zero;
            
            heldItem = tempHeldTicket;
            
        
            if (criticalOption != null)
            {
                var renderer = tempHeldTicket.transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
                if (renderer != null)
                    renderer.material.color = criticalOption.GetCurrentColor();
            }
            if (ticketSelector != null)
            {
                int index = ticketSelector.GetCurrentIndex();
                string[] hazardNames = {"ELECTRICAL", "TRIPPING HAZARD", "NO PPE", "OTHER", "GOOD", "CHEMICALS/DUST" };
                string selectedHazard = hazardNames[index % hazardNames.Length];

                tempHeldTicket.name = "Ticket_" + selectedHazard;
                tempHeldTicket.tag = (string)ticketTags[index];
                TMP_Text label = tempHeldTicket.GetComponentInChildren<TMP_Text>();
                if (label != null)
                    label.text = selectedHazard;
            }
            if(gun.activeSelf) tempHeldTicket.transform.SetParent(null);

        }

        if(m_RightGripInput.ReadValue() > 0.7f && heldItem != null && !gun.activeSelf)
        {
            //heldItem.transform.SetParent(ticketTransform);
            //heldItem.transform.localPosition = Vector3.zero;
            //heldItem.transform.localRotation = new Quaternion(0,0,0,0);
            //Debug.Log(rightController.GetComponent<Rigidbody>().angularVelocity);
            
        }

        //Holding down grip
        else if (m_RightGripInput.ReadValue() > 0.7f && heldItem == null && !gun.activeInHierarchy)
        {
            foreach (Collider col in Physics.OverlapSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius, fixLayer))
            {
                heldItem = col.gameObject;
                heldItem.GetComponent<Rigidbody>().isKinematic = true;
                col.gameObject.transform.SetParent(ticketTransform);

                col.transform.localPosition = Vector3.zero;

                return;
            }

            foreach (Collider col in Physics.OverlapSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius, ticketLayer))
            {
                heldItem = col.gameObject;
                col.gameObject.transform.SetParent(ticketTransform);

                col.transform.localPosition = Vector3.zero;

                //puts the ticket in the correct orientation relative to the controller
                col.transform.localEulerAngles = new Vector3(0, 90, 0);
                return;
            }
        }


        //Letting go of grip
        if (m_RightGripInput.ReadValue() < 0.3f && heldItem != null)
        {
            //bool inAttachArea = false;
            //bool inHazard = false;
            foreach (Collider col in Physics.OverlapSphere(heldItem.transform.position, 0.01f))
            {
                if (col.CompareTag("AttatchArea") && heldItem.gameObject.transform.GetComponent<AttatchObject>() != null)
                {
                    heldItem.gameObject.transform.GetComponent<AttatchObject>().StartLerp();
                    heldItem.transform.SetParent(col.gameObject.transform);
                    //inHazard = true;
                    break;
                }
            }

            //foreach (Collider col in Physics.OverlapSphere(heldItem.transform.position, 0.01f))
            //{
            //    if (!inAttachArea)
            //    {
            //        if (col.CompareTag("hazard"))
            //        {
            if (heldItem.GetComponent<Rigidbody>() != null) heldItem.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.transform.SetParent(null);
            
            //            //inHazard = true;
            //            break;
            //        }
            //    }
            //}
            //if (!inHazard)
            //{
            //    Destroy(heldItem);
            //}
            Timing.CallDelayed(0.25f, () => gripInputReady = true);
            heldItem = null;

        }
    }

}
