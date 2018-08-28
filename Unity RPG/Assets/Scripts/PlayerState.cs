using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerState : MonoBehaviour {
    private bool useskil = true;
    public State m_CurrentState = State.Idle;
    [SerializeField]
    PlayerAni ani;
    // Image ShieldImageCoolTime;
    SkillManager skilM;
    public List<Image> ShieldImageCoolTime = new List<Image>();
    Vector3 m_CurTargetPos;
   
    [SerializeField] public GameObject m_CurEnemy;

    public GameObject bullet;
    private static int Deathcount = 0;
    public Transform firePos;
    public float Angle = 360f;
    public float moveSpeed = 2f;
    float ShieldTime = 0f;
    float attackDelay = 0.3f;
    float attackTimer = 0f;
    public float attackDistance; 

    //float chaseDistance = 2.5f;
    PlayerParams myParams;
    public MonsterParams curEnemyParams;
    public enum State
    {
        Idle,
        Move,
        Attack,
        AttackWait,      
        Dead
    }
    void Start()
    {
        skilM = GameManager.getInstance().m_cskillManager;
        ani = GetComponent<PlayerAni>();
        myParams = GetComponent<PlayerParams>();
        for (int i = 0; i < ShieldImageCoolTime.Count; i++)
        {
            if (myParams.m_CurrentJob == PlayerParams.Job.Warrior)
            {
                ShieldImageCoolTime[i] = UIManager.instance.m_ShieldImageCoolTime[i];
            }
            if (myParams.m_CurrentJob == PlayerParams.Job.Wizard)
            {
                ShieldImageCoolTime[i] = UIManager.instance.m_ShieldImageCoolTime[i+2];
            }
            if (myParams.m_CurrentJob == PlayerParams.Job.Soldier)
            {
                ShieldImageCoolTime[i] = UIManager.instance.m_ShieldImageCoolTime[i + 4];
            }
        }
        myParams.DeadEvent.AddListener(PlayerDead);
        //myParams.EnemyKillEvent.AddListener(CurrentEnemyDead);
        if (myParams.m_CurrentJob == PlayerParams.Job.Warrior)
        {
            attackDistance = 2.0f;
        }
        else
        {
            attackDistance = 6.0f;
        }
        ChangeState(State.Idle, PlayerAni.ANI_IDLE);
        StartCoroutine(UsedSkill());
    }
    IEnumerator UsedSkill()
    {     
        while (useskil)
        {
            yield return null;
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            if (Input.GetKey(KeyCode.Alpha1)&& myParams.m_CurrentJob == PlayerParams.Job.Soldier
              && BattleSceneManager.getInstance().mainCharacter.GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Soldier && !myParams.characterListSkill(0).AvailableSkill)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // tansform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                float rayDistance;
                if (groundPlane.Raycast(ray, out rayDistance))
                {
                    Vector3 point = ray.GetPoint(rayDistance);
                    //pos.x = 0;
                    //pos.z = 0;
                    transform.LookAt(point);                   
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha1) && myParams.m_CurrentJob == PlayerParams.Job.Soldier
          && BattleSceneManager.getInstance().mainCharacter.GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Soldier && !myParams.characterListSkill(0).AvailableSkill)
            {
                Debug.Log(myParams.characterListSkill(0).strName);
                myParams.UseSkill(myParams.characterListSkill(0));
                myParams.characterListSkill(0).AvailableSkill = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (myParams.m_CurrentJob == PlayerParams.Job.Warrior
                 && BattleSceneManager.getInstance().mainCharacter.GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Warrior && !myParams.characterListSkill(0).AvailableSkill)
                {
                    myParams.UseSkill(myParams.characterListSkill(0));
                    myParams.characterListSkill(0).AvailableSkill = true;
                }
                if (myParams.m_CurrentJob == PlayerParams.Job.Wizard
               && BattleSceneManager.getInstance().mainCharacter.GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Wizard && !myParams.characterListSkill(0).AvailableSkill)
                {
                    Debug.Log(myParams.characterListSkill(0).strName);
                    myParams.UseSkill(myParams.characterListSkill(0));
                    myParams.characterListSkill(0).AvailableSkill = true;
                }
              //  if (myParams.m_CurrentJob == PlayerParams.Job.Soldier
              //&& GameManager.getInstance().mainCharacter.GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Soldier && !myParams.characterListSkill(0).AvailableSkill)
              //  {
              //      Debug.Log(myParams.characterListSkill(0).strName);
              //      myParams.UseSkill(myParams.characterListSkill(0));
              //      myParams.characterListSkill(0).AvailableSkill = true;
              //  }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (myParams.m_CurrentJob == PlayerParams.Job.Warrior
                 && BattleSceneManager.getInstance().mainCharacter.GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Warrior && !myParams.characterListSkill(1).AvailableSkill)
                {
                    myParams.UseSkill(myParams.characterListSkill(1));
                    myParams.characterListSkill(1).AvailableSkill = true;
                }
                if (myParams.m_CurrentJob == PlayerParams.Job.Wizard
                && BattleSceneManager.getInstance().mainCharacter.GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Wizard && !myParams.characterListSkill(1).AvailableSkill)
                {
                    myParams.UseSkill(myParams.characterListSkill(1));
                    myParams.characterListSkill(1).AvailableSkill = true;
                }
                if (myParams.m_CurrentJob == PlayerParams.Job.Soldier
             && BattleSceneManager.getInstance().mainCharacter.GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Soldier && !myParams.characterListSkill(1).AvailableSkill)
                {
                   // Debug.Log("스킬눌림");
                    myParams.UseSkill(myParams.characterListSkill(1));
                    myParams.characterListSkill(1).AvailableSkill = true;
                }
            }
            CoolTime();
        }
    }
    void CoolTime()
    {
        if (myParams.characterListSkill(0).AvailableSkill)
        {
            //Debug.Log("들어옴");
            ShieldImageCoolTime[0].fillAmount += 1 / myParams.characterListSkill(0).Cooltime * Time.deltaTime;
            if (ShieldImageCoolTime[0].fillAmount >= 1)
            {
                ShieldImageCoolTime[0].fillAmount = 0;
                myParams.characterListSkill(0).AvailableSkill = false;
            }
        }
        if (myParams.characterListSkill(1).AvailableSkill)
        {
            //Debug.Log("들어옴");
            ShieldImageCoolTime[1].fillAmount += 1 / myParams.characterListSkill(1).Cooltime * Time.deltaTime;
            if (ShieldImageCoolTime[1].fillAmount >= 1)
            {
                ShieldImageCoolTime[1].fillAmount = 0;
                myParams.characterListSkill(1).AvailableSkill = false;
            }
        }
    }
    public void PlayerDead()
    {
        print("사망");
        Deathcount++;
        //ChangeState(State.Dead, PlayerAni.ANI_DIE);
        m_CurrentState = State.Dead;
        //Debug.Log(Deathcount);
        
    }
    public void CurrentEnemyDead()
    {
        print("enemy Killed");
        ChangeState(State.Idle, PlayerAni.ANI_IDLE);

        m_CurEnemy = null;
        //Destroy(m_CurEnemy);
    }
    public void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
    }

    public void AttackCalculate()
    {
        if (m_CurEnemy == null)
            return;
        //Debug.Log("Attack" + m_CurEnemy.name);
        //m_CurEnemy.GetComponent<MonsterState>().ShowEffect();

        int attackPower = myParams.GetRandomAttack();
        curEnemyParams.SetEnemyAttack(attackPower);
        // int attackPower = myParams.GetRandomAttack();


        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackDistance, 1 << 10);
        //int num = hitColliders.Length;
        //for (int i = 0; i < num; i++)
        //{
        //    Debug.Log(hitColliders[i].name + i + "감지됨");


        //    Vector3 direction = hitColliders[i].transform.position - transform.position;
        //    if (Vector3.Angle(direction, transform.forward) < 45f)
        //    {
              
        //        hitColliders[i].GetComponent<Creature>().SetEnemyAttack(attackPower);
        //    }

        //}
    }
    public void AttackEnemy(GameObject Enemy)
    {
        //Debug.Log(Enemy);
        if (m_CurEnemy != null && Enemy == m_CurEnemy)
            return;

        curEnemyParams = Enemy.GetComponent<MonsterParams>();

        if (curEnemyParams.isDead == false)
        {
            Debug.Log("적군확인");
            m_CurEnemy = Enemy;
            m_CurTargetPos = m_CurEnemy.transform.position;
            ChangeState(State.Move, PlayerAni.ANI_RUN);
        }
        else
        {
            //    m_CurEnemy = null;
           // m_CurEnemy = null;
           // curEnemyParams = null;
        //    ChangeState(State.Idle, PlayerAni.ANI_IDLE);
        }
    }
    public void MoveTo(Vector3 Pos)
    {
        
        if (m_CurrentState == State.Dead)
            return;
        m_CurEnemy = null;
        m_CurTargetPos = Pos;
        // navMeshAgent.destination = m_CurTargetPos;
        ChangeState(State.Move, PlayerAni.ANI_RUN);
    }
    // Use this for initialization

    void ChangeState(State newState, int aniNumber)
    {
        if (m_CurrentState == newState)
            return;
              
        ani.ChangeAni(aniNumber);
        m_CurrentState = newState;
    }
    void UpDateState()
    {
        switch (m_CurrentState)
        {
            case State.Idle:
                // Debug.Log("idle");
                IdleState();
                break;
            case State.Move:
                MoveState();
                break;
            case State.Attack:
                AttatckState();
                break;
            case State.AttackWait:
                AttackWaitState();
                break;        
            case State.Dead:
                DeadState();
                break;

        }
    }
    
    void IdleState()
    {
    }
    void MoveState()
    {
        TurnToDestination();
        MoveToDestination();
    }

    void AttatckState()
    {
        if (m_CurEnemy.GetComponent<MonsterState>().isDie == true)
        {            
            ChangeState(State.Idle, PlayerAni.ANI_IDLE);
        }
        attackTimer = 0f;
        transform.LookAt(m_CurTargetPos);
        ChangeState(State.AttackWait, PlayerAni.ANI_ATTACKIDLE);
    }
    void AttackWaitState()
    {
        if (m_CurEnemy.GetComponent<MonsterState>().isDie == true)
        {           
            ChangeState(State.Idle, PlayerAni.ANI_IDLE);
        }
        if (attackTimer >= attackDelay)
        {
            ChangeState(State.Attack, PlayerAni.ANI_ATTACK);
        }
        attackTimer += Time.deltaTime;
    }
    void DeadState()
    {
        //Debug.Log("죽음");        
        //this.GetComponent<PlayerState>().enabled = false;
        //this.GetComponent<PlayerAni>().enabled = false;
        //this.GetComponent<PlayerParams>().enabled = false;
        //Destroy(this);      

    }
    void TurnToDestination()
    {
        Quaternion lookRotation = Quaternion.LookRotation(m_CurTargetPos - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * Angle);
    }
    void MoveToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_CurTargetPos, moveSpeed * Time.deltaTime);

        if (m_CurEnemy == null)
        {
            if (transform.position == m_CurTargetPos)
            {
                ChangeState(State.Idle, PlayerAni.ANI_IDLE);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, m_CurTargetPos) < attackDistance)
            {
                ChangeState(State.Attack, PlayerAni.ANI_ATTACK);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpDateState();     
      
    }   
    



    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, attackDistance);
    //}
}
