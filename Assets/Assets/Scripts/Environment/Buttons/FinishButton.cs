using UnityEngine;
using MEC;

public class FinishButton : MonoBehaviour, ButtonInterface
{

    [SerializeField] GameObject gamemanager;
    [SerializeField] GameObject restartButton;

    [SerializeField] private bool buttonReady = true;


    public void interactWithButton()
    {
        if (!buttonReady) return;

        buttonReady = false;

        gamemanager.GetComponent<GameManager>().finished();

        restartButton.SetActive(true);
        this.gameObject.transform.parent.gameObject.SetActive(false);

        Timing.CallDelayed(1f, () => buttonReady = true);
    }
}