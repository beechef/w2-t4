using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public GameObject HourPrefab;
    public Transform HourBase;
    public Transform CentralPoint;
    public Transform SecondClockwise;
    public Transform MinuteClockwise;
    public Transform HourClockwise;

    public float TimeZone;

    public bool ChangeOverSecond;

    const float CIRCLE_DEGREE = 360.0f;

    const float MSECOND = 1.0f;
    const float SECOND = 60.0f;
    const float MINUTE = 60.0f;
    const float HOUR = 24.0f;

    const float SECOND_DEGREE = CIRCLE_DEGREE / SECOND; // 360 độ / 60 giây
    const float MINIUTE_DEGREE = CIRCLE_DEGREE / MINUTE; // 360 độ / 60 phút
    const float HOUR_DEGREE = CIRCLE_DEGREE / (HOUR / 2); // 360 độ / 12 giờ

    float mSecond;
    float second;
    float minute;
    float hour;
    Vector3 centralPosition;


    void Start()
    {
        SetupClock();
    }

    void FixedUpdate()
    {
        mSecond += Time.fixedDeltaTime;
        RotateSecond();
        RotateMinute();
        RotateHour();
        UpdateTime();
    }
    void UpdateTime()
    {
        if (mSecond >= MSECOND)
        {
            mSecond = 0;
            second++;
        }
        if (second >= SECOND)
        {
            second = 0;
            minute++;
        }

        if (minute >= MINUTE)
        {
            minute = 0;
            hour++;
        }

        if (hour >= (HOUR / 2))
        {
            hour = 0;
        }
    }
    void SetupHour()
    {
        GameObject hourPrefab;
        for (int i = 0; i < 12; i++)
        {
            hourPrefab = Instantiate(HourPrefab, transform);
            hourPrefab.name = i.ToString() + " Hour";
            hourPrefab.transform.position = HourBase.position;
            hourPrefab.transform.rotation = HourBase.rotation;
            hourPrefab.transform.RotateAround(centralPosition, Vector3.forward, i * HOUR_DEGREE);
        }
    }
    void SetupTime()
    {
        DateTime now = DateTime.Now;
        second = now.Second;
        minute = now.Minute;
        hour = now.Hour + TimeZone;
        if (ChangeOverSecond)
        {
            SecondClockwise.RotateAround(centralPosition, Vector3.forward, SECOND_DEGREE * second);
            MinuteClockwise.RotateAround(centralPosition, Vector3.forward, MINIUTE_DEGREE * minute + (second / SECOND));
            HourClockwise.RotateAround(centralPosition, Vector3.forward, HOUR_DEGREE * hour + (minute / MINUTE));

        }
        else
        {
            SecondClockwise.RotateAround(centralPosition, Vector3.forward, SECOND_DEGREE * second);
            MinuteClockwise.RotateAround(centralPosition, Vector3.forward, MINIUTE_DEGREE * minute);
            HourClockwise.RotateAround(centralPosition, Vector3.forward, HOUR_DEGREE * hour);
        }

    }
    void SetupClock()
    {
        centralPosition = CentralPoint.position;
        SetupHour();
        SetupTime();
    }
    void RotateSecond()
    {
        if (ChangeOverSecond)
        {
            SecondClockwise.RotateAround(centralPosition, Vector3.forward, SECOND_DEGREE * Time.fixedDeltaTime);
        }
        else
        {
            if (mSecond >= MSECOND)
                SecondClockwise.RotateAround(centralPosition, Vector3.forward, SECOND_DEGREE);
        }

    }
    void RotateMinute()
    {
        if (ChangeOverSecond)
        {
            MinuteClockwise.RotateAround(centralPosition, Vector3.forward, (MINIUTE_DEGREE / SECOND) * Time.fixedDeltaTime);
        }
        else
        {
            if (second >= SECOND)
                MinuteClockwise.RotateAround(centralPosition, Vector3.forward, MINIUTE_DEGREE);
        }

    }
    void RotateHour()
    {
        if (ChangeOverSecond)
        {
            HourClockwise.RotateAround(centralPosition, Vector3.forward, (HOUR_DEGREE / (SECOND * MINUTE)) * Time.fixedDeltaTime);
        }
        else
        {
            if (minute >= MINUTE)
                HourClockwise.RotateAround(centralPosition, Vector3.forward, HOUR_DEGREE);
        }

    }
}
