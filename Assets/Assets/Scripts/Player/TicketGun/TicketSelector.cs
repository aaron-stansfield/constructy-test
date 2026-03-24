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

    [SerializeField] private float gripLatchThreshold = 0.6f;
    [SerializeField] private float gripReleaseThreshold = 0.3f;
    [SerializeField] private float twistStepDegrees = 30f;
    [SerializeField] private float stepCooldown = 0.12f;

    private bool isLatched;
    private Quaternion latchControllerRotation;
    private bool stepOnCooldown;

    //new 24/2
    private float latchStartRotation = 0f;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        baseRotation = cylinder.localRotation;
        currentRotation = 0f;
    }

    private void Update()
    {
        HandleLatchTwist();
    }

    //replaces old up/down scrolling with latch and wristtwist snap
    private void HandleLatchTwist()
    {
        bool near = IsControllerNearCylinder();
        float grip = m_LeftGripInput.ReadValue();
        bool dialAllows = (dialScip == null) || dialScip.gripReady;

        if (!isLatched)
        {
            if (near && dialAllows && gripReady && grip > gripLatchThreshold)
            {
                isLatched = true;
                latchControllerRotation = leftController.transform.rotation;

                //new 24/2
                latchStartRotation = currentRotation;
            }
            return;
        }

        if (!near || grip < gripReleaseThreshold)
        {
            isLatched = false;

            SnapToNearestIndex();
            return;
        }



        //if (stepOnCooldown) return;

        Quaternion currentRot = leftController.transform.rotation;
        //Quaternion delta = Quaternion.Inverse(latchControllerRotation) * currentRot;

        //delta.ToAngleAxis(out float angle, out Vector3 axis);
        //if (angle > 180f) angle -= 360f;

        //float sign = Mathf.Sign(Vector3.Dot(axis, cylinder.up));
        //float signedAngle = angle * sign;

        //new 24/2
        currentRotation = latchStartRotation + currentRot.eulerAngles.z;
        currentRotation = Mathf.Repeat(currentRotation, 360f);
        cylinder.localRotation =/* baseRotation * */Quaternion.Euler(0.0f, -180.0f, -currentRot.eulerAngles.z);



    }

    //snap with event and should play sound
    private void StepIndex(int delta)
    {
        int newIndex = (currentIndex + delta) % 6;
        if (newIndex < 0) newIndex += 6;

        if (newIndex == currentIndex) return;

        currentIndex = newIndex;
        OnTicketChanged?.Invoke(currentIndex);

        currentRotation = currentIndex * rotationPerTicket;
        cylinder.localRotation = baseRotation * Quaternion.Euler(0f, currentRotation, 0f);

        if (sound != null) sound.Play();
    }

    //cooldown to prevent wthe weird continuous spinning
    private IEnumerator StepCooldown()
    {
        stepOnCooldown = true;
        gripReady = false;
        yield return new WaitForSeconds(stepCooldown);
        gripReady = true;
        stepOnCooldown = false;
    }

    //new24/2
    private void SnapToNearestIndex()
    {
        int newIndex = Mathf.RoundToInt(currentRotation / rotationPerTicket) % 6;
        if (newIndex < 0) newIndex += 6;

        if (newIndex == currentIndex)
        {
            //still force exact snap to slot
            currentRotation = currentIndex * rotationPerTicket;
            cylinder.localRotation = baseRotation * Quaternion.Euler(0f, currentRotation, 0f);
            return;
        }

        currentIndex = newIndex;
        OnTicketChanged?.Invoke(currentIndex);

        currentRotation = currentIndex * rotationPerTicket;
        cylinder.localRotation = /*baseRotation **/ Quaternion.Euler(0f, -currentRotation, 0f);

        if (sound != null) sound.Play();
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