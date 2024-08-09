using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class WebRequest : MonoBehaviour
{
    public string BiggestImageURL = "https://images.unsplash.com/photo-1546083381-2bed38b42cac?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
    public RawImage rawImage;
    public Image image;
    private void Start()
    {
        _ = StartCoroutine(GetWebTexture(BiggestImageURL));
        // _ = 의미 없는 변수 할당

    }
    IEnumerator GetWebTexture(string url)
    {
        //http로 웹 요청을 보낼 객체 생성
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        // 비동기식(을 모방한 코루틴)으로 Response를 받을때 까지 대기
        var operation = www.SendWebRequest();
        yield return operation; // 비동기식(사실 아님) 으로 요청을 보내고, 그 결과를 받을때까지 대기

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"요청 실패 : {www.error}");
        }
        else
        {
            Debug.Log("요청 성공");

            /*image.texture = DownloadHandlerTexture.GetContent(www);*/

            Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(
                (Texture2D)texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.zero);

            image.sprite = sprite;
            image.SetNativeSize(); // 이미지의 원래 크기로 설정
            

        }
    }

}

