using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterParams : CharacterParam
{
    float attacktemp1, attacktemp2;
    public bool isProvoke;
    public string name;
    public Image HpBar;
    public Image provokeState;
    // Use this for initialization
    public int SendExp { get; set; }
    public int Money { get; set; }
    public override void InitParams()
    {
        if (this.gameObject.tag == "Boss")
        {           
            m_cStatus = new Status(400, 20, 25, 0, 0, 3, 0);
            maxHp = m_cStatus.m_nHp;
            isProvoke = false;
            name = "파란버섯";
            level = 1;

            SendExp = 10;
            Money = Random.Range(10, 31);
            isDead = false;
            InitHPBar();
        }
        else
        {           
            m_cStatus = new Status(70, 10, 17, 0, 0, 1, 0);
            maxHp = m_cStatus.m_nHp;
            isProvoke = false;
            name = "버섯";
            level = 1;

            SendExp = 10;
            Money = Random.Range(10, 31);
            isDead = false;
            InitHPBar();
        }
    }
    void InitHPBar()
    {
        HpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        provokeState.gameObject.SetActive(false);
    }

    protected override void UpdateAfterReceiveAttack()
    {
        base.UpdateAfterReceiveAttack();     
        HpBar.rectTransform.localScale = new Vector3((float)m_cStatus.m_nHp/(float)maxHp, 1f,1f);       
      
    }
    public void Provoke()
    {
        attacktemp1 = m_cStatus.min_Phy_Attack;
        attacktemp2 = m_cStatus.Max_Phy_Attack;
        provokeState.gameObject.SetActive(true);
        isProvoke = true;       
        m_cStatus.min_Phy_Attack =  m_cStatus.min_Phy_Attack - (m_cStatus.min_Phy_Attack * 0.1f);
        m_cStatus.Max_Phy_Attack = m_cStatus.Max_Phy_Attack - (m_cStatus.Max_Phy_Attack * 0.1f);
        StartCoroutine("ProvokeTime");
    }
    IEnumerator ProvokeTime()
    {
        yield return new WaitForSeconds(3.0f);
        m_cStatus.min_Phy_Attack = attacktemp1;
        m_cStatus.Max_Phy_Attack = attacktemp2;
        provokeState.gameObject.SetActive(false);
        isProvoke = false;
    }
    void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.tag == "BULLET")
        {
            m_cStatus.m_nHp -= other.gameObject.GetComponent<BulletCtrl>().damage;
            UpdateAfterReceiveAttack();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bomb")
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.0f, 1 << 10);
            int num = hitColliders.Length;
            for (int i = 0; i < num; i++)
            {
                Debug.Log(hitColliders[i].name + i + "감지됨");
                hitColliders[i].GetComponent<MonsterParams>().m_cStatus.m_nHp -= other.gameObject.GetComponent<SoliderSkillShot>().damage;
                hitColliders[i].GetComponent<MonsterParams>().UpdateAfterReceiveAttack();
                //Debug.Log(hitColliders[i].GetComponent<MonsterParams>().m_cStatus.m_nHp);
            }            
            Destroy(other.gameObject);
        }
       
    }
}
