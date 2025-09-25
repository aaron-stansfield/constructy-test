using UnityEngine;

public class buttonScript1 : MonoBehaviour
{
    public GameObject clipboard;

    private clipboardScrip clippy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clippy = clipboard.GetComponent<clipboardScrip>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("plo");
        if (other.CompareTag("GameController")){
            clippy.spawnOtherTicket();
        }
    }


}
