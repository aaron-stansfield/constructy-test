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

    [SerializeField] GameObject fixIcons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        foreach (Collider col in Physics.OverlapSphere(this.transform.position, checkRadius, ticketLayer))
        {
            fixIcons.SetActive(true);
            return;
            
        }
        foreach (Collider col in Physics.OverlapSphere(this.transform.position, checkRadius, fixLayer))
        {
            //if (!fixIcons.transform.GetChild(0).gameObject.activeInHierarchy)
            //{
                fixIcons.SetActive(true);
                
            //}

            //else
            //{
            //    fixIcons.transform.GetChild(0).gameObject.SetActive(false);
            //    fixIcons.transform.GetChild(1).gameObject.SetActive(true);
            //}
            return;
        }
        fixIcons.SetActive(false);
        //fixIcons.transform.GetChild(1).gameObject.SetActive(false);
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
