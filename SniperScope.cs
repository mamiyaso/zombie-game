using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScope : MonoBehaviour
{
    public Animator animator;

    public GameObject scopeOverlay;
    public GameObject weaponCam;
    public Camera mainCam;

    public float scopedFOV = 15f;
    private float normalFOV;

    public float normalSensitivityX = 400f;
    public float normalSensitivityY = 400f;
    public float scopedSensitivityX = 100f;
    public float scopedSensitivityY = 100f;
    private CameraControll2 cameraControl;

    private bool isScoped = false;

    private void Start()
    {
        cameraControl = FindObjectOfType<CameraControll2>();
        if (cameraControl == null)
        {
            Debug.LogError("sc yok");
        }
        normalFOV = mainCam.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);

            if (isScoped)
            {
                StartCoroutine(OnScope());
            }
            else
            {
                OnUnscoped();
            }
        }
    }

    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCam.SetActive(true);


        mainCam.fieldOfView = normalFOV;


        if (cameraControl != null)
        {
            cameraControl.sensX = normalSensitivityX;
            cameraControl.sensY = normalSensitivityY;
        }
    }

    IEnumerator OnScope()
    {
        yield return new WaitForSeconds(.15f);

        scopeOverlay.SetActive(true);
        weaponCam.SetActive(false);

        mainCam.fieldOfView = scopedFOV;

  
        if (cameraControl != null)
        {
            cameraControl.sensX = scopedSensitivityX;
            cameraControl.sensY = scopedSensitivityY;
        }
    }
}