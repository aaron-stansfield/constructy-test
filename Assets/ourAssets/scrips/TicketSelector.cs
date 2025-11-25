using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.UIElements;

public class TicketSelector : MonoBehaviour
{
    [SerializeField] private Transform cylinder;
    [SerializeField] private GameObject leftController;
    [SerializeField] private LayerMask cylinderLayer;

    [SerializeField] private XRInputValueReader<float> m_LeftGripInput = new XRInputValueReader<float>("Grip");

    [SerializeField] CriticalOption dialScip;

    //newnewnew
    [SerializeField] private float rotationPerTicket = 60f; 
    [SerializeField] private float scrollSensitivity = 5f;
    [SerializeField] private float snapSpeed = 10f; 

    public bool gripReady = true;
    private float currentRotation = 0f; // continuous rotation value
    private int currentIndex = 0;

    private Quaternion baseRotation;

    [SerializeField] private Collider TopBoxCollider;
    [SerializeField] private Collider BottomBoxCollider;

    public delegate void TicketChangedEvent(int newIndex);
    public event TicketChangedEvent OnTicketChanged;

    private void Start()
    {
        baseRotation = cylinder.localRotation;
        currentRotation = 0f;
    }

    private void InteractionComplete()
    {
        float a = cylinder.localEulerAngles.x;
        a %= 360;
        OnTicketChanged?.Invoke((int)(a / 6));
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
