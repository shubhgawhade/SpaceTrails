using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLeaderboard : MonoBehaviour
{
    public static Action Score;
    private void Start()
    {
        
    }

    public void LeaderBoardClear()
    {
        PlayerPrefs.DeleteAll();
        if (Score != null)
        {
            Score();
        }
    }
}
