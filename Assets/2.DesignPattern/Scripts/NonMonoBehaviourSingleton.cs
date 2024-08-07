using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour�� ��ӹ��� �ʴ� �Ϲ����� �̱��� ���� ������
public class NonMonoBehaviourSingleton
{
    // ���� instance�ʵ�� private static���� �����ϰ�
    private static NonMonoBehaviourSingleton instance;

    // �ܺο��� ������ �� �ֵ��� public static���� ����,
    //Getter�޼��� �Ǵ� c#�� get���� ������Ƽ�� ���� �б��������� ����.
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

    private NonMonoBehaviourSingleton() //�ٸ� Ŭ�������� �����ڸ� ȣ������ ���ϵ��� private�� ����
    {
        // ������
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
