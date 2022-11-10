using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour {
    private float turnSpeed = 100f;
    private bool effectReleased = false;

    public GameObject menu;
    public GameObject scoreEffect;
    private Text highScoreText;

    private Transform star;
    private Transform play;

    // Use this for initialization
    void Start() {
        PlayerPrefs.SetString("last_scene", SceneManager.GetActiveScene().name);
        play = GameObject.FindWithTag("play").GetComponent<Transform>();
        star = GameObject.FindWithTag("star").GetComponent<Transform>();
        highScoreText = GameObject.FindWithTag("highscoretext").GetComponent<Text>();

        PlayerPrefs.SetFloat("score", 0);

        if (PlayerPrefs.HasKey("highscore"))
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetFloat("highscore").ToString();
        }
        else
        {
            highScoreText.text = "High Score: 0";
        }

        menu.SetActive(false);
        
    }
	
	// Update is called once per frame
	void Update () {
        star.Rotate(0, 0, -1f);
        //play.transform.localScale += new Vector3(0.01F, 0.01f, 0.01f);
        if (transform.rotation.z >= 0)
        {
            if (!effectReleased)
            {
                GameObject instance = Instantiate(scoreEffect, transform.position, transform.rotation);
                menu.SetActive(true);
                effectReleased = true;
            }    
        }
        else
        {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("game");
    }

    public void ToScore()
    {
        PlayerPrefs.SetFloat("score", 0);
        SceneManager.LoadScene("score");
    }

    public void ToCredits()
    {
        PlayerPrefs.SetFloat("score", 0);
        SceneManager.LoadScene("credits");
    }
}
