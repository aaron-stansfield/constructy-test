using UnityEngine;
using MEC;

public class FinishButton : MonoBehaviour, ButtonInterface
{

    [SerializeField] GameObject gamemanager;
    [SerializeField] GameObject restartButton;
    //WhiteboardReview whiteboardReview;

    [SerializeField] private bool buttonReady = true;

    [SerializeField] private float interactPos;
    private RigidbodyConstraints notPressed =RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
        RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
    private RigidbodyConstraints pressed = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY |
        RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;

    public void interactWithButton()
    {
        if (!buttonReady || transform.localPosition.y !< interactPos) return;

        GetComponent<Rigidbody>().constraints = pressed;
        transform.localPosition = new Vector3(transform.localPosition.x, interactPos, transform.localPosition.z);

        Debug.Log(" ------ Button Interacted With ------ ");
        buttonReady = false;

        GameManager.Instance.finished();

        //if whiteboard review != null
        //      startReview

        restartButton.SetActive(true);
        this.gameObject.transform.parent.gameObject.SetActive(false);

        Timing.CallDelayed(0.5f, () => buttonReady = true);
        Timing.CallDelayed(0.5f, () => GetComponent<Rigidbody>().constraints = notPressed);
    }

    public void OnCollisionExit(Collision col)
    {
        GetComponent<Rigidbody>().constraints = notPressed;
    }
}