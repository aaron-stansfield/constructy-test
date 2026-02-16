using MEC;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class GunToggleScript : MonoBehaviour
{

    [SerializeField] XRInputValueReader<float> gunButton = new XRInputValueReader<float>("primaryButton");

    [SerializeField] Transform gun;

    [SerializeField] Transform controller;

    private bool canToggleGun = true;


    // Update is called once per frame
    void Update()
    {
        HandleGunToggle();
    }

    private void HandleGunToggle()
    {
        if (gunButton.ReadValue() > 0.75f && canToggleGun)
        {
            canToggleGun = false;
            if (controller.gameObject.activeInHierarchy) controller.GetComponent<ControllerScript>().drop();

            if (gun.gameObject.activeInHierarchy) gun.GetComponent<GunScript>().stopTicketing();
            
            controller.gameObject.SetActive(!controller.gameObject.activeSelf);
            gun.gameObject.SetActive(!gun.gameObject.activeSelf);
            
            Timing.CallDelayed(0.25f, () => canToggleGun = true);
        }
    }

}
