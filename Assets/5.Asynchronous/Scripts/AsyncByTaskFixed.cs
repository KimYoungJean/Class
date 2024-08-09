using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;



public class AsyncByTaskFixed : MonoBehaviour
{
    async private void Start() 
        // async: 함수 앞에 붙는 키워드로, 해당 함수가 대기가능(비동기) 함수임을 나타낸다.
        // awiat: async(비동기) 함수 내에서 사용되며, 함수 내의 대기 가능요소(Task등)이 완료 될 때까지 대기하도록 함.

        // 멀티 쓰레딩을 수행할 때 처럼 새로운 쓰레드를 생성하는 것이 아니라, Task를 생성하여 수행하므로
        // 단일 쓰레드에서 수행이 가능함.

        // new Thread(GetFood("",1).Start(); 이런식으로 쓰레드를 생성하는 것이 아니라
        // Task를 생성하여 수행한다.

        // Task의 장점 : 쓰레드를 생성하는 것보다 가볍고, 쓰레드 풀을 사용하여 쓰레드를 관리하므로, 쓰레드 생성 비용이 적다.
        // 단점은 쓰레드를 생성하는 것보다 느릴 수 있다. (쓰레드 풀을 사용하기 때문에)

    {
        print("햄버거 만드는 중...");


        Task breadTask = GetFood("빵", 2);
        Task lettuceTask = GetFood("양상추", 4);
        Task pattyTask = GetFood("패티", 2);
        Task pickleTask = GetFood("피클", 8);

        await Task.WhenAll(breadTask, lettuceTask, pattyTask, pickleTask);        
        print("햄버거가 완성됨");

         // WhenAll은 모든 Task가 완료되면 실행 
        // ContinueWith는 Task가 완료되면 실행
        // 그럼 모든 Task가 완료되었을때 햄버거 완성됨을 출력하는것인가? 
        // t는 뭐야 : Task.WhenAll(breadTask, lettuceTask, pattyTask, pickleTask)의 결과값을 받는다.

    }

    async Task GetFood(string name, int count)
    {
        for (int i = 1; i <= count; i++)
        {
            await Task.Delay(Random.Range(1000, 3000));
            print($"{name} {i}개 완성됨");
        }
    }

    
}

