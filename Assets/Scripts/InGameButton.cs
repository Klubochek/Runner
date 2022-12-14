using Firebase.Auth;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButton : MonoBehaviour
{
    [SerializeField] private Ad ad;
    [SerializeField] private FirebaseAuth auth;
    [SerializeField] private GameObject leaderboardMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Score score;
    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    public void OnResetButtonClick()
    {
        Time.timeScale = 1;
        StartCoroutine(RestartLevel());
    }
    public void OnLeaderboardButtonClick()
    {
        score.OpenLeaderboard();
        leaderboardMenu.SetActive(true);
    }
    public void OnCloseLeaderboardButtonClick()
    {
        leaderboardMenu.SetActive(false);
        score.ClearLeaderboard();
    }
    public void OnLogoutButtonClick()
    {
        auth.SignOut();
        SceneManager.LoadScene(0);
    }
    public void OnMenuButtonClick()
    {
        Time.timeScale = 0;
        mainMenu.SetActive(true);
    }
    public void OnCloseMenuButtonClick()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
    }
    private IEnumerator RestartLevel()
    {
        ad.ShowAd();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
