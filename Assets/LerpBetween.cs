using System;
using UnityEngine;

public class LerpBetween : MonoBehaviour
{
    [SerializeField]
    public GameObject[] objects = new GameObject[1];

    [SerializeField, Range(0, 0.25f)]
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
        if (Vector3.Distance(transform.position, objects[target].transform.position) <= 0.01f)
        {
            target = 1 - target;
        }

        transform.position = Vector3.Lerp(transform.position, objects[target].transform.position, speed);
    }
}