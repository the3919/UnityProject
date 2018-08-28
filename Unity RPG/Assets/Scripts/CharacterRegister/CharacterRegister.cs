using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterRegister : MonoBehaviour
{
    public Image CharacterPanel;
    public List<GameObject> PlayerCharacter = new List<GameObject>();
    public Button[] BattleCharacterbtn = new Button[3];
    public Button[] PlayerCharacterbtn = new Button[3];
    public Text[] txt = new Text[3];
    public Text[] txt2 = new Text[3];
    //List<GameObject> m_Character = new List<GameObject>();
    [SerializeField]
    public static GameObject[] Character;    
    //public static List<GameObject> m_Character = new List<GameObject>();
    int num;
  
    void Start()
    {
        Character = new GameObject[3];
        for (int i = 0; i<GameManager.getInstance().m_Character.Length; i++)
        {
            PlayerCharacter[i] = GameManager.getInstance().m_Character[i];
        }
    }
    // Use this for initialization
    public void CharacterSelectNum(int select)
    {
        CharacterPanel.gameObject.SetActive(true);
        num = select;        
    }
    public void CharacterRegisterOne(int select)
    {
        for (int i = 0; i < Character.Length; i++)
        {
            if (Character[i] == PlayerCharacter[select])
            {
                Debug.Log("중복");
                return;
            }
        }
        txt[num] = BattleCharacterbtn[num].GetComponentInChildren<Text>();    
        txt2[select] = PlayerCharacterbtn[select].GetComponentInChildren<Text>();
        //m_Character.Add(PlayerCharacter[select]);
        Character[num] = PlayerCharacter[select];
        txt[num].text = txt2[select].text;               
        CharacterPanel.gameObject.SetActive(false);
    }
    public void PanelExit()
    {
        CharacterPanel.gameObject.SetActive(false);
    }
    public void EntranceBattle()
    {
        //Application.LoadLevel("");
        if (Character[num] != null)
        {
            SceneManager.LoadScene("BattleScene");
        }
    }
}
