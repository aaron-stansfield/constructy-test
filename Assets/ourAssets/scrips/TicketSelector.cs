using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class TicketSelector : MonoBehaviour
{
    [SerializeField] private Transform cylinder;
    [SerializeField] private GameObject leftController;
    [SerializeField] private LayerMask cylinderLayer;

    [SerializeField] private XRInputValueReader<float> m_LeftGripInput = new XRInputValueReader<float>("Grip");


    private AudioSource sound;

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
        sound = GetComponent<AudioSource>();
        baseRotation = cylinder.localRotation;
        currentRotation = 0f;
    }

    private void Update()
    {
        HandleScroll();
    }

    private void HandleScroll()
    {
        if (!IsControllerNearCylinder()) return;
        float controllerY = leftController.transform.position.y;
        float deltaY = controllerY * scrollSensitivity * Time.deltaTime;

        currentRotation += deltaY;
        cylinder.localRotation = baseRotation * Quaternion.Euler(0f, currentRotation, 0f);

        int newIndex = Mathf.RoundToInt(currentRotation / rotationPerTicket) % 6;
        if (newIndex < 0) newIndex += 6; 
        if (newIndex != currentIndex)
        {
            currentIndex = newIndex;
            OnTicketChanged?.Invoke(currentIndex);
        }

        if (Mathf.Abs(deltaY) < 0.001f)
        {
            float targetRotation = currentIndex * rotationPerTicket;
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * snapSpeed);

            sound.Play();

            cylinder.localRotation = baseRotation * Quaternion.Euler(0f, currentRotation, 0f);
        }
    }

    private bool IsControllerNearCylinder()
    {
        Collider[] hits = Physics.OverlapSphere(leftController.transform.position, 0.05f);

        foreach (Collider col in hits)
        {
            if (col.transform == cylinder || col.gameObject.layer == LayerMask.NameToLayer("Cylinder"))
            {
                return true;
            }
        }
        return false;
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
