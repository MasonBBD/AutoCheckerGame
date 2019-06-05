using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text TimeText;
    public Text PeriodText;
    public Text RoundText;
    public int PhaseCount;
    public int RoundCount = 0;
    public float TimeStamp;
    public float FightTimer;
    public float GracePeriod;
    public bool isGracePeriod = false;
    public bool isReady = false;
    public bool isBattling = false;
    public bool UsingTimer;


    // Start is called before the first frame update
    void Start()
    {
        SetTimer(1);
        UsingTimer = true;
    }

    // Update is called once per frame
    void Update()
    {

            SetUIText();
    }

    public void SetTimer(float time)
    {
        TimeStamp = Time.time + time;
    }

    public void SetUIText()
    {
        float timeLeft = TimeStamp - Time.time;
        if (timeLeft <= 0)
        {
            if (UsingTimer)
                FinishStart();

            else if (isGracePeriod)
                FinishGracePeriod();

            else if (isReady)
            {
                FinishReady();
                RoundCount += 1;
            }

            return;


        }
        float hours;
        float minutes;
        float seconds;
        float miniseconds;
        GetTimeValues(timeLeft, out hours, out minutes, out seconds, out miniseconds);


            TimeText.text = string.Format("{0}:{1}", seconds, miniseconds);
        if (PhaseCount == 1)
        {
            PeriodText.text = string.Format("GRACE!");
            RoundText.text = string.Format("ROUND {0}", RoundCount);
            
        }
        else if (PhaseCount == 2)
        {
            PeriodText.text = string.Format("READY!");
        }
        else if (PhaseCount == 3)
        {
            PeriodText.text = string.Format("BATTLE!");
        }

    }

    public void GetTimeValues(float time, out float hours, out float minutes, out float seconds, out float miniseconds)
    {
        hours = (int)(time / 3600f);
        minutes = (int)((time - hours * 3600) / 60f);
        seconds = (int)((time - hours * 3600 - minutes * 60));
        miniseconds = (int)((time - hours * 3600 - minutes * 60 - seconds) * 100);
    }


    public void FinishStart()
    {
        SetTimer(30);
        UsingTimer = false;
        isGracePeriod = true;
        PhaseCount = 1;
        
    }

    public void FinishGracePeriod()
    {
        SetTimer(5);
        isGracePeriod = false;
        isReady = true;
        PhaseCount = 2;
    }

    public void FinishReady()
    {
        SetTimer(60);
        isReady = false;
        UsingTimer = true;
        PhaseCount = 3;
    }
}
