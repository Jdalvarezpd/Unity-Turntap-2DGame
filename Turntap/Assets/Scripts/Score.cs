using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private float score;
    private float scoreFill;

    private Text scoreText;
    private Text highScoreText;
    private Image scorebar;

    private Text l1text, l3text, l4text, l5text, l6text, l7text, l8text, l9text, l10text;
    private Text l2text;

    private string lastScene;
    public GameObject play_refresh_btn;
    public GameObject back_btn;
    public GameObject scoreObject;
    public Sprite playbtnsprite;

    public GameObject score_special;
    private float scoreSpecial;

    private float scoreFinal;
    private float scoreTopAnimation;
    private float maxScoreBar = 600;

    private float scoreTopFill = 0;

    // Use this for initialization
    void Start() {
        lastScene = PlayerPrefs.GetString("last_scene");
        Debug.Log("Last Scene: " + lastScene);
        PlayerPrefs.SetString("last_scene", SceneManager.GetActiveScene().name);

        highScoreText = GameObject.FindWithTag("highscoretext").GetComponent<Text>();
        scoreText = GameObject.FindWithTag("scoretext").GetComponent<Text>();
        scorebar = GameObject.FindWithTag("scorebar").GetComponent<Image>();

        l1text = GameObject.FindWithTag("l1").GetComponent<Text>();
        l2text = GameObject.FindWithTag("l2").GetComponent<Text>();
        l3text = GameObject.FindWithTag("l3").GetComponent<Text>();
        l4text = GameObject.FindWithTag("l4").GetComponent<Text>();
        l5text = GameObject.FindWithTag("l5").GetComponent<Text>();
        l6text = GameObject.FindWithTag("l6").GetComponent<Text>();
        l7text = GameObject.FindWithTag("l7").GetComponent<Text>();
        l8text = GameObject.FindWithTag("l8").GetComponent<Text>();
        l9text = GameObject.FindWithTag("l9").GetComponent<Text>();
        l10text = GameObject.FindWithTag("l10").GetComponent<Text>();

        if (PlayerPrefs.HasKey("score")) {
            score = PlayerPrefs.GetFloat("score");
            //score = 100f;
        }
        else
        {
            score = 0;
        }

        //score = 400;

        //CAMBIO LA IMAGEN DEL BOTON DE PLAY POR SI VIENE DE LA SCENA 1
        if(lastScene == "first")
        {
            play_refresh_btn.GetComponentInChildren<Image>().sprite = playbtnsprite;
            scoreObject.SetActive(false);
        }

        if(lastScene == "game")
        {
            score_special.SetActive(true);

            if (PlayerPrefs.HasKey("score_special"))
            {
                scoreSpecial = PlayerPrefs.GetFloat("score_special");
                score_special.GetComponent<Text>().text = "+ " + scoreSpecial.ToString();

                scoreFinal = score + scoreSpecial;
                Debug.Log("Final Score: " + scoreFinal);
            }
        }

        //scoreFinal = 10;

        if (PlayerPrefs.HasKey("highscore"))
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetFloat("highscore").ToString();
            float hs = PlayerPrefs.GetFloat("highscore");
            if (hs >= 10)
            {
                l1text.text = "BEGINNER";
                l1text.color = new Color(0.3f, 0.3f, 0.3f);
            }

            if (hs >= 30)
            {
                l2text.text = "AMATEUR";
                l2text.color = new Color(0.3f, 0.3f, 0.3f);
            }

            if (hs >= 50)
            {
                l3text.text = "CASUAL PLAYER";
                l3text.color = new Color(0.3f, 0.3f, 0.3f);
            }

            if (hs >= 100)
            {
                l4text.text = "INTERMEDIATE PLAYER";
                l4text.color = new Color(0.3f, 0.3f, 0.3f);
            }

            if (hs >= 170)
            {
                l5text.text = "ADVANCE PLAYER";
                l5text.color = new Color(0.3f, 0.3f, 0.3f);
            }

            if (hs >= 250)
            {
                l6text.text = "PRO PLAYER";
                l6text.color = new Color(0.3f, 0.3f, 0.3f);
            }

            if (hs >= 300)
            {
                l7text.text = "MASTER";
                l7text.color = new Color(0.3f, 0.3f, 0.3f);
            }

            if (hs >= 400)
            {
                l8text.text = "BOSS";
                l8text.color = new Color(0.3f, 0.3f, 0.3f);
            }

            if (hs >= 500)
            {
                l9text.text = "INSANE";
                l9text.color = new Color(0.3f, 0.3f, 0.3f);
            }

            //TOP ES 400
            if (hs >= maxScoreBar)
            {
                l10text.text = "PSYCHOPATH";
                l10text.color = new Color(0.3f, 0.3f, 0.3f);
            }
        }


        if (scoreFinal >= 10)
        {
            scoreTopFill = 10;

        }

        if (scoreFinal >= 30)
        {
            scoreTopFill = 20;

        }

        if (scoreFinal >= 50)
        {
            scoreTopFill = 30;

        }

        if (scoreFinal >= 100)
        {
            scoreTopFill = 40;

        }

        if (scoreFinal >= 170)
        {
            scoreTopFill = 50;

        }

        if (scoreFinal >= 250)
        {
            scoreTopFill = 60;

        }

        if (scoreFinal >= 300)
        {
            scoreTopFill = 70;

        }

        if (scoreFinal >= 400)
        {
            scoreTopFill = 80;

        }

        if (scoreFinal >= 500)
        {
            scoreTopFill = 90;

        }

        if (scoreFinal >= 600)
        {
            scoreTopFill = 100;
        }

        //score = 0.3f;
    }
	
	// Update is called once per frame
	void Update () {
        /*if (scoreFill < scoreFinal/maxScoreBar)
        {    
            scoreFill += 0.01f;
        }*/

        if (scoreFill < scoreTopFill/100)
        {
            scoreFill += 0.01f;
        }

        scorebar.fillAmount = scoreFill;

        if (scoreTopAnimation < scoreFinal)
        {
            if (scoreTopAnimation < scoreFinal - 50)
            {
                scoreTopAnimation+=5;
            }
            else
            {
                scoreTopAnimation++;
            }
            
            scoreText.text = "Score: " + scoreTopAnimation.ToString();
        }
        
    }

    public void Play()
    {
        SceneManager.LoadScene("game");
    }

    public void Back()
    {
        SceneManager.LoadScene("first");
    }
}
