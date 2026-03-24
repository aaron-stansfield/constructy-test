using UnityEngine;
using MEC;

public class FinishButton : MonoBehaviour, ButtonInterface
{

    [SerializeField] GameObject gamemanager;
    [SerializeField] GameObject restartButton;
    //WhiteboardReview whiteboardReview;

    [SerializeField] private bool buttonReady = true;


    public void interactWithButton()
    {
        if (!buttonReady) return;

        buttonReady = false;

        gamemanager.GetComponent<GameManager>().finished();

        //if whiteboard review != null
        //      startReview

        restartButton.SetActive(true);
        this.gameObject.transform.parent.gameObject.SetActive(false);

        Timing.CallDelayed(1f, () => buttonReady = true);
    }
}