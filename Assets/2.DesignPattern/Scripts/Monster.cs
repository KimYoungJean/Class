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
            GameManager.instance.OnDayNightChange += OnDayNightChange; // �̺�Ʈ�� OnDayNightChange �޼��带 ���
            OnDayNightChange(GameManager.instance.isDay); // ������ ���۵� �� ���� ���� ���¸� üũ

            //this��gameObject�� ��������? : this�� �ڱ��ڽ��� ����Ű��, gameObject�� �ڱ��ڽ��� ���ӿ�����Ʈ�� ����Ų��.            
        }



        private void OnDestroy() // ���ӿ�����Ʈ�� �ı��� �� ȣ��Ǵ� �Լ�
        {
            //    GameManager.instance.OnMonsterDespawn(this);
            GameManager.instance.OnDayNightChange -= OnDayNightChange; // �̺�Ʈ�� ��ϵ� �޼��带 ����
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
