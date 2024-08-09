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

        text.text = $"빵 : {bread} \n패티 : {patty} \n피클 : {pickle} \n양상추 : {lettuce}";

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
        print($"햄버거 완성! 소요시간 :{Time.time}");
    }


}

public class FoodMakerTask
{
    public int amount = 0;

    public void StartCook(int count)
    {
        Task<int> cookTask = Cook(count);
        cookTask.ContinueWith((task) => { amount = task.Result; });
        // ContinueWith() : Task가 끝난 후에 실행할 작업을 지정
    }

    private async Task<int> Cook(int count)
    {
        int result = 0;

        for (int i = 0; i < count; i++)
        {
            int time = Random.Range(1000, 3000);//
            await Task.Delay(time);
            // Task.Delay() : 비동기적으로 지정된 시간만큼 대기
            result++;
        }
        return result;

        //task.Delay()를 사용하면 비동기적으로 지정된 시간만큼 대기할 수 있다.
        // yield return new WaitForSeconds(time);와 같은 기능
        // Task딜레이와 코루틴의 딜레이는 다르다.
        // Task.Delay()는 비동기적으로 대기하고, 코루틴은 동기적으로 대기한다.



    }


}

