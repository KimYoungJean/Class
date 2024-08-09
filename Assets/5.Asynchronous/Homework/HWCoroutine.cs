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

    // new 를 쓰는 리스트와 안쓴는 리스트의 차이
    // new 를 쓰면 새로운 리스트를 만들어서 할당하고 안쓰면 null로 할당한다.
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
                Debug.LogError($"요청 실패 : {www.error}");
            }
            else
            {
                Debug.Log("요청 성공");

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


