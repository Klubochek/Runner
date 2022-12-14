using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    private DatabaseReference dbr;
    private FirebaseAuth auth;
    private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI leaderboardText;
    private Coroutine scoreCount;

    private void Start()
    {
        dbr = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
    }
    public void StartCountScore()
    {
        scoreCount = StartCoroutine(ScoreCount());
    }
    public void StopCountScore()
    {
        if (scoreCount != null)
        {
            StopCoroutine(scoreCount);
        }
        StartCoroutine(WriteNewScore());



    }
    private IEnumerator ScoreCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            score += 4;
            scoreText.text = "Score:" + score.ToString();

        }
    }
    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score:" + score.ToString();
        StopCountScore();
    }

    private IEnumerator WriteNewScore()
    {
        var lastscore = dbr.Child("LeaderBoard").Child(auth.CurrentUser.Email.Replace(".", "")).Child("Score").GetValueAsync();
        yield return new WaitUntil(predicate: () => lastscore.IsCompleted);
        DataSnapshot ds = lastscore.Result;
        ;
        if (lastscore == null)
        {
            Debug.Log("Nulllastscore");
        }
        if (lastscore.Exception != null)
        {
            Debug.Log("DbExeption");
        }
        else if (lastscore.Result.Value == null)
        {
            dbr.Child("LeaderBoard").Child(auth.CurrentUser.Email.Replace(".", "")).Child("Score").SetValueAsync(score);
            dbr.Child("LeaderBoard").Child(auth.CurrentUser.Email.Replace(".", "")).Child("Email").SetValueAsync(auth.CurrentUser.Email);
        }
        else if (score > ds.Value.ConvertTo<int>())
        {
            Debug.Log("NewScore");
            dbr.Child("LeaderBoard").Child(auth.CurrentUser.Email.Replace(".", "")).Child("Score").SetValueAsync(score);
            dbr.Child("LeaderBoard").Child(auth.CurrentUser.Email.Replace(".", "")).Child("Email").SetValueAsync(auth.CurrentUser.Email);
        }

    }
    public void OpenLeaderboard()
    {
        StartCoroutine(CreateLeaderboard());
    }
    private IEnumerator CreateLeaderboard()
    {
        var leader = dbr.Child("LeaderBoard").OrderByChild("Score").GetValueAsync();
        yield return new WaitUntil(() => leader.IsCompleted);
        if (leader.Exception != null)
        {
            Debug.Log(leader.Exception);
        }
        else if (leader.Result.Value == null)
        {
            Debug.Log("Empty db");
        }
        else
        {
            DataSnapshot ds = leader.Result;
            foreach (DataSnapshot dataSnapshot in ds.Children.Reverse())
            {
                leaderboardText.text += "\n" +dataSnapshot.Child("Email").Value + "  "+dataSnapshot.Child("Score").Value;
            }
        }
    }
    public void ClearLeaderboard()
    {
        leaderboardText.text = String.Empty;
    }
}
