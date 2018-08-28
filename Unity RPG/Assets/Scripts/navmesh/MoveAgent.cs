using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    private readonly float traceSpeed = 2.0f;
    private float damping = 1.0f;
    private NavMeshAgent agent;
    private Vector3 traceTarget_;
    private Transform enemyTransform;
    // Use this for initialization
    public Vector3 TraceTarget
    {
        get { return traceTarget_; }
        set
        {
            traceTarget_ = value;
            agent.speed = traceSpeed;
            damping = 7.0f;
            TraceCharacter(traceTarget_);
        }
    }

    void Start()
    {
        enemyTransform = GetComponent<Transform>();
        //NavMeshAgent 컴포넌트를 추출한 후 변수에 저장
        agent = GetComponent<NavMeshAgent>();
        //목적지에 가까워질수록 속도를 줄이는 옵션을 비활성화
        agent.autoBraking = false;
        //자동으로 회전하는 기능을 비활성화
        agent.updateRotation = false;
        agent.speed = traceSpeed;
    }
    void TraceCharacter(Vector3 pos)
    {
        if (agent.isPathStale) return;
        agent.destination = pos;
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
        //바로 정지하기 위해 속도를 0으로 설정
        agent.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isStopped == false && agent.desiredVelocity.magnitude > 0.01f)
        {
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, rot, Time.deltaTime * damping);
        }
    }
}
