using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // public Image m_ShieldImageCoolTime;
    public List<Image> SkillImage = new List<Image>();
    public List<Image> m_ShieldImageCoolTime = new List<Image>();
    public Text[] PlayerName = new Text[3];
    public List<Text> PlayerName2 = new List<Text>();
    public Image[] PlayerHpBar = new Image[3];
    public Image PlayerShieldBar;
    public static UIManager instance;
    private static int NameCount = 0;
    private static int HpBarCount = 0;
    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void UpdatePlayerText(PlayerParams playerParams)
    {
        PlayerName[NameCount].text = playerParams.name;

        PlayerHpBar[HpBarCount].rectTransform.localScale =
             new Vector3((float)playerParams.m_cStatus.m_nHp / (float)playerParams.maxHp, 1f, 1f);
      
        NameCount++;
        HpBarCount++;
        if (NameCount == 3)
        {
            NameCount = 0;
        }
        if (HpBarCount == 3)
        {
            HpBarCount = 0;
        }
    }
    public void UpdatePlayerUI(PlayerParams playerParams)
    {      
        if (playerParams.m_CurrentJob == PlayerParams.Job.Warrior)
        {
            // PlayerName[0].text = playerParams.name;
            //PlayerHpBar[0].rectTransform.localScale =
            //    new Vector3((float)playerParams.m_cStatus.m_nHp / (float)playerParams.maxHp, 1f, 1f);
            Debug.Log("워리어 UI업데이트");
            PlayerHpBar[BattleSceneManager.getInstance().CharacterFirstIndex].rectTransform.localScale =
            new Vector3((float)playerParams.m_cStatus.m_nHp / (float)playerParams.maxHp, 1f, 1f);
            if (PlayerShieldBar.gameObject.activeSelf)
            {
                PlayerShieldBar.rectTransform.localScale =
                     new Vector3((float)playerParams.Shield / (float)playerParams.MaxShield, 1f, 1f);
            }
        }
        if (playerParams.m_CurrentJob == PlayerParams.Job.Wizard)
        {
            // PlayerName[1].text = playerParams.name;
            Debug.Log("위저드 UI업데이트");
            PlayerHpBar[BattleSceneManager.getInstance().CharacterSecondIndex].rectTransform.localScale =
                new Vector3((float)playerParams.m_cStatus.m_nHp / (float)playerParams.maxHp, 1f, 1f);
        }
        if (playerParams.m_CurrentJob == PlayerParams.Job.Soldier)
        {
            //PlayerName[2].text = playerParams.name;
            Debug.Log("솔져 UI업데이트");
            PlayerHpBar[BattleSceneManager.getInstance().CharacterThirdIndex].rectTransform.localScale =
                new Vector3((float)playerParams.m_cStatus.m_nHp / (float)playerParams.maxHp, 1f, 1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
