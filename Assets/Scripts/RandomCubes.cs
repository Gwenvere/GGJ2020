using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCubes : MonoBehaviour
{

    public Transform cubePrefab;
    public float cubeTimer = 1;
    private float timer;
    public float distance;
    public float spawnHeight;
    public Slider cubeSlider;

    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CubeTimer: " + cubeTimer + " slider value: " + cubeSlider.value);
        if (timer < 22) cubeTimer = 2;
        else cubeTimer = cubeSlider.value;
     //   cubeSlider.GetComponent<Text>().text = "Cube slider value: " + cubeTimer;
        timer += Time.deltaTime;
        if(timer > cubeTimer)
        {
            timer -= cubeTimer;
            MakeCube();
        }
    }
    void MakeCube()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        float x = distance * Mathf.Cos(angle);
        float z = distance * Mathf.Sin(angle);

        Vector3 cubePos = new Vector3(x, spawnHeight, z);

        Instantiate(cubePrefab, cubePos, Quaternion.identity, this.transform);
    }

    public void setSpawnSpeed()
    {
        cubeTimer = cubeSlider.value;
        cubeSlider.GetComponent<Text>().text = "Cube slider value: " + cubeTimer;
            
    }
}
