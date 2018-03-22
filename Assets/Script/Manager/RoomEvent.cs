using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent : MonoBehaviour {

    public static RoomEvent Instance = null;

    //暗杠
    public delegate void ActionAnGang(string pos, int mj);
    public static event ActionAnGang AnGangEvent;

    //明杠
    public delegate void ActionMingGang(string pos, int mj);
    public static event ActionMingGang MingGangEvent;

    //直杠
    public delegate void ActionZhiGang(string pos, int mj, int fangGangPos);
    public static event ActionZhiGang ZhiGangEvent;

    //碰牌
    public delegate void ActionPengPai(string pos, int mj);
    public static event ActionPengPai PengPaiEvent;

    //自摸
    public delegate void ActionZiMo(string pos, int mj,List<PlayerData> list);
    public static event ActionZiMo ZiMoEvent;

    //吃胡
    public delegate void ActionChiHu(string pos, int mj, int fangpao, List<PlayerData> list);
    public static event ActionChiHu ChiHuEvent;

    //可操作列表
    public delegate void ActionList(List<int> list);
    public static event ActionList ActionListEvent;

    //当前操作位置方向
    public delegate void PlayerDiction(int pos, int clearOutMj);
    public static event PlayerDiction PlayerDictionEvent;

    //摸牌
    public delegate void ActionMoPai();
    public static event ActionMoPai ActionMoPaiEvent;


    //更新剩余牌的数量
    public delegate void ActionLeftMj(int value);
    public static event ActionLeftMj LeftMjEvent;

    //流局
    public delegate void ActionLiuJu(List<PlayerData> list);
    public static event ActionLiuJu LiuJuEvent;

    //换桌面
    public delegate void ChangeTableColor();
    public static event ChangeTableColor ChangeTableColorEvent;

    //断线
    public delegate void PlayerOffLine(int pos);
    public static event PlayerOffLine PlayerOffLineEvent;

    //上线
    public delegate void PlayerOnLine(int pos);
    public static event PlayerOnLine PlayerOnLineEvent;

    //玩家准备 
    public delegate void PlayerReady(string pos);
    public static event PlayerReady PlayerReadyEvent;

    //玩家发起解散房间
    public delegate void InitiateDisMiss(string pname);
    public static event InitiateDisMiss InitiateDisMissEvent;

    //投票结果
    public delegate void VoteDisResults(int cmod);
    public static event VoteDisResults VoteDisResultsEvent;

    //投票结果
    public static void DoVoteDisResults(int cmod)
    {
        if (VoteDisResultsEvent != null)
        {
            VoteDisResultsEvent(cmod);
        }
        else
        {
            Debug.Log("VoteDisResultsEvent is Empty");
        }
    }

    //玩家发起解散房间
    public static void DoInitiateDisMiss(string pname)
    {
        if (InitiateDisMissEvent != null)
        {
            InitiateDisMissEvent(pname);
        }
        else
        {
            Debug.Log("InitiateDisMissEvent is Empty");
        }
    }

    //玩家准备
    public static void DoPlayerReady(string pos)
    {
        if (PlayerReadyEvent != null)
        {
            PlayerReadyEvent(pos);
        }
        else
        {
            Debug.Log("PlayerReadyEvent is Empty");
        }
    }

    //上线
    public static void DoPlayerOnLine(int pos)
    {
        if (PlayerOnLineEvent != null)
        {
            PlayerOnLineEvent(pos);
        }
        else
        {
            Debug.Log("PlayerOnLineEvent is Empty");
        }
    }

    //断线
    public static void DoPlayerOffLine(int pos)
    {
        if (PlayerOffLineEvent != null)
        {
            PlayerOffLineEvent(pos);
        }
        else
        {
            Debug.Log("PlayerOffLineEvent is Empty");
        }
    }

    //换桌面
    public static void DoChangeTableColor()
    {
        if (ChangeTableColorEvent != null)
        {
            ChangeTableColorEvent();
        }
        else
        {
            Debug.Log("ChangeTableColorEvent is Empty");
        }
    }

    //流局
    public static void DoActionLiuJu(List<PlayerData> list)
    {
        if (LiuJuEvent != null)
        {
            LiuJuEvent(list);
        }
        else
        {
            Debug.Log("LiuJuEvent is Empty");
        }
    }

    //更新剩余牌的数量
    public static void DoActionLeftMj(int value)
    {
        if (LeftMjEvent != null)
        {
            LeftMjEvent(value);
        }
        else
        {
            Debug.Log("ActionLeftMjEvent is Empty");
        }
    }

    //摸牌
    public static void DoActionMoPai()
    {
        if (ActionMoPaiEvent != null)
        {
            ActionMoPaiEvent();
        }
        else
        {
            Debug.Log("ActionMoPaiEvent is Empty");
        }
    }

    //当前操作位置方向
    public static void DoPlayerDiction(int pos, int clearOutMj=0)
    {
        if (PlayerDictionEvent != null)
        {
            PlayerDictionEvent(pos, clearOutMj);
        }
        else
        {
            Debug.Log("PlayerDictionEvent is Empty");
        }
    }

    //可操作列表
    public static void DoActionList(List<int> list)
    {
        if (ActionListEvent != null)
        {
            ActionListEvent(list);
        }
        else
        {
            Debug.Log("ActionListEvent is Empty");
        }
    }

    //吃胡
    public static void DoChiHu(string pos, int mj, int fangpao, List<PlayerData> list)
    {
        if (ChiHuEvent != null)
        {
            ChiHuEvent(pos, mj, fangpao, list);
        }
        else
        {
            Debug.Log("ChiHuEvent is Empty");
        }
    }

    //自摸
    public static void DoZiMo(string pos, int mj, List<PlayerData> list)
    {
        if (ZiMoEvent != null)
        {
            ZiMoEvent(pos, mj, list);
        }
        else
        {
            Debug.Log("ZiMoEvent is Empty");
        }
    }

    //碰牌
    public static void DoPengPai(string pos, int mj)
    {
        if (PengPaiEvent != null)
        {
            PengPaiEvent(pos, mj);
        }
        else
        {
            Debug.Log("PengPaiEvent is Empty");
        }
    }

    //直杠
    public static void DoZhiGang(string pos, int mj, int fangGangPos)
    {
        if (ZhiGangEvent != null)
        {
            ZhiGangEvent(pos, mj, fangGangPos);
        }
        else
        {
            Debug.Log("MingGangEvent is Empty");
        }

    }

    //暗杠
    public static void DoAnGang(string pos, int mj)
    {
        if (AnGangEvent != null)
        {
            AnGangEvent(pos, mj);
        }
        else
        {
            Debug.Log("AnGangEvent is Empty");
        }

    }

    //明杠
    public static void DoMingGang(string pos, int mj)
    {
        if (MingGangEvent != null)
        {
            MingGangEvent(pos, mj);
        }
        else
        {
            Debug.Log("MingGangEvent is Empty");
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        if (Instance == null)
        {
            Debug.Log(" RoomEvent Instance == this");
            Instance = this;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Debug.Log("RoomEvent Instance ！= this");
                Destroy(transform.gameObject);
            }
        }
    }
}
