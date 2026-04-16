using UnityEngine;

public class npcrandomiser : MonoBehaviour
{

    [SerializeField] Material[] tones;
    SkinnedMeshRenderer target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GetComponentInChildren<SkinnedMeshRenderer>();
        target.material = tones[Random.Range(0, 3)];
    }

}
