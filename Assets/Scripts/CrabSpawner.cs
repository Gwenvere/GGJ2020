using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrabSpawner : MonoBehaviour
{
    public float crabsPerSecond;
    public GameObject crabPrefab;
    public float distance;
    public float spawnHeight;
    public Slider crabSlider;
    public float minDistance;
    public float maxDistance;


    float previousSpawnTime = 0;
    float timePerCrab;
    float timer;

    void Start()
    {
        timer = 0;
        timePerCrab = 1 / crabsPerSecond;
    }

    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log("crabsPerSecond: " + crabsPerSecond + " slider value: " + crabSlider.value);
        if (timer < 22) crabsPerSecond = 0.2f;
        else crabsPerSecond = crabSlider.value;
        timePerCrab = 1 / crabsPerSecond;
        //crabSlider.GetComponent<Text>().text = "Crab slider value: " + crabsPerSecond;
        if (Time.time > previousSpawnTime + timePerCrab)
        {
            spawnCrab();
            previousSpawnTime = Time.time;
        }
    }

    void spawnCrab()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        float distance = Random.Range(minDistance, maxDistance);
        float x = distance * Mathf.Cos(angle);
        float z = distance * Mathf.Sin(angle);

        Vector3 crabPos = new Vector3(x, spawnHeight, z);

        GameObject crab = Instantiate(crabPrefab, crabPos, Quaternion.identity, this.transform);
        if (timer > 10 && timer <= 15) crab.GetComponent<Crab>().setSpeed(70);
        else if (timer > 15 && timer <= 20) crab.GetComponent<Crab>().setSpeed(80);
        else if (timer > 20 && timer <= 25) crab.GetComponent<Crab>().setSpeed(90);
        else if (timer > 25 && timer <= 30) crab.GetComponent<Crab>().setSpeed(100);
        else if (timer > 30 && timer <=35) crab.GetComponent<Crab>().setSpeed(110);
        else if (timer > 35) crab.GetComponent<Crab>().setSpeed(120);
    }

    public void setSpawnSpeed()
    {
        crabsPerSecond = crabSlider.value;
        crabSlider.GetComponent<Text>().text = "Crab slider value: " + crabsPerSecond;
        
    }

}
