using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class CriticalOption : MonoBehaviour
{
    [SerializeField] private Transform cylinder;
    [SerializeField] private GameObject leftController;
    [SerializeField] private LayerMask cylinderLayer;

    [SerializeField] private XRInputValueReader<float> m_LeftGripInput = new XRInputValueReader<float>("Grip");

    [SerializeField] TicketSelector selectorScrip;

    [SerializeField] private float rotation = 90f; 
    [SerializeField] private float coolDown = 0.3f;

    public bool gripReady = true;
    private bool isRotated;

    private Quaternion baseRotation;

    [SerializeField] private Material color1, color2;

    //new
    [SerializeField] private Renderer[] cubeRenderers;

    private void Start()
    {
        baseRotation = cylinder.localRotation;
    }

    private void Update()
    {
        if (m_LeftGripInput.ReadValue() > 0.5f && gripReady && IsControllerNearCylinder() && selectorScrip.gripReady)
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
            SetAllCubeColors(color2); 
        }
        else
        {
            cylinder.localRotation = baseRotation;
            SetAllCubeColors(color1); 
        }
    }

    private void SetAllCubeColors(Material mat)
    {
        if (cubeRenderers == null || cubeRenderers.Length == 0) return;

        foreach (Renderer r in cubeRenderers)
        {
            if (r != null)
                r.material = mat;
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

    public Material GetCurrentColor()
    {
        return isRotated ? color2 : color1;
    }
}
