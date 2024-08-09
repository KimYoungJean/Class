using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class HWCoroutine : MonoBehaviour
{
    public List<string> ImageURL;
    public List<Texture> Wallpapers = new List<Texture>();

    bool isDone = false;

    // new �� ���� ����Ʈ�� �Ⱦ��� ����Ʈ�� ����
    // new �� ���� ���ο� ����Ʈ�� ���� �Ҵ��ϰ� �Ⱦ��� null�� �Ҵ��Ѵ�.
    public RawImage rawImage;

    public void Start()
    {
        StartCoroutine(GetWallpaper(ImageURL));
        StartCoroutine(SetWallpaper());

    }
    IEnumerator GetWallpaper(List<string> urls)
    {
        foreach (string url in urls)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            var operation = www.SendWebRequest();

            yield return operation;

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"��û ���� : {www.error}");
            }
            else
            {
                Debug.Log("��û ����");

                Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Wallpapers.Add(texture);
            }

        }
        isDone = true;
    }
    IEnumerator SetWallpaper()
    {
        while (true)
        {
            while (!isDone)
            {
                yield return null;
            }

            rawImage.texture = Wallpapers[Random.Range(0, Wallpapers.Count)]; ;
            yield return new WaitForSeconds(1f);
        }
    }

}


