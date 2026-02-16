using UnityEngine;

public class ObjectInteraction : MonoBehaviour, InteractionInterface
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

    void InteractionInterface.interactWithObj(Transform player)
    {

        Debug.Log("Interacted with Object");

        held = true;

        this.GetComponent<Rigidbody>().isKinematic = true;

        this.transform.SetParent(player);

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
            if (col.CompareTag("AttatchArea") && this.gameObject.GetComponent<AttachObject>() != null)
            {
                this.gameObject.transform.GetComponent<AttachObject>().targetTransform = col.transform;
                this.gameObject.transform.GetComponent<AttachObject>().StartLerp();
                this.transform.SetParent(col.gameObject.transform);                //inHazard = true;
                break;
            }
        }

        //if (this.transform.parent == null) return;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
    
}
