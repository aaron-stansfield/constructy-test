using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class CriticalOption : MonoBehaviour
{
    [SerializeField] private Transform cylinder;
    [SerializeField] private GameObject leftController;
    [SerializeField] private LayerMask cylinderLayer;

    [SerializeField] private XRInputValueReader<float> m_LeftGripInput = new XRInputValueReader<float>("Grip");


    [SerializeField] private float rotation = 90f; //
    [SerializeField] private float coolDown = 0.3f;

    private bool gripReady = true;
    private bool isRotated = false;

    private Quaternion baseRotation;

    [SerializeField] private Renderer cubeRenderer; //may work

    private void Start()
    {
        baseRotation = cylinder.localRotation;
    }

    private void Update()
    {
        if (m_LeftGripInput.ReadValue() > 0.5f && gripReady && IsControllerNearCylinder())
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
        isRotated = !isRotated;

        if (isRotated)
        {

            cylinder.localRotation = baseRotation * Quaternion.Euler(0f, rotation, 0f);

            if (cubeRenderer != null)
                cubeRenderer.material.color = Color.red; //new
        }
        else
        {
            cylinder.localRotation = baseRotation;

            if (cubeRenderer != null)
                cubeRenderer.material.color = Color.yellow; //new

        }
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

    public Color GetCurrentColor()
    {
        return isRotated ? Color.red : Color.yellow;
    }

}