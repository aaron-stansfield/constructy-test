using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class situationScrip : MonoBehaviour
{
    [SerializeField] float checkRadius;

    [SerializeField] string expectedTicket;
    
    [SerializeField] string expectedFix;

    public bool idCheck;
    public bool fixCheck;

    [SerializeField] LayerMask ticketLayer;
    [SerializeField] LayerMask fixLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool[] report()
    {
        idCheck = false;
        fixCheck = false;
        foreach (Collider col in Physics.OverlapSphere(this.transform.position, checkRadius, ticketLayer)){
            if (!col.transform.CompareTag(expectedTicket))
            {

                idCheck = false;
                break;
            }
            else
            {

                idCheck = true;
            }
        }

        foreach (Collider col in Physics.OverlapSphere(this.transform.position, checkRadius, fixLayer))
        {
            if (!col.transform.CompareTag(expectedFix))
            {
                fixCheck = false;
                break;
            }
            else
            {

                fixCheck = true;
            }
        }

        bool[] returnval;
        returnval = new bool[2];

        returnval[0] = idCheck;
        returnval[1] = fixCheck;
        
        return returnval;
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(this.transform.position, checkRadius);
    //}

}
