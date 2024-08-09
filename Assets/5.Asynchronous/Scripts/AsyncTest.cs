using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AsyncTest : MonoBehaviour
{
    /*async void Start()
    {
        // await WaitRandom();  <- 불가능;
        WaitRandom(); // <- 가능
        print("WaitRandom 호출");


        await Wait3STask();
        print("Wait3STask 호출");

        int delayTime = await WaitRandomAndReturn();
        print($"{(float)delayTime / 1000}초 WaitRandomAndReturn호출");

        *//*Task.Run();
        new Task().ContinueWith();*//*
                       
    }*/
    private void Start()
    {
        //Wait3STask();
        //async가 아닌데도 Wait3STask()가 끝난 후에 무언가 해야할 경우.
        Wait3STask().ContinueWith((Task result) =>
        {
            if (result.IsCanceled || result.IsFaulted) 
            { print("Wait3STask 실패"); }
            else if (result.IsCompleted) 
            { print("Wait3STask 호출"); }
            print("3초");
        }); 
    }

    



    // 1. void를 반환하는 async함수 : 비동기 함수이지만, 반환값이 없다.
    // 함수 자체는 대기 가능이나, 다른 함수에서 대기가능 형식으로 호출이 불가능 하다.

    async void WaitRandom()
    {
        print($"대기시작 {Time.time}");
        await Task.Delay(Random.Range(1000, 2000));
        print($"대기종료 {Time.time}");
    }
    // 2.task를 반환하는 async함수 : 함수 자체도 대기가능 이며, 다른 대기가능 함수에서 비동기 식으로 호출이 가능하다.
    // return 이 없어도, 알아서 프로세스를 Task로 묶어서 반환함.
    async Task Wait3STask()
    {
        print($"3초 대기 기작 {Time.time}");
        await Task.Delay(3000);
        print($"3초 대기 종료 {Time.time}");
    }

    //3. Task<T>를 반환하는 async 함수 : 대기가능 함수인건 task를 반환하는 함수와 같으나, 반환값이 있어야 한다.
    // 반환값이 없으면 Task를 반환하는 함수와 같은 형태로 사용하면 된다.

    async Task<int> WaitRandomAndReturn()
    {
        int delay = Random.Range(1000, 2000);
        print($"{(float)delay/1000}초대기 시작 {Time.time}");
        await Task.Delay(delay);
        print($"{(float)delay/1000}초대기 종료 {Time.time}");        
        return delay;
    }



    
}

