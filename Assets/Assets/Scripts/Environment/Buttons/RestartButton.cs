using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour, ButtonInterface
{
    [SerializeField] private bool buttonReady = false;

    public void Awake()
    {
        Timing.CallDelayed(1.5f, () => buttonReady = true);
    }

    public void interactWithButton()
    {
        if (!buttonReady) return;

        buttonReady = false;

        SceneManager.LoadScene("mainScene");

        Timing.CallDelayed(1f, () => buttonReady = true);
    }
}
