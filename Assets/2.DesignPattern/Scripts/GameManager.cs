using MyProject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱�������(Singleton Pattern)�� ������ GameManager Ŭ����
    // ������ �̱��� �������� ���� ���ΰ�? >> ����å�ӿ�Ģ(Single Responsibility Principle)�� �ؼ��ϴ°�?
    // SRP: Ŭ������ �� �ϳ��� å�Ӹ� ������ �Ѵ�.

    public static GameManager instance { get; private set; }

    public new Light light; // UnityEngine.Light�� �̸��� ��ġ�Ƿ� new Ű���带 ����Ͽ� �������̵�

    public float dayLength = 5;
    public bool isDay = true;

    /*
     ������ ����: Ư�� �ӹ��� �����ϴ� ��ü( ������)���� ���� ��ȭ �Ǵ� Ư���̺�Ʈ�� ȣ�� ������ �߻��� ��,
     �ش��̺�Ʈ ȣ���� �ʿ��� ��ü���� "���� ���� ���ϸ� �˷��ּ���." ��� ���(Subscribe)�ϴ� ����
     */
    private List<Monster> monsters = new(); // �����ڵ�. ���͵��� �������� ���

    //C#�� event�� ������ ���Ͽ� ����ȭ�� ������ ������� �����Ƿ�,
    // Event�� Ȱ���ϴ� �� �����ε� ������ ������ �����ߴٰ� �� �� ����.

    public event Action<bool> OnDayNightChange; // �̺�Ʈ ����
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            // Destroy�� DestroyImmediate�� ��������? : Destroy�� ���� �����ӿ� ����ǰ�, DestroyImmediate�� ��� ����ȴ�.            
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // ���� ����Ǿ ���ӿ�����Ʈ�� �ı����� �ʵ��� ����
    }
    private float dayTemp;

    private void Update()
    {
        if (Time.time - dayTemp > dayLength) // ���� ���� ���̸� ����
        {
            isDay = !isDay;
            dayTemp = Time.time; // ���� �ð��� ����
            light.gameObject.SetActive(isDay); // ���� �㿡 ���� ���� �Ѱ� ��

            // ������ ������ ���� ���͵鿡�� ���� ���� ���¸� ����
            foreach (var monster in monsters)
            {
                monster.OnDayNightChange(isDay); // ������ ���� �� ������ ����
            }

            OnDayNightChange?.Invoke(isDay); // �̺�Ʈ ȣ��
        }

    }

    public void OnMonsterSpawn(Monster monster)
    {
        monsters.Add(monster); // ���Ͱ� �����Ǹ� ������ ���� ����Ʈ�� �߰�
        monster.OnDayNightChange(isDay); // ������ ���� �� ������ ����
    }
    public void OnMonsterDespawn(Monster monster)
    {
        monsters.Remove(monster); // ���Ͱ� ���ŵǸ� ������ ���� ����Ʈ���� ����
    }
}
