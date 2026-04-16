using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SituationScript : MonoBehaviour
{
    [SerializeField] float checkRadius;

    [SerializeField] string expectedTicket;
    
    [SerializeField] string expectedFix;

    public bool idCheck;
    public bool fixCheck;

    [SerializeField] AudioDamageIndicatorScript audioDamage;

    private GameObject targetText;

    [SerializeField] LayerMask ticketLayer;
    [SerializeField] LayerMask fixLayer;

    [SerializeField] string boardText;

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
            try
            {
                fixIcons.SetActive(true);

                targetText.transform.GetChild(0).gameObject.SetActive(true);
                targetText.GetComponent<TMP_Text>().text = boardText;
                
                //obselete i think
                //targetText.GetComponentInChildren<Image>().image = fixIcons.GetComponentInChildren<Image>().image;
            }
            catch { }
            return;
            
        }
        foreach (Collider col in Physics.OverlapSphere(this.transform.position, checkRadius, fixLayer))
        {


            fixIcons.SetActive(true);
            targetText.GetComponent<TMP_Text>().text = boardText;
            try
            {
                targetText.transform.GetChild(1).gameObject.SetActive(true);
                if (col.transform.CompareTag(expectedFix) && expectedFix == "ear defenders")
                {
                    audioDamage.hasPPE = true;
                }
                else
                {
                    audioDamage.hasPPE = false;
                }
            }
            
            catch { }
            return;
        }

        targetText.transform.GetChild(0).gameObject.SetActive(false);
        targetText.transform.GetChild(1).gameObject.SetActive(false);
        fixIcons.SetActive(false);
    }


    public void takeTextSpot()
    {
        Transform updateText = GameObject.Find("update text array").transform;
        // this is to add ownership of one of the 8 report things in the shed to this specific object
        // then sets its tag so other scenarios know its taken
        int count = 0;
        for (int i = 0; i < updateText.childCount + 1; i++)
        {
            count++;
            if (!updateText.GetChild(i).CompareTag("textTaken"))
            {
                updateText.GetChild(i).tag = "textTaken";
                targetText = updateText.GetChild(i).gameObject;
                break;
            }

            Debug.Log("goobed as fuck, there aint enough text fields for this many scenarios");
        }
        targetText.GetComponent<TMP_Text>().text = ("situation "+count.ToString());
    }


    public bool[] report()
    {
        idCheck = false;
        fixCheck = false;
        foreach (Collider col in Physics.OverlapSphere(this.transform.position, checkRadius, ticketLayer)){
            if (col.transform.CompareTag(expectedTicket) && idCheck == false)
            {

                idCheck = true;
                break;
            }
        }

        foreach (Collider col in Physics.OverlapSphere(this.transform.position, checkRadius, fixLayer))
        {
            if (col.transform.CompareTag(expectedFix) && fixCheck == false)
            {
                fixCheck = true;
                break;
            }
        }

        bool[] returnVal;
        returnVal = new bool[2];

        returnVal[0] = idCheck;
        returnVal[1] = fixCheck;
        
        return returnVal;
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(this.transform.position, checkRadius);
    //}

}
