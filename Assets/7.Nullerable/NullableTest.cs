using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NullableTest : MonoBehaviour
{
        
    int normalInt; // �⺻�� 0
    // �ʵ忡�� ������ ���ͷ��� ���, �ʱ�ȭ �Ҵ��� ���� �ʾƵ� �⺻������ �Ҵ���

    int? nullableInt; // �⺻�� null
                      // Nullable������ ���, ���۷��� Ÿ�԰� ���� �⺻���� null;

    private Vector3 vector3;
    private GameObject obj;

    private void Start()
    {
        print($" normalInt: {normalInt}");
        print($" nullableInt: {nullableInt}");

        print(vector3);
        print(obj);
    }

}

