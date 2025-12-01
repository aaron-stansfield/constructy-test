using UnityEngine;

public class hatScript : MonoBehaviour
{
    private void Update()
    {
        if(this.transform.childCount > 0)
        {
            tutorialDoor.instance.hatOn = true;
            GetComponent<hatScript>().enabled = false;
        }
    }
}
