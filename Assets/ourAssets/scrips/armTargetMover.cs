using UnityEngine;
using UnityEngine.XR.Hands;

public class armTargetMover : MonoBehaviour
{

    [SerializeField] Transform[] targets;

    ActiveTarget activeTarget;
    public struct ActiveTarget
    {
        public Transform target;
        public int index;
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeTarget.index = 0;

        activeTarget.target = targets[activeTarget.index];
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position,activeTarget.target.position) < 0.01f){

            switch (activeTarget.index)
            {
                case 0:
                    activeTarget.index = 1;
                    break;

                case 1:
                    activeTarget.index = 0;
                    break;
            }
        }

        activeTarget.target = targets[activeTarget.index];

        transform.position = Vector3.Lerp(transform.position, activeTarget.target.position, 0.1f);

    }
}
