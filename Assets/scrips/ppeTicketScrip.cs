using UnityEngine;

public class ppeTicketScript : MonoBehaviour
{
    public clipboardScrip clipboard;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GameController")){
            clipboard.spawnPPETicket();
        }
    }


}
