using UnityEngine;

public class MOVECAMERATHING : MonoBehaviour
{
    GameObject controller;
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            this.transform.position = controller.transform.position;
        }    
    }
}
