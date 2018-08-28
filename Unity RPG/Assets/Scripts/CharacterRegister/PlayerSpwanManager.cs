using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpwanManager : MonoBehaviour
{  
    List<Transform> spawnPos = new List<Transform>();    
   
    void Start()
    {
       
        foreach (Transform pos in transform)
        {
            spawnPos.Add(pos);
        }
        for (int i = 0; i < CharacterRegister.Character.Length; i++)
        {
            if (i == 0)
            {
                BattleSceneManager.getInstance().CharacterFirstIndex = i;
            }
            if (i == 1)
            {
                BattleSceneManager.getInstance().CharacterSecondIndex = i;
            }
            if (i == 2)
            {
                BattleSceneManager.getInstance().CharacterThirdIndex = i;
            }
            BattleSceneManager.getInstance().Character.Add(Instantiate(CharacterRegister.Character[i], spawnPos[i].position, Quaternion.identity));
        }
        BattleSceneManager.getInstance().camara3.GetComponent<OrbitCameraC>().target = BattleSceneManager.getInstance().Character[0].transform;
    }
	
}
