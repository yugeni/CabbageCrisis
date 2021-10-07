using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using ChartboostSDK;

public class GameManager : MonoBehaviour {

    public GameObject startMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject quitMenu;
    public GameObject winMenu;
    public Button pauseButton;
    public GameObject[] toolButtons;
    public Text highScoreText;
    public Text scoreText;
    public string endlessName;
    public string levelSelectionName;
    AudioSource[] audios;
    int score;
    GameObject cabbage;
    public bool endless;

    void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            if(Time.timeScale != 0)
            {
                audios[2].Pause();
                pauseMenu.SetActive(true);
                pauseButton.interactable = false;
                Time.timeScale = 0;
                foreach (GameObject button in toolButtons)
                {
                    button.SetActive(false);
                }
            }
        }
        else
        {
            if (!Social.localUser.authenticated)
            {
                Social.localUser.Authenticate((bool success) =>
                {
                });
            }
        }
    }

	void Start ()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        cabbage = GameObject.FindGameObjectWithTag("Cabbage");
        audios = GetComponents<AudioSource>();
        startMenu.SetActive(true);
        pauseButton.interactable = false;
        Time.timeScale = 0;
        foreach(GameObject button in toolButtons)
        {
            button.SetActive(false);
        }
        if (endless)
        {
            highScoreText.text = "Highscore: " + PlayerPrefs.GetInt(endlessName) + "s";
            score = 0;
        }
	}
	

	void FixedUpdate ()
    {
        if (cabbage != null)
        {
            if (endless)
            {
                ScoreKeeper();
                HighScore();
            }
            else
            {
                if(GameObject.FindGameObjectWithTag("Caterpillar") == null && GameObject.FindGameObjectWithTag("Aphid") == null && GameObject.FindGameObjectWithTag("Ant") == null && GameObject.FindGameObjectWithTag("Snail") == null && GameObject.FindGameObjectWithTag("Grasshopper") == null && GameObject.FindGameObjectWithTag("Spawner") == null)
                {
                    Win();
                }
            }
        }
        else
        {
            GameOver();
        }
	}

    void GameOver()
    {
        if (!gameOverMenu.activeInHierarchy)
        {
            audios[2].Stop();
            audios[1].Play();
            gameOverMenu.SetActive(true);
            pauseButton.interactable = false;
            Time.timeScale = 0f;
            foreach (GameObject button in toolButtons)
            {
                button.SetActive(false);
            }
            ShowAd();
        }
    }

    void Win()
    {
        if (!winMenu.activeInHierarchy)
        {
            audios[2].Stop();
            audios[3].Play();
            winMenu.SetActive(true);
            pauseButton.interactable = false;
            Time.timeScale = 0f;
            foreach (GameObject button in toolButtons)
            {
                button.SetActive(false);
            }
            UnlockLevel();
            ShowAd();
        }
    }

    public void Play()
    {
        audios[0].Play();
        startMenu.SetActive(false);
        pauseButton.interactable = true;
        Time.timeScale = 1.0f;
        foreach (GameObject button in toolButtons)
        {
            button.SetActive(true);
        }
    }

    public void Pause()
    {
        audios[0].Play();
        audios[2].Pause();
        pauseMenu.SetActive(true);
        pauseButton.interactable = false;
        Time.timeScale = 0;
        foreach (GameObject button in toolButtons)
        {
            button.SetActive(false);
        }
    }

    public void Resume()
    {
        audios[0].Play();
        audios[2].UnPause();
        pauseMenu.SetActive(false);
        pauseButton.interactable = true;
        Time.timeScale = 1.0f;
        foreach (GameObject button in toolButtons)
        {
            button.SetActive(true);
        }
    }

    public void Quit()
    {
        audios[0].Play();
        quitMenu.SetActive(true);
    }

    public void Yes()
    {
        audios[0].Play();
        SceneManager.LoadScene(levelSelectionName);
    }

    public void No()
    {
        audios[0].Play();
        quitMenu.SetActive(false);
    }

    public void Restart()
    {
        audios[0].Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ScoreKeeper()
    {
        score = (int)Time.timeSinceLevelLoad;
        scoreText.text = "Score: " + score + "s";
    }

    void HighScore()
    {
        if (score > PlayerPrefs.GetInt(endlessName))
        {
            PlayerPrefs.SetInt(endlessName, score);
            PlayerPrefs.Save();
        }
        PostScore();
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt(endlessName) + "s";
    }

    void PostScore()
    {
        if(Social.localUser.authenticated)
        {
            Social.ReportScore(PlayerPrefs.GetInt("Endless1"), "CgkI_Pul8I8ZEAIQBQ", (bool success) => {
            });
            Social.ReportScore(PlayerPrefs.GetInt("Endless2"), "CgkI_Pul8I8ZEAIQBg", (bool success) => {
            });
            Social.ReportScore(PlayerPrefs.GetInt("Endless3"), "CgkI_Pul8I8ZEAIQBw", (bool success) => {
            });
        }
    }

    void UnlockLevel()
    {
        if(PlayerPrefs.GetInt("Level") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }
    }

    void ShowAd()
    {
        PlayerPrefs.SetInt("Ad", PlayerPrefs.GetInt("Ad") + 1);
        if (PlayerPrefs.GetInt("Ad") >= 3)
        {
            Chartboost.showInterstitial(CBLocation.Default);
            PlayerPrefs.SetInt("Ad", 0);
        }
    }
}
