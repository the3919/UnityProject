using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    //총알의 파괴력
    GameObject p;
    public int damage { get; set; }
    //총알 발사 속도
    public float speed = 1000.0f;
    void Awake()
    {
        p = GameObject.FindGameObjectWithTag("Mage");
        damage = p.GetComponent<PlayerParams>().GetRandomAttack();
    }

    void Start()
    {        
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }
}