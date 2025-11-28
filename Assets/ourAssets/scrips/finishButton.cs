using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using MEC;

public class finishButton : MonoBehaviour, buttonInterface
{

    private bool buttonReady = true;


    public void interactWithButton()
    {
        if (!buttonReady) return;

        buttonReady = false;



        Timing.CallDelayed(1f, () => buttonReady = true);
    }


}
