using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public Transform attachObject;

    // Update is called once per frame
    void Update()
    {
        transform.position = attachObject.position;
    }
}
