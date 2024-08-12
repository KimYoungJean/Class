using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;



public class MultipleWebRequestByDotnet : MonoBehaviour
{
    private string url = "Https://picsum.photos/500";

    public List<Image> images;

    float time;

    void Start()
    {
       
          
                time += Time.deltaTime;
                images.ForEach(DownloadImage); // DownloadImage�� Parameter�� Image�� �޾Ƽ� ����
        
        // images.ForEach((image) => DownloadImage(image)); // ���� ���� �ڵ�

        print($"�ٿ�ε� ��û �Ϸ�");
    }

    async void DownloadImage(Image targetImage)
    {
        using (HttpClient client = new HttpClient())
        {
            byte[] response = await client.GetByteArrayAsync(url);
            Texture2D texture = new Texture2D(1, 1);

            texture.LoadImage(response);
            targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
