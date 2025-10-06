using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using MEC;

public class GunScrip : MonoBehaviour
{
    [SerializeField] XRInputValueReader<float> gunButton = new XRInputValueReader<float>("primaryButton");

    private bool canInput = true;
    private GameObject gun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gun = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(gunButton.ReadValue() > 0.75f && canInput)
        {
            canInput = false;
            Timing.CallDelayed(0.75f, () => { canInput = true; });
            gun.SetActive(!gun.activeSelf);
        }
    }
}
