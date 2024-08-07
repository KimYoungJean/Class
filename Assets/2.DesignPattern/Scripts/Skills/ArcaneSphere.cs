using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill
{
    public class ArcaneSphere : SkillBehaviour
    {
        public GameObject projectilePrefab;
        private Transform spinner;

        public override void Apply()
        {
            // �� ������ ��ü 2�� ����
            Instantiate(projectilePrefab, spinner);
            Instantiate(projectilePrefab, spinner);

            spinner.GetChild(0).localPosition = Vector3.left;
            spinner.GetChild(1).localPosition = Vector3.right;


        }
        public override void Use()
        {
            print("���� ��ü ��ų�� ��� ȿ���� �����ϴ�.");
        }
        public override void Remove()
        {
            foreach (Transform child in spinner)
            {
                Destroy(child.gameObject);
            }
            print("���� ��ü ���� ���");
        }

        private void Awake()
        {
            if (transform.childCount > 0)
            {
                spinner = transform.GetChild(0);
            }
            else
            {
                spinner = new GameObject("Spinner").transform;
                spinner.SetParent(transform);
                spinner.localPosition = Vector3.up;
            }
        }
        private void Update()
        {
            spinner.Rotate(Vector3.up, 180 * Time.deltaTime);

        }
    }
}
