using UnityEngine;
using System.Collections;
using System.Data;
using Assets.Assets.Scripts.Environment.Objects;


public enum AttachPointType
{
    Head,
    Hands,
    Torso,
    Legs,
    Feet
}

public class AttachObject : MonoBehaviour
{
    public Transform targetTransform;
    public float lerpDuration = 2f;

    public bool hasPivot;
    public float pivotDifference;

    public ObjectInteraction interactScript;
    public AttachPointType acceptPoint;

    private bool isLerping = false;

    // Call this from another script or with a button
    public void StartLerp()
    {
        if (targetTransform != null && !isLerping)
        {
            StartCoroutine(LerpToPositionAndRotation(targetTransform.position, targetTransform.rotation, lerpDuration));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AttatchArea") && !isLerping)
        {
            if(other.GetComponent<AttachPoint>() is AttachPoint attachPoint && attachPoint.attachPointType != acceptPoint)
                return;
            targetTransform = other.transform;
            StartLerp();
        }
    }

    private IEnumerator LerpToPositionAndRotation(Vector3 targetPos, Quaternion targetRot, float duration)
    {
        isLerping = true;

        Vector3 newTarget;
        newTarget = targetPos;
        if (hasPivot)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponentInChildren<MeshCollider>().enabled = false;

            newTarget = new Vector3(targetPos.x, targetPos.y + pivotDifference, targetPos.z);
        }

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;

            // Interpolate position and rotation
            transform.position = Vector3.Lerp(startPos, newTarget, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Snap to final position/rotation
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        

        isLerping = false;
    }


    /*private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("AttatchArea"))
        {
            Debug.Log(col.gameObject.name);
            targetTransform = col.transform;
            StopAllCoroutines();
            StartLerp();
        }
    }*/
}