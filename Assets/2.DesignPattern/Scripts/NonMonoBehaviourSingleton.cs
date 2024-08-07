using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour를 상속받지 않는 일반적인 싱글턴 패턴 적용방법
public class NonMonoBehaviourSingleton
{
    // 실제 instance필드는 private static으로 선언하고
    private static NonMonoBehaviourSingleton instance;

    // 외부에서 접근할 수 있도록 public static으로 선언,
    //Getter메서드 또는 c#의 get전용 프로퍼티를 통해 읽기전용으로 선언.
    public static NonMonoBehaviourSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NonMonoBehaviourSingleton();
            }
            return instance;
        }

    }

    private NonMonoBehaviourSingleton() //다른 클래스에서 생성자를 호출하지 못하도록 private로 선언
    {
        // 생성자
    }
    public static NonMonoBehaviourSingleton GetInstance()
    {
        if (instance == null)
        {
            instance = new NonMonoBehaviourSingleton();
        }
        return instance;
    }

}
