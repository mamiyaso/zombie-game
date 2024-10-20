using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public static AudioSource centralAudioSource;
    private Gun currentGun;
    void Start()
    {
        centralAudioSource = gameObject.AddComponent<AudioSource>();
        SelectWeapon();
    }


    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (transform.GetChild(selectedWeapon).GetComponent<Gun>() != null)
        {
            currentGun = transform.GetChild(selectedWeapon).GetComponent<Gun>();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsReloading())
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && !IsReloading())
        {
            selectedWeapon = 1;

        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && !IsReloading())
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4 && !IsReloading())
        {
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

        bool IsReloading()
    {
        return currentGun != null && currentGun.IsReloading;
    }

    void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}