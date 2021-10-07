using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Loading : MonoBehaviour {


	void Start ()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        SignIn();
        PlayerPrefs.SetInt("Ad", 2);
        Invoke("MainMenu", 3.0f);
	}
	
    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void SignIn()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    UpdateScore();
                }
            });
        }
        else
        {
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        Social.ReportScore(PlayerPrefs.GetInt("Endless1"), "CgkI_Pul8I8ZEAIQBQ", (bool success) => {
        });
        Social.ReportScore(PlayerPrefs.GetInt("Endless2"), "CgkI_Pul8I8ZEAIQBg", (bool success) => {
        });
        Social.ReportScore(PlayerPrefs.GetInt("Endless3"), "CgkI_Pul8I8ZEAIQBw", (bool success) => {
        });
    }
}
