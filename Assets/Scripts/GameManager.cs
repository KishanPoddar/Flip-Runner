using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public bool gameStarted = false;
    int lives = 3;
    int score = 0;
    //public int highScore = 0;
    Vector3 originalCamPos;

    public Text scoreText;
    public Text scoreTextRestart;
    public Text highScoreText;
    public Text livesText;
    public GameObject gamePlayUI;
    public GameObject MenuUI;
    public GameObject spawner;
    public GameObject bgParticle;
    public GameObject restartUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originalCamPos= Camera.main.transform.position;
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void StartGame()
    {
        MenuUI.SetActive(false);
        gamePlayUI.SetActive(true);
        spawner.SetActive(true);
        gameStarted = true;
        bgParticle.SetActive(true);
    }

    public void GameOver()
    {
        gameStarted= false;
        player.SetActive(false);
        MenuUI.SetActive(false);
        gamePlayUI.SetActive(false);
        spawner.SetActive(false);
        restartUI.SetActive(true);

        //Invoke(nameof(ReloadLevel), 0.7f);
    }

    public void ReloadLevel()
    {
        InterstitialAd.instance.ShowAd();
        SceneManager.LoadScene("Game");
    }

    public void UpdateLive()
    {
        if(lives <= 1)
        {
            GameOver();
        }
        else
        {
            lives--;
            livesText.text = "Lives: " + lives;
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        scoreTextRestart.text = "Score: " + score;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Shake()
    {
        StartCoroutine("CameraShake");
    }

    IEnumerator CameraShake()
    {
        for(int i=0; i < 10; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle * 0.5f;
            Camera.main.transform.position = new Vector3(randomPos.x, randomPos.y, originalCamPos.z);
            yield return null;
        }
        Camera.main.transform.position = originalCamPos;
    }
}
