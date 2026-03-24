using UnityEngine;
using MEC;

public class SlideChangeButton : MonoBehaviour, ButtonInterface
{

    [SerializeField] GameObject[] whiteboardState;

    [SerializeField] int index;

    private bool buttonReady = true;

    [SerializeField] private float interactPos;
    private RigidbodyConstraints notPressed = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
        RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
    private RigidbodyConstraints pressed = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY |
        RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;


    public void interactWithButton()
    {
        if (!buttonReady || transform.localPosition.x !< interactPos) return;

        GetComponent<Rigidbody>().constraints = pressed;
        transform.localPosition = new Vector3(interactPos, 0.04f, 6.319f);

        Debug.Log(" ------ Button Interacted With ------ ");
        buttonReady = false;
        whiteboardState[index].gameObject.SetActive(false);

        if (index < whiteboardState.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }

        whiteboardState[index].gameObject.SetActive(true);
        TutorialDoor.instance.slidesWatched = true;

        Timing.CallDelayed(0.5f, () => buttonReady = true);
    }

    public void OnCollisionExit(Collision col)
    {
        GetComponent<Rigidbody>().constraints = notPressed;
    }
}
