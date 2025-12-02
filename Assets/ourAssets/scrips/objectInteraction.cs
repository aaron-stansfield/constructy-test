using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class objectInteraction : MonoBehaviour, interactionInterface
{

    public bool held = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //virtual public void pickUpObject(bool ongoing)
    //{

    //}

    public void Update()
    {
        if (this.transform.parent != null)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }

    void interactionInterface.interactWithObj(Transform guy)
    {

        Debug.Log("shleeb");

        held = true;

        this.GetComponent<Rigidbody>().isKinematic = true;

        this.transform.SetParent(guy);

        this.transform.localPosition = Vector3.zero;

        //puts the ticket in the correct orientation relative to the controller
        this.transform.localRotation = Quaternion.identity;
    }

    public void drop()
    {

        this.transform.SetParent(null);

        held = false;

        foreach (Collider col in Physics.OverlapSphere(this.transform.position, 0.01f))
        {
            if (col.CompareTag("AttatchArea") && this.gameObject.GetComponent<AttatchObject>() != null)
            {
                this.gameObject.transform.GetComponent<AttatchObject>().targetTransform = col.transform;
                this.gameObject.transform.GetComponent<AttatchObject>().StartLerp();
                this.transform.SetParent(col.gameObject.transform);                //inHazard = true;
                break;
            }
        }

        //if (this.transform.parent == null) return;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
    
}
