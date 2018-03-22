using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomControl : MonoBehaviour, IPointerClickHandler
{
    public GameObject backView;
    public GameObject startView;
    public GameObject mjView;
    public GameObject chatView;
    public GameObject readyView;
    public GameObject actionView;
    public GameObject jiesuanView;


    private int startFlag ;

    //摸的什么牌
    private int MP_mj ;

    //出牌玩家位置
    private int CP_positon ;
    //出的什么牌
    private int CP_mj ;
    //是否保留
    private bool isKeep;

    //dri 方向指示
    private int dir;
    private int clearOutMj;

    //打牌区添加打出的牌
    private string outMJid = "";
    private string outPos = "";

    //重置
    private int resetFlag;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameInfo.Instance.jieSuanRoundData != null&& GameInfo.Instance.isEndStat==1)
        {
            GameInfo.Instance.isEndStat = 0;
            GameEvent.DoJieSuan();
        }
    }

    void Awake()
    {
        GameEvent.GameStartEvent+= GameStart;
        GameEvent.MoPaiEvent += MoPai;
        GameEvent.ChuPaiEvent += ChuPai;
        GameEvent.PlayEvent += addOutMj;
        GameEvent.ReSetRoomEvent += ReSetRoom;
        RoomEvent.PlayerDictionEvent += SetDir;
        //
        if (GameInfo.Instance.skm != null)
        {
            reJoinRoom();
            GameInfo.Instance.skm = null;
        }
        else
        {
            reset();
        }
    }

    //重置
    private void ReSetRoom()
    {
        resetFlag = 1;
    }

    //打牌区添加打出的牌
    private void addOutMj(string pos,string mjid)
    {
        outPos = pos;
        outMJid = mjid;
    }

    //出牌
    private void ChuPai(int pos, int data,bool boo)
    {
        this.CP_positon = pos;
        this.CP_mj = data;
        this.isKeep = boo;
    }
    //摸牌
    private void MoPai(int data)
    {
        this.MP_mj = data;
       
    }
    //开局
    private void GameStart(int type)
    {
        if (type == 1)
        {
            startFlag = 1;
        }

    }

    //方向指示
    private void SetDir(int pos,int clearMj)
    {
        dir = pos;
        clearOutMj = clearMj;
    }

    private void setStartView(bool boo)
    {
        startView.gameObject.SetActive(boo);
    }

    private void setMjView(bool boo)
    {
        mjView.gameObject.SetActive(boo);
    }

    private void setReadyView(bool boo)
    {
        readyView.transform.gameObject.SetActive(boo);
    }

    private void setActionView(bool boo)
    {
        actionView.gameObject.SetActive(boo);
    }

    private void setJieSuanView(bool boo)
    {
        jiesuanView.gameObject.SetActive(boo);
    }

    //重连
    private void reJoinRoom()
    {
        List<int> infoList = GameInfo.Instance.skm.GetData();
        GameInfo.Instance.roomId = infoList[0];
        GameInfo.Instance.roomPassWord = infoList[1];
        GameInfo.Instance.hostId = infoList[2];
        GameInfo.Instance.positon = infoList[3];
        GameInfo.Instance.maxRound = infoList[4];
        GameInfo.Instance.maxPoint = infoList[5];
        GameInfo.Instance.isZjDouble = infoList[6];
        GameInfo.Instance.canQiangGang = infoList[7];
        GameInfo.Instance.isDaiGen = infoList[8];
        GameInfo.Instance.isZiMoHu = infoList[9];
        GameInfo.Instance.round = infoList[10];
        GameInfo.Instance.zhuangjia = infoList[11];
        GameInfo.Instance.mjLeft = infoList[12];
        GameInfo.Instance.actionFlag = infoList[13];
        int moMj= infoList[14];//自己摸的牌，0就是没摸
        int chuPaiPos = infoList[15];//出牌的位置
        int chuPaiMj = infoList[16];//出的什么牌
        int stat = infoList[17];//有没人操作
        int roomStat = infoList[18];
        //游戏的当前阶段 1.准备。2，游戏中
        if (roomStat == 2)
        {
            if (stat > 0)
            {
                GameEvent.DoChuPai(chuPaiPos, chuPaiMj, true);
            }
            //GameInfo.Instance
            List<PlayerData> playerDataList = GameInfo.Instance.skm.GetPdata();
            for (int i = 0; i < playerDataList.Count; i++)
            {
                PlayerData pd = playerDataList[i];
                if (pd.getUserId() == GameInfo.Instance.positon)//自己的数据
                {
                    GameInfo.Instance.myGold = pd.getWinGold();
                    GameInfo.Instance.myHandMj = pd.gethandlist();
                    GameInfo.Instance.myHandMj.Reverse();
                    if (pd.getactionlist() != null)
                    {
                        GameInfo.Instance.myAcionList = pd.getactionlist();
                    }
                    mjView.GetComponent<CardView>().ReJoinSetHandlist("bot", moMj, stat);//还原手牌
                    mjView.GetComponent<CardView>().ReJoinSetCpglist("bot", pd.getactionlist());//还原碰，杠
                    mjView.GetComponent<CardView>().reJoinSetOutlist("bot", pd.getoutlist());//还原出牌

                    if (pd.GetPghList() != null && pd.GetPghList().Count > 0)
                    {
                        RoomEvent.DoActionList(pd.GetPghList());
                    }
                    else
                    {
                        Debug.Log("为什么我不能杠？=");
                    }
                    if (pd.GetGangList() != null && pd.GetGangList().Count > 0)
                    {
                        GameInfo.Instance.gangList = pd.GetGangList();//杠的数据保存在INFO, 选择牌型后清空
                    }
                }
                else
                {
                    GameInfo.Instance.addNewPlayer(pd.getUserId(), pd.getPlayerName(), pd.getPlayerIcon().ToString(), pd.getWinGold(), pd.getactionlist());
                    string pos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, pd.getUserId());
                    mjView.GetComponent<CardView>().ReJoinSetHandlist(pos, pd.getHupai());//还原手牌
                    mjView.GetComponent<CardView>().ReJoinSetCpglist(pos, pd.getactionlist());//还原碰，杠
                    mjView.GetComponent<CardView>().reJoinSetOutlist(pos, pd.getoutlist());//还原出牌
                }

            }
            //
            setReadyView(false);
            backView.GetComponent<BackView>().resetView();
            startView.GetComponent<RoomView>().initPlayerInfo();
            startView.GetComponent<RoomView>().setCurrentPos(GameInfo.Instance.actionFlag);
            startView.GetComponent<RoomView>().updateMjNum(GameInfo.Instance.mjLeft);
            actionView.GetComponent<RoomAction>().reset();
        }
        else//游戏阶段在准备
        {
            GameInfo.Instance.isGameStart = 1;
            reset();
        }

    }

    public void reset()
    {
        backView.GetComponent<BackView>().resetView();
        startView.GetComponent<RoomView>().reset();
        setStartView(false);
        mjView.GetComponent<CardView>().reset();
        setMjView(false);
        setReadyView(true);
        readyView.GetComponent<ReadyView>().reset();
        readyView.GetComponent<ReadyView>().InitReadyInfo();
        actionView.GetComponent<RoomAction>().reset();
        setActionView(false);
        ///
        chatView.SetActive(false);
    }

	
	// Update is called once per frame
	void Update () {
        if (startFlag == 1)
        {
            setReadyView(false);
            setStartView(true);
            setMjView(true);
            setActionView(true);
            chatView.SetActive(true);
            mjView.GetComponent<CardView>().setStartHandMj();
            startView.GetComponent<RoomView>().initPlayerInfo();
            startView.GetComponent<RoomView>().setCurrentPos(GameInfo.Instance.zhuangjia);
            startFlag = 0;
        }

        if (MP_mj > 0)
        {
            mjView.GetComponent<CardView>().moPai(MP_mj, MP_mj);
            MP_mj = 0;
        }


        if (CP_positon > 0)
        {
            actionView.GetComponent<RoomAction>().showChuPai(CP_positon, CP_mj, isKeep);
            CP_positon = 0;
            CP_mj = 0;
            isKeep = false;
        }

        if (dir > 0)
        {
            startView.GetComponent<RoomView>().setCurrentPos(dir);
            //clearOutMj 如果为0 就是不做处理
            if (clearOutMj ==1)//pass
            {
                actionView.GetComponent<RoomAction>().hideall();//PASS 清除牌，添加出的牌
            }
            else if(clearOutMj == 2)//碰 直杠 
            {
                actionView.GetComponent<RoomAction>().clearOutMj();//PASS 清除牌，不添加出的牌
            }
            //else
            //{
            //    actionView.GetComponent<RoomAction>().addOutMj();//只通知添加打的牌，不清除打的牌
            //}
            clearOutMj = 0;
             dir = 0;
        }

        if (outMJid !=""&& outPos!="")
        {
            mjView.GetComponent<CardView>().addMjToOutBar(outPos, outMJid);
            outMJid = "";
            outPos = "";
        }

        if (resetFlag>0)
        {
            reset();
            resetFlag = 0;
        }
    }
}
