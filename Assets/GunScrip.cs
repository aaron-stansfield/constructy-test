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

    private GameObject currentDude;

    [SerializeField] private TicketSelector ticketSelector;
    [SerializeField] private CriticalOption criticalOption;


    private bool triggerHeld;


    private bool canToggleGun = true;
    private bool gripInputReady = true;
    [SerializeField] GameObject heldTicket = null;

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
        if ( heldTicket != null && gun.activeSelf)
        {
            heldTicket.transform.LookAt(ticketTransformGun.position);
            heldTicket.transform.GetChild(0).transform.localScale = new Vector3(heldTicket.transform.localScale.x, heldTicket.transform.localScale.y, Vector3.Distance(heldTicket.transform.position, gun.transform.position) *  8);
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
        if (m_RightGripInput.ReadValue() > 0.8f && gripInputReady && heldTicket == null && gun.activeSelf)
        {
            gripInputReady = false;

            //taken and slightly changed from clipboard script
            GameObject dude = Instantiate(ticketPrefab);
            dude.transform.SetParent(ticketTransformGun);
            dude.transform.localPosition = Vector3.zero;
            
            heldTicket = dude;
            
        
            if (criticalOption != null)
            {
                var renderer = dude.transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
                if (renderer != null)
                    renderer.material.color = criticalOption.GetCurrentColor();
            }
            if (ticketSelector != null)
            {
                int index = ticketSelector.GetCurrentIndex();
                string[] hazardNames = {"ELECTRICAL", "TRIPPING HAZARD", "NO PPE", "OTHER", "GOOD", "CHEMICALS/DUST" };
                string selectedHazard = hazardNames[index % hazardNames.Length];

                dude.name = "Ticket_" + selectedHazard;
                TMP_Text label = dude.GetComponentInChildren<TMP_Text>();
                if (label != null)
                    label.text = selectedHazard;
            }
            if(gun.activeSelf) dude.transform.SetParent(null);

        }

        if(m_RightGripInput.ReadValue() > 0.7f && heldTicket != null && !gun.activeSelf)
        {
            //heldTicket.transform.SetParent(ticketTransform);
            //heldTicket.transform.position = Vector3.zero;
        }

        else if (m_RightGripInput.ReadValue() > 0.7f && heldTicket == null && !gun.activeInHierarchy)
        {
            foreach (Collider col in Physics.OverlapSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius, fixLayer))
            {
                heldTicket = col.gameObject;
                col.gameObject.transform.SetParent(ticketTransform);

                col.transform.localPosition = Vector3.zero;

                return;
            }

            foreach (Collider col in Physics.OverlapSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius, ticketLayer))
            {
                heldTicket = col.gameObject;
                col.gameObject.transform.SetParent(ticketTransform);

                col.transform.localPosition = Vector3.zero;

                //puts the ticket in the correct orientation relative to the controller
                col.transform.localEulerAngles = new Vector3(0, 180, 0);
                return;
            }
        }



        if (m_RightGripInput.ReadValue() < 0.3f && heldTicket != null)
        {
            bool inHazard = false;
            foreach (Collider col in Physics.OverlapSphere(heldTicket.transform.position, 0.01f))
            {
                if (col.CompareTag("AttatchArea"))
                {
                    heldTicket.gameObject.transform.GetComponent<AttatchObject>().StartLerp();
                    heldTicket.transform.SetParent(col.gameObject.transform);
                    inHazard = true;
                    continue;
                }
                //else if (col.CompareTag("hazard"))
                //{
                //    
                //    heldTicket.transform.SetParent(null);
                //    inHazard = true;
                //    conti;
                //}
            }
            if (!inHazard)
            {
                Destroy(heldTicket);
            }
            Timing.CallDelayed(0.25f, () => gripInputReady = true);
            heldTicket = null;

        }
    }

}
