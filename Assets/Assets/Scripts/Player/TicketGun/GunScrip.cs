using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class GunScrip : MonoBehaviour
{
    [SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");
    [SerializeField] XRInputValueReader<float> m_TriggerInput = new XRInputValueReader<float>("Trigger");

    [SerializeField] GameObject controllerVisual;
    [SerializeField] GameObject rightController;

    [SerializeField] private GameObject gun;
    [SerializeField] private Transform ticketTransform;
    [SerializeField] private GameObject ticketPrefab;
    [SerializeField] private LayerMask ticketLayer;
    [SerializeField] private LayerMask fixLayer;
    [SerializeField] private LayerMask fixAndTicketLayer;
    [SerializeField] Transform ticketTransformGun;

    //newnewnew
    [SerializeField] private Renderer[] gunRenderers;

    private string[] ticketTags = new string[]
    {
        "electrical",
        "trippingHazard",
        "noPPE",
        "other",
        "lifting",
        "chemical"
    };

    [SerializeField] private TicketSelector ticketSelector;
    [SerializeField] private CriticalOption criticalOption;

    private bool TriggerInputReady = true;
    public GameObject heldItem = null;

    void Start()
    {
        if (gun == null && transform.childCount > 0)
            gun = transform.GetChild(0).gameObject;

        if (ticketSelector != null)
        {
            ticketSelector.OnTicketChanged += UpdateGunColor;
            UpdateGunColor(ticketSelector.GetCurrentIndex());
        }
    }

    void Update()
    {
        if (heldItem == null)
        {
            if (m_TriggerInput.ReadValue() > 0.8f)
            {
                HandleTicketSpawn();
            }
            if (m_RightGripInput.ReadValue() > 0.8f)
            {
                foreach (Collider col in Physics.OverlapSphere(
                    rightController.transform.position,
                    ticketTransformGun.GetComponent<SphereCollider>().radius,
                    ticketLayer))
                {
                    if (col.gameObject.GetComponent<InteractionInterface>() == null) return;
                    Destroy(col.gameObject);
                    break;
                }
            }
        }
        else if (m_TriggerInput.ReadValue() < 0.2f)
        {
            stopTicketing();
        }
    }

    public void stopTicketing()
    {
        try
        {
            heldItem.GetComponent<TapeScript>().stopDispensing();
        }
        catch { }

        heldItem = null;
        TriggerInputReady = true;
    }

    private void HandleTicketSpawn()
    {
        if (TriggerInputReady && heldItem == null && gun.activeSelf)
        {
            TriggerInputReady = false;

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
                string[] hazardNames = {
                    "VIBRATION", "NOISE", "DUST",
                    "GOOD", "MANUAL HANDLING", "CHEMICALS/FUMES"
                };
                tempHeldTicket.name = "Ticket_" + hazardNames[index];
                tempHeldTicket.tag = ticketTags[index];

                TMP_Text label = tempHeldTicket.GetComponentInChildren<TMP_Text>();
                if (label != null)
                    label.text = hazardNames[index];
            }

            tempHeldTicket.transform.SetParent(null);

            try
            {
                tempHeldTicket.GetComponent<TapeScript>()
                    .startDispensing(ticketTransformGun, tempHeldTicket);
            }
            catch { }
        }
    }

    private void UpdateGunColor(int index)
    {
        if (gunRenderers == null || gunRenderers.Length == 0) return;

        Color targetColor = Color.white;

        switch (index)
        {
            case 0: targetColor = Color.red; break;
            case 1: targetColor = new Color(0.5f, 0f, 0.5f); break;
            case 2: targetColor = new Color(1f, 0.5f, 0f); break;
            case 3: targetColor = Color.green; break;
            case 4: targetColor = Color.blue; break;
            case 5: targetColor = Color.yellow; break;
        }

        foreach (Renderer r in gunRenderers)
        {
            if (r != null)
                r.material.color = targetColor;
        }
    }

    private void OnDestroy()
    {
        if (ticketSelector != null)
            ticketSelector.OnTicketChanged -= UpdateGunColor;
    }
}
