using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterState : MonoBehaviour
{
    [SerializeField]
    public enum State
    {
        Idle,
        Chase,
        Attack,
        Dead,
        NoState
    }
    public static int DeadMonster = 0;
    public State m_CurrentState = State.Idle;
    public bool isDie = false;
    MonsterAni myAni;
    MonsterParams myParams;
    private MoveAgent moveAgent;
    private Transform cachedTransform;
    public float attackDist = 1.5f;
    public float traceDist = 7.0f;
    private float MinDistancePlayer;
    public GameObject[] player;
    int attack_flag = 0;

    private WaitForSeconds ws;
    public bool attackstate = true;
    [SerializeField]
    PlayerParams[] playerParams = new PlayerParams[3];


    GameObject RespawnObj;
    public int spwanID;
    Vector3 originPos;

    //void callDeadEvent()
    //{
    //    StartCoroutine(RemoveMonsterWorld());
    //}
    //IEnumerator RemoveMonsterWorld()
    //{
    //    Debug.Log("몬스터삭제실행");
    //    yield return new WaitForSeconds(1f);
    //    ChangeState(State.Idle, MonsterAni.IDLE);
    //    RespawnObj.GetComponent<RespwanObj>().RemoveMonster(spwanID);
    //}
    public void SetRespwanObj(GameObject respawnObj, int id, Vector3 originPos_)
    {
        RespawnObj = respawnObj;
        spwanID = id;
        originPos = originPos_;
    }
    void Awake()
    {
        cachedTransform = GetComponent<Transform>();
        myAni = GetComponent<MonsterAni>();
        myParams = GetComponent<MonsterParams>();
        moveAgent = GetComponent<MoveAgent>();
    }
    void Start()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i] = BattleSceneManager.getInstance().Character[i];
            playerParams[i] = player[i].GetComponent<PlayerParams>();
        }
        //ChangeState(State.Idle, MonsterAni.IDLE);
        myParams.DeadEvent.AddListener(CallDeadEvent);

        ws = new WaitForSeconds(0.2f);
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    private void CallDeadEvent()
    {
        m_CurrentState = State.Dead;
        DeadMonster++;      
        //for (int i = 0; i < 3; i++)
        //{
        //    player[i].gameObject.SendMessage("CurrentEnemyDead");
        //}
    }

    public void ChangeState(State newState, string aniNumber)
    {
        if (m_CurrentState == newState && m_CurrentState != State.Chase)
            return;

        m_CurrentState = newState;
        myAni.ChangeAni(aniNumber);
    }
    float[] GetDistanceFromPlayer()
    {
        float[] result = new float[3];
        for (int i = 0; i < result.Length; i++)
        {           
            //if (player[i].GetComponent<PlayerParams>().m_cStatus.m_nHp <= 0)
            //{
            //    continue;
            //}
            result[i] = Vector3.Distance(cachedTransform.position, player[i].transform.position);
        }
        return result;
    }
    float[] GetDistanceFromPlayer2()
    {
        float[] result = new float[3];
        for (int i = 0; i < 2; i++)
        {
            if (player[0]==null)
            {
                result[i] = Vector3.Distance(cachedTransform.position, player[i+1].transform.position);
            }
            if (player[1] == null)
            {
                if (i == 1)
                {
                    continue;
                }
                result[i] = Vector3.Distance(cachedTransform.position, player[i].transform.position);
            }
            if (player[2] == null)
            {              
                result[i] = Vector3.Distance(cachedTransform.position, player[i].transform.position);
            }
        }
        return result;
    }
    float[] GetDistanceFromPlayer3()
    {
        //float[] result2 = new float[1];
        float[] result2 = new float[1];
        for (int i = 0; i < 3; i++)
        {
            if (player[i] == null)
            {
                continue;
            }
            result2[0] = Vector3.Distance(cachedTransform.position, player[i].transform.position);
        }
        return result2;
    }
    float Min(float a, float b, float c)
    {
        return Math.Min(a, Math.Min(b, c));
    }
    public void AttackCalculate()
    {
        if (attack_flag == 1)
        {
            if (BattleSceneManager.getInstance().CharacterCount == 3)
            {
                Debug.Log(myParams.GetRandomAttack());
                playerParams[0].SetEnemyAttack(myParams.GetRandomAttack());               
            }
            if (BattleSceneManager.getInstance().CharacterCount == 2)
            {
                //if (player[0].GetComponent<PlayerParams>().m_cStatus.m_nHp <= 0)
                //{                    
                //    playerParams[1].SetEnemyAttack(myParams.GetRandomAttack());
                //}
               
                if (player[0]==null)
                {
                    playerParams[1].SetEnemyAttack(myParams.GetRandomAttack());
                }
                if (player[1]==null)
                {                   
                    playerParams[0].SetEnemyAttack(myParams.GetRandomAttack());
                }
            }
            if (BattleSceneManager.getInstance().CharacterCount == 1)
            {
                if (player[0]==null && player[1]==null)
                {
                    //Debug.Log(playerParams[2].name);
                    playerParams[2].SetEnemyAttack(myParams.GetRandomAttack());
                }

                if (player[0]==null && player[2]==null)
                {
                    //Debug.Log(playerParams[2].name);
                    playerParams[1].SetEnemyAttack(myParams.GetRandomAttack());
                }
                if (player[1]==null&& player[2]==null)
                {
                    //Debug.Log(playerParams[2].name);
                    playerParams[0].SetEnemyAttack(myParams.GetRandomAttack());
                }
            }
        }
        if (attack_flag == 2)
        {
            if (BattleSceneManager.getInstance().CharacterCount == 3)
            {
                //Debug.Log(playerParams[1].name);
                playerParams[1].SetEnemyAttack(myParams.GetRandomAttack());
            }
            if (BattleSceneManager.getInstance().CharacterCount == 2)
            {
                if (player[1]==null)
                {
                    Debug.Log(playerParams[2].name);
                    playerParams[2].SetEnemyAttack(myParams.GetRandomAttack());
                }
                else
                {
                    if (player[0]==null)
                    {
                        Debug.Log(playerParams[2].name);
                        playerParams[2].SetEnemyAttack(myParams.GetRandomAttack());
                    }
                    else
                    {
                        Debug.Log(playerParams[1].name);
                        playerParams[1].SetEnemyAttack(myParams.GetRandomAttack());
                    }
                }
            }
        }
        if (attack_flag == 3)
        {
            Debug.Log(playerParams[2].name);
            playerParams[2].SetEnemyAttack(myParams.GetRandomAttack());
        }
    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            //Debug.Log(BattleSceneManager.getInstance().CharacterCount);
            if (BattleSceneManager.getInstance().CharacterCount == 3)
            {
                //Debug.Log("캐릭터 숫자 3");
                MinDistancePlayer = Min(GetDistanceFromPlayer()[0], GetDistanceFromPlayer()[1], GetDistanceFromPlayer()[2]);
            }
            if (BattleSceneManager.getInstance().CharacterCount == 2)
            {
                //Debug.Log("캐릭터 숫자 2");
                MinDistancePlayer = Math.Min(GetDistanceFromPlayer2()[0], GetDistanceFromPlayer2()[1]);
            }
            if (BattleSceneManager.getInstance().CharacterCount == 1)
            {
                //Debug.Log("캐릭터 숫자 1");
                MinDistancePlayer = GetDistanceFromPlayer3()[0];
            }
            if (m_CurrentState == State.Dead) yield break;
           
            if (MinDistancePlayer <= traceDist)
            {
                m_CurrentState = State.Chase;
            }
            else
            {
                m_CurrentState = State.Idle;
            }
            if (MinDistancePlayer <= attackDist)
            {
                m_CurrentState = State.Attack;              
            }
           
            yield return null;
        }
    }
    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return null;
            switch (m_CurrentState)
            {
                case State.Idle:
                    myAni.ChangeAni(MonsterAni.IDLE);
                    break;
                case State.Chase:
                    if (BattleSceneManager.getInstance().CharacterCount == 3)
                    {
                        if (MinDistancePlayer == GetDistanceFromPlayer()[0])
                        {
                           // attack_flag = 1;
                            moveAgent.TraceTarget = player[0].transform.position;
                            //Debug.Log("1번 캐릭터감지");
                        }
                        if (MinDistancePlayer == GetDistanceFromPlayer()[1])
                        {
                            //attack_flag = 2;
                            moveAgent.TraceTarget = player[1].transform.position;
                            //Debug.Log("2번 캐릭터감지");
                        }
                        if (MinDistancePlayer == GetDistanceFromPlayer()[2])
                        {
                            //attack_flag = 3;
                            moveAgent.TraceTarget = player[2].transform.position;
                            //Debug.Log("3번 캐릭터감지");
                        }
                    }
                    if (BattleSceneManager.getInstance().CharacterCount == 2)
                    {
                        //if (MinDistancePlayer == GetDistanceFromPlayer2()[0])
                        //{
                        //    attack_flag = 1;
                        //    moveAgent.TraceTarget = player[0].transform.position;

                        //}
                        //if (MinDistancePlayer == GetDistanceFromPlayer2()[1])
                        //{
                        //    attack_flag = 2;
                        //    moveAgent.TraceTarget = player[1].transform.position;
                        //    //Debug.Log("2번 캐릭터감지");
                        //}

                        if (MinDistancePlayer == GetDistanceFromPlayer2()[0])
                        {
                            //attack_flag = 1;
                            if (player[0]==null)
                            {
                                moveAgent.TraceTarget = player[1].transform.position;
                            }
                            else
                            {
                                moveAgent.TraceTarget = player[0].transform.position;
                            }
                        }
                        if (MinDistancePlayer == GetDistanceFromPlayer2()[1])
                        {
                            //attack_flag = 2;
                            if (player[1]==null)
                            {
                                moveAgent.TraceTarget = player[2].transform.position;
                            }
                            else
                            {
                                moveAgent.TraceTarget = player[1].transform.position;
                            }                            
                            //Debug.Log("2번 캐릭터감지");
                        }

                    }
                    if (BattleSceneManager.getInstance().CharacterCount == 1)
                    {
                        if (MinDistancePlayer == GetDistanceFromPlayer3()[0])
                        {
                           // attack_flag = 1;
                            if (player[0]!=null)
                            {
                                moveAgent.TraceTarget = player[0].transform.position;
                            }
                            if (player[1] != null)
                            {
                                moveAgent.TraceTarget = player[1].transform.position;
                            }
                            if (player[2] != null)
                            {
                                moveAgent.TraceTarget = player[2].transform.position;
                            }
                        }                     
                    }
                    myAni.ChangeAni(MonsterAni.WALK);
                    break;
                case State.Attack:
                    moveAgent.Stop();
                    if (BattleSceneManager.getInstance().CharacterCount == 3)
                    {
                        if (MinDistancePlayer == GetDistanceFromPlayer()[0])
                        {                          
                            attack_flag = 1;
                            Quaternion rot = Quaternion.LookRotation(player[0].transform.position - cachedTransform.position);
                            cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                        }
                        if (MinDistancePlayer == GetDistanceFromPlayer()[1])
                        {                           
                            attack_flag = 2;
                            Quaternion rot = Quaternion.LookRotation(player[1].transform.position - cachedTransform.position);
                            cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                        }
                        if (MinDistancePlayer == GetDistanceFromPlayer()[2])
                        {                           
                            attack_flag = 3;
                            Quaternion rot = Quaternion.LookRotation(player[2].transform.position - cachedTransform.position);
                            cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                        }
                    }
                    if (BattleSceneManager.getInstance().CharacterCount == 2)
                    {
                        //if (player[0].GetComponent<PlayerState>().m_CurrentState == PlayerState.State.Dead)
                        //{
                        //    myAni.ChangeAni(MonsterAni.IDLE);
                        //    m_CurrentState = State.Idle;                           
                        //}
                        if (MinDistancePlayer == GetDistanceFromPlayer2()[0])
                        {                            
                            attack_flag = 1;
                            if (player[2] == null)
                            {
                                Quaternion rot = Quaternion.LookRotation(player[0].transform.position - cachedTransform.position);
                                cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                            }
                            if (player[1] == null)
                            {
                                Quaternion rot = Quaternion.LookRotation(player[0].transform.position - cachedTransform.position);
                                cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                            }
                            if (player[0] == null)
                            {
                                Quaternion rot = Quaternion.LookRotation(player[1].transform.position - cachedTransform.position);
                                cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                            }
                        }
                        if (MinDistancePlayer == GetDistanceFromPlayer2()[1])
                        {
                            attack_flag = 2;
                            if (player[2] == null)
                            {
                                Quaternion rot = Quaternion.LookRotation(player[1].transform.position - cachedTransform.position);
                                cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                            }
                            if (player[1] == null)
                            {
                                Quaternion rot = Quaternion.LookRotation(player[2].transform.position - cachedTransform.position);
                                cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                            }
                            if (player[0] == null)
                            {
                                Quaternion rot = Quaternion.LookRotation(player[2].transform.position - cachedTransform.position);
                                cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                            }
                        }                     
                    }
                    if (BattleSceneManager.getInstance().CharacterCount == 1)
                    {
                        if (MinDistancePlayer == GetDistanceFromPlayer3()[0])
                        {
                            attack_flag = 1;
                            if (player[0] != null)
                            {
                                Quaternion rot = Quaternion.LookRotation(player[0].transform.position - cachedTransform.position);
                                cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                            }
                            if (player[1] != null)
                            {
                                Quaternion rot = Quaternion.LookRotation(player[1].transform.position - cachedTransform.position);
                                cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                            }
                            if (player[2] != null)
                            {
                                Quaternion rot = Quaternion.LookRotation(player[2].transform.position - cachedTransform.position);
                                cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, rot, Time.deltaTime * 360f);
                            }
                        }
                       
                    }
                    if (attackstate == true)
                    {
                        myAni.ChangeAni(MonsterAni.ATTACK);
                    }
                    break;
                case State.NoState:

                    break;
                case State.Dead:
                    isDie = true;
                    moveAgent.Stop();
                    myAni.ChangeAni(MonsterAni.DIE);
                    GetComponent<SphereCollider>().enabled = false;
                    //myParams.isDead = true;
                    break;
            }

        }
    }
    void Update()
    {
        //if (MinDistancePlayer == GetDistanceFromPlayer()[0])
        //{
        //    Quaternion rot = Quaternion.LookRotation(player[0].transform.position - cachedTransform.position);
        //    cachedTransform.rotation = Quaternion.RotateTowards(cachedTransform.rotation, rot, Time.deltaTime * 360f);
        //}
    }

}