using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NullableTest : MonoBehaviour
{
        
    int normalInt; // 기본값 0
    // 필드에서 선언한 리터럴의 경우, 초기화 할당을 하지 않아도 기본값으로 할당함

    int? nullableInt; // 기본값 null
                      // Nullable변수의 경우, 레퍼런스 타입과 같이 기본값이 null;

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

