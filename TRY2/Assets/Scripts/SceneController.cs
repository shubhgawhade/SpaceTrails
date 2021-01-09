using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    void Start()
    {
        PlayerBehaviour.CameraLoop += Wrap;
    }
    
    //THERE IS A BUG WHERE THE PLAYER IS ABLE TO ESCAPE THE SCENE FROM ANY CORNER.
    private void Wrap(Vector2 playerPosition, GameObject player)
    {
        if (playerPosition.y > 4.6f) //5
        {
            player.transform.position = new Vector2(playerPosition.x, 4.6f);
        }
        else if (playerPosition.y < -4.6f)
        {
            player.transform.position = new Vector2(playerPosition.x, -4.6f);
        }

        if (playerPosition.x > 8.7f)
        {
            player.transform.position = new Vector2(8.7f ,playerPosition.y);
        }
        else if(playerPosition.x < -8.7f)
        {
            player.transform.position = new Vector2(-8.7f, playerPosition.y);
        }
    }
}
