using MyProject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글턴패턴(Singleton Pattern)을 적용한 GameManager 클래스
    // 무엇을 싱글턴 패턴으로 만들 것인가? >> 단일책임원칙(Single Responsibility Principle)을 준수하는가?
    // SRP: 클래스는 단 하나의 책임만 가져야 한다.

    public static GameManager instance { get; private set; }

    public new Light light; // UnityEngine.Light와 이름이 겹치므로 new 키워드를 사용하여 오버라이딩

    public float dayLength = 5;
    public bool isDay = true;

    /*
     옵저버 패턴: 특정 임무를 수행하는 객체( 옵저버)에게 상태 변화 또는 특정이벤트의 호출 조건이 발생할 시,
     해당이벤트 호출이 필요한 객체들이 "나도 상태 변하면 알려주세요." 라고 등록(Subscribe)하는 패턴
     */
    private List<Monster> monsters = new(); // 구독자들. 몬스터들이 옵저버로 등록

    //C#의 event는 옵저버 패턴에 최적화된 구조로 만들어져 있으므로,
    // Event를 활용하는 것 만으로도 옵저버 패턴을 적용했다고 볼 수 있음.

    public event Action<bool> OnDayNightChange; // 이벤트 선언
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            // Destroy와 DestroyImmediate의 차이점은? : Destroy는 다음 프레임에 실행되고, DestroyImmediate는 즉시 실행된다.            
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // 씬이 변경되어도 게임오브젝트가 파괴되지 않도록 설정
    }
    private float dayTemp;

    private void Update()
    {
        if (Time.time - dayTemp > dayLength) // 낮과 밤의 길이를 설정
        {
            isDay = !isDay;
            dayTemp = Time.time; // 현재 시간을 저장
            light.gameObject.SetActive(isDay); // 낮과 밤에 따라 빛을 켜고 끔

            // 옵저버 패턴을 통해 몬스터들에게 낮과 밤의 상태를 전달
            foreach (var monster in monsters)
            {
                monster.OnDayNightChange(isDay); // 몬스터의 낮과 밤 색상을 변경
            }

            OnDayNightChange?.Invoke(isDay); // 이벤트 호출
        }

    }

    public void OnMonsterSpawn(Monster monster)
    {
        monsters.Add(monster); // 몬스터가 생성되면 구독자 몬스터 리스트에 추가
        monster.OnDayNightChange(isDay); // 몬스터의 낮과 밤 색상을 변경
    }
    public void OnMonsterDespawn(Monster monster)
    {
        monsters.Remove(monster); // 몬스터가 제거되면 구독자 몬스터 리스트에서 제거
    }
}
