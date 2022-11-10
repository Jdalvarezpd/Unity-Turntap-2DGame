using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rotate : MonoBehaviour {
    //Tiempo para la salida antes de pasar a la pantalla de score
    int timeToGo = 70;

    private float score;
    private float scoreTens;
    private float scoreSpecial;
    private int scoreLevels = 0;

    //VARIABLES PARA CONTAR LAS VUELTAS
    private float rounds;
    private float roundsRate = 30f;
    private float roundsTime;

    //VARIABLES PARA HACER EL MULTIPLICADOR DE PUNTOS
    private bool inARow = false;
    private float scoreMultiplier;
    private GameObject rowpoint;
    private float rowPointDistancePairing;

    private Color[] colors1 = new Color[10];
    private Color[] colors2 = new Color[10];
    private Color[] colorBackground = new Color[10];

    private int randomDirection;
    private float secondSpeed = 1.2f;
    private float randomSpeed = 1;
    private float thirdSpeed = 1.8f;
    private float secondRandomSpeed = 1.8f;

    private float turnSpeed = 130f;
    private float sliceSpeed = 50f;
    //private float turnSpeed = 50f;
    private GameObject slice;
    private GameObject background;
    private GameObject point1, point2;
    private float distancePairing;
    private float minDistancePairing = 0.2f;

    public Sprite[] shape1;
    public Sprite[] shape2;
    //private GameObject gameOverPanel;
    private Animator gameOverAnim;
    private bool game_over;

    private Text scoreText;
    private Text finalScoreText;
    private Text highScoreText;

    public GameObject scoreEffect;
    public GameObject failEffect;

    public GameObject scoreMultiplierText;
    public GameObject specialScoreText;

    //int indexcolors = 0;

    public GameObject scoreTextObject;


    void Start()
    {
        PlayerPrefs.SetString("last_scene", SceneManager.GetActiveScene().name);
        FloatingTextController.Initialize();

        colors1[0] = new Color32(0, 146, 255, 255);
        colors2[0] = new Color32(255, 190, 0, 255);
        colorBackground[0] = new Color32(192, 243, 255, 255);

        colors1[1] = new Color32(254, 86, 121, 255);
        colors2[1] = new Color32(0, 174, 168, 255);
        colorBackground[1] = new Color32(254, 255, 247, 255);

        colors1[2] = new Color32(49, 95, 118, 255);
        colors2[2] = new Color32(249, 107, 95, 255);
        colorBackground[2] = new Color32(255, 210, 151, 255);

        colors1[3] = new Color32(194, 224, 4, 255);
        colors2[3] = new Color32(186, 186, 186, 255);
        colorBackground[3] = new Color32(93, 154, 181, 255);

        colors1[4] = new Color32(221, 100, 100, 255);
        colors2[4] = new Color32(37, 71, 70, 255);
        colorBackground[4] = new Color32(219, 232, 163, 255);

        colors1[5] = new Color32(247, 233, 204, 255);
        colors2[5] = new Color32(244, 102, 52, 255);
        colorBackground[5] = new Color32(0, 122, 143, 255);

        colors1[6] = new Color32(44, 43, 49, 255);
        colors2[6] = new Color32(57, 193, 169, 255);
        colorBackground[6] = new Color32(255, 126, 123, 255);

        colors1[7] = new Color32(244, 102, 52, 255);
        colors2[7] = new Color32(88, 214, 141, 255);
        colorBackground[7] = new Color32(89, 83, 87, 255);

        colors1[8] = new Color32(255, 255, 255, 255);
        colors2[8] = new Color32(40, 40, 40, 255);
        colorBackground[8] = new Color32(134, 160, 93, 255);

        colors1[9] = new Color32(255, 72, 72, 255);
        colors2[9] = new Color32(226, 255, 13, 255);
        colorBackground[9] = new Color32(22, 18, 19, 255);

        slice = GameObject.FindWithTag("slice");
        background = GameObject.FindWithTag("background");
        point1 = GameObject.FindWithTag("p1");
        point2 = GameObject.FindWithTag("p2");
        rowpoint = GameObject.FindWithTag("rowpoint");
        scoreText = GameObject.FindWithTag("scoretext").GetComponent<Text>();
        finalScoreText = GameObject.FindWithTag("scoretextfinal").GetComponent<Text>();
        //commentText = GameObject.FindWithTag("comments").GetComponent<Text>();
        highScoreText = GameObject.FindWithTag("highscoretext").GetComponent<Text>();
        //gameOverPanel = GameObject.Find("Game Over Panel Holder");
        //gameOverAnim = gameOverPanel.GetComponent<Animator>();
        //gameOverPanel.SetActive(false);

        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", 0);
        }
    }

    void Update()
    {
        if(game_over){
            timeToGo--;
            if (timeToGo <= 0)
            {
                SceneManager.LoadScene("score");
            }
        }
        distancePairing = Vector2.Distance(point1.transform.position, point2.transform.position);
        rowPointDistancePairing = Vector2.Distance(rowpoint.transform.position, point2.transform.position);
        roundsTime--;

        if (scoreLevels <= 9) { 
            this.GetComponent<SpriteRenderer>().color = colors1[scoreLevels];
            slice.GetComponent<SpriteRenderer>().color = colors2[scoreLevels];
            background.GetComponent<SpriteRenderer>().color = colorBackground[scoreLevels];
        }
        //CONTADOR DE VUELTAS
        /*if(distancePairing < 0.5)
        {
            rounds++;
            Debug.Log(rounds);
        }*/
        //Debug.Log(distancePairing);

        //NIVELES DEL JUEGO
        if (distancePairing <= minDistancePairing && roundsTime<=0)
        {
            inARow = false;
            rounds++;
            roundsTime = roundsRate;
        }

        if (rowPointDistancePairing <= minDistancePairing && roundsTime <= 0 && inARow == false)
        {
            /*if (scoreMultiplier >= 2)
            {
                score = score + scoreMultiplier;
            }*/
            scoreMultiplier = 0;
        }
        //Debug.Log("inARow: " + inARow);
        //Debug.Log("scoreMultiplier: " + scoreMultiplier);
        //Debug.Log("puntaje especial" + scoreSpecial);

        if (scoreLevels < 1)//LEVEL1
        {
           transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
        else if(scoreLevels>=1 && scoreLevels < 2)//LEVEL2 levels=1
        {
            transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
            scoreText.color = Color.black;
        }
        else if (scoreLevels > 1 && scoreLevels <= 2)//LEVEL3
        {
            scoreText.color = Color.white;
            if (randomDirection < 5) { 
                transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
            }
        }
        else if (scoreLevels > 2 && scoreLevels <= 3)//LEVEL4
        {
            if (randomDirection < 5)
            {
                transform.Rotate(0, 0, turnSpeed * Time.deltaTime * secondSpeed);
            }
            else
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime * secondSpeed);
            }
        }
        else if (scoreLevels > 3 && scoreLevels <= 4)//LEVEL5
        {
            scoreText.color = Color.black;
            if (randomDirection < 5)
            {
                transform.Rotate(0, 0, turnSpeed * Time.deltaTime * randomSpeed);
            }
            else
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime * randomSpeed);
            }
        }
        else if (scoreLevels > 4 && scoreLevels <= 5)//LEVEL6
        {
            if (randomDirection < 5)
            {
                transform.Rotate(0, 0, turnSpeed * Time.deltaTime * secondSpeed);
                slice.transform.Rotate(0, 0, -sliceSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime * secondSpeed);
                slice.transform.Rotate(0, 0, sliceSpeed * Time.deltaTime);
            }
        }
        else if (scoreLevels > 5 && scoreLevels <= 6)//LEVEL7
        {
            if (randomDirection < 5)
            {
                transform.Rotate(0, 0, turnSpeed * Time.deltaTime * randomSpeed);
                slice.transform.Rotate(0, 0, -sliceSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime * randomSpeed);
                slice.transform.Rotate(0, 0, sliceSpeed * Time.deltaTime);
            }
        }
        else if (scoreLevels > 6 && scoreLevels <= 7)//LEVEL8
        {
            if (randomDirection < 5)
            {
                transform.Rotate(0, 0, turnSpeed * Time.deltaTime * thirdSpeed);
                slice.transform.Rotate(0, 0, -sliceSpeed * Time.deltaTime * thirdSpeed);
            }
            else
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime * thirdSpeed);
                slice.transform.Rotate(0, 0, sliceSpeed * Time.deltaTime * thirdSpeed);
            }
        }
        else if (scoreLevels > 7 && scoreLevels <= 8)//LEVEL9
        {
            if (randomDirection < 5)
            {
                transform.Rotate(0, 0, turnSpeed * Time.deltaTime * randomSpeed);
                slice.transform.Rotate(0, 0, -sliceSpeed * Time.deltaTime * secondRandomSpeed);
            }
            else
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime * randomSpeed);
                slice.transform.Rotate(0, 0, sliceSpeed * Time.deltaTime * secondRandomSpeed);
            }
        }
        else if (scoreLevels > 8)//LEVEL10
        {
            scoreText.color = Color.white;
            if (randomDirection < 5)
            {
                transform.Rotate(0, 0, turnSpeed * Time.deltaTime * secondRandomSpeed);
                slice.transform.Rotate(0, 0, -sliceSpeed * Time.deltaTime * secondRandomSpeed);
            }
            else
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime * secondRandomSpeed);
                slice.transform.Rotate(0, 0, sliceSpeed * Time.deltaTime * secondRandomSpeed);
            }
        }

        if (Input.GetButtonDown("Fire1")){

            if (distancePairing <= minDistancePairing)
            {
                if (!game_over) {
                    inARow = true;
                    randomDirection = Random.Range(1, 10);
                    randomSpeed = Random.Range(1.3f, 2.2f);
                    secondRandomSpeed = Random.Range(1.5f, 2.5f);
                    //Debug.Log("Random Speed: " + randomSpeed);

                    score++;
                    scoreMultiplier++;

                    //INSTRUCCION PARA EL TEXTO DE SUMA DE PUNTOS Y SUMA EN LA VARIABLE DE PUNTOS ESPECIALES
                    if (scoreMultiplier > 2 && scoreMultiplier <= 7)
                    {
                        FloatingTextController.CreateFloatingText("1", transform);
                        scoreSpecial++;
                        scoreTextObject.GetComponent<Animator>().SetTrigger("bounce_trigger");
                    }

                    if (scoreMultiplier > 7 && scoreMultiplier <= 17)
                    {
                        FloatingTextController.CreateFloatingText("2", transform);
                        scoreSpecial += 2;
                        scoreTextObject.GetComponent<Animator>().SetTrigger("bounce_trigger");
                    }

                    if (scoreMultiplier > 17 && scoreMultiplier <= 32)
                    {
                        FloatingTextController.CreateFloatingText("5", transform);
                        scoreSpecial += 5;
                        scoreTextObject.GetComponent<Animator>().SetTrigger("bounce_trigger");
                    }

                    if (scoreMultiplier > 32)
                    {
                        FloatingTextController.CreateFloatingText("10", transform);
                        scoreSpecial += 10;
                        scoreTextObject.GetComponent<Animator>().SetTrigger("bounce_trigger");
                    }

                    //specialScoreText.GetComponent<Text>().text = "+ " + scoreSpecial.ToString();

                    if (scoreTens <= 9)
                    {
                        scoreTens++;
                    }
                    if (scoreTens >= 10)
                    {
                        scoreTens = 0;
                        scoreLevels++;
                        //Debug.Log(scoreLevels);
                    }
                    slice.transform.Rotate(0, 0, Random.Range(-175, 175));
                    int index = Random.Range(1, shape1.Length);
                    this.GetComponent<SpriteRenderer>().sprite = shape1[index];
                    slice.GetComponent<SpriteRenderer>().sprite = shape2[index];
                    GameObject instance = Instantiate(scoreEffect, transform.position, transform.rotation);
                    instance.gameObject.GetComponent<SpriteRenderer>().color = this.GetComponent<SpriteRenderer>().color;
                    Destroy(instance, 1f);

                    //TEXTO DEL SCORE + SCORESPECIAL EN LA PANTALLA
                    float scoreFinal = score + scoreSpecial;
                    scoreText.text = scoreFinal.ToString();

                    //scoreText.text = score.ToString();
                }
            }
            else
            {
                if (!game_over)
                {
                    gameOver();
                }
            }
        }

    }

    public void gameOver()
    {
        game_over = true;
        PlayerPrefs.SetFloat("score", score);
        PlayerPrefs.SetFloat("score_special", scoreSpecial);
        float score_final = score + scoreSpecial;

        if (score_final > PlayerPrefs.GetFloat("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", score_final);
        }

        highScoreText.text = "High Score: " + PlayerPrefs.GetFloat("highscore").ToString();
        scoreText.GetComponent<Text>().enabled = false;
        //gameOverPanel.SetActive(true);
        //finalScoreText.text = score.ToString();
        //gameOverAnim.Play("fade");
        GameObject instance = Instantiate(failEffect, transform.position, transform.rotation);
        Destroy(instance, 1f);
        slice.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void replay()
    {
        SceneManager.LoadScene("game");
    }

    public void Over()
    {
        gameOver();
    }

}
