using MEC;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class controllerScrip : MonoBehaviour
{
    [SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");



    [SerializeField] GameObject rightController;

    [SerializeField] GameObject gun;

    [SerializeField] Transform holdPointTransform;

    [SerializeField] LayerMask fixAndTicketLayer;

    [SerializeField] GameObject heldItem = null;

    private bool gripInputReady = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputHandler();
    }


    public void inputHandler()
    {
         //Holding down grip
        if (m_RightGripInput.ReadValue() > 0.7f && heldItem == null && !gun.activeInHierarchy)
        {
            pickUp();
        }

        //Letting go of grip
        else if (m_RightGripInput.ReadValue() < 0.3f && heldItem != null && gripInputReady)
        {
            drop();
        }


        buttonSlapCheck();


    }

    void buttonSlapCheck()
    {
        foreach (Collider col in Physics.OverlapSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius))
        {
            if (col.GetComponent<buttonInterface>() == null) return;

            col.GetComponent<buttonInterface>().interactWithButton();
        }
    }


    void pickUp()
    {
        foreach (Collider col in Physics.OverlapSphere(rightController.transform.position, rightController.GetComponent<SphereCollider>().radius, fixAndTicketLayer))
        {
            if (col.gameObject.GetComponent<interactionInterface>() == null) return;

            heldItem = col.gameObject;
            Debug.Log("gleep");
            col.gameObject.GetComponent<interactionInterface>().interactWithObj(holdPointTransform);

            break;
        }
    }


    public void drop()
    {
        if (heldItem == null || heldItem.GetComponent<interactionInterface>() == null) return;
        
        heldItem.GetComponent<interactionInterface>().drop();


        Timing.CallDelayed(0.25f, () => gripInputReady = true);
        heldItem = null;
    }

}
