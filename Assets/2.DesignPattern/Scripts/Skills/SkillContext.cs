using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill
{
    public class SkillContext : SkillBehaviour
    {
        public Player owner;
        internal List<SkillBehaviour> skills = new();
        //linternal: ���� ����� �������� ���� ����.
        // ���� ���� Ŭ���� ������ ���� �����ϳ�, ����Ƽ ������ ������ �� �����Ƿ� instpector���� ������ �ȵ�.

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
            currentSkill?.Remove();//��ų�� �ٲ� ������ ����
            currentSkill = skills[index];
            currentSkill?.Apply();
        }
        public void UseSkill()
        {
            currentSkill.Use();
        }
    }
}
