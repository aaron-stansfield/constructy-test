using UnityEngine;

public class testingAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetTrigger("loud");
    }
}
