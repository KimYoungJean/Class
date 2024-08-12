using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DateTimeCounter : MonoBehaviour
{
    public DateTime? dateTime;
    // DisplayDateTime 호출 시 세팅된 시간을 출력하는데,
    // 세팅되지 않았다면 "Not Initialized"를 출력하도록 구현해보세요.

    public void DisplayDateTime()
    {
        print(dateTime?.ToString() ?? "Not Initialized"); //Debug.LogerError("Not Initialized");

        if(dateTime != null) print(dateTime);
        else Debug.LogError("Not Initialized");

        if(dateTime.HasValue) print(dateTime.Value);
        else Debug.LogError("Not Initialized");


    }

    private void Start()
    {
        DisplayDateTime();
    }
}

