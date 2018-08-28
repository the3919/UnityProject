using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderSkillShot : MonoBehaviour {

    GameObject p;
    public int damage { get; set; }
    //총알 발사 속도
    public float speed = 1000.0f;
    public float range = 1f;
    void Awake()
    {
        p = GameObject.FindGameObjectWithTag("Soldier");
        damage = p.GetComponent<PlayerParams>().GetRandomAttack()+50;
    }

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    } 
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, range);
    }
}
