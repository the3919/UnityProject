using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParams : CharacterParam
{
    Collider[] buff;
    int buffcount;
    SkillManager skillmanager;
    public bool IsCooldown;
    public bool provokeCooldown;
    public float CoolTime = 10.0f;
    public float CoolTime2 = 10.0f;
    public float ProvokeRange = 5.0f;
    public Job m_CurrentJob;
    public enum Job
    {
        Warrior,
        Soldier,
        Wizard
    }
    public GameObject SkillBullet;
    public Transform firePos;
    public List<Skill> m_CharacterSkillList = new List<Skill>(2);
    public string name { get; set; }
    public int curExp { get; set; }
    public int MaxExp { get; set; }
    public int Money { get; set; }
    public int Shield { get; set; }
    public int MaxShield { get; set; }
    // Status(int hp_ = 0, int p_attack = 0, int max_p_attack = 0, int m_attack = 0, int Max_m_attack = 0, int p_def = 0, int m_def = 0)
   
    public Skill characterListSkill(int idx)
    {
        return m_CharacterSkillList[idx];
    }
    public void Load()
    {
        skillmanager = GameManager.getInstance().m_cskillManager;      
        if (m_CurrentJob == Job.Warrior)
        {
            name = PlayerPrefs.GetString("Name", "N/A");
            Debug.Log(name);
            m_cStatus.m_nHp = PlayerPrefs.GetInt("HP");
            maxHp = m_cStatus.m_nHp;
            Debug.Log(m_cStatus.m_nHp);
            m_cStatus.min_Phy_Attack = PlayerPrefs.GetFloat("AttackMin");
            m_cStatus.Max_Phy_Attack = PlayerPrefs.GetFloat("AttackMax");
            m_cStatus.min_Magic_Attack = PlayerPrefs.GetFloat("MagicAttackMin");
            m_cStatus.Max_Magic_Attack = PlayerPrefs.GetFloat("MagicAttackMax");
            m_cStatus.m_Phy_Def = PlayerPrefs.GetFloat("PhyDef");
            m_cStatus.m_Magic_Def = PlayerPrefs.GetFloat("MagicDef");
            Shield = 0;
            level = 1;
            curExp = 0;
            MaxExp = 100 * level;
            //Money = 300;
            isDead = false;
            for (int i = 0; i < 2; i++)
            {
                SetSkill(skillmanager.GetSKill(i));
                Debug.Log(characterListSkill(i).strName);
            }
           

        }
        if (m_CurrentJob == Job.Wizard)
        {
            name = PlayerPrefs.GetString("Wi_Name", "N/A");
            Debug.Log(name);
            m_cStatus.m_nHp = PlayerPrefs.GetInt("Wi_HP");
            maxHp = m_cStatus.m_nHp;
            m_cStatus.min_Phy_Attack = PlayerPrefs.GetFloat("Wi_AttackMin");
            m_cStatus.Max_Phy_Attack = PlayerPrefs.GetFloat("Wi_AttackMax");
            m_cStatus.min_Magic_Attack = PlayerPrefs.GetFloat("Wi_MagicAttackMin");
            m_cStatus.Max_Magic_Attack = PlayerPrefs.GetFloat("Wi_MagicAttackMax");
            m_cStatus.m_Phy_Def = PlayerPrefs.GetFloat("Wi_PhyDef");
            m_cStatus.m_Magic_Def = PlayerPrefs.GetFloat("Wi_MagicDef");
            Shield = 0;
            level = 1;
            curExp = 0;
            MaxExp = 100 * level;
            //Money = 300;
            isDead = false;
            for (int i = 0; i < 2; i++)
            {
                SetSkill(skillmanager.GetSKill(i + 2));
                Debug.Log(characterListSkill(i).strName);
            }
            
        }
        if (m_CurrentJob == Job.Soldier)
        {
            name = PlayerPrefs.GetString("So_Name", "N/A");
            Debug.Log(name);
            m_cStatus.m_nHp = PlayerPrefs.GetInt("So_HP");
            maxHp = m_cStatus.m_nHp;
            m_cStatus.min_Phy_Attack = PlayerPrefs.GetFloat("So_AttackMin");
            m_cStatus.Max_Phy_Attack = PlayerPrefs.GetFloat("So_AttackMax");
            m_cStatus.min_Magic_Attack = PlayerPrefs.GetFloat("So_MagicAttackMin");
            m_cStatus.Max_Magic_Attack = PlayerPrefs.GetFloat("So_MagicAttackMax");
            m_cStatus.m_Phy_Def = PlayerPrefs.GetFloat("So_PhyDef");
            m_cStatus.m_Magic_Def = PlayerPrefs.GetFloat("So_MagicDef");
            Shield = 0;
            level = 1;
            curExp = 0;
            MaxExp = 100 * level;
            //Money = 300;
            isDead = false;
            for (int i = 0; i < 2; i++)
            {
                SetSkill(skillmanager.GetSKill(i + 4));
                Debug.Log(characterListSkill(i).strName);
            }
            
        }
        UIManager.instance.UpdatePlayerText(this);

    }
    public void Set(string name_, int level_, int maxHp_, float attackMin_, float attackMax_, int def_)
    {
        
        
        m_cStatus = new Status(maxHp_, attackMin_, attackMax_, 0, 0, def_, 0);
        maxHp = m_cStatus.m_nHp;
        //Shield = 0;
        name = name_;
        //level = level_;


        //curExp = 0;
        //MaxExp = 100 * level;
        ////Money = 300;
        //isDead = false;
        if (m_CurrentJob == Job.Warrior)
        {
            PlayerPrefs.SetString("Name", name);
            PlayerPrefs.SetInt("HP", m_cStatus.m_nHp);
            PlayerPrefs.SetFloat("AttackMin", m_cStatus.min_Phy_Attack);
            PlayerPrefs.SetFloat("AttackMax", m_cStatus.Max_Phy_Attack);
            PlayerPrefs.SetFloat("MagicAttackMin", m_cStatus.min_Magic_Attack);
            PlayerPrefs.SetFloat("MagicAttackMax", m_cStatus.Max_Magic_Attack);
            PlayerPrefs.SetFloat("PhyDef", m_cStatus.m_Phy_Def);
            PlayerPrefs.SetFloat("MagicDef", m_cStatus.m_Magic_Def);
        }
        if (m_CurrentJob == Job.Wizard)
        {
            PlayerPrefs.SetString("Wi_Name", name);
            PlayerPrefs.SetInt("Wi_HP", m_cStatus.m_nHp);
            PlayerPrefs.SetFloat("Wi_AttackMin", m_cStatus.min_Phy_Attack);
            PlayerPrefs.SetFloat("Wi_AttackMax", m_cStatus.Max_Phy_Attack);
            PlayerPrefs.SetFloat("Wi_MagicAttackMin", m_cStatus.min_Magic_Attack);
            PlayerPrefs.SetFloat("Wi_MagicAttackMax", m_cStatus.Max_Magic_Attack);
            PlayerPrefs.SetFloat("Wi_PhyDef", m_cStatus.m_Phy_Def);
            PlayerPrefs.SetFloat("Wi_MagicDef", m_cStatus.m_Magic_Def);
        }
        if (m_CurrentJob == Job.Soldier)
        {
            PlayerPrefs.SetString("So_Name", name);
            PlayerPrefs.SetInt("So_HP", m_cStatus.m_nHp);
            PlayerPrefs.SetFloat("So_AttackMin", m_cStatus.min_Phy_Attack);
            PlayerPrefs.SetFloat("So_AttackMax", m_cStatus.Max_Phy_Attack);
            PlayerPrefs.SetFloat("So_MagicAttackMin", m_cStatus.min_Magic_Attack);
            PlayerPrefs.SetFloat("So_MagicAttackMax", m_cStatus.Max_Magic_Attack);
            PlayerPrefs.SetFloat("So_PhyDef", m_cStatus.m_Phy_Def);
            PlayerPrefs.SetFloat("So_MagicDef", m_cStatus.m_Magic_Def);
        }

    }
    //public void Set(string name_, int level_, int maxHp_, float attackMin_, float attackMax_, int def_ )
    //{
    //    skillmanager = GameManager.getInstance().m_cskillManager;
    //    m_cStatus = new Status(maxHp_ , attackMin_, attackMax_,0,0,def_,0);
    //    maxHp = m_cStatus.m_nHp;
    //    Shield = 0;
    //    name = name_;
    //    level = level_;

    //    curExp = 0;
    //    MaxExp = 100 * level;
    //    Money = 300;
    //    isDead = false;

    //    if (m_CurrentJob == Job.Warrior)
    //    {
    //        for (int i = 0; i < 2; i++)
    //        {
    //            SetSkill(skillmanager.GetSKill(i));
    //            Debug.Log(characterListSkill(i).strName);
    //        }            
    //        //UIManager.instance.UpdatePlayerUI(this);
    //    }
    //    if (m_CurrentJob == Job.Wizard)
    //    {           
    //        for (int i = 0; i < 2; i++)
    //        {
    //            SetSkill(skillmanager.GetSKill(i + 2));
    //            Debug.Log(characterListSkill(i).strName);
    //        }
    //        //UIManager.instance.UpdatePlayerUI_two(this);
    //    }
    //    if (m_CurrentJob == Job.Soldier)
    //    {
    //        for (int i = 0; i < 2; i++)
    //        {
    //            SetSkill(skillmanager.GetSKill(i + 4));
    //            Debug.Log(characterListSkill(i).strName);
    //        }
    //        //UIManager.instance.UpdatePlayerUI_three(this);
    //    }
    //   // UIManager.instance.UpdatePlayerUI(this);
    //}
    public override void SetEnemyAttack(int enemyAttackPower)
    {
        Debug.Log("적군공격력: " + enemyAttackPower);
        if (Shield <= 0)
        {
            m_cStatus.m_nHp -= enemyAttackPower;
        }
        else
        {
            if (Shield > enemyAttackPower)
            {
                Shield -= enemyAttackPower;
            }
            else
            {               
                m_cStatus.m_nHp -= enemyAttackPower-Shield;
                Shield = 0;
            }
        }
        UpdateAfterReceiveAttack();
    }
    public void UpdateUI()
    {
        UIManager.instance.UpdatePlayerUI(this);
    }
    public void SetSkill(Skill skill)
    {
        m_CharacterSkillList.Add(skill);
    }
    public void UseSkill(Skill skill)
    {
        if (skill == skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.SHIELD))
        {
            Debug.Log("쉴드씀");
            MaxShield += 60;
            Shield = MaxShield;
            UIManager.instance.PlayerShieldBar.gameObject.SetActive(true);
            StartCoroutine("ShieldTime");
            UIManager.instance.UpdatePlayerUI(this);
        }
        if (skill == skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.PROVOKE))
        {
            Debug.Log("도발씀");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ProvokeRange, 1 << 10);
            int num = hitColliders.Length;
            for (int i = 0; i < num; i++)
            {
                Debug.Log(hitColliders[i].name + i + "감지됨");
                hitColliders[i].GetComponent<MonsterParams>().Provoke();
            }
        }
        if (skill == skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.HEAL))
        {
            Debug.Log("힐씀");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ProvokeRange, 1 << 9);
            int num = hitColliders.Length;
            for (int i = 0; i < num; i++)
            {
                Debug.Log(hitColliders[i].name + i + "감지됨");
                hitColliders[i].GetComponent<PlayerParams>().m_cStatus.m_nHp+=220;
                if (hitColliders[i].GetComponent<PlayerParams>().m_cStatus.m_nHp >= hitColliders[i].GetComponent<PlayerParams>().maxHp)
                {
                    hitColliders[i].GetComponent<PlayerParams>().m_cStatus.m_nHp = hitColliders[i].GetComponent<PlayerParams>().maxHp;
                }                
                UIManager.instance.UpdatePlayerUI(hitColliders[i].GetComponent<PlayerParams>());            
            }
        }
        if (skill == skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.BUFF))
        {
            //int Stat = 20;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ProvokeRange, 1 << 9);
          
            int num = hitColliders.Length;
            buff = new Collider[num];
            buffcount = num;
            for (int i = 0; i < num; i++) 
            {
                buff[i] = hitColliders[i];
                Debug.Log(hitColliders[i].name + i + "감지됨");
                // hitColliders[i].GetComponent<PlayerParams>().m_cStatus.BuffAddStatus(Stat);
                hitColliders[i].GetComponent<PlayerParams>().m_cStatus.BuffAddStatus(skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.BUFF).sFuction);
                hitColliders[i].GetComponent<PlayerParams>().m_cStatus.Show();
            }
            StartCoroutine("BuffTime");
        }
        if(skill == skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.FIRESHOOT))
        {
            Instantiate(SkillBullet, firePos.position, firePos.rotation);
        }
        if (skill == skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.POWERUP))
        {
            Debug.Log("파워업");
            m_cStatus.min_Phy_Attack += skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.BUFF).sFuction.min_Phy_Attack;
            m_cStatus.Max_Phy_Attack += skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.BUFF).sFuction.Max_Phy_Attack;
            Debug.Log(m_cStatus.min_Phy_Attack);
            Debug.Log(m_cStatus.Max_Phy_Attack);
           // StartCoroutine("PowerUpTime");
        }
    }
    IEnumerator PowerUpTime()
    {
        yield return new WaitForSeconds(7.0f);
        m_cStatus.min_Phy_Attack -= skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.BUFF).sFuction.min_Phy_Attack;
        m_cStatus.Max_Phy_Attack -= skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.BUFF).sFuction.Max_Phy_Attack;
    }
    IEnumerator ShieldTime()
    {
        yield return new WaitForSeconds(3.0f);
        UIManager.instance.PlayerShieldBar.gameObject.SetActive(false);
        MaxShield = 0;
        Shield = MaxShield;
    }
    IEnumerator BuffTime()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("버프꺼짐");
        for (int i = 0; i < buffcount; i++)
        {
            buff[i].GetComponent<PlayerParams>().m_cStatus.BuffSubStatus(skillmanager.GetSKill((int)SkillManager.E_SKILL_LIST.BUFF).sFuction);
            buff[i].GetComponent<PlayerParams>().m_cStatus.Show();
        }
    }
    protected override void UpdateAfterReceiveAttack()
    {
        if (m_cStatus.m_nHp <= 0)
        {
            m_cStatus.m_nHp = 0;
            isDead = true;
            DeadEvent.Invoke();
            if (BattleSceneManager.getInstance().CharacterCount == 3)
            {
                BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[BattleSceneManager.getInstance().CharacterSecondIndex];
                BattleSceneManager.getInstance().camara3.GetComponent<OrbitCameraC>().target = BattleSceneManager.getInstance().Character[BattleSceneManager.getInstance().CharacterSecondIndex].transform;
            }
            if (BattleSceneManager.getInstance().CharacterCount == 3)
            {
                BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[BattleSceneManager.getInstance().CharacterThirdIndex];
                BattleSceneManager.getInstance().camara3.GetComponent<OrbitCameraC>().target = BattleSceneManager.getInstance().Character[BattleSceneManager.getInstance().CharacterThirdIndex].transform;
            }
            if (BattleSceneManager.getInstance().CharacterCount == 1)
            {
                Debug.Log("종료");
            }
            BattleSceneManager.getInstance().CharacterCount--;           
      
           Destroy(this.gameObject);
            
            
        }
        UIManager.instance.UpdatePlayerUI(this);
       
       
    }
}
