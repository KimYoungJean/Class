using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{

    public class Monster : MonoBehaviour
    {
        public Renderer[] bodyRenderers;
        public Renderer[] eyeRenderers;

        public Color bodyDaycolor;
        public Color eyeDaycolor;

        public Color bodyNightColor;
        public Color eyeNightColor;

        public void Start()
        {
            // GameManager.instance.OnMonsterSpawn(this); 
            GameManager.instance.OnDayNightChange += OnDayNightChange; // 이벤트에 OnDayNightChange 메서드를 등록
            OnDayNightChange(GameManager.instance.isDay); // 게임이 시작될 때 낮과 밤의 상태를 체크

            //this와gameObject의 차이점은? : this는 자기자신을 가리키고, gameObject는 자기자신의 게임오브젝트를 가리킨다.            
        }



        private void OnDestroy() // 게임오브젝트가 파괴될 때 호출되는 함수
        {
            //    GameManager.instance.OnMonsterDespawn(this);
            GameManager.instance.OnDayNightChange -= OnDayNightChange; // 이벤트에 등록된 메서드를 해제
        }
        public void OnDayNightChange(bool isDay)
        {
            if (isDay)
            {
                DayColor();
            }
            else
            {
                NightColor();
            }
        }

        public void DayColor()
        {
            foreach (var renderer in bodyRenderers)
            {
                renderer.material.color = bodyDaycolor;
            }
            foreach (var renderer in eyeRenderers)
            {
                renderer.material.color = eyeDaycolor;
            }
        }
        public void NightColor()
        {

            foreach (var renderer in bodyRenderers)
            {
                renderer.material.color = bodyNightColor;
            }
            foreach (var renderer in eyeRenderers)
            {
                renderer.material.color = eyeNightColor;
            }
        }

    }
}
