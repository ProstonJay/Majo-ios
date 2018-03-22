using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginServer
{

    public void handlerSubCmd(SocketModel socketModel)
    {
        switch (socketModel.GetSubCmd())
        {
            case ProtocolSC.Sub_Cmd_LoginRqs:
                loginRqs(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_HEARTBEAT_心跳:
                HeartBeatRqs(socketModel);
                break;
            case ProtocolSC.Sub_Cmd_GAME_REJOINROOM_房间重连:
                ReJoinRoomRqs(socketModel);
                break;
        }
    }

    //返回登录结果
    public void loginRqs(SocketModel socketModel)
    {
        GameEvent.DoNetSocket(2);
        if (socketModel.GetCommand() == 10)
        {

            List<String> list = socketModel.GetMessage();
            GameInfo.Instance.ToKen = list[0];
            GameInfo.Instance.UserID = int.Parse(list[1]);
            GameInfo.Instance.UserName = list[2];
            GameInfo.Instance.UserIcon = int.Parse(list[3]);
            GameInfo.Instance.UserFK = int.Parse(list[4]);
            GameInfo.Instance.roomId = int.Parse(list[5]);//根据ROOMID判断，是否在房间中，如果roomID不为0，说明上次下线是在房间，请求加入房间
            //邮件
            GameInfo.Instance.mailList = socketModel.GetMailList();
            //战绩
            GameInfo.Instance.battleList = socketModel.GetBattleList();
            if (GameInfo.Instance.UserIcon > 4)
            {
                GameInfo.Instance.sex = "boy";
            }
            else
            {
                GameInfo.Instance.sex = "girl";
            }

            Debug.Log("登录成功," + "用户房卡=" + GameInfo.Instance.UserFK);
            //重连
            if (GameInfo.Instance.roomId > 0)
            {
                GameEvent.DoNetSocket(1);
                SocketModel HeartBeatRequset = new SocketModel();
                HeartBeatRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
                HeartBeatRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_REJOINROOM_房间重连);
                HeartBeatRequset.SetCommand(GameInfo.Instance.roomId);
                NettySocket.Instance.SendMsg(HeartBeatRequset);
            }
            else
            {
                LoginView.str = "hall";
            }
        }
        else if(socketModel.GetCommand() == 30)
        {
            GameEvent.DoMsgEvent("数据异常，登录失败!");
        }
        else
        {
            Debug.Log("登录失败!");
        }
    }

    //重连房间
    public void ReJoinRoomRqs(SocketModel socketModel)
    {
        GameEvent.DoNetSocket(2);
        if (socketModel.GetCommand() == 25)
        {
            LoginView.str = "hall";
            Debug.Log("房间已解散");
        }
        else
        {
            GameInfo.Instance.skm = socketModel;
            LoginView.str = "game";
        }

    }


    //心跳
    public void HeartBeatRqs(SocketModel socketModel)
    {
        SocketModel HeartBeatRequset = new SocketModel();
        HeartBeatRequset.SetMainCmd(ProtocolMC.Main_Cmd_LOGIN);
        HeartBeatRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_HEARTBEAT_心跳);
        NettySocket.Instance.SendMsg(HeartBeatRequset);
    }
}
