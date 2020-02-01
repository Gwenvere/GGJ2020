﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bucket : MonoBehaviour
{
    public Transform cubePrefab;
    bool buttonDown = false;
    int bucketCounter;
    public AudioSource audio;
    public AudioClip fill;
    public AudioClip empty;


    // Start is called before the first frame update
    void Start()
    {
        bucketCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 1)
        {
            if (bucketCounter > 0 && transform.Find("BucketFull").gameObject.activeSelf && !buttonDown)
            {
                HapticEvent(0.5f);
                buttonDown = true;
                bucketCounter -= 1;
                Transform cube = Instantiate(cubePrefab, this.transform.position, Quaternion.identity);
                audio.clip = empty;
                audio.Play();
                // cube.gameObject.GetComponent<SandCube>().hasNotBeenGrabbed = false;
                if (bucketCounter == 0)
                {
                    transform.Find("BucketEmpty").gameObject.SetActive(true);
                    transform.Find("BucketFull").gameObject.SetActive(false);
                }
            }
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0)
        {
            buttonDown = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "sandblock")
        {
            if (collision.gameObject.GetComponent<SandCube>().hasNotBeenGrabbed)
            {
                if (bucketCounter == 0)
                {
                    transform.Find("BucketEmpty").gameObject.SetActive(false);
                    transform.Find("BucketFull").gameObject.SetActive(true);
                }
                if (bucketCounter < 5)
                {
                    bucketCounter += 1;
                    HapticEvent(0.5f);
                    Destroy(collision.gameObject);
                    audio.clip = fill;
                    audio.Play();
                }

            }

        }
        else if (collision.gameObject.tag == "vogel") HapticEvent(1f);
    }

    public void HapticEvent(float haptStrength)
    {
        // 1.
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        InputDevice activeController = leftController;
        // 2.
        HapticCapabilities capabilities;
        OVRGrabber hand = this.gameObject.GetComponent<OVRGrabbable>().grabbedBy;
        if (hand.name == "LeftHandAnchor") activeController = leftController;
        else if (hand.name == "RightHandAnchor") activeController = rightController;
        if (activeController.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                uint channel = 0;
                float amplitude = haptStrength;
                float duration = 0.1f;
                // 3.
                activeController.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }
}
