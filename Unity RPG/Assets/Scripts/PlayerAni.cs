using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour {

    public const int ANI_IDLE = 0;
    public const int ANI_RUN = 1;
    public const int ANI_ATTACK = 2;
    public const int ANI_ATTACKIDLE = 3;
    public const int ANI_DIE = 4;
    Animator anim;
    public void ChangeAni(int number)
    {        
        //anim.SetInteger("AniMain", number);
        anim.SetInteger("main", number);
    }
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
