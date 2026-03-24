using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public Transform attachObject;

    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.position = attachObject.position;

        }
        catch 
        {
/*            if (this.transform.parent.parent != null)
            {

                Debug.LogWarning($"setPositionScrip is giving error on {this.transform.parent.parent.name}, just remove the script if ur not gonna add a target smh");
            }
            else
            {

                Debug.LogWarning($"setPositionScrip is giving error on {this.transform.name}, just remove the script if ur not gonna add a target smh");
            }*/
        }
    }
}
