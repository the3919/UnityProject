using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour {

    // Use this for initialization
    public GameObject bullet;
    public Transform firePos;

    void Update()
    {
        //마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        if (Input.GetKeyDown("space"))
        {
            Fire();
        }
    }

    void Fire()
    {
        //Bullet 프리팹을 동적으로 생성
        Instantiate(bullet, firePos.position, firePos.rotation);        
      
    }
}
