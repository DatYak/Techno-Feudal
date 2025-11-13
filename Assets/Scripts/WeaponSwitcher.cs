using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{

    InputAction fireAction;
    InputAction wep1Action;
    InputAction wep2Action;
    InputAction wep3Action;
    InputAction wep4Action;

    [SerializeField]
    public GunBase[] guns;
    int activeGun = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwapGun(activeGun);

        fireAction = transform.root.GetComponent<PlayerInput>().actions.FindAction("Attack");
        wep1Action = transform.root.GetComponent<PlayerInput>().actions.FindAction("Weapon 1");
        wep2Action = transform.root.GetComponent<PlayerInput>().actions.FindAction("Weapon 2");
        wep3Action = transform.root.GetComponent<PlayerInput>().actions.FindAction("Weapon 3");
        wep4Action = transform.root.GetComponent<PlayerInput>().actions.FindAction("Weapon 4");

        wep1Action.performed += SwapWeapon1;
        wep2Action.performed += SwapWeapon2;
        wep3Action.performed += SwapWeapon3;
        wep4Action.performed += SwapWeapon4;

    }

    private void Update()
    {
        if (fireAction.WasPressedThisFrame())
        {
            FireGun();
        }

        if (fireAction.WasReleasedThisFrame())
        {
            ReleaseGun();
        }
    }


    bool gunHeld = false;
    public void FireGun()
    {
        guns[activeGun].FireGun();
        gunHeld = true;
    }
    public void ReleaseGun()
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

    private void SwapWeapon1(InputAction.CallbackContext context)
    {
        SwapGun(0);
    }
    private void SwapWeapon2(InputAction.CallbackContext context)
    {
        SwapGun(1);
    }
    private void SwapWeapon3(InputAction.CallbackContext context)
    {
        SwapGun(2);
    }
    private void SwapWeapon4(InputAction.CallbackContext context)
    {
        SwapGun(3);
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
