using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class LevelManager : MonoBehaviour {

    AudioSource[] audios;
    private int totalL = 24;

	void Start ()
    {
        audios = GetComponents<AudioSource>();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
	}
	
    public void MainMenu()
    {
        audios[0].Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void LevelSelection()
    {
        audios[0].Play();
        SceneManager.LoadScene("LevelSelection");
    }

    public void ModeSelection()
    {
        audios[0].Play();
        SceneManager.LoadScene("ModeSelection");
    }

    public void Level(int index)
    {
        audios[0].Play();
        SceneManager.LoadScene(index);
    }

    public void Mode(int index)
    {
        audios[0].Play();
        SceneManager.LoadScene(index + totalL);
    }

    public void SignIn()
    {
        audios[0].Play();
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                    Social.ShowLeaderboardUI();
            });
        }
        else
            Social.ShowLeaderboardUI();
    }
}
