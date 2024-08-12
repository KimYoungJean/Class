using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

using UnityImage = UnityEngine.UI.Image;
using SteamImage = Steamworks.Data.Image;


public class SteamworksTest : MonoBehaviour
{
    public UnityImage imagePrefab;
    public Transform canvasTransform;
    public Sprite defaultAvatar;

    private async void Start()
    {
        // 스팀 클라이언트 초기화
        SteamClient.Init(480);
        // 다른 게임은 ? https://steamdb.info/apps/ 에서 찾을 수 있음.

        /*print(SteamClient.AppId);
        print(SteamClient.SteamId);
        print(SteamClient.SteamId.AccountId);*/


        // 스팀에 접속한 계정의 친구목록을 로드하여, 초상화와 접속 여부를 표시       
        
        SteamImage? myAvatar = await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);// 내 초상화

        UnityImage myAvatarImage = Instantiate(imagePrefab, canvasTransform);


        if (myAvatar.HasValue)
            myAvatarImage.sprite = SteamImageToSprite(myAvatar.Value);
        else
            myAvatarImage.sprite = defaultAvatar;

        myAvatarImage.transform.GetChild(0).gameObject.SetActive(false);

        // 친구 목록을 가져옴
        foreach (Friend friend in SteamFriends.GetFriends())
        {
            SteamImage? friendAvatar = await SteamFriends.GetLargeAvatarAsync(friend.Id);
            UnityImage friendAvatarImage = Instantiate(imagePrefab, canvasTransform);

            if (friendAvatar.HasValue)
                friendAvatarImage.sprite = SteamImageToSprite(friendAvatar.Value);
            else
                friendAvatarImage.sprite = defaultAvatar;

            //만약 접속중이라면 Child를 비활성화
            friendAvatarImage.transform.GetChild(0).gameObject.SetActive(!friend.IsOnline);
        }
        
    }    

    private void OnApplicationQuit()
    {
        SteamClient.Shutdown();
    }
    private void OnDestroy()
    {
        SteamClient.Shutdown();
    }

    public Sprite SteamImageToSprite(SteamImage steamImage)
    {
        // mipChain : 미리 만들어 놓은 이미지의 크기를 줄이는 것
        Texture2D tex = new Texture2D((int)steamImage.Width, (int)steamImage.Height, TextureFormat.ARGB32, false);

        tex.filterMode = FilterMode.Trilinear; // Trilinear : 선형 보간법을 사용하여 텍스처를 확대 또는 축소할 때 텍스처의 픽셀을 보간하는 방법

        // 스팀과 유니티의 픽셀 형식이 다르기 때문에, 픽셀을 복사해야 함
        // Steam Image 와 Unity Sprite텍스텨의 픽셀 표시 순서가 다르므로, 반전이 필요함
        for (int x = 0; x < steamImage.Width; x++)
        {
            for (int y = 0; y < steamImage.Height; y++)
            {
                var pixel = steamImage.GetPixel(x, y);
                var color = new Color(pixel.r / 255f, pixel.g / 255f, pixel.b / 255f, pixel.a / 255f);

                // 픽셀과 색상을 가져옴

                tex.SetPixel(x, (int)steamImage.Height - y, color); // y축을 반전시킴                
            }
        }

        tex.Apply(); // 텍스처를 적용
        Sprite sprite =
            Sprite.Create
            (texture:tex,  //
            new Rect(x: 0, y: 0, tex.width, tex.height), // 파라미터 명을 기재 해주면서 가독성을 올릴 수 있다.
            new Vector2(x:0.5f,y: 0.5f)); // 스프라이트 생성  

        return sprite;
    }


}

