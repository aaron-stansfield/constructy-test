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

    [SerializeField] private Renderer[] cubeRenderers;


    //new24/02
    [SerializeField] private Transform buttonTransform;//asign mesh to move
    [SerializeField] private float pressDepth = 0.015f;//how far it moves
    [SerializeField] private float pressLerpSpeed = 18f;//snappy feel
    private Vector3 buttonBaseLocalPos;

    private void Start()
    {
        baseRotation = cylinder.localRotation;

        //new24/02
        if (buttonTransform == null) buttonTransform = cylinder;
        buttonBaseLocalPos = buttonTransform.localPosition;
        ApplyButtonPositionImmediate();
        SetAllCubeColors(isRotated ? Color.red : Color.yellow);
    }

    private void Update()
    {
        if (m_LeftGripInput.ReadValue() > 0.5f && gripReady && IsControllerNearCylinder() && selectorScrip.gripReady)
        {
            Rotating();
            StartCoroutine(GripCooldown());
        }

        //new24/02
        ApplyButtonPositionSmooth();
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
            cylinder.localRotation = baseRotation; // * Quaternion.Euler(0f, rotation, 0f);
            SetAllCubeColors(Color.red);
        }
        else
        {
            cylinder.localRotation = baseRotation;
            SetAllCubeColors(Color.yellow);
        }
    }

    private void SetAllCubeColors(Color color)
    {
        if (cubeRenderers == null || cubeRenderers.Length == 0) return;

        foreach (Renderer r in cubeRenderers)
        {
            if (r != null)
                r.material.color = color;
        }
    }

    //new24/02
    private void ApplyButtonPositionSmooth()
    {
        if (buttonTransform == null) return;

        Vector3 target = buttonBaseLocalPos + (isRotated ? Vector3.down * pressDepth : Vector3.zero);
        buttonTransform.localPosition = Vector3.Lerp(buttonTransform.localPosition, target, Time.deltaTime * pressLerpSpeed);
    }
    //new as well
    private void ApplyButtonPositionImmediate()
    {
        if (buttonTransform == null) return;

        Vector3 target = buttonBaseLocalPos + (isRotated ? Vector3.down * pressDepth : Vector3.zero);
        buttonTransform.localPosition = target;
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
