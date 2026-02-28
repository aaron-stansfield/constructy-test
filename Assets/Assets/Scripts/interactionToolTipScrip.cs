using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class interactionToolTipScrip : MonoBehaviour
{
    [SerializeField] Transform canvasTransform;
    [SerializeField] Transform canvasTarget;

    [SerializeField] XRInputValueReader<float> m_RightGripInput = new XRInputValueReader<float>("Grip");

    [SerializeField] bool controllerPresent = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      if (controllerPresent && m_RightGripInput.ReadValue() < 0.5f)
      {
          canvasTransform.gameObject.SetActive(true);
      }
      else
      {
          canvasTransform.gameObject.SetActive(false);
      }

      canvasTransform.position = Vector3.Lerp(canvasTransform.position, canvasTarget.position, 0.3f);

    }

    void OnTriggerEnter(Collider col)
    { 
        if (col.gameObject.CompareTag("GameController"))
        {
            controllerPresent = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("GameController"))
        {
            controllerPresent = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, canvasTarget.position);
    }

}
