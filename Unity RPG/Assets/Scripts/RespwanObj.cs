using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespwanObj : MonoBehaviour {

    List<Transform> spawnPos = new List<Transform>();
    GameObject[] monsters;

    public GameObject monPrefab;
    public GameObject BossPrefab;
    public int spawnNumber = 1;    
    public float respawnDelay = 3f;

    int deadMonsters = 0;

    void Start()
    {
        foreach (Transform pos in transform)
        {
            if (pos.tag == "Respawn")
            {
                spawnPos.Add(pos);
            }
        }
        if (spawnNumber > spawnPos.Count)
        {
            spawnNumber = spawnPos.Count;
        }
        monsters = new GameObject[spawnNumber];
        MakeMonster();
    }
    void MakeMonster()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            if (i == 3)
            {
                GameObject mon = Instantiate(BossPrefab, spawnPos[i].position, Quaternion.identity) as GameObject;

                mon.GetComponent<MonsterState>().SetRespwanObj(gameObject, i, spawnPos[i].position);
                mon.SetActive(false);
                monsters[i] = mon;
            }
            else
            {
                GameObject mon = Instantiate(monPrefab, spawnPos[i].position, Quaternion.identity) as GameObject;
                mon.GetComponent<MonsterState>().SetRespwanObj(gameObject, i, spawnPos[i].position);
                mon.SetActive(false);
                monsters[i] = mon;
            }
        }
    }   
    //public void RemoveMonster(int id)
    //{
    //    //Destory(monsters[id]);
    //    deadMonsters++;
    //    monsters[id].SetActive(false);
    //    if (deadMonsters == monsters.Length)
    //    {
    //        StartCoroutine(InitMonsters());
    //        deadMonsters = 0;
    //    }
    //}
    //IEnumerator InitMonsters()
    //{
    //    yield return new WaitForSeconds(respawnDelay);
    //    GetComponent<SphereCollider>().enabled = true;
    //}

    void SpawnMonster()
    {
        for (int i = 0; i < monsters.Length; i++)
	    {
            monsters[i].SetActive(true);
        }

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Warrior" || col.gameObject.tag == "Soldier" || col.gameObject.tag == "Mage")
        {
            Debug.Log("작동");
            SpawnMonster();
            GetComponent<SphereCollider>().enabled = false;
        }
    }
    void Update()
    {

    }

}
