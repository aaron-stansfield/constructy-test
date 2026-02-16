using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        //this.transform.rotation = new quaternion(this.transform.rotation.x,this.transform.rotation.y - 180,this.transform.rotation.z, this.transform.rotation.w);
        this.transform.LookAt(player.position);
        
    }
}
