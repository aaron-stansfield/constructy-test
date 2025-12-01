using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialDoor : MonoBehaviour
{
    public static tutorialDoor instance = null;
    public GameObject lDoor, rDoor;
    public bool open = false;
    private float timeElapsed = 0f;
    public bool hatOn, slidesWatched = false;

    private void Start()
    {
        if(instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Update()
    {
        if(open || (hatOn&&slidesWatched))
        {
            open = !open;
            Timing.RunCoroutine(animateDoors());
        }
    }

    public IEnumerator<float> animateDoors()
    {
        while (timeElapsed < 1.5f)
        {
            lDoor.transform.localRotation = Quaternion.Euler(new(0, Mathf.Lerp(0f, 150f, timeElapsed), 0));
            rDoor.transform.localRotation = Quaternion.Euler(new(0, Mathf.Lerp(0f, -150f, timeElapsed), 0));

            timeElapsed += Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }
        GetComponent<tutorialDoor>().enabled = false;
    }
}
