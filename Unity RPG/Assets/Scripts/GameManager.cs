using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public PlayerParams[] m_Character;  

    public SkillManager m_cskillManager;
    public GameObject[] m_Character;
    private static GameManager instance;
    public static GameManager getInstance()
    {
        return instance;
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }    // Use this for initialization
    void Start()
    {
        m_Character[0].GetComponent<PlayerParams>().Set("워리어", 1, 5600, 10, 13, 1);
        m_Character[1].GetComponent<PlayerParams>().Set("마법사", 1, 5600, 10, 13, 1);
        m_Character[2].GetComponent<PlayerParams>().Set("솔져", 1, 5600, 10, 13, 1);
    }
    void OnApplicationQuit()
    {
        instance = null;
    }
}