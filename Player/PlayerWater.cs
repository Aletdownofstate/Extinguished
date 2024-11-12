using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    public int maxWater = 100;
    public float currentWater;

    private WaterBar waterBar;    

    private void Start()
    {
        waterBar = FindObjectOfType<WaterBar>();

        currentWater = GameData.currentWater;

        if (GameData.currentWater == 0)
        {
            currentWater = maxWater;
        }
        else
        {
            currentWater = GameData.currentWater;
        }

        waterBar.SetMaxWater(maxWater);
        waterBar.SetWater(currentWater);
    }

    private void Update()
    {
        if (currentWater > 100)
        {
            currentWater = 100;
        }

        waterBar.SetWater(currentWater);
        GameData.currentWater = currentWater;
    }
}