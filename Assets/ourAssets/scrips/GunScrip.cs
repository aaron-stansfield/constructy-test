using MEC;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
public class GunScrip : MonoBehaviour
{

    //[SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");


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
        "good",
        "chemical"
    };

    private GameObject currentDude;

    [SerializeField] private TicketSelector ticketSelector;
    [SerializeField] private CriticalOption criticalOption;


    private bool triggerHeld;

    private bool TriggerInputReady = true;
    public GameObject heldItem = null;

    void Start()
    {
        if (gun == null && transform.childCount > 0)
            gun = transform.GetChild(0).gameObject;

        if (ticketSelector != null)
        {
            ticketSelector.OnTicketChanged += UpdateGunColor;
            UpdateGunColor(ticketSelector.GetCurrentIndex()); // set initial colour
        }

    }

    void Update()
    {
        if (m_TriggerInput.ReadValue() > 0.8f)
        {
            HandleTicketSpawn();
        }
        else if (m_TriggerInput.ReadValue() < 0.2f)
        {
            stopTicketing();
        }


        // ticket pointing at gun end
        if (heldItem != null && gun.activeSelf)
        {
            heldItem.transform.LookAt(ticketTransformGun.position);
            heldItem.transform.transform.localScale = new Vector3(heldItem.transform.localScale.x, heldItem.transform.localScale.y, Vector3.Distance(heldItem.transform.position, gun.transform.position) * 8);
            //if (currentDude.transform.GetChild(0).transform.localScale.z > currentDude.transform.GetChild(1).GetComponent<RectTransform>().)

        }
    }


    // turn gun on / off


    public void stopTicketing()
    {
        if (heldItem == null) return;

        heldItem.transform.SetParent(null);

        heldItem = null;

        TriggerInputReady = true;
    }

    private void HandleTicketSpawn()
    {
        if (TriggerInputReady && heldItem == null && gun.activeSelf)
        {
            TriggerInputReady = false;

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
                string[] hazardNames = {"VIBRATION", "NOISE", "DUST", "GOOD", "MANUAL HANDLING", "CHEMICALS/FUMES" };
                string selectedHazard = hazardNames[index % hazardNames.Length];

                tempHeldTicket.name = "Ticket_" + selectedHazard;
                tempHeldTicket.tag = (string)ticketTags[index];
                TMP_Text label = tempHeldTicket.GetComponentInChildren<TMP_Text>();
                if (label != null)
                    label.text = selectedHazard;
            }
            if(gun.activeSelf) tempHeldTicket.transform.SetParent(null);

        }

        //if(m_RightGripInput.ReadValue() > 0.7f && heldItem != null && !gun.activeSelf)
        //{
        //    heldItem.transform.SetParent(ticketTransform);
        //    heldItem.transform.localPosition = Vector3.zero;
        //    heldItem.transform.localRotation = new Quaternion(0, 0, 0, 0);
        //    Debug.Log(rightController.GetComponent<Rigidbody>().angularVelocity);

        //}


    }

    //newnewnew
    private void UpdateGunColor(int index)
    {
        if (gunRenderers == null || gunRenderers.Length == 0) return;

        Color targetColor = Color.white;

        switch (index)
        {
            case 0: //vibration
                targetColor = Color.red;
                break;
            case 1: //noise
                targetColor = new Color(0.5f, 0f, 0.5f); // purple
                break;
            case 2: //dust
                targetColor = new Color(1f, 0.5f, 0f); // orange
                break;
            case 3: //good
                targetColor = Color.green;
                break;
            case 4: //manual handling
                targetColor = Color.blue;
                break;
            case 5: //chemicals/fumes
                targetColor = Color.yellow;
                break;
        }

        //made into an array because gun object has 3 3d objects
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
