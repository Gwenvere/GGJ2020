using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas canvas;
    public Canvas cameraCanvas;
    float timer;
    public Text timerText;
    bool timerCounting;
    float highscore;
    public GameObject GOtext;
    bool inMenu;
    float audio1Volume = 1.0f;
    public AudioSource gameOver;
    public AudioSource music;
    bool fade;

    public GameObject rCubes;

    public GameObject firstNumberObj;
    public GameObject secondNumberObj;
    public GameObject thirdNumberObj;

    public GameObject n0;
    public GameObject n1;
    public GameObject n2;
    public GameObject n3;
    public GameObject n4;
    public GameObject n5;
    public GameObject n6;
    public GameObject n7;
    public GameObject n8;
    public GameObject n9;

    GameObject secondGame;
    GameObject firstGame;
    GameObject thirdGame;

    public GameObject bucket;
    public GameObject introObj;
    // Start is called before the first frame update


    float introTimer;
    bool introPlaying;
    public float introDuration = 10f;
    void Start()
    {
        rCubes.GetComponent<RandomCubes>().enabled = false;
        cameraCanvas.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        timer = 0;
        timerCounting = false;
        highscore = 0;
        fade = false;
        this.GetComponent<CrabSpawner>().enabled = false;
        introTimer = 0;
        introPlaying = true;
        intro();
    }

    // Update is called once per frame
    void Update()
    {
        if (introPlaying)
        {
            introTimer += Time.deltaTime;
            if (introTimer > introDuration)
            {
                introObj.SetActive(false);
                introPlaying = false;
                this.GetComponent<CrabSpawner>().enabled = true;
                timerCounting = true;
                rCubes.GetComponent<RandomCubes>().enabled = true;
            }

        }
        else if (timerCounting)
        {
            timer += Time.deltaTime;
            timerText.gameObject.GetComponent<Text>().text = "You've lasted " + Mathf.Round(timer).ToString() + " seconds.";


            setScore(timer);
            
            if (timer > 35f)
            {
                GetComponent<BirdSpawner>().enabled = true;
            } 
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
            {
                respawnBucket();
            }  
        }


        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (inMenu) canvas.gameObject.SetActive(false);
            else canvas.gameObject.SetActive(true);
            inMenu = !inMenu;
        }
   
        if (fade)
        {
            fadeOut(music);
        }

    }

    public void respawnBucket()
    {
        bucket.transform.position = new Vector3(0.1f, 1f, 0);

    }
    public void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        timerCounting = true;


    }

    public void GameOver()
    {
        gameOver.Play();
        fade = true;
        timerCounting = false;
        if (timer > highscore)
        {
            highscore = timer;
            timerText.gameObject.GetComponent<Text>().text = "You have the new highscore of " + Mathf.Round(timer).ToString() + " seconds.";
        }
        else
        {
            timerText.gameObject.GetComponent<Text>().text = "You made it to " + Mathf.Round(timer).ToString() + " seconds. \n High score this session: " + Mathf.Round(highscore).ToString();
        }

        GOtext.SetActive(true);
        canvas.transform.Find("SliderCrab").gameObject.SetActive(false);
        canvas.transform.Find("SliderCubes").gameObject.SetActive(false);

        //cameraCanvas.gameObject.SetActive(true);
        //cameraCanvas.transform.Find("GameOver").gameObject.SetActive(true);
    }

    void fadeOut(AudioSource audio)
    {
        if (audio1Volume > 0.1)
        {
            audio1Volume -= 0.5f * Time.deltaTime;
            audio.volume = audio1Volume;
        }
    }

    void setScore(float score)
    {
        float firstNb = Mathf.Floor(timer / 100);
        float secondNb = Mathf.Floor(timer / 10f);
        float thirdNb = Mathf.Round(timer % 10f);

        Destroy(firstGame);
        Destroy(secondGame);
        Destroy(thirdGame);
        if (firstNb != 0)
        {
            switch (firstNb)
            {
                case 0:
                    firstGame = Instantiate(n0, firstNumberObj.transform);
                    break;
                case 1:
                    firstGame = Instantiate(n1, firstNumberObj.transform);
                    break;
                case 2:
                    firstGame = Instantiate(n2, firstNumberObj.transform);
                    break;
                case 3:
                    firstGame = Instantiate(n3, firstNumberObj.transform);
                    break;
                case 4:
                    firstGame = Instantiate(n4, firstNumberObj.transform);
                    break;
                case 5:
                    firstGame = Instantiate(n5, firstNumberObj.transform);
                    break;
                case 6:
                    firstGame = Instantiate(n6, firstNumberObj.transform);
                    break;
                case 7:
                    firstGame = Instantiate(n7, firstNumberObj.transform);
                    break;
                case 8:
                    firstGame = Instantiate(n8, firstNumberObj.transform);
                    break;
                case 9:
                    firstGame = Instantiate(n9, firstNumberObj.transform);
                    break;
                default:
                    break;

            }
        }
        if (secondNb != 0)
        {
            switch (secondNb)
            {
                case 0:
                    secondGame = Instantiate(n0, secondNumberObj.transform);
                    break;
                case 1:
                    secondGame = Instantiate(n1, secondNumberObj.transform);
                    break;
                case 2:
                    secondGame = Instantiate(n2, secondNumberObj.transform);
                    break;
                case 3:
                    secondGame = Instantiate(n3, secondNumberObj.transform);
                    break;
                case 4:
                    secondGame = Instantiate(n4, secondNumberObj.transform);
                    break;
                case 5:
                    secondGame = Instantiate(n5, secondNumberObj.transform);
                    break;
                case 6:
                    secondGame = Instantiate(n6, secondNumberObj.transform);
                    break;
                case 7:
                    secondGame = Instantiate(n7, secondNumberObj.transform);
                    break;
                case 8:
                    secondGame = Instantiate(n8, secondNumberObj.transform);
                    break;
                case 9:
                    secondGame = Instantiate(n9, secondNumberObj.transform);
                    break;
                default:
                    break;

            }
        }
        switch (thirdNb)
        {
            case 0:
                thirdGame = Instantiate(n0, thirdNumberObj.transform);
                break;
            case 1:
                thirdGame = Instantiate(n1, thirdNumberObj.transform);
                break;
            case 2:
                thirdGame = Instantiate(n2, thirdNumberObj.transform);
                break;
            case 3:
                thirdGame = Instantiate(n3, thirdNumberObj.transform);
                break;
            case 4:
                thirdGame = Instantiate(n4, thirdNumberObj.transform);
                break;
            case 5:
                thirdGame = Instantiate(n5, thirdNumberObj.transform);
                break;
            case 6:
                thirdGame = Instantiate(n6, thirdNumberObj.transform);
                break;
            case 7:
                thirdGame = Instantiate(n7, thirdNumberObj.transform);
                break;
            case 8:
                thirdGame = Instantiate(n8, thirdNumberObj.transform);
                break;
            case 9:
                thirdGame = Instantiate(n9, thirdNumberObj.transform);
                break;
            default:
                break;

        }
    


}

void intro()
    {
        //play animation
    }
}

