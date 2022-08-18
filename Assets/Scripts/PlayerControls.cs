using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down left and right")][SerializeField] float controlSpeed = 30f;
    [Tooltip("How far player moves horizontally")][SerializeField] float xrange = 10f;
    [Tooltip("How far player moves vertically")][SerializeField] float yrange = 7f;



    [Header("Screen position based tuning (Rotation)")]
    [SerializeField] float positionPitchFactor = -6f;
    [SerializeField] float positionYawFactor = 2f;


    [Header("Player Control based tuning (Rotation)")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -40f;


    [Header("Laser Gun Array")]
    [SerializeField] GameObject[] lasers;

    float rotationFactor = 1f;


    float xThrow, yThrow;


    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }


    void ProcessRotation()
    {
        float pitchDuetoPosition = transform.localPosition.y * -positionPitchFactor;
        float pitchDueToControlThrow = -yThrow * controlPitchFactor;
        float pitch = pitchDuetoPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * -controlRollFactor;

        // On next line, we get the target rotation but we don't assign it to transform.localRotation
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        // here, we use Quaternion.RotateTowards from the current rotation
        // to the target rotation. NOTE that the rotationFactor has to be small, such as 1, otherwise the rotation will be too fast and will be janky.
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationFactor);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");


        float xOffset = xThrow * Time.deltaTime * controlSpeed * 5;
        float rawxPos = transform.localPosition.x + xOffset;

        float clampedxPos = Mathf.Clamp(rawxPos, -xrange, xrange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawyPos = transform.localPosition.y + yOffset;

        float clampedyPos = Mathf.Clamp(rawyPos, -yrange, yrange);


        transform.localPosition = new Vector3(clampedxPos, clampedyPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var EmissionModule = laser.GetComponent<ParticleSystem>().emission;

            EmissionModule.enabled = isActive;
        }
    }


}
