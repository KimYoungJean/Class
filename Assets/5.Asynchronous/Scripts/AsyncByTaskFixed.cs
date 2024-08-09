using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;



public class AsyncByTaskFixed : MonoBehaviour
{
    async private void Start() 
        // async: �Լ� �տ� �ٴ� Ű�����, �ش� �Լ��� ��Ⱑ��(�񵿱�) �Լ����� ��Ÿ����.
        // awiat: async(�񵿱�) �Լ� ������ ���Ǹ�, �Լ� ���� ��� ���ɿ��(Task��)�� �Ϸ� �� ������ ����ϵ��� ��.

        // ��Ƽ �������� ������ �� ó�� ���ο� �����带 �����ϴ� ���� �ƴ϶�, Task�� �����Ͽ� �����ϹǷ�
        // ���� �����忡�� ������ ������.

        // new Thread(GetFood("",1).Start(); �̷������� �����带 �����ϴ� ���� �ƴ϶�
        // Task�� �����Ͽ� �����Ѵ�.

        // Task�� ���� : �����带 �����ϴ� �ͺ��� ������, ������ Ǯ�� ����Ͽ� �����带 �����ϹǷ�, ������ ���� ����� ����.
        // ������ �����带 �����ϴ� �ͺ��� ���� �� �ִ�. (������ Ǯ�� ����ϱ� ������)

    {
        print("�ܹ��� ����� ��...");


        Task breadTask = GetFood("��", 2);
        Task lettuceTask = GetFood("�����", 4);
        Task pattyTask = GetFood("��Ƽ", 2);
        Task pickleTask = GetFood("��Ŭ", 8);

        await Task.WhenAll(breadTask, lettuceTask, pattyTask, pickleTask);        
        print("�ܹ��Ű� �ϼ���");

         // WhenAll�� ��� Task�� �Ϸ�Ǹ� ���� 
        // ContinueWith�� Task�� �Ϸ�Ǹ� ����
        // �׷� ��� Task�� �Ϸ�Ǿ����� �ܹ��� �ϼ����� ����ϴ°��ΰ�? 
        // t�� ���� : Task.WhenAll(breadTask, lettuceTask, pattyTask, pickleTask)�� ������� �޴´�.

    }

    async Task GetFood(string name, int count)
    {
        for (int i = 1; i <= count; i++)
        {
            await Task.Delay(Random.Range(1000, 3000));
            print($"{name} {i}�� �ϼ���");
        }
    }

    
}

