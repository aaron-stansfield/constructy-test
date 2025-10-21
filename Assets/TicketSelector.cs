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

    private void Start()
    {
        baseRotation = cylinder.localRotation;
    }

    private void Update()
    {
        if (m_LeftGripInput.ReadValue() > 0.5f && gripReady && IsControllerNearCylinder() && dialScip.gripReady)
        {
            Rotating();
            StartCoroutine(GripCooldown());
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

    private void Rotating()
    {
        currentIndex = (currentIndex + 1) % 6;
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
