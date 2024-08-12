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
        // ���� Ŭ���̾�Ʈ �ʱ�ȭ
        SteamClient.Init(480);
        // �ٸ� ������ ? https://steamdb.info/apps/ ���� ã�� �� ����.

        /*print(SteamClient.AppId);
        print(SteamClient.SteamId);
        print(SteamClient.SteamId.AccountId);*/


        // ������ ������ ������ ģ������� �ε��Ͽ�, �ʻ�ȭ�� ���� ���θ� ǥ��       
        
        SteamImage? myAvatar = await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);// �� �ʻ�ȭ

        UnityImage myAvatarImage = Instantiate(imagePrefab, canvasTransform);


        if (myAvatar.HasValue)
            myAvatarImage.sprite = SteamImageToSprite(myAvatar.Value);
        else
            myAvatarImage.sprite = defaultAvatar;

        myAvatarImage.transform.GetChild(0).gameObject.SetActive(false);

        // ģ�� ����� ������
        foreach (Friend friend in SteamFriends.GetFriends())
        {
            SteamImage? friendAvatar = await SteamFriends.GetLargeAvatarAsync(friend.Id);
            UnityImage friendAvatarImage = Instantiate(imagePrefab, canvasTransform);

            if (friendAvatar.HasValue)
                friendAvatarImage.sprite = SteamImageToSprite(friendAvatar.Value);
            else
                friendAvatarImage.sprite = defaultAvatar;

            //���� �������̶�� Child�� ��Ȱ��ȭ
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
        // mipChain : �̸� ����� ���� �̹����� ũ�⸦ ���̴� ��
        Texture2D tex = new Texture2D((int)steamImage.Width, (int)steamImage.Height, TextureFormat.ARGB32, false);

        tex.filterMode = FilterMode.Trilinear; // Trilinear : ���� �������� ����Ͽ� �ؽ�ó�� Ȯ�� �Ǵ� ����� �� �ؽ�ó�� �ȼ��� �����ϴ� ���

        // ������ ����Ƽ�� �ȼ� ������ �ٸ��� ������, �ȼ��� �����ؾ� ��
        // Steam Image �� Unity Sprite�ؽ����� �ȼ� ǥ�� ������ �ٸ��Ƿ�, ������ �ʿ���
        for (int x = 0; x < steamImage.Width; x++)
        {
            for (int y = 0; y < steamImage.Height; y++)
            {
                var pixel = steamImage.GetPixel(x, y);
                var color = new Color(pixel.r / 255f, pixel.g / 255f, pixel.b / 255f, pixel.a / 255f);

                // �ȼ��� ������ ������

                tex.SetPixel(x, (int)steamImage.Height - y, color); // y���� ������Ŵ                
            }
        }

        tex.Apply(); // �ؽ�ó�� ����
        Sprite sprite =
            Sprite.Create
            (texture:tex,  //
            new Rect(x: 0, y: 0, tex.width, tex.height), // �Ķ���� ���� ���� ���ָ鼭 �������� �ø� �� �ִ�.
            new Vector2(x:0.5f,y: 0.5f)); // ��������Ʈ ����  

        return sprite;
    }


}

