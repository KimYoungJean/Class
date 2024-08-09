using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TaskHam : MonoBehaviour
{

    int bread = 0;
    int patty = 0;
    int pickle = 0;
    int lettuce = 0;

    public Text text;

    FoodMakerTask breadMaker = new FoodMakerTask();
    FoodMakerTask pattyMaker = new FoodMakerTask();
    FoodMakerTask pickleMaker = new FoodMakerTask();
    FoodMakerTask lettuceMaker = new FoodMakerTask();

    private void Start()
    {
        breadMaker.StartCook(2);
        pattyMaker.StartCook(2);
        pickleMaker.StartCook(8);
        lettuceMaker.StartCook(4);

        StartCoroutine(MakeHambergerCoroutine());

    }

    private void Update()
    {
        bread = breadMaker.amount;
        patty = pattyMaker.amount;
        pickle = pickleMaker.amount;
        lettuce = lettuceMaker.amount;

        text.text = $"�� : {bread} \n��Ƽ : {patty} \n��Ŭ : {pickle} \n����� : {lettuce}";

        if (HambergerReady()) MakeHamberger();
    }

    bool HambergerReady()
    {
        return bread >= 2 && patty >= 2 && pickle >= 8 && lettuce >= 4;
    }

    IEnumerator MakeHambergerCoroutine()
    {

        yield return new WaitUntil(HambergerReady);
        MakeHamberger();

    }

    void MakeHamberger()
    {
        print($"�ܹ��� �ϼ�! �ҿ�ð� :{Time.time}");
    }


}

public class FoodMakerTask
{
    public int amount = 0;

    public void StartCook(int count)
    {
        Task<int> cookTask = Cook(count);
        cookTask.ContinueWith((task) => { amount = task.Result; });
        // ContinueWith() : Task�� ���� �Ŀ� ������ �۾��� ����
    }

    private async Task<int> Cook(int count)
    {
        int result = 0;

        for (int i = 0; i < count; i++)
        {
            int time = Random.Range(1000, 3000);//
            await Task.Delay(time);
            // Task.Delay() : �񵿱������� ������ �ð���ŭ ���
            result++;
        }
        return result;

        //task.Delay()�� ����ϸ� �񵿱������� ������ �ð���ŭ ����� �� �ִ�.
        // yield return new WaitForSeconds(time);�� ���� ���
        // Task�����̿� �ڷ�ƾ�� �����̴� �ٸ���.
        // Task.Delay()�� �񵿱������� ����ϰ�, �ڷ�ƾ�� ���������� ����Ѵ�.



    }


}

