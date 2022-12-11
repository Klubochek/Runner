using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameButton : MonoBehaviour
{
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
        SceneManager.LoadScene(1);
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
        mainMenu.SetActive(true);
    }
    public void OnCloseMenuButtonClick()
    {
        mainMenu.SetActive(false);
    }
}
