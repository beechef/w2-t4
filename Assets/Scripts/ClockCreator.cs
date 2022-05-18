using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockCreator : MonoBehaviour
{
    public GameObject ClockPrefab;
    public float SPACE;
    void Start()
    {
        GameObject clock;
        for (int i = 0; i < 12; i++)
        {
            clock = Instantiate(ClockPrefab, transform);
            clock.name = "Time Zone: " + i.ToString();
            clock.transform.Translate(Vector3.left * i * SPACE, Space.World);
            clock.GetComponent<ClockController>().TimeZone = i;
        }
    }

    void Update()
    {

    }
}
