using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartPointer : MonoBehaviour
{
    internal Vector3 startPoint;
    public bool isInitialized = false;

    private void Start()
    {
        DisplayPoint();
    }
    public void SetInitialValue(Vector3 sp)
    {
        this.startPoint = sp;
        isInitialized = true;
    }
    public void DisplayPoint()
    {
        if (isInitialized) print(startPoint);
        else Debug.LogError("Not Initialized");

    }

    public void DPbyNullable()
    {
        Vector3? nullableStartPoint = startPoint;
        if (nullableStartPoint != null) print(nullableStartPoint);
        else Debug.LogError("Not Initialized");
    }

    // .? 연산자를 사용하여 null 체크를 하고, null이 아닐 때만 메서드를 호출
    // .? ?? 연산자를 사용하여 null 체크를 하고, null일 때 대체값을 지정
    public void DPbyNullConditional()
    {
        
        StartPointer sp = new GameObject().AddComponent<StartPointer>();
        sp.DisplayPoint();
    }
}

