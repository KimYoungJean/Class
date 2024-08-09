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
        // _ = �ǹ� ���� ���� �Ҵ�

    }
    IEnumerator GetWebTexture(string url)
    {
        //http�� �� ��û�� ���� ��ü ����
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        // �񵿱��(�� ����� �ڷ�ƾ)���� Response�� ������ ���� ���
        var operation = www.SendWebRequest();
        yield return operation; // �񵿱��(��� �ƴ�) ���� ��û�� ������, �� ����� ���������� ���

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"��û ���� : {www.error}");
        }
        else
        {
            Debug.Log("��û ����");

            /*image.texture = DownloadHandlerTexture.GetContent(www);*/

            Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(
                (Texture2D)texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.zero);

            image.sprite = sprite;
            image.SetNativeSize(); // �̹����� ���� ũ��� ����
            

        }
    }

}

