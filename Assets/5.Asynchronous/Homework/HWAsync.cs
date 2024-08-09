using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;



public class HWAsync : MonoBehaviour
{
    public List<string> URLList;
    public List<Texture> Wallpapers = new List<Texture>();





    IEnumerator GetImage(int URLIndex)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URLList[URLIndex]);
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

    async Task IsertImage(int URLIndex)
    {
        await Task.Run(() => StartCoroutine(GetImage(URLIndex)));
    }

    
}

