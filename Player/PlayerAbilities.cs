using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        
        GameData.shootEnabled = player.shootEnabled;

        //player.shootEnabled = (PlayerPrefs.GetInt("shoot") != 0);
        //player.dashEnabled = (PlayerPrefs.GetInt("dash") != 0);
        //player.wallJumpEnabled = (PlayerPrefs.GetInt("wall jump") != 0);
        //player.doubleJumpEnabled = (PlayerPrefs.GetInt("dbl jump") != 0);

    }

    // Update is called once per frame
    void Update()
    {
        player.shootEnabled = GameData.shootEnabled;

        //PlayerPrefs.SetInt("shoot", (player.shootEnabled ? 1 : 0));
        //PlayerPrefs.SetInt("dash", (player.dashEnabled ? 1 : 0));
        //PlayerPrefs.SetInt("wall jump", (player.dashEnabled ? 1 : 0));
        //PlayerPrefs.SetInt("dbl jump", (player.dashEnabled ? 1 : 0));
    }

    private void OnApplicationQuit()
    {        
        //PlayerPrefs.DeleteKey("shoot");
        //PlayerPrefs.DeleteKey("dash");
        //PlayerPrefs.DeleteKey("wall jump");
        //PlayerPrefs.DeleteKey("dbl jump");
    }
}