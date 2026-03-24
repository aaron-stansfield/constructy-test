using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour, ButtonInterface
{
    [SerializeField] private bool buttonReady = false;

    [SerializeField] private float interactPos;
    private RigidbodyConstraints notPressed = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
        RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
    private RigidbodyConstraints pressed = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY |
        RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;

    public void Awake()
    {
        Timing.CallDelayed(1.5f, () => buttonReady = true);
    }

    public void interactWithButton()
    {
        if (!buttonReady || transform.localPosition.y !< interactPos) return;

        GetComponent<Rigidbody>().constraints = pressed;
        transform.localPosition = new Vector3(transform.localPosition.x, interactPos, transform.localPosition.z);

        Debug.Log(" ------ Button Interacted With ------ ");
        buttonReady = false;

        Timing.CallDelayed(1f, () => buttonReady = true);
        Timing.CallDelayed(0.25f, () => SceneManager.LoadScene("mainScene 1"));
    }

    public void OnCollisionExit(Collision col)
    {
        GetComponent<Rigidbody>().constraints = notPressed;
    }
}
