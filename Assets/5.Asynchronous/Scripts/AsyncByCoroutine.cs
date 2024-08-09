using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;



public class AsyncByCoroutine : MonoBehaviour
{
    // 햄버거를 만들고 싶다.

    int bread = 0; //빵 개수
    int patty = 0; //패티 개수
    int pickle = 0; //피클 개수
    int lettuce = 0; //양상추 개수

    FoodMaker breadMaker = new FoodMaker();
    FoodMaker pattyMaker = new FoodMaker();
    FoodMaker pickleMaker = new FoodMaker();
    FoodMaker lettuceMaker = new FoodMaker();

    public Text text;
    private void Start()
    {
        breadMaker.StartCook();
        pattyMaker.StartCook();
        pickleMaker.StartCook();
        lettuceMaker.StartCook();

        StartCoroutine(MakeHambergerCoroutine());

    }

    private void Update()
    {
        bread = breadMaker.amount;
        patty = pattyMaker.amount;
        pickle = pickleMaker.amount;
        lettuce = lettuceMaker.amount;

        text.text = $"빵 : {bread} \n패티 : {patty} \n피클 : {pickle} \n양상추 : {lettuce}";

        

        // if (HambergerReady()) MakeHamberger(); 

        //Task.Run(() => MakeHamberger()); // Task를 이용한 비동기 처리 // Task는 스레드풀을 사용한다.
        //Task : 필요할 때만 스레드를 생성한다. 스레드풀을 사용한다.

        //AwaitAsync(); // async, await를 이용한 비동기 처리

        //코루틴을 업데이트에 넣으면

    }

    IEnumerator MakeHambergerCoroutine()
    {

        yield return new WaitUntil(HambergerReady);
        MakeHamberger();

    }
    bool HambergerReady()
    {
        return bread >= 2 && patty >= 2 && pickle >= 8 && lettuce >= 4;
    }

    void MakeHamberger()
    {
      /*  breadMaker.amount -= 2;
        pattyMaker.amount -= 2;
        pickleMaker.amount -= 8;
        lettuceMaker.amount -= 4;*/

        print($"햄버거가 만들어졌습니다. 소요시간 : {Time.time}");
    }
}

public class FoodMaker
{
    public int amount; // 식재료의 양

    private System.Random rand = new System.Random(); // 랜덤 객체


    public void StartCook()
    {
        Thread cookThread = new Thread(Cook); // 스레드 생성
        cookThread.IsBackground = true; // 백그라운드 스레드로 설정
        cookThread.Start(); // 스레드 시작
    }

    private void Cook()
    {
        while (true)
        {
            int time = rand.Next(500, 1000); // 1초 ~ 3초 사이의 시간이 걸림
            // time = Random.Range(1000, 3000);            


            Thread.Sleep(time); // 시간이 걸리는 작업을 수행
            amount++; // 식재료의 양을 1 증가
        }
    }
}

