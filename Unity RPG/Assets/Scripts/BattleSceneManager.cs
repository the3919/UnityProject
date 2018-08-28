using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BattleSceneManager : MonoBehaviour
{
    public int CharacterFirstIndex;
    public int CharacterSecondIndex;
    public int CharacterThirdIndex;

    public OrbitCameraC camara3;
    public int CharacterCount = 3;
    public List<GameObject> Character = new List<GameObject>();
    //public List<GameObject> m_cam = new List<GameObject>();
    //public PlayerParams[] m_Character;
    [SerializeField]
    //public GameObject m_mainCamera;
    public GameObject mainCharacter;
    //public SkillManager m_cskillManager;   
    public Button[] button;
    public List<Image> ShieldBar = new List<Image>(3);
    [SerializeField]
    private List<Image> SkillObject = new List<Image>(6);
    private static BattleSceneManager instance;
    public static BattleSceneManager getInstance()
    {
        return instance;
    }
    public bool CameraOne()
    {        
        button[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
        button[1].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        button[2].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        mainCharacter = Character[0];
        if (PlayerParams.Job.Warrior == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(true);
                SkillObject[i + 2].gameObject.SetActive(false);
                SkillObject[i + 4].gameObject.SetActive(false);
            }
        }
        if (PlayerParams.Job.Wizard == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(false);
                SkillObject[i + 2].gameObject.SetActive(true);
                SkillObject[i + 4].gameObject.SetActive(false);
            }
        }
        if (PlayerParams.Job.Soldier == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(false);
                SkillObject[i + 2].gameObject.SetActive(false);
                SkillObject[i + 4].gameObject.SetActive(true);
            }
        }
        return true;
    }
    public bool CameraTwo()
    {      
        button[0].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        button[1].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
        button[2].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //mainCharacter = Character[1];
        Debug.Log(mainCharacter.name);
        if (PlayerParams.Job.Warrior == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(true);
                SkillObject[i + 2].gameObject.SetActive(false);
                SkillObject[i + 4].gameObject.SetActive(false);
            }
        }
        if (PlayerParams.Job.Wizard == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(false);
                SkillObject[i + 2].gameObject.SetActive(true);
                SkillObject[i + 4].gameObject.SetActive(false);
            }
        }
        if (PlayerParams.Job.Soldier == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(false);
                SkillObject[i + 2].gameObject.SetActive(false);
                SkillObject[i + 4].gameObject.SetActive(true);
            }
        }
        return true;
    }
    public bool CameraThree()
    {     
        button[0].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        button[1].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        button[2].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
        //mainCharacter = Character[2];
        Debug.Log(mainCharacter.name);
        if (PlayerParams.Job.Warrior == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(true);
                SkillObject[i + 2].gameObject.SetActive(false);
                SkillObject[i + 4].gameObject.SetActive(false);
            }
        }
        if (PlayerParams.Job.Wizard == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(false);
                SkillObject[i + 2].gameObject.SetActive(true);
                SkillObject[i + 4].gameObject.SetActive(false);
            }
        }
        if (PlayerParams.Job.Soldier == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(false);
                SkillObject[i + 2].gameObject.SetActive(false);
                SkillObject[i + 4].gameObject.SetActive(true);
            }
        }
        return true;
    }
    void Awake()
    {
        if (instance == null)
            instance = this;               
    }
    // Use this for initialization
    void Start()
    {
        CharacterCount = 3;
        //if (Character.Count <= 1)
        //{
        //    Character[0].GetComponent<PlayerParams>().Load();
        //}
        //if (Character.Count <= 2 &&)
        //{
        //    Character[0].GetComponent<PlayerParams>().Load();
        //    Character[1].GetComponent<PlayerParams>().Load();
        //}

       
        if (Character.Count <= 3)
        {
            Character[0].GetComponent<PlayerParams>().Load();
            Character[1].GetComponent<PlayerParams>().Load();
            Character[2].GetComponent<PlayerParams>().Load();
        }    
        mainCharacter = Character[0];       
        button[0].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
        button[1].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        button[2].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        for (int i = 0; i < 6; i++)
        {
            SkillObject[i] = UIManager.instance.SkillImage[i];
        }
        for (int j = 0; j < Character.Count; j++)
        {
            if (Character[j].GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Warrior)
            {
                CharacterFirstIndex = j;
                UIManager.instance.PlayerShieldBar = ShieldBar[CharacterFirstIndex];
                Debug.Log("1번째 인덱스" + CharacterFirstIndex);
            }
            if (Character[j].GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Wizard)
            {
                CharacterSecondIndex = j;
                Debug.Log("2번째 인덱스" + CharacterSecondIndex);
            }
            if (Character[j].GetComponent<PlayerParams>().m_CurrentJob == PlayerParams.Job.Soldier)
            {
                CharacterThirdIndex = j;
                Debug.Log("3번째 인덱스" + CharacterThirdIndex);
            }
        }
        if (PlayerParams.Job.Warrior == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
                 
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(true);
                SkillObject[i + 2].gameObject.SetActive(false);
                SkillObject[i + 4].gameObject.SetActive(false);
            }
        }
        if (PlayerParams.Job.Wizard == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
          
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(false);
                SkillObject[i + 2].gameObject.SetActive(true);
                SkillObject[i + 4].gameObject.SetActive(false);
            }
        }
        if (PlayerParams.Job.Soldier == mainCharacter.GetComponent<PlayerParams>().m_CurrentJob)
        {
            for (int i = 0; i < 2; i++)
            {
                SkillObject[i].gameObject.SetActive(false);
                SkillObject[i + 2].gameObject.SetActive(false);
                SkillObject[i + 4].gameObject.SetActive(true);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterCount==0)
        {
            SceneManager.LoadScene("CharacterRegister");
        }
        if (MonsterState.DeadMonster == 4)
        {
            MonsterState.DeadMonster = 0;
            SceneManager.LoadScene("CharacterRegister");
        }
      
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (Character[0]!=null&&hit.collider.gameObject.name == "Plane" && mainCharacter == Character[0])
                {
                    Character[0].GetComponent<PlayerState>().MoveTo(hit.point);
                }
                if (Character[1] != null && hit.collider.gameObject.name == "Plane" && mainCharacter == Character[1])
                {
                   Character[1].GetComponent<PlayerState>().MoveTo(hit.point);
                }
                if (Character[2] != null && hit.collider.gameObject.name == "Plane" && mainCharacter == Character[2])
                {
                   Character[2].GetComponent<PlayerState>().MoveTo(hit.point);
                }
                if (Character[0] != null && (hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "Boss") && mainCharacter == Character[0])
                {
                    //Debug.Log("공격");
                    Character[0].GetComponent<PlayerState>().AttackEnemy(hit.collider.gameObject);
                }
                if (Character[1] != null && (hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "Boss") && mainCharacter == Character[1])
                {
                    Character[1].GetComponent<PlayerState>().AttackEnemy(hit.collider.gameObject);
                }
                if (Character[2] != null && (hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "Boss") && mainCharacter == Character[2])
                {
                    Character[2].GetComponent<PlayerState>().AttackEnemy(hit.collider.gameObject);
                }
                //Debug.Log(hit.collider.gameObject.name);
            }
        }
                
        

    }
}
