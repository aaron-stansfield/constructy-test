using UnityEngine;
using UnityEngine.Rendering;

public class situationScrip : MonoBehaviour
{
    [SerializeField] float checkRadius;

    [SerializeField] string expectedTicket;

    public int checks;

    [SerializeField] LayerMask ticketLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int report()
    {
        foreach (Collider col in Physics.OverlapSphere(this.transform.position, checkRadius, ticketLayer)){
            if (col.transform.CompareTag(expectedTicket))
            {
                checks = 1;
            }
        }
        return checks;
    }

}
