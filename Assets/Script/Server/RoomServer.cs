

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomServer
{

    public void handlerSubCmd(SocketModel socketModel)
    {
        switch (socketModel.GetSubCmd())
        {
            case ProtocolSC.Sub_Cmd_PLayerEnterRoom:
                PlayerEnterRoomRps(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_PUSH_GAMESTART:
                RoomGameStart(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_PUSH_GIVECARD:
                //GiveHandCard(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_CHUPAI:
                ChuPai(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_MOPAI:
                MoPai(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_HUPAI:
                HuPai(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_PENGPAI:
                PengPai(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_GUOPAI:
                GuoPai(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_END:
                GameEnd(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_OVER:
                GameOver(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_GANGPAI://直杠
                ZhiGang(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_MINGGANG://明杠
                MingGang(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_ANGANG://暗杠
                AnGang(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_QIANGGANG://抢杠
                QiangGang(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_ZIMO://自摸
                ZiMo(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_LIUJU://流局
                LiuJu(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_CHAT://聊天
                Chat(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_OFFLINE_断线://断线了
                PlayerOffLine(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_ONLINE_上线://上线了
                PlayerOnLine(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_READY://上线了
                PlayerReady(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_INITIATE_DISMISS_发起解散:
                InitiateDismiss(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_VOTE_DISMISS_投票解散:
                VoteDismiss(socketModel);
                break;
        }
    }

    //投票解散结果
    public void VoteDismiss(SocketModel socketModel)
    {
        int comd = socketModel.GetCommand();
        RoomEvent.DoVoteDisResults(comd);
    }

    //有玩家发起解散房间
    public void InitiateDismiss(SocketModel socketModel)
    {
        string Pname = socketModel.GetMessage()[0];
        RoomEvent.DoInitiateDisMiss(Pname);
    }

    //玩家准备了
    public void PlayerReady(SocketModel socketModel)
    {
        int pos = socketModel.GetCommand();
        GameInfo.Instance.PlayerGetReady(pos);
    }

    //上线了
    public void PlayerOnLine(SocketModel socketModel)
    {
        int pos = socketModel.GetCommand();
        RoomEvent.DoPlayerOnLine(pos);
    }

    //断线了
    public void PlayerOffLine(SocketModel socketModel)
    {
        int pos = socketModel.GetCommand();
        RoomEvent.DoPlayerOffLine(pos);
    }

    //聊天
    public void Chat(SocketModel socketModel)
    {
        List<int> list = socketModel.GetData();
        int pos = list[0];
        int txt = list[1];
        GameEvent.DoChat(pos, txt);
    }

    //流局
    public void LiuJu(SocketModel socketModel)
    {
        List<PlayerData> plist = socketModel.GetPdata();
        RoomEvent.DoActionLiuJu(plist);
        //
        GameInfo.Instance.PlayFlag = false;
    }

    //吃胡
    public void HuPai(SocketModel socketModel)
    {
        List<int> list = socketModel.GetData();
        int pos = list[0];
        int mj = list[1];
        int fangpaoPos = list[2];
        string sendPos = String.Empty;
        if (pos == GameInfo.Instance.positon)
        {
            sendPos = "bot";
        }
        else
        {
            sendPos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, pos);
        }
        List<PlayerData> plist = socketModel.GetPdata();
        RoomEvent.DoChiHu(sendPos, mj, fangpaoPos, plist);
        //
        GameInfo.Instance.PlayFlag = false;
    }

    //自摸
    public void ZiMo(SocketModel socketModel)
    {
        List<int> dlist = socketModel.GetData();
        int pos = dlist[0];
        int mj = dlist[1];
        string sendPos = "";
        if (pos == GameInfo.Instance.positon)
        {
            sendPos = "bot";
        }
        else{
            sendPos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon,pos);
        }
        List<PlayerData> plist = socketModel.GetPdata();
        RoomEvent.DoZiMo(sendPos, mj, plist);
        //
        GameInfo.Instance.PlayFlag = false;
    }

    //抢杠
    public void QiangGang(SocketModel socketModel)
    {
        //收到这个数据说明，能抢杠胡
        List<int> list = socketModel.GetData();
        RoomEvent.DoActionList(list);
    }


    //暗杠
    public void AnGang(SocketModel socketModel)
    {
        Debug.Log("收到暗杠数据");
        List<int> plist = socketModel.GetData();
        int pos = plist[0];
        int mj = plist[1];
        List<int> alist = new List<int>();
        alist.Add(mj); alist.Add(mj); alist.Add(mj); alist.Add(mj);
        Action act = new Action();
        act.setActionType(CardView.CHI_ANGANG_暗杠);
        act.setActionData(alist);
        Debug.Log("暗杠 pos="+ pos+"mj="+mj);
        GameInfo.Instance.addAnGang(pos, act);

    }
    //直杠
    public void ZhiGang(SocketModel socketModel)
    {
        Debug.Log("收到直杠数据");
        List<int> plist = socketModel.GetData();
        int pos = plist[0];
        int mj = plist[1];
        int beiGangPos = plist[2];
        //
        List<int> alist = new List<int>();
        alist.Add(mj); alist.Add(mj); alist.Add(mj); alist.Add(mj);
        Action act = new Action();
        act.setActionType(CardView.CHI_GANG_杠牌);
        act.setActionData(alist);
        //
        GameInfo.Instance.addZhiGang(pos, act, beiGangPos);
        //设置操作方向
        RoomEvent.DoPlayerDiction(pos,2);
    }
    //明杠
    public void MingGang(SocketModel socketModel)
    {
        Debug.Log("收到明杠数据");
        List<int> list = socketModel.GetData();
        int pos = list[0];
        int mj = list[1];
        GameInfo.Instance.addMingGang(pos, mj);
    }
    //PaSS牌
    public void GuoPai(SocketModel socketModel)
    {
        int value = socketModel.GetCommand();
        //
        Debug.Log("都PASS了 通知清理MJ 添加MJ到出牌区");
        RoomEvent.DoPlayerDiction(value,1);
        //
        if (value ==GameInfo.Instance.positon)
        {
            Debug.Log("本轮操作权是自己,告诉服务器摸牌");
            //告诉服务器摸牌
            RoomEvent.DoActionMoPai();
        }

    }

    //碰牌
    public void PengPai(SocketModel socketModel)
    {
        List<int> list = socketModel.GetData();
        int pos = list[0];
        int mj = list[1];
        List<int> alist = new List<int>();
        alist.Add(mj); alist.Add(mj);
        Action act = new Action();
        act.setActionType(CardView.CHI_PENG_碰牌);
        act.setActionData(alist);
        GameInfo.Instance.addPengPai(pos, act);
        RoomEvent.DoPlayerDiction(pos,2);
    }

    //其他玩家加入房间
    public void PlayerEnterRoomRps(SocketModel socketModel)
    {
         List<String> list = socketModel.GetMessage();
         GameInfo.Instance.addNewPlayer(int.Parse(list[0]), list[1], list[2], int.Parse(list[3]));
         Debug.Log("玩家加入房间 " + "位置 = " + list[0] + "名字= " + list[1]  );
         GameEvent.DoPlayerEnterRoom(int.Parse(list[0]), list[1]);
    }
    //开局
    public void RoomGameStart(SocketModel socketModel)
    {
        //
        GameInfo.Instance.isGameStart = 1;
        int value = socketModel.GetCommand();
        if (value > 0)
        {
            //手牌
            List<int> list = socketModel.GetData();
            list.Reverse();
            GameInfo.Instance.myHandMj = list;

            //房间信息
            List<string> msg = socketModel.GetMessage();
            GameInfo.Instance.zhuangjia = int.Parse(msg[0]);
            GameInfo.Instance.round = int.Parse(msg[1]);
            GameInfo.Instance.mjLeft = int.Parse(msg[2]);
            //开始后，设置所有人状态为未准备
            GameInfo.Instance.isMyReady = 0;
            GameInfo.Instance.isRightReady = 0;
            GameInfo.Instance.isTopReady = 0;
            GameInfo.Instance.isLeftReady = 0;
            //通知UI游戏开始了
            GameEvent.DoGameStartEvent(1);
            Debug.Log("人满开局发牌 "+ list);
        }
        int mjNum = socketModel.GetCommand();
        RoomEvent.DoActionLeftMj(mjNum);
     }

    //摸牌
    public void MoPai(SocketModel socketModel)
    {
        //GameEvent.DoMsgTipEvent(" 收到摸牌数据");
        List<string> msgList = socketModel.GetMessage();
        //如果是自己摸牌
        Debug.Log("摸牌的位置=" + int.Parse(msgList[0]));
        if (int.Parse(msgList[0]) == GameInfo.Instance.positon)
        {
            Debug.Log("是自己摸牌 摸的牌= " + int.Parse(msgList[1]));
            //GameEvent.DoMsgTipEvent("是自己摸牌 摸的牌= " + int.Parse(msgList[1]));
            //摸起的牌放到手牌队列里
            int mj = int.Parse(msgList[1]);
            GameInfo.Instance.putMjtoHandList(mj);
            //广播给UI显示
            GameEvent.DoMoPai(mj);
            Debug.Log("摸牌==" + mj);
            //是否能,杠,糊
            List<int> list = socketModel.GetData();
            RoomEvent.DoActionList(list);
            //如果Action有数据,可以杠
            if (socketModel.GetAdata() != null && socketModel.GetAdata().Count > 0)
            {
                Debug.Log("可以杠牌=" + socketModel.GetAdata().Count);
                GameInfo.Instance.gangList = socketModel.GetAdata();//杠的数据保存在INFO, 选择牌型后清空
            }
        }
        //不管是谁摸，更新牌的数量
        Debug.Log("剩余牌的数量=" + socketModel.GetCommand());
        int mjNum = socketModel.GetCommand();
        RoomEvent.DoActionLeftMj(mjNum);

    }
    //出牌
    public void ChuPai(SocketModel socketModel)
    { 
        //1.先把出的牌显示出来
        List<string> list = socketModel.GetMessage();
        //2.其他玩家有没有操作
        int stats = socketModel.GetCommand();
        if (stats == 0)
        {
            GameEvent.DoChuPai(int.Parse(list[0]), int.Parse(list[1]), false);
        }
        else
        {
            GameEvent.DoChuPai(int.Parse(list[0]), int.Parse(list[1]), true);
        }
     
        Debug.Log("位置="+ list[0]+"出牌 = " + list[1]);
        //3如果没人操作，设置指示方向
        if (stats == 0)
        {
            Debug.Log("设置指示方向");
            RoomEvent.DoPlayerDiction(int.Parse(list[2]));
        }
        //如果是玩家自己出的牌就不继续判断读取数据了
        if (int.Parse(list[0]) == GameInfo.Instance.positon)
        {
            Debug.Log("如果是玩家自己出的牌就不继续判断读取数据了");
            return;
        }
        //4.看看自己是否可以可以吃,碰 等操作;
        List<int> mylist = socketModel.GetData();
        Debug.Log("mylist=" + mylist);
        if (mylist != null)
        {  
            //如果有操作,通知UI显示相关按键
            Debug.Log("自己有操作=" + mylist[0]);
            //如果Action有数据,可以杠
            List<Action> actionlist = socketModel.GetAdata();
            if (actionlist != null)
            {
                Debug.Log("可以杠牌=" + actionlist.Count);
                GameInfo.Instance.gangList = actionlist;//杠的数据保存在INFO, 选择牌型后清空
            }
            RoomEvent.DoActionList(mylist);
        }
        else
        {
            //3.如果自己没有操作,再看其他玩家可以,吃,碰等操作.如果有,就等待.
            if (stats == 0)
            {
                Debug.Log("没有人可以吃,碰操作");
                //4.如果其他玩家也没操作,那看看本轮出牌权是否是自己,如果是自己那就向服务器摸牌
                if (int.Parse(list[2]) == GameInfo.Instance.positon)
                {
                    RoomEvent.DoActionMoPai();
                }
            }
            else
            {
                Debug.Log("有人操作,吃,碰等操作,要等待");
            }
        }
    }

    //一局结束
    public void GameEnd(SocketModel socketModel)
    {
        Debug.Log("收到小结算数据");
        List<PlayerData> plist = socketModel.GetPdata();
        GameInfo.Instance.jieSuanRoundData = plist;
    }

    //总结算
    public void GameOver(SocketModel socketModel)
    {
        Debug.Log("收到总结算数据");
        List<PlayerData> plist = socketModel.GetPdata();
        GameInfo.Instance.jieSuanEndData = plist;
        GameInfo.Instance.isGameStart = 0;
    }
}
