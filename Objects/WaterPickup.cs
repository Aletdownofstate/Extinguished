using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : MonoBehaviour
{
    private PlayerWater playerWater;    

    private void Start()
    {
        playerWater = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWater>();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if (playerWater.currentWater == 100)
            {
                return;
            }
            else
            {
                playerWater.currentWater += 40;
                if (!GameData.shootEnabled)
                {
                    GameData.shootEnabled = true;
                }
                Destroy(gameObject);
            }
        }
    }
}