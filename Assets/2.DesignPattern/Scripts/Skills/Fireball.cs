using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill
{
    public class Fireball : SkillBehaviour
    {
        public FireballProjectile FireballProjectile;// ����ü ������
        public float projectileSpeed = 20f;// ����ü �ӵ�

        public override void Apply()
        {
            print("���̾ ����");
        }
        public override void Use()
        {
            Transform shotpoint = context.owner.shotPoint;           

            var obj = Instantiate(FireballProjectile, shotpoint.position, shotpoint.rotation);
            obj.SetProjectile(projectileSpeed);

            Destroy(obj.gameObject, 3f);

            print("���̾ ���");
        }
        public override void Remove()
        {
            print("���̾ ����");
        }
    }
}
