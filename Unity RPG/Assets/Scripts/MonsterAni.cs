using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAni : MonoBehaviour {

    public const string IDLE = "Idle";
    public const string WALK = "Run";
    public const string ATTACK = "Attack_new";
    public const string DIE = "Death";


    // Use this for initialization
    Animation anim;
    MonsterState mons;
    public void ChangeAni(string aniName)
    {
        anim.CrossFade(aniName);
        if (aniName == ATTACK)
        {
            mons.attackstate = false;
            StartCoroutine(AttackWait());
        }
    }
    IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(1.0f);
        mons.attackstate = true;
    }
    void Start()
    {
        // anim = GetComponentInChildren<Animator>();
        anim = GetComponentInChildren<Animation>();
        mons = GetComponent<MonsterState>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
