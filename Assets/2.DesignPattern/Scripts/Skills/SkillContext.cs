using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill
{
    public class SkillContext : SkillBehaviour
    {
        public Player owner;
        internal List<SkillBehaviour> skills = new();
        //linternal: 같은 어셈블리 내에서만 접근 가능.
        // 내가 만든 클래스 끼리는 접근 가능하나, 유니티 엔진은 접근할 수 없으므로 instpector에서 수정이 안됨.

        public SkillBehaviour currentSkill;

        public void Addskill(SkillBehaviour skill)
        {
            skill.context = this;
            skills.Add(skill);
        }

        public void SetCurrentSkill(int index)
        {
            if(index>= skills.Count)
            {
                Debug.LogError("Invalid skill index");
                return;
            }
            currentSkill?.Remove();//스킬을 바꿀 때마다 제거
            currentSkill = skills[index];
            currentSkill?.Apply();
        }
        public void UseSkill()
        {
            currentSkill.Use();
        }
    }
}
