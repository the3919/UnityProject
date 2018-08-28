using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Status
{
    public int m_nHp; //캐릭터 Hp
    //public int m_nMaxHp; //캐릭터 MaxHp	i
    public float min_Phy_Attack; //물리 공격력
    public float Max_Phy_Attack; //물리 공격력
    public float min_Magic_Attack; //마법 공격력
    public float Max_Magic_Attack; //마법 공격력
    public float m_Phy_Def; //물리 방어력
    public float m_Magic_Def; //마법 방어력

    public Status(int hp_ = 0, float p_attack = 0 ,float max_p_attack=0 ,float m_attack = 0,float Max_m_attack = 0 ,float p_def = 0, float m_def = 0)
    {
        m_nHp = hp_;        
        min_Phy_Attack = p_attack;
        Max_Phy_Attack = max_p_attack;
        min_Magic_Attack = m_attack;
        Max_Magic_Attack = Max_m_attack;
        m_Phy_Def = p_def;
        m_Magic_Def = m_def;
        //cout << "m_nHp" << m_nHp << endl;
    }   
    public void AddStatus(int var)
    {
        m_nHp += var;      
        min_Phy_Attack += var;
        Max_Phy_Attack += var;
        min_Magic_Attack += var;
        Max_Magic_Attack += var;
        m_Phy_Def += var;
        m_Magic_Def += var;
    }
    public void BuffAddStatus(Status var)
    {
        min_Phy_Attack += var.min_Phy_Attack;
        Max_Phy_Attack += var.Max_Phy_Attack;
        min_Magic_Attack += var.min_Magic_Attack;
        Max_Magic_Attack += var.Max_Magic_Attack;
        m_Phy_Def += var.m_Phy_Def;
        m_Magic_Def += var.m_Magic_Def;
    }
    public void BuffSubStatus(Status var)
    {
        min_Phy_Attack -= var.min_Phy_Attack;
        Max_Phy_Attack -= var.Max_Phy_Attack;
        min_Magic_Attack -= var.min_Magic_Attack;
        Max_Magic_Attack -= var.Max_Magic_Attack;
        m_Phy_Def -= var.m_Phy_Def;
        m_Magic_Def -= var.m_Magic_Def;
    }  
    public void SubStatus(int var)
    {
        m_nHp -= var;
        min_Phy_Attack -= var;
        Max_Phy_Attack -= var;
        min_Magic_Attack -= var;
        Max_Magic_Attack -= var;
        m_Phy_Def -= var;
        m_Magic_Def -= var;
    }
    public static Status operator +(Status a, Status b)
    {
        return new Status(a.m_nHp + b.m_nHp, a.min_Phy_Attack + b.min_Phy_Attack, a.Max_Phy_Attack+b.Max_Phy_Attack,
            a.min_Magic_Attack + b.min_Magic_Attack, a.Max_Magic_Attack+b.Max_Magic_Attack,a.m_Phy_Def + b.m_Phy_Def, a.m_Magic_Def + b.m_Magic_Def);

    }
    public static Status operator -(Status a, Status b)
    {
        return new Status(a.m_nHp - b.m_nHp, a.min_Phy_Attack - b.min_Phy_Attack, a.Max_Phy_Attack - b.Max_Phy_Attack,
             a.min_Magic_Attack - b.min_Magic_Attack, a.Max_Magic_Attack - b.Max_Magic_Attack, a.m_Phy_Def - b.m_Phy_Def, a.m_Magic_Def - b.m_Magic_Def);
    }
    public void Show()
    {
        Debug.Log(m_nHp);
        Debug.Log(min_Phy_Attack);
        Debug.Log(Max_Phy_Attack);
        Debug.Log(min_Magic_Attack);
        Debug.Log(Max_Magic_Attack);
        Debug.Log(m_Phy_Def);
        Debug.Log(m_Magic_Def);
    }
}
public class CharacterParam : MonoBehaviour
{
    public int level { get; set; }
    public Status m_cStatus;
    public int maxHp { get; set; }
    //public int curHp { get; set; }
    // public float attackMin { get; set; }
    //public float attackMax { get; set; }
    //public int defense { get; set; }   

    public bool isDead { get; set; }
   
    [System.NonSerialized] 
    public UnityEvent DeadEvent = new UnityEvent();
    //[System.NonSerialized]
  
    //public UnityEvent DeadEvent2 = new UnityEvent();
    // Use this for initialization
    void Start()
    {
        InitParams();
    }
    public virtual void InitParams()
    {

    }
    public int GetRandomAttack()
    {
        int randomAttack = (int)Random.Range(m_cStatus.min_Phy_Attack, m_cStatus.Max_Phy_Attack + 1);
        return randomAttack;
    }
    public virtual void SetEnemyAttack(int enemyAttackPower)
    {
        Debug.Log("아군AttackPower: " + enemyAttackPower);        
        m_cStatus.m_nHp -= enemyAttackPower;
        UpdateAfterReceiveAttack();
    }
    protected virtual void UpdateAfterReceiveAttack()
    {
        //print(name +"s Hp:" + m_cStatus.m_nHp);
        if (m_cStatus.m_nHp <= 0)
        {
            m_cStatus.m_nHp = 0;
            isDead = true;
            DeadEvent.Invoke();          
           
            //DeadEvent[1].Invoke();
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
