using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using MEC;

public class finishButton : MonoBehaviour, buttonInterface
{

    [SerializeField] GameObject gamemanager;
    [SerializeField] GameObject restartButton;

    [SerializeField] private bool buttonReady = true;


    public void interactWithButton()
    {
        if (!buttonReady) return;

        buttonReady = false;

        gamemanager.GetComponent<gamerManage>().finished();

        restartButton.SetActive(true);
        this.gameObject.transform.parent.gameObject.SetActive(false);

        Timing.CallDelayed(1f, () => buttonReady = true);
    }


}
