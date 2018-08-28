using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{   
	public enum E_SKILL_KIND { ACTIVE, PASSIVE }
    public E_SKILL_KIND eSkillKind { get; set; }
    public string strName { get; set; }
    public string strComment { get; set; }
    public Status sFuction { get; set; }
    public int s_Level { get; set; }
    public float Cooltime { get; set; }
    public bool AvailableSkill { get; set; }
    public float Range { get; set; }
    public Skill(E_SKILL_KIND kind , string name, string comment , Status status , int level_ , int Cooltime_ , int Range_ )
    {
        eSkillKind = kind;
        strName = name;
        strComment = comment;
        sFuction = status;
        s_Level = level_;
        Cooltime = Cooltime_;
        Range = Range_;
        AvailableSkill = false;
    }  
};
//Status(int hp_ = 0, float p_attack = 0, float max_p_attack = 0, int m_attack = 0, int Max_m_attack = 0, int p_def = 0, int m_def = 0)
public class SkillManager : MonoBehaviour
{
    List <Skill> m_listSkill;
    private static SkillManager instance;
    public static SkillManager getInstance()
    {
        return instance;
    }
    public enum E_SKILL_LIST { SHIELD, PROVOKE, HEAL, BUFF, FIRESHOOT, POWERUP ,MAX};
    SkillManager()
    {
        m_listSkill = new List<Skill>((int)E_SKILL_LIST.MAX);
        m_listSkill.Add(new Skill(Skill.E_SKILL_KIND.ACTIVE, "쉴드", "쉴드증가", new Status(),1,6,0));
        m_listSkill.Add(new Skill(Skill.E_SKILL_KIND.ACTIVE, "도발", "몬스터도발", new Status(), 1, 10, 5));

        m_listSkill.Add(new Skill(Skill.E_SKILL_KIND.ACTIVE, "힐", "캐릭터중심주변힐", new Status(), 1, 10, 0));
        m_listSkill.Add(new Skill(Skill.E_SKILL_KIND.ACTIVE, "스텟업", "캐릭터중심 주변 스텟업", new Status(20,20,20,20,20,20,20), 1, 10, 5));

        m_listSkill.Add(new Skill(Skill.E_SKILL_KIND.ACTIVE, "다중공격", "쉴드증가", new Status(), 1, 10, 0));
        m_listSkill.Add(new Skill(Skill.E_SKILL_KIND.ACTIVE, "공격력업", "공격력업", new Status(0,20,20,0,0,0,0), 1, 10, 5));       
    }
    public Skill GetSKill(int idx)
    {
        return m_listSkill[idx];
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    
}
