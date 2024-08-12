using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;



public class WebRequestByDotnet : MonoBehaviour
{
    public string url;
    public Image image;

    async void Start()
    {
        //Ienumerator > yield , async > await // 차이: async는 return값을 받을 수 있음

        HttpClient client = new HttpClient();
        // httpClient 사용 후에 메모리 해제가 필요.
        byte[] response = await client.GetByteArrayAsync(url); // getbytearrayasync가 완료될때 까지 기다림
        //byte배열을 Unity에서 활용할 수 있는 Texture Instance로 변환
        print(response.Length);

        Texture2D texture = new Texture2D(1, 1);

        texture.LoadImage(response); //Parameter로 받은 byte 배열을 이미지로 변환

        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)); //Texture2D를 Sprite로 변환



        //c++ 의 경우 ~httpClient(); 같은 식으로 소멸자를 호출

        client.Dispose();

        // 최근 축약 버번
        /*
          using문을 통해 특정 블록 안에서만 사용 되고 블록 밖에서는 자동으로 해제되는 IDisposable 객체를 사용할 수 있다.
        
        using(HttpClient client = new HttpClient())
        {
        // 사용할 코드 
        } 
        블록 밖에서는 자동으로 해제되어 메모리 누수를 방지할 수 있다. 

         */


    }    



}

