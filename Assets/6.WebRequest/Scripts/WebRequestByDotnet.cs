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
        //Ienumerator > yield , async > await // ����: async�� return���� ���� �� ����

        HttpClient client = new HttpClient();
        // httpClient ��� �Ŀ� �޸� ������ �ʿ�.
        byte[] response = await client.GetByteArrayAsync(url); // getbytearrayasync�� �Ϸ�ɶ� ���� ��ٸ�
        //byte�迭�� Unity���� Ȱ���� �� �ִ� Texture Instance�� ��ȯ
        print(response.Length);

        Texture2D texture = new Texture2D(1, 1);

        texture.LoadImage(response); //Parameter�� ���� byte �迭�� �̹����� ��ȯ

        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)); //Texture2D�� Sprite�� ��ȯ



        //c++ �� ��� ~httpClient(); ���� ������ �Ҹ��ڸ� ȣ��

        client.Dispose();

        // �ֱ� ��� ����
        /*
          using���� ���� Ư�� ��� �ȿ����� ��� �ǰ� ��� �ۿ����� �ڵ����� �����Ǵ� IDisposable ��ü�� ����� �� �ִ�.
        
        using(HttpClient client = new HttpClient())
        {
        // ����� �ڵ� 
        } 
        ��� �ۿ����� �ڵ����� �����Ǿ� �޸� ������ ������ �� �ִ�. 

         */


    }    



}

