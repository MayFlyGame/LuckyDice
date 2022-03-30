using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    const int DICE_NUM = 2;
    const int NUM_COUNT = 6;
    const float LOW_ANGLE = 30f;
    const float UP_ANGLE = 150f;

    public GameObject[] dices = new GameObject[DICE_NUM];

    public Dictionary<int, int> numWeigth = new Dictionary<int, int>();

    // Start is called before the first frame update
    void Start()
    {
        initMap();

        int seed = (int)GetTime();
        UnityEngine.Random.InitState( seed );
    }

    private void initMap()
    {
        for (int idx = 1; idx <= NUM_COUNT; ++idx)
        {
            numWeigth[idx] = 0;
        }
    }

    public void OnRollBtnclick( )
    {
        for( int idx = 0; idx < DICE_NUM; ++ idx )
        {
            RandDice(idx);
        }

        int dice1 = GetDiceValue(0);
        int dice2 = GetDiceValue(1);
        // Debug.Log(" " + dice1 + "  " + dice2);

        randTest(0);
    }

    private void randTest(int index)
    {
        initMap();

        for ( int idx = 0; idx < 100000; ++ idx )
        {
            int randNum = RandDice(0);
            numWeigth[randNum] += 1;
        }
        String debugInfo = "";
        for( int idx = 1; idx <= NUM_COUNT; ++ idx )
        {
            float chance = numWeigth[idx] / 100000f;
            debugInfo += " Idx: " + idx + " Chance:" + chance;   
        }
        Debug.Log( debugInfo );
    }

    private int RandDice(int index)
    {
        float xAngle = UnityEngine.Random.Range(0, 4000) * 90f;
        float yAngle = UnityEngine.Random.Range(0, 4000) * 90f;
        float zAngle = UnityEngine.Random.Range(0, 4000) * 90f;
        dices[index].transform.Rotate(xAngle, yAngle, zAngle);
        return GetDiceValue(index);
    }

    private long GetTime()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    private int GetDiceValue(int index)
    {
        float upAngle = Vector3.Angle(dices[index].transform.up, Vector3.up);
        if( upAngle < LOW_ANGLE)
        {
            return 4;
        }
        if( upAngle > UP_ANGLE)
        {
            return 3;
        }
        float rightAngle = Vector3.Angle(dices[index].transform.right, Vector3.up);
        if(rightAngle < LOW_ANGLE)
        {
            return 6;
        }
        if (rightAngle > UP_ANGLE)
        {
            return 1;
        }
        float forwardAngle = Vector3.Angle(dices[index].transform.forward, Vector3.up);
        if (forwardAngle < LOW_ANGLE )
        {
            return 5;
        }
        if (forwardAngle > UP_ANGLE)
        {
            return 2;
        }
        Debug.LogError("Invalid Value! upAngle:" + upAngle + " rightAngle:" + rightAngle + " forwardAngle:" + forwardAngle );
        return 0;
    }

}
