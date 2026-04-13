using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioDamageIndicatorScript : MonoBehaviour
{
    int damageDone = 0;
    public Image bar;
    public Canvas canvas;
    void Start()
    {
        damageDone = 0;
        Timing.RunCoroutine(damageCoroutine());
    }
    
    private void Update()
    {
        if(Vector3.Distance(transform.position, Player.instance.transform.position) < 2f)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
        
    }

    private IEnumerator<float> damageCoroutine()
    {
        while (damageDone <= 100)
        {
            if (Vector3.Distance(transform.position, Player.instance.transform.position) < 2f)
            {
                print("fucking some ears");
                damageDone += 1;
            }
            bar.fillAmount = damageDone/100f;
            yield return Timing.WaitForSeconds(0.5f);
        }
    }
}
