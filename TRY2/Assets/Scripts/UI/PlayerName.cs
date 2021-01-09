using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text placeholder;

    private char[] badChars = { '*' };

    void Start()
    {
        StartGame.Error += ErrorMessage;
    }

    private void ErrorMessage()
    {
        if (text.text == "")
        {
            placeholder.color = new Color(1, 0, 0);
            StartCoroutine("Wait", 1);
        }
        else if (text.text.IndexOfAny(badChars) >= 0 || text.text.Length > 17)
        {
            text.color= new Color(1, 0, 0);
            StartCoroutine("Wait", 1);
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", text.text);
            SceneManager.LoadScene("GameScene");
        }
    }

    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        placeholder.color = new Color(0.5f, 0.5f, 0.5f, 128);
        text.color = new Color(0, 0, 0);
    }
}
