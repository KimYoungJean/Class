using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DateTimeCounter : MonoBehaviour
{
    public DateTime? dateTime;
    // DisplayDateTime ȣ�� �� ���õ� �ð��� ����ϴµ�,
    // ���õ��� �ʾҴٸ� "Not Initialized"�� ����ϵ��� �����غ�����.

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

