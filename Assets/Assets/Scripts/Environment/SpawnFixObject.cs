using UnityEngine;

public class SpawnFixObject : MonoBehaviour
{
    [SerializeField] GameObject fixPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject SpawnPrefab()
    {
        GameObject obj = Instantiate(fixPrefab);

        return obj;
    }

}
