using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vogel : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float explosionRadius;
    public GameObject explosionPSPrefab;
    public GameObject impactAudioPrefab;

    float speed;

    Vector3 playerpos;
    Rigidbody rb;

    void Start()
    {
        //moet nog aangepast worden naar positie speler
        playerpos = new Vector3(0, -0.5f, 0);
        rb = this.GetComponent<Rigidbody>();

        this.transform.LookAt(playerpos);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        this.transform.LookAt(playerpos);
        rb.velocity = (playerpos - this.transform.position).normalized * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, explosionRadius);

        foreach (Collider col in hitColliders)
        {
            if (col.tag == "sandblock")
            {
                col.gameObject.GetComponent<SandCube>().destroyCube();
            }
        }

        Instantiate(impactAudioPrefab, this.transform.position, Quaternion.identity);
        GameObject ps = Instantiate(explosionPSPrefab, this.transform.position, Quaternion.identity);
        ps.GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject);
    }
}
