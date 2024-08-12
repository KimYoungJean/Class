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

    // .? �����ڸ� ����Ͽ� null üũ�� �ϰ�, null�� �ƴ� ���� �޼��带 ȣ��
    // .? ?? �����ڸ� ����Ͽ� null üũ�� �ϰ�, null�� �� ��ü���� ����
    public void DPbyNullConditional()
    {
        
        StartPointer sp = new GameObject().AddComponent<StartPointer>();
        sp.DisplayPoint();
    }
}

