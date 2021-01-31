using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    [SerializeField] private float tempScore;
    [SerializeField] private int multiplier = 1;
    [SerializeField] private float killStreak = 0;

    [SerializeField] private Text highscoreText1;
    [SerializeField] private Text highscoreText2;
    [SerializeField] private Text highscoreText3;

    [SerializeField] private Text highscoreNum1;
    [SerializeField] private Text highscoreNum2;
    [SerializeField] private Text highscoreNum3;


    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject quit;
    [SerializeField] private GameObject clearLocalLeaderboard;

    [SerializeField] private Text localLeaderBoard;
    [SerializeField] private Text globalLeaderBoard;
    
    [SerializeField] private Text livesText;
    [SerializeField] private Text lives;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text multiplierText;

    
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemies;

    public List<int> value= new List<int>(); 

    const string privateCode = "";
    const string publicCode = "";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoreList;
    public List<string> onlineName = new List<string>();
    public List<float> onlineScore = new List<float>();

    [SerializeField] private Text globalHighScoreName1;
    [SerializeField] private Text globalHighScoreName2;
    [SerializeField] private Text globalHighScoreName3;
    [SerializeField] private Text globalHighScoreName4;
    [SerializeField] private Text globalHighScoreName5;

    [SerializeField] private Text globalHighScore1;
    [SerializeField] private Text globalHighScore2;
    [SerializeField] private Text globalHighScore3;
    [SerializeField] private Text globalHighScore4;
    [SerializeField] private Text globalHighScore5;

    // Start is called before the first frame update
    void Start()
    {
        ClearLeaderboard.Score += ClearHighScore;

        //PlayerPrefs.DeleteAll(); // CLEAR LOCAL LEADERBOARD
        highscoreText1.enabled = false;
        highscoreText2.enabled = false;
        highscoreText3.enabled = false;

        highscoreNum1.enabled = false;
        highscoreNum2.enabled = false;
        highscoreNum3.enabled = false;

        restart.SetActive(false);
        quit.SetActive(false);
        clearLocalLeaderboard.SetActive(false);

        localLeaderBoard.enabled = false;
        globalLeaderBoard.enabled = false;

        highscoreText1.text = PlayerPrefs.GetString("Name1").ToString();
        highscoreNum1.text = PlayerPrefs.GetFloat("HighScore1").ToString();
        highscoreText2.text = PlayerPrefs.GetString("Name2").ToString();
        highscoreNum2.text = PlayerPrefs.GetFloat("HighScore2").ToString();
        highscoreText3.text = PlayerPrefs.GetString("Name3").ToString();
        highscoreNum3.text = PlayerPrefs.GetFloat("HighScore3").ToString();

        EnemyBehaviour.Score += ScoreAdd;
        PlayerBehaviour.HighScore += HighScore;
    }

    private void ClearHighScore()
    {
        highscoreText1.text = PlayerPrefs.GetString("Name1").ToString();
        highscoreNum1.text = PlayerPrefs.GetFloat("HighScore1").ToString();
        highscoreText2.text = PlayerPrefs.GetString("Name2").ToString();
        highscoreNum2.text = PlayerPrefs.GetFloat("HighScore2").ToString();
        highscoreText3.text = PlayerPrefs.GetString("Name3").ToString();
        highscoreNum3.text = PlayerPrefs.GetFloat("HighScore3").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddNewHighScore(string username, float score)
    {
        StartCoroutine(UploadNewHighScore(username, score));
    }

    IEnumerator UploadNewHighScore(string username, float score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            DownloadHighScores();
        }
        else
        {
            print("ERROR UPLOADING: " + www.error);
            globalHighScoreName3.text = "  " + "Connect to the Internet...";
        }
    }

    private void DownloadHighScores()
    {
        StartCoroutine("DownloadHighScoresFromDatabase");
    }

    IEnumerator DownloadHighScoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScores(www.text);
        }
        else
        {
            print("ERROR DOWNLOADING: " + www.error);
        }
    }

    private void FormatHighScores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Highscore[entries.Length];

        if (entries.Length >= 5)
        {
            for(int i = 0; i < 5; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                float score = float.Parse(entryInfo[1]);
                highscoreList[i] = new Highscore(username, score);

                onlineName.Add(highscoreList[i].username);
                onlineScore.Add(highscoreList[i].score);
            }
        }
        else
        {
            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                float score = float.Parse(entryInfo[1]);
                
                highscoreList[i] = new Highscore(username, score);

                onlineName.Add(highscoreList[i].username);
                onlineScore.Add(highscoreList[i].score);
            }
        }
            DisplayGlobal();
    }

    private void DisplayGlobal()
    {
        for (int i = 0; i < 5; i++)
        {
            if (onlineName[i] != null)
            {
                switch (i)
                {
                    case 0:
                        globalHighScoreName1.text = onlineName[0];
                        globalHighScore1.text = onlineScore[0].ToString();
                        break;
                    case 1:
                        globalHighScoreName2.text = onlineName[1];
                        globalHighScore2.text = onlineScore[1].ToString();
                        break;
                    case 2:
                        globalHighScoreName3.text = onlineName[2];
                        globalHighScore3.text = onlineScore[2].ToString();
                        break;
                    case 3:
                        globalHighScoreName4.text = onlineName[3];
                        globalHighScore4.text = onlineScore[3].ToString();
                        break;
                    case 4:
                        globalHighScoreName5.text = onlineName[4];
                        globalHighScore5.text = onlineScore[4].ToString();
                        break;
                }

            }
        }
    }

    public struct Highscore
    {
        public string username;
        public float score;

        public Highscore(string _username,float _score)
        {
            username = _username;
            score = _score;
        }
    }

    public void HighScore()
    {
        name = PlayerPrefs.GetString("PlayerName").ToString();

        AddNewHighScore(name, tempScore);

        highscoreText1.enabled = true;
        highscoreText2.enabled = true;
        highscoreText3.enabled = true;

        highscoreNum1.enabled = true;
        highscoreNum2.enabled = true;
        highscoreNum3.enabled = true;

        restart.SetActive(true);
        quit.SetActive(true);
        clearLocalLeaderboard.SetActive(true);

        localLeaderBoard.enabled = true;
        globalLeaderBoard.enabled = true;

        multiplierText.enabled = false;
        lives.enabled = false;

        if (tempScore > PlayerPrefs.GetFloat("HighScore1"))
        {
            string previousholdername = PlayerPrefs.GetString("Name1");
            float previousHolderScore = PlayerPrefs.GetFloat("HighScore1");

            PlayerPrefs.SetString("Name2", previousholdername);
            PlayerPrefs.SetFloat("HighScore2", previousHolderScore);

            PlayerPrefs.SetString("Name1", name);
            PlayerPrefs.SetFloat("HighScore1", tempScore);
            if (value.Contains(1))
            {
                value.Remove(1);
            }
            value.Add(1);
        }
        else if (tempScore > PlayerPrefs.GetFloat("HighScore2"))
        {
            string previousholdername = PlayerPrefs.GetString("Name2");
            float previousHolderScore = PlayerPrefs.GetFloat("HighScore2");

            PlayerPrefs.SetString("Name3", previousholdername);
            PlayerPrefs.SetFloat("HighScore3", previousHolderScore);

            PlayerPrefs.SetString("Name2", name);
            PlayerPrefs.SetFloat("HighScore2", tempScore);
            if (value.Contains(2))
            {
                value.Remove(2);
            }
            value.Add(2);
        }
        else if (tempScore > PlayerPrefs.GetFloat("HighScore3"))
        {
            PlayerPrefs.SetString("Name3", name);
            PlayerPrefs.SetFloat("HighScore3", tempScore);
            if (value.Contains(3))
            {
                value.Remove(3);
            }
            value.Add(3);
        }
        else
        {
            value.Add(4);
        }

        foreach(int a in value)
        {
            if (a < 5)
            {
                highscoreText1.text = PlayerPrefs.GetString("Name1").ToString();
                highscoreNum1.text = PlayerPrefs.GetFloat("HighScore1").ToString();
                highscoreText2.text = PlayerPrefs.GetString("Name2").ToString();
                highscoreNum2.text =  PlayerPrefs.GetFloat("HighScore2").ToString();
                highscoreText3.text = PlayerPrefs.GetString("Name3").ToString();
                highscoreNum3.text = PlayerPrefs.GetFloat("HighScore3").ToString();
            }
        }
        PlayerPrefs.DeleteKey("PlayerName");

        EnemyBehaviour.Score -= ScoreAdd;
        PlayerBehaviour.HighScore -= HighScore;
    }

    private void ScoreAdd()
    {
        if (killStreak < 1)
        {
            tempScore += 0.5f;
        }
        else if (killStreak >= 1)
        {
            tempScore += 0.5f * multiplier;
        }

        scoreText.text = tempScore.ToString();

        killStreak += 0.5f;
        multiplier = 5;
        multiplierText.text = "X" + multiplier.ToString();
        StartCoroutine("ScoreMultiplier");
    }

    IEnumerator ScoreMultiplier()
    {
        yield return new WaitForSeconds(1);
        killStreak -= 0.5f;
        if (killStreak <= 0)
        {
            multiplier = 1;
            multiplierText.text = "X" + multiplier.ToString();
        }
    }
}
