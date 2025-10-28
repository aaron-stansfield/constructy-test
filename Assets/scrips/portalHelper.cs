using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalHelper : MonoBehaviour
{
    [SerializeField] float OpenSpeed;
    private float state = 0;
    private float radiusOpen;
    private float radiusClosed;

    [SerializeField] bool open;
    [SerializeField] bool close;

    private void Start()
    {
        radiusOpen = GetComponent<ParticleSystem>().shape.radius;
        radiusClosed = 0;
    }

    private void Update()
    {
        if (open)
        {
            open = !open;
            Timing.RunCoroutine(OpenPortal());
        }
        if (close)
        {
            close = !close;
            Timing.RunCoroutine(ClosePortal());
        }
    }

    private IEnumerator<float> OpenPortal()
    {
        GetComponent<ParticleSystem>().Play();
        var sh = GetComponent<ParticleSystem>().shape;
        float pos = 0;
        while (sh.radius <= radiusOpen-0.3f)
        {
            sh.radius = Mathf.Lerp(radiusClosed, radiusOpen, pos+Time.deltaTime*10*OpenSpeed);
            pos += Time.deltaTime * OpenSpeed;
            yield return Timing.WaitForOneFrame;
        }
    }

    private IEnumerator<float> ClosePortal()
    {
        var sh = GetComponent<ParticleSystem>().shape;
        float pos = 0;
        while (sh.radius >= 0.01f) 
        {
            sh.radius = Mathf.Lerp(radiusOpen, radiusClosed, pos + Time.deltaTime *10* OpenSpeed);
            pos += Time.deltaTime * OpenSpeed;
            yield return Timing.WaitForOneFrame;
        }
        GetComponent<ParticleSystem>().Stop();
    }

}
