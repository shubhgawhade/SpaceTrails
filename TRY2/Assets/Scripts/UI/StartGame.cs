using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class StartGame : MonoBehaviour
{
    public static Action Error;

    public void Scene2()
    {
       if (Error != null)
       { 
            {
                Error();
            }
        }
    }
}
