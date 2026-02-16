using UnityEngine;

public class HatScript : MonoBehaviour
{
    private void Update()
    {
        if(this.transform.parent.childCount > 0 && this.transform.parent.CompareTag("AttatchArea"))
        {
            TutorialDoor.instance.hatOn = true;
            GetComponent<HatScript>().enabled = false;
        }
    }
}
