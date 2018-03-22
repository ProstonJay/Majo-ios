using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour {
    //
    public static GameEvent Instance = null;
    //
    public delegate void MsgDelegate(string message);
    public static event MsgDelegate MsgEvent;
    public static event MsgDelegate MsgTipEvent;
    public static event MsgDelegate SenceEvent;
    public static event MsgDelegate SocketConnetEvent;
    public static event MsgDelegate StringEvent; //改昵称
    public static event MsgDelegate PicEvent; //改头像

    public delegate void RoomDelegate(string pos,string mjid);//出牌区添加打出的牌
    public static event RoomDelegate PlayEvent;

    public delegate void PlayerEnterRoom(int postion, string name);//玩家加入房间
    public static event PlayerEnterRoom PlayerEnterRoomEvent;

    public delegate void GameDelegate(int type);//牌局开始
    public static event GameDelegate GameStartEvent;

    public delegate void MoPai(int data);//摸牌
    public static event MoPai MoPaiEvent;

    public delegate void ChuPai(int pos, int data,Boolean iskeep);//出牌
    public static event ChuPai ChuPaiEvent;

    public delegate void JieSuan();//小结算
    public static event JieSuan JieSuanEvent;

    public delegate void ZongJieSuan();//总结算
    public static event ZongJieSuan ZongJieSuanEvent;

    public delegate void ReSetRoom();//重置
    public static event ReSetRoom ReSetRoomEvent;

    public delegate void NetSocket(int value);//通讯
    public static event NetSocket NetSocketEvent;

    public delegate void ChatSocket(int pos,int value);//聊天
    public static event ChatSocket ChatSocketEvent;

    public delegate void UpdateFkSocket();
    public static event UpdateFkSocket UpdateFkEvent;

    ///更新钻石
    public static void UpdateFk()
    {
        if (UpdateFkEvent != null)
        {
            UpdateFkEvent();
        }
        else
        {
            Debug.Log("UpdateFkEvent is Empty");
        }

    }

    ///聊天
    public static void DoChat(int pos, int value)
    {
        if (ChatSocketEvent != null)
        {
            ChatSocketEvent(pos,value);
        }
        else
        {
            Debug.Log("ChatSocketEvent is Empty");
        }

    }

    ///通讯
    public static void DoNetSocket(int value)
    {
        if (NetSocketEvent != null)
        {
            NetSocketEvent(value);
        }
        else
        {
            Debug.Log("NetSocketEvent is Empty");
        }

    }

    //总结算
    public static void DoZongJieSuan()
    {
        if (ZongJieSuanEvent != null)
        {
            ZongJieSuanEvent();
        }
        else
        {
            Debug.Log("ZongJieSuanEvent is Empty");
        }

    }

    //重置
    public static void DoReSetRoom()
    {
        if (ReSetRoomEvent != null)
        {
            ReSetRoomEvent();
        }
        else
        {
            Debug.Log("JieSuanEvent is Empty");
        }

    }


    //结算
    public static void DoJieSuan()
    {
        if (JieSuanEvent != null)
        {
            JieSuanEvent();
        }
        else
        {
            Debug.Log("JieSuanEvent is Empty");
        }

    }

    //出牌
    public static void DoChuPai(int pos, int data , Boolean iskeep)
    {
        if (ChuPaiEvent != null)
        {
            ChuPaiEvent(pos, data, iskeep);
        }
        else
        {
            Debug.Log("ChuPaiEvent is Empty");
        }
    }

    //摸牌
    public static void DoMoPai(int data)
    {
        if (MoPaiEvent != null)
        {
            MoPaiEvent(data);
        }
        else
        {
            Debug.Log("MoPaiEvent is Empty");
        }
    }

    //玩家进入房间
    public static void DoPlayerEnterRoom(int postion, string name)
    {
        if (PlayerEnterRoomEvent != null)
        {
            PlayerEnterRoomEvent(postion, name);
        }
        else
        {
            Debug.Log("PlayerEnterRoomEvent is Empty");
        }
    }

    public static void DoPlayEvent(string pos,string mjid)
    {
        if (PlayEvent != null)
        {
            PlayEvent(pos,mjid);
        }
        else
        {
            Debug.Log("PlayEvent is Empty");
        }
    }

        public static void DoSenceEvent(string message)
    {
        if (SenceEvent != null)
        {
            SenceEvent(message);
        }
        else
        {
            Debug.Log("SenceEvent is Empty");
        }
    }

        public static void DoMsgEvent(string message)
    {
        if (MsgEvent != null)
        {
            MsgEvent(message);
        }
        else
        {
            Debug.Log("MsgEvent is Empty");
        }
      
    }

    public static void DoMsgTipEvent(string message)
    {
        if (MsgTipEvent != null)
        {
            MsgTipEvent(message);
        }
        else
        {
            Debug.Log("MsgTipEvent is Empty");
        }

    }

    public static void DoGameStartEvent(int type)
    {
        if (GameStartEvent != null)
        {
            GameStartEvent(type);
        }
        else
        {
            Debug.Log("GameStartEvent is Empty");
        }

    }

    public static void DoSocketConnetEvent(string str)
    {
        if (SocketConnetEvent != null)
        {
            SocketConnetEvent(str);
        }
        else
        {
            Debug.Log("SocketConnetEvent is Empty");
        }

    }

    public static void DoStringEvent(string str)
    {
        if (StringEvent != null)
        {
            StringEvent(str);
        }
        else
        {
            Debug.Log("StringEvent is Empty");
        }

    }
    
        public static void DoPicEvent(string str)
        {
            if (PicEvent != null)
            {
                 PicEvent(str);
            }
            else
            {
                Debug.Log("PicEvent is Empty");
            }

         }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(transform.gameObject);
            }
        }
    }
}
