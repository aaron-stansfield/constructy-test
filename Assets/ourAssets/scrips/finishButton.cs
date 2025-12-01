using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using MEC;

public class finishButton : MonoBehaviour, buttonInterface
{

    [SerializeField] GameObject gamemanager;

    private bool buttonReady = true;


    public void interactWithButton()
    {
        if (!buttonReady) return;

        buttonReady = false;

        gamemanager.GetComponent<gamerManage>().finished();

        Timing.CallDelayed(1f, () => buttonReady = true);
    }


}
