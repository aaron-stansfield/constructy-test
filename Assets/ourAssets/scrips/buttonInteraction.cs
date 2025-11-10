using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using MEC;

public class buttonInteraction : MonoBehaviour
{

    [SerializeField] GameObject[] whiteboardState;

    [SerializeField] int index;

     private bool buttonReady = true;


    public void interact()
    {
        if (!buttonReady) return;

        buttonReady = false;
        whiteboardState[index].gameObject.SetActive(false);

        if (index < whiteboardState.Length - 1){
            index++;
        }
        else{
            index = 0;
        }

        whiteboardState[index].gameObject.SetActive(true);

        Timing.CallDelayed(1f, () => buttonReady = true);
    }

    
}
