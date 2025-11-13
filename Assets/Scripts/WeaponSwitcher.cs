using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{

    InputAction fireAction;
    InputAction nextAction;
    InputAction previousAction;

    [SerializeField]
    public GunBase[] guns;
    int activeGun = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwapGun(activeGun);

        fireAction = transform.root.GetComponent<PlayerInput>().actions.FindAction("Attack");
        nextAction = transform.root.GetComponent<PlayerInput>().actions.FindAction("Next");
        previousAction = transform.root.GetComponent<PlayerInput>().actions.FindAction("Previous");


        fireAction.started += FireGun;
        fireAction.performed += ReleaseGun;

        nextAction.performed += NextGun;
        previousAction.performed += PreviousGun;
    }

    bool gunHeld = false;
    public void FireGun(InputAction.CallbackContext context)
    {
        guns[activeGun].FireGun();
        gunHeld = true;
    }
    public void ReleaseGun(InputAction.CallbackContext context)
    {
        guns[activeGun].ReleaseGun();
        gunHeld = false;
    }

    public void NextGun(InputAction.CallbackContext context)
    {
        int index = activeGun + 1;
        if (index >= guns.Length) {
            index = 0;
        }
        SwapGun (index);
    }

    public void PreviousGun(InputAction.CallbackContext context)
    {
        int index = activeGun - 1;
        if (index < 0)
        {
            index = guns.Length - 1;
        }
        SwapGun(index);
    }

    public void SwapGun(int index)
    {
        if (gunHeld)
        {
            guns[activeGun].ReleaseGun();
            gunHeld = false;
        }

        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].gameObject.SetActive(false);
        }

        activeGun = index;
        guns[activeGun].gameObject.SetActive(true);
    }
}
