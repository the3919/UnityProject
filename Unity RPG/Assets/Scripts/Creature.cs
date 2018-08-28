using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{   
    protected string m_nName;
    protected Status m_nStatus;
    protected int m_Level; //레벨
    protected int m_Exp; //경험치
    protected int m_MaxExp; //최대 경험치
   
    public Status Status { get { return m_nStatus; } set { m_nStatus = value; } }
    public int Level { get { return m_Level; } set { m_Level = value;  } }
    public int Exp { get { return m_Exp; } set { m_Exp = value; } }
    public int MaxEx { get { return m_MaxExp; } set { m_MaxExp = value; } }
    public string Name { get { return m_nName; } set { m_nName = value; } }
    
    public virtual bool Dead()
    {
         if (m_nStatus.m_nHp<= 0)
         {
               return true;
         }
           return false;
    }
    public void SetEnemyAttack(int power)
    {
    }
}
