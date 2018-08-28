using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEventControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void SendAttackEnemy()
    {
        transform.parent.gameObject.SendMessage("AttackCalculate");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
