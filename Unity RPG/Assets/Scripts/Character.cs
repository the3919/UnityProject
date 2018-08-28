using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Creature
{

    //public GameObject m_nPlayer;

    //public enum eEqumentKind { Weapon, Armor, Acc, MAX }
    //List<Item> m_listEqument = new List<Item>((int)eEqumentKind.MAX);
    //// Use this for initialization

    //public void SetEquemnt(Item item) //아이템장착
    //{


    //    //장비아이템일때만 해당 아이템을 셋팅한다.
    //    if (item.ItemKind < Item.eItemKind.Etc)
    //    {
    //        ReleaseEquemnt((eEqumentKind)item.ItemKind);
    //        //장비할아이템을 장착하고, 능력치를 증가시킨다.
    //        m_listEqument[(int)eEqumentKind.Weapon] = item;
    //        m_nStatus += item.Function;
    //        m_nPlayer.GetComponent<Player>().DeleteIventory(item);
    //    }
    //}
    //public void ReleaseEquemnt(eEqumentKind eEqument)
    //{
    //    Item cEqumentItem = m_listEqument[(int)eEqument];

    //    if (cEqumentItem != null)
    //    {
    //        m_nPlayer.GetComponent<Player>().SetIventory(cEqumentItem);
    //        m_nStatus -= cEqumentItem.Function;
    //        m_nPlayer.GetComponent<Player>().DeleteIventory(cEqumentItem);
    //    }
    //} //아이템해제
}
