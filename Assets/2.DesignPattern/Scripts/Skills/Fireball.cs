using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill
{
    public class Fireball : SkillBehaviour
    {
        public FireballProjectile FireballProjectile;// 투사체 프리펩
        public float projectileSpeed = 20f;// 투사체 속도

        public override void Apply()
        {
            print("파이어볼 장착");
        }
        public override void Use()
        {
            Transform shotpoint = context.owner.shotPoint;           

            var obj = Instantiate(FireballProjectile, shotpoint.position, shotpoint.rotation);
            obj.SetProjectile(projectileSpeed);

            Destroy(obj.gameObject, 3f);

            print("파이어볼 사용");
        }
        public override void Remove()
        {
            print("파이어볼 제거");
        }
    }
}
