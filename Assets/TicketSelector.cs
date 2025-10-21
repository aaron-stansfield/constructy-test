using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class TicketSelector : MonoBehaviour
{
    [SerializeField] private Transform cylinder;
    [SerializeField] private GameObject leftController;
    [SerializeField] private LayerMask cylinderLayer;

    [SerializeField] private XRInputValueReader<float> m_LeftGripInput = new XRInputValueReader<float>("Grip");

    [SerializeField] CriticalOption dialScip;

    [SerializeField] private float rotation = 60f;
    [SerializeField] private float coolDown = 0.3f;

    public bool gripReady = true;
    private int currentIndex = 0;

    private Quaternion baseRotation;

    [SerializeField] private Collider TopBoxCollider;
    [SerializeField] private Collider BottomBoxCollider;
    private bool wasTop = false;
    private bool wasBottom = false;
    private bool swipeLocked = false; //stops multiple rotations per swipe

    private void Start()
    {
        baseRotation = cylinder.localRotation;
    }

    private void Update()
    {
        if (gripReady && dialScip.gripReady)
        {
            DetectSwipe();//iterated so grip isnt needed
        }
        else
        {
            if (!TopBoxCollider.bounds.Contains(leftController.transform.position) &&
                !BottomBoxCollider.bounds.Contains(leftController.transform.position))
            {
                swipeLocked = false;
            }
        }
    }

    private void DetectSwipe()
    {
        if (swipeLocked) return;

        bool Top = TopBoxCollider.bounds.Contains(leftController.transform.position);
        bool Bottom = BottomBoxCollider.bounds.Contains(leftController.transform.position);

        //rotate backwards
        if (wasBottom && Top)
        {
            Rotating(false);
            swipeLocked = true;
            StartCoroutine(GripCooldown());
        }
        //rotate forwards
        else if (wasTop && Bottom)
        {
            Rotating(true);
            swipeLocked = true;
            StartCoroutine(GripCooldown());
        }

        wasTop = Top;
        wasBottom = Bottom;
    }

    private void Rotating(bool forward = true)
    {
        if (forward)
            currentIndex = (currentIndex + 1) % 6;
        else 
            currentIndex = (currentIndex - 1 + 6) % 6;

        Mathf.Clamp(currentIndex, 1 ,6); 

            float newRotationY = currentIndex * rotation;
        cylinder.localRotation = baseRotation * Quaternion.Euler(0f, newRotationY, 0f);
    }

    private IEnumerator GripCooldown()
    {
        gripReady = false;
        yield return new WaitForSeconds(coolDown);
        gripReady = true;
    }

    private void OnDrawGizmos()
    {
        // can add later if wanted
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

}

