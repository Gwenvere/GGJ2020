﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    Vector3 playerpos;
    public float speed;
    public float blockCarryHeight;
    public float despawnDistance;

    Rigidbody rb;

    bool attack = true;
    bool runAway = false;


    void Start()
    {
        //moet nog aangepast worden naar positie speler
        playerpos = new Vector3(0, 0, 0);
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (attack)
        {
            Vector3 direction = playerpos - transform.position;
            direction = new Vector3(direction.x, 0, direction.z);
            direction = direction.normalized;

            rb.velocity = direction * speed * Time.deltaTime;
            this.transform.rotation = Quaternion.LookRotation(direction);
        }
        else if (runAway)
        {
            Vector3 direction = transform.position - playerpos;
            direction = new Vector3(direction.x, 0, direction.z);
            direction = direction.normalized;

            rb.velocity = direction * speed * Time.deltaTime;
            this.transform.rotation = Quaternion.LookRotation(direction);

            if ((this.transform.position - playerpos).magnitude > despawnDistance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "sandblock" && attack)
        {
            attack = false;

            other.collider.tag = "Untagged";
            other.transform.SetParent(this.transform);
            other.transform.localPosition = new Vector3(0, blockCarryHeight, 0);

            Destroy(other.gameObject.GetComponent<Rigidbody>());
            Destroy(other.gameObject.GetComponent<Collider>());

            runAway = true;
        }
    }

}