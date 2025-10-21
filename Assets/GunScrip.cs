using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using MEC;
using TMPro;

public class GunScrip : MonoBehaviour
{
    [SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");
    [SerializeField] XRInputValueReader<float> gunButton = new XRInputValueReader<float>("primaryButton");

    [SerializeField] GameObject controllerVisual;

    [SerializeField] private GameObject gun;
    [SerializeField] private Transform ticketTransform;
    [SerializeField] private GameObject ticketPrefab;
    [SerializeField] private LayerMask ticketLayer;
    [SerializeField] Transform ticketTransformGun;

    private GameObject currentDude;

    [SerializeField] private TicketSelector ticketSelector;
    [SerializeField] private CriticalOption criticalOption;


    private bool triggerHeld;


    private bool canToggleGun = true;
    private bool gripInputReady = true;
    private Transform heldTicket = null;

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
        if ( currentDude != null && gun.activeSelf)
        {
            currentDude.transform.LookAt(ticketTransformGun.position);
            currentDude.transform.GetChild(0).transform.localScale = new Vector3(currentDude.transform.localScale.x, currentDude.transform.localScale.y, Vector3.Distance(currentDude.transform.position, gun.transform.position) *  8);
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
            
            heldTicket = dude.transform;
            
        
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
            currentDude = dude;

        }

        if(m_RightGripInput.ReadValue() > 0.7f && heldTicket != null && !gun.activeSelf)
        {
            heldTicket.SetParent(ticketTransform);
            heldTicket.transform.position = Vector3.zero;
        }

        if (m_RightGripInput.ReadValue() < 0.3f && heldTicket != null)
        {
            currentDude = null;
            Timing.CallDelayed(0.25f, () => gripInputReady = true);
            heldTicket.SetParent(null);
            heldTicket = null;
        }
    }

}
