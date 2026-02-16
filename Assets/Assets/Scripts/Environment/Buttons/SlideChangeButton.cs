using UnityEngine;
using MEC;

public class SlideChangeButton : MonoBehaviour, ButtonInterface
{

    [SerializeField] GameObject[] whiteboardState;

    [SerializeField] int index;

    private bool buttonReady = true;


    public void interactWithButton()
    {
        if (!buttonReady) return;

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
        Timing.CallDelayed(1f, () => buttonReady = true);
    }


}
