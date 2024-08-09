using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;



public class AsyncByCoroutine : MonoBehaviour
{
    // �ܹ��Ÿ� ����� �ʹ�.

    int bread = 0; //�� ����
    int patty = 0; //��Ƽ ����
    int pickle = 0; //��Ŭ ����
    int lettuce = 0; //����� ����

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

        text.text = $"�� : {bread} \n��Ƽ : {patty} \n��Ŭ : {pickle} \n����� : {lettuce}";

        

        // if (HambergerReady()) MakeHamberger(); 

        //Task.Run(() => MakeHamberger()); // Task�� �̿��� �񵿱� ó�� // Task�� ������Ǯ�� ����Ѵ�.
        //Task : �ʿ��� ���� �����带 �����Ѵ�. ������Ǯ�� ����Ѵ�.

        //AwaitAsync(); // async, await�� �̿��� �񵿱� ó��

        //�ڷ�ƾ�� ������Ʈ�� ������

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

        print($"�ܹ��Ű� ����������ϴ�. �ҿ�ð� : {Time.time}");
    }
}

public class FoodMaker
{
    public int amount; // ������� ��

    private System.Random rand = new System.Random(); // ���� ��ü


    public void StartCook()
    {
        Thread cookThread = new Thread(Cook); // ������ ����
        cookThread.IsBackground = true; // ��׶��� ������� ����
        cookThread.Start(); // ������ ����
    }

    private void Cook()
    {
        while (true)
        {
            int time = rand.Next(500, 1000); // 1�� ~ 3�� ������ �ð��� �ɸ�
            // time = Random.Range(1000, 3000);            


            Thread.Sleep(time); // �ð��� �ɸ��� �۾��� ����
            amount++; // ������� ���� 1 ����
        }
    }
}

