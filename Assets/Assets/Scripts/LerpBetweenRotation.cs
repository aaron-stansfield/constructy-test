using UnityEngine;

public class LerpBetweenRotation : MonoBehaviour
{
    [SerializeField]
    public GameObject[] objects = new GameObject[1];

    [SerializeField, Range(0, 1f)]
    public float speed;

    [SerializeField]
    public bool inverse = false;

    private int target;

    private void Start()
    {
        target = inverse ? 1 : 0;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.eulerAngles, objects[target].transform.rotation.eulerAngles) <= 0.01f)
        {
            target = 1 - target;
        }

        Vector3 rotation = Vector3.Lerp(transform.rotation.eulerAngles, objects[target].transform.rotation.eulerAngles, speed);
        transform.rotation = Quaternion.Euler(rotation);
    }
}