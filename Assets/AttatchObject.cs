using UnityEngine;
using System.Collections;

public class AttatchObject : MonoBehaviour
{
    public Transform targetTransform;
    public float lerpDuration = 2f;


    private bool isLerping = false;

    // Call this from another script or with a button
    public void StartLerp()
    {
        if (targetTransform != null && !isLerping)
        {
            StartCoroutine(LerpToPositionAndRotation(targetTransform.position, targetTransform.rotation, lerpDuration));


        }
    }


    private void Update()
    {

    }


    private IEnumerator LerpToPositionAndRotation(Vector3 targetPos, Quaternion targetRot, float duration)
    {
        isLerping = true;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;

            // Interpolate position and rotation
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Snap to final position/rotation
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        isLerping = false;
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("AttatchArea"))
        {
            Debug.Log(col.gameObject.name);
            targetTransform = col.transform;
        }
    }
}