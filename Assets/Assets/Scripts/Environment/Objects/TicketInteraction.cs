using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class TicketInteraction : MonoBehaviour, InteractionInterface
{



    [SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //virtual public void pickUpObject(bool ongoing)
    //{

    //}

    public void Update()
    {
        if (this.transform.parent != null)
        {
            transform.localPosition = Vector3.zero;
            //transform.localEulerAngles = new Vector3(0,90,0);
        }
    }

    void InteractionInterface.interactWithObj(Transform guy)
    {
        Debug.Log("Interacted with Tickets");



        this.transform.SetParent(guy);

        this.transform.localPosition = Vector3.zero;

        //puts the ticket in the correct orientation relative to the controller
        //transform.localEulerAngles = new Vector3(0, 90, 0);
    }

    public void drop()
    {
        this.transform.SetParent(null);


    }
    
}
