using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

    public static GameInfo Instance = null;

    /// <summary>
    /// 全局用户数据
    /// </summary>
    public string ToKen;//验证码
    public string DeviceID; //设备唯一ID
    public int UserID; //游戏唯一ID
    public string UserName; //昵称
    public int UserIcon; //头像
    public int UserFK; //房卡
    public string sex;// boy girl
    public List<MailData> mailList = new List<MailData>();//从大到小
    public List<BattleData> battleList = new List<BattleData>();//从大到小

    /// <summary>
    /// 游戏房间数据
    /// </summary>
    public int roomId; //房间号
    public int roomPassWord; //房间密码
    public int hostId; //房主ID
    public int maxRound; //最大圈数
    public int maxPoint; //最大番数
    public int isZjDouble; //庄家加倍
    public int canQiangGang; //抢杠胡
    public int isDaiGen; //带根
    public int isZiMoHu; //自摸胡
    
    public SocketModel skm;//重连数据
    public int VoteDisFlag;//解散标记，禁止重复申请

    //
    public int zhuangjia; //当前庄家的位置(1-4)
    public int round; //当前圈数
    public int mjLeft; //剩余牌数量
    public int actionFlag;//当前操作玩家位置
    //
    public int isEndStat;//标记是否可以显示结算
    public int isGameStart;//标记游戏是否开始
    public List<PlayerData> jieSuanRoundData;//小结算数据
    public List<PlayerData> jieSuanEndData;//总结算数据

    /// <summary>
    /// 游戏内个人数据
    /// </summary>
    public int isMyReady;//0.No 1.Yes
    public int positon ; //位置(1234--东南西北)
    public int myGold;//金币数量
    public List<int> myHandMj = new List<int>();//从大到小
    public List<Action> myAcionList = new List<Action>();//操作牌型的列表
    //每回合临时缓存
    public List<Action> gangList = new List<Action>();//杠牌牌型的列表

    /// <summary>
    /// 右边玩家数据
    /// </summary>
    public int rightPositon;
    public string rightName;
    public string rightIcon;
    public int rightGlod;
    public int isRightReady;//0.No 1.Yes
    public List<Action> rightAcionList = new List<Action>();//操作牌型的列表

    /// <summary>
    /// 对家玩家数据
    /// </summary>
    public int topPostion;
    public string topName;
    public string topIcon;
    public int topGold;
    public int isTopReady;//0.No 1.Yes
    public List<Action> topAcionList = new List<Action>();//操作牌型的列表

    /// <summary>
    /// 左边玩家数据
    /// </summary>
    public int leftPostion;
    public string leftName;
    public string leftIcon;
    public int leftGold;
    public int isLeftReady;//0.No 1.Yes
    public List<Action> leftAcionList = new List<Action>();//操作牌型的列表

    //是否在操作回合,只有在自己摸牌的时候才能操作
    public Boolean PlayFlag;

    //更新用户钻石
    public void setUserFk(int value)
    {
        this.UserFK = value;
        GameEvent.UpdateFk();
    }

    //小结算重置
    public void reset()
    {
        VoteDisFlag = 0;
        mjLeft = 0;
        PlayFlag = false;
        zhuangjia = 0;
        gangList.Clear();
        myAcionList.Clear();
        rightAcionList.Clear();
        topAcionList.Clear();
        leftAcionList.Clear();
        myHandMj.Clear();
    }

    //大结算重置
    public void resetAll()
    {
        roomId = 0;
        maxRound = 0;
        maxPoint = 0;
        round = 0;
        positon = 0;
        myGold = 0;
        //
        rightPositon = 0;
        rightName = "";
        rightIcon = "";
        rightGlod = 0;
        //
        topPostion = 0;
        topName = "";
        topIcon = "";
        topGold = 0;
        //
        leftPostion = 0;
        leftName = "";
        leftIcon = "";
        leftGold = 0;
        //
        reset();
    }

    //玩家准备了
    public void PlayerGetReady(int rdPos)
    {
        string pos = "";
        if (GameInfo.Instance.positon == rdPos)
        {
            pos = "bot";
        }
        else
        {
            pos = TryGetLocPos(GameInfo.Instance.positon,rdPos);
        }
        switch (pos)
        {
            case "bot":
                isMyReady = 1;
                break;
            case "right":
                isRightReady = 1;
                break;
            case "top":
                isTopReady = 1;
                break;
            case "left":
                isLeftReady = 1;
                break;
        }

        RoomEvent.DoPlayerReady(pos);
    }

    //新玩家进入房间
    public void addNewPlayer(int othpos, string pname,string icon,int glod, List<Action> alist=null)
    {
        switch (TryGetLocPos(positon, othpos))
        {
            case "right":
                rightPositon = othpos;
                rightName = pname;
                rightIcon = icon;
                rightGlod = glod;
                if (alist != null) { rightAcionList = alist; }
                break;
            case "top":
                topPostion = othpos;
                topName = pname;
                topIcon = icon;
                topGold = glod;
                if (alist != null) { topAcionList = alist; }
                break;
            case "left":
                leftPostion = othpos;
                leftName = pname;
                leftIcon = icon;
                leftGold = glod;
                if (alist != null) { leftAcionList = alist; }
                break;
        }
    }

    //玩家碰牌操作
    public void addPengPai(int pos, Action act)
    {
        string sendPos = "";
        if (positon == pos)
        {
            myAcionList.Add(act);
            sendPos = "bot";
        }
        else
        {
            switch (TryGetLocPos(positon, pos))
            {
                case "right":
                    rightAcionList.Add(act);
                    sendPos = "right";
                    break;
                case "top":
                    topAcionList.Add(act);
                    sendPos = "top";
                    break;
                case "left":
                    leftAcionList.Add(act);
                    sendPos = "left";
                    break;
            }
        }
        RoomEvent.DoPengPai(sendPos, act.getValue());
    }

    //玩家明杠操作
    public void addMingGang(int pos,int mj)
    {
        string sendPos = "";
        List<Action> alist = null;
        if (positon == pos)
        {
            alist = myAcionList;
            sendPos = "bot";
        }
        else
        {
            switch (TryGetLocPos(positon, pos))
            {
                case "right":
                    alist = rightAcionList;
                    sendPos = "right";
                    break;
                case "top":
                    alist = topAcionList;
                    sendPos = "top";
                    break;
                case "left":
                    alist = leftAcionList;
                    sendPos = "left";
                    break;
            }
        }

        //找到明杠的碰牌,修改为明杠类型
        for (int i = 0; i < alist.Count; i++)
        {
            if (alist[i].getActionType() == CardView.CHI_PENG_碰牌)
            {
                int pengMj = alist[i].getValue();
                if (pengMj == mj)
                {
                    alist[i].setActionType(CardView.CHI_MINGGANG_明杠);
                    alist[i].getActionData().Add(mj);
                    break;
                }
            }
        }
        //
        RoomEvent.DoMingGang(sendPos, mj);
    }
       //玩家暗杠操作
    public void addAnGang(int pos ,Action act)
    {
        string sendPos = "";
        if (positon == pos)
        {
            myAcionList.Add(act);
            sendPos = "bot";
        }
        else
        {
            switch (TryGetLocPos(positon, pos))
            {
                case "right":
                    rightAcionList.Add(act);
                    sendPos = "right";
                    break;
                case "top":
                    topAcionList.Add(act);
                    sendPos = "top";
                    break;
                case "left":
                    leftAcionList.Add(act);
                    sendPos = "left";
                    break;
            }
        }
        RoomEvent.DoAnGang(sendPos, act.getValue());
    }

    //玩家直杠
    public void addZhiGang(int pos, Action act, int fangGangPos)
    {
        string sendPos = "";
        if (positon == pos)
        {
            myAcionList.Add(act);
            sendPos = "bot";
        }
        else
        {
            switch (TryGetLocPos(positon, pos))
            {
                case "right":
                    rightAcionList.Add(act);
                    sendPos = "right";
                    break;
                case "top":
                    topAcionList.Add(act);
                    sendPos = "top";
                    break;
                case "left":
                    leftAcionList.Add(act);
                    sendPos = "left";
                    break;
            }
        }
        RoomEvent.DoZhiGang(sendPos, act.getValue(), fangGangPos);
    }

    public string TryGetLocPos(int myPos, int othPos)
    {
        string str = "";
        switch (myPos)
        {
            case 1:
                if (othPos == 2) { str = "right"; } else if (othPos == 3) { str = "top"; } else if (othPos == 4) { str = "left"; }
                break;
            case 2:
                if (othPos == 1) { str = "left"; } else if (othPos == 3) { str = "right"; } else if (othPos == 4) { str = "top"; }
                break;
            case 3:
                if (othPos == 1) { str = "top"; } else if (othPos == 2) { str = "left"; } else if (othPos == 4) { str = "right"; }
                break;
            case 4:
                if (othPos == 1) { str = "right"; } else if (othPos == 2) { str = "top"; } else if (othPos == 3) { str = "left"; }
                break;
        }
        return str;
    }

    //摸牌放入手牌
    public void putMjtoHandList(int mj)
    {
        myHandMj.Add(mj);
    }

    //出牌,重新排序手牌
    public void sortHandList(int mj)
    {
        for(int i = 0; i < myHandMj.Count; i++)
        {
            if (myHandMj[i] == mj)
            {
                myHandMj.RemoveAt(i);
            }
        }
        myHandMj.Sort((x, y) => -x.CompareTo(y));
    }

    //删除
    public void deleteArrInList(int mj,int count)
    {
        int repeat = 0;
        for (int i = 0; i < myHandMj.Count; i++)
        {
            if (myHandMj[i].Equals( mj))
            {
                myHandMj.RemoveAt(i);
                repeat++;
                i--;
            }
            if (repeat == count)
            {
                break;
            }
        }
        myHandMj.Sort((x, y) => -x.CompareTo(y));
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
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
