using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AsyncTest : MonoBehaviour
{
    /*async void Start()
    {
        // await WaitRandom();  <- �Ұ���;
        WaitRandom(); // <- ����
        print("WaitRandom ȣ��");


        await Wait3STask();
        print("Wait3STask ȣ��");

        int delayTime = await WaitRandomAndReturn();
        print($"{(float)delayTime / 1000}�� WaitRandomAndReturnȣ��");

        *//*Task.Run();
        new Task().ContinueWith();*//*
                       
    }*/
    private void Start()
    {
        //Wait3STask();
        //async�� �ƴѵ��� Wait3STask()�� ���� �Ŀ� ���� �ؾ��� ���.
        Wait3STask().ContinueWith((Task result) =>
        {
            if (result.IsCanceled || result.IsFaulted) 
            { print("Wait3STask ����"); }
            else if (result.IsCompleted) 
            { print("Wait3STask ȣ��"); }
            print("3��");
        }); 
    }

    



    // 1. void�� ��ȯ�ϴ� async�Լ� : �񵿱� �Լ�������, ��ȯ���� ����.
    // �Լ� ��ü�� ��� �����̳�, �ٸ� �Լ����� ��Ⱑ�� �������� ȣ���� �Ұ��� �ϴ�.

    async void WaitRandom()
    {
        print($"������ {Time.time}");
        await Task.Delay(Random.Range(1000, 2000));
        print($"������� {Time.time}");
    }
    // 2.task�� ��ȯ�ϴ� async�Լ� : �Լ� ��ü�� ��Ⱑ�� �̸�, �ٸ� ��Ⱑ�� �Լ����� �񵿱� ������ ȣ���� �����ϴ�.
    // return �� ���, �˾Ƽ� ���μ����� Task�� ��� ��ȯ��.
    async Task Wait3STask()
    {
        print($"3�� ��� ���� {Time.time}");
        await Task.Delay(3000);
        print($"3�� ��� ���� {Time.time}");
    }

    //3. Task<T>�� ��ȯ�ϴ� async �Լ� : ��Ⱑ�� �Լ��ΰ� task�� ��ȯ�ϴ� �Լ��� ������, ��ȯ���� �־�� �Ѵ�.
    // ��ȯ���� ������ Task�� ��ȯ�ϴ� �Լ��� ���� ���·� ����ϸ� �ȴ�.

    async Task<int> WaitRandomAndReturn()
    {
        int delay = Random.Range(1000, 2000);
        print($"{(float)delay/1000}�ʴ�� ���� {Time.time}");
        await Task.Delay(delay);
        print($"{(float)delay/1000}�ʴ�� ���� {Time.time}");        
        return delay;
    }



    
}

