using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerSkillEnum
{
    Dash = 1,
    Gun,
}
public class PlayerSkillManager : MonoSingleton<PlayerSkillManager>
{
    private Dictionary<Type, PlayerSkill> _skills;

    private void Awake()
    {
        _skills = new Dictionary<Type, PlayerSkill>();
        foreach (PlayerSkillEnum skillEnum in Enum.GetValues(typeof(PlayerSkillEnum)))
        {
            string skillName = skillEnum.ToString();
            try
            {
                PlayerSkill skillComponent = GetComponent($"{skillName}Skill") as PlayerSkill;
                Type type = skillComponent.GetType();
                _skills.Add(type, skillComponent);
            }
            catch (Exception ex)
            {
                Debug.LogError($"{skillName} is missing! check skill manager : {ex.Message}");
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public T GetSkill<T>() where T : PlayerSkill
    {
        Type t = typeof(T);
        if (_skills.TryGetValue(t, out PlayerSkill target))
        {
            return target as T;
        }

        return null;
    }

    public PlayerSkill GetSkill(PlayerSkillEnum skillEnum)
    {
        Type t = Type.GetType($"{skillEnum.ToString()}Skill");

        if (t == null) return null;

        if (_skills.TryGetValue(t, out PlayerSkill target))
        {
            return target;
        }
        return null;
    }

    public void UseSkillFeedback(PlayerSkillEnum skillType)
    {
        //����� ��ų ���� �ǵ���� ���鲫�� ������ ������� �ϴ� �ּ�
    }
}
