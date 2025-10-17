using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using MEC;
using TMPro;

public class GunScrip : MonoBehaviour
{
    [SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");
    [SerializeField] XRInputValueReader<float> gunButton = new XRInputValueReader<float>("primaryButton");

    [SerializeField] private GameObject gun;
    [SerializeField] private Transform ticketTransform;
    [SerializeField] private GameObject ticketPrefab;
    [SerializeField] private LayerMask ticketLayer;
    [SerializeField] Transform ticketTransformGun;

    private GameObject currentDude;

    //new
    [SerializeField] private TicketSelector ticketSelector;
    [SerializeField] private CriticalOption criticalOption;

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

        if ( currentDude != null)
        {
            currentDude.transform.LookAt(ticketTransformGun.position);
        }
    }

    private void HandleGunToggle()
    {
        if (gunButton.ReadValue() > 0.75f && canToggleGun)
        {
            canToggleGun = false;
            gun.SetActive(!gun.activeSelf);
            Timing.CallDelayed(0.75f, () => canToggleGun = true);
        }
    }

    private void HandleTicketSpawn()
    {
        if (m_RightGripInput.ReadValue() > 0.8f && gripInputReady && heldTicket == null && gun.activeSelf)
        {
            gripInputReady = false;

            //taken and slightly changed from clipboard script
            GameObject dude = Instantiate(ticketPrefab);
            dude.transform.SetParent(ticketTransform);
            dude.transform.localPosition = Vector3.zero;
            dude.transform.SetParent(null);
            currentDude = dude;
            heldTicket = dude.transform;
            //

            if (criticalOption != null)
            {
                var renderer = dude.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.material.color = criticalOption.GetCurrentColor();
            }
            if (ticketSelector != null)
            {
                int index = ticketSelector.GetCurrentIndex();
                string[] hazardNames = {"ELECTRICAL", "TRIPPING HAZARD", "NO PPE", "OTHER", "GOOD", "CHEMICALS/DUST" };
                string selectedHazard = hazardNames[(index % hazardNames.Length) ];

                dude.name = "Ticket_" + selectedHazard;
                TMP_Text label = dude.GetComponentInChildren<TMP_Text>();
                if (label != null)
                    label.text = selectedHazard;
            }

            
        }

        if (m_RightGripInput.ReadValue() < 0.3f && heldTicket != null)
        {
            currentDude = null;
            Timing.CallDelayed(0.75f, () => gripInputReady = true);
            heldTicket.SetParent(null);
            heldTicket = null;
        }
    }

    private void OnDrawGizmos()
    {
        // can add later if wanted
    }
}
