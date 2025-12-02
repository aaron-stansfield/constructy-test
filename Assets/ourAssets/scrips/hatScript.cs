using UnityEngine;

public class hatScript : MonoBehaviour
{
    private void Update()
    {
        if(this.transform.parent.childCount > 0 && this.transform.parent.CompareTag("AttatchArea"))
        {
            tutorialDoor.instance.hatOn = true;
            GetComponent<hatScript>().enabled = false;
        }
    }
}
