using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallServer  {

        public void handlerSubCmd(SocketModel socketModel)
        {
            switch (socketModel.GetSubCmd())
            {
                case ProtocolSC.Sub_Cmd_CreatRoomRqs:
                    CreatRoomRqs(socketModel);
                    break;
                case ProtocolSC.Sub_Cmd_JoinRoomRqs:
                    JoinRoomRqs(socketModel);
                    break;
                case ProtocolSC.Sub_Cmd_HALL_CHANGENAME:
                    reNameRqs(socketModel);
                    break;
                case ProtocolSC.Sub_Cmd_GAME_CHANGEICON:
                    rePicRqs(socketModel);
                    break;
                case ProtocolSC.Sub_Cmd_GAME_FENXIANG:
                    FenXiangRqs(socketModel);
                    break;
            }
        }

        //分享钻石
        public void FenXiangRqs(SocketModel socketModel)
        {
            if (socketModel.GetCommand() == 18)
            {
                List<int> list = socketModel.GetData();
                int fk = list[0];
                GameInfo.Instance.setUserFk(fk);
                GameEvent.DoMsgTipEvent("分享钻石成功!");

                List<string> mList = socketModel.GetMessage();
                MailData md = new MailData();
                md.setDataTime(mList[0]);
                md.setFk(int.Parse(mList[1]));
                md.setGid(int.Parse(mList[2]));
                md.setStype(int.Parse(mList[3]));
                GameInfo.Instance.mailList.Insert(0, md);

            }
            else if (socketModel.GetCommand() == 17)
            {
                GameEvent.DoMsgTipEvent("用户不存在！");
            }
            else
            {
                GameEvent.DoMsgTipEvent("未知错误!");
            }
        }

        //修改昵称
        public void reNameRqs(SocketModel socketModel)
        {
            GameInfo.Instance.UserName = socketModel.GetMessage()[0];
            GameEvent.DoMsgTipEvent("修改昵称成功!");
            GameEvent.DoStringEvent(GameInfo.Instance.UserName);
        }

        //修改头像
        public void rePicRqs(SocketModel socketModel)
        {
            GameInfo.Instance.UserIcon = int.Parse(socketModel.GetMessage()[0]);
            if (GameInfo.Instance.UserIcon > 4)
            {
                GameInfo.Instance.sex = "boy";
            }
            else
            {
                GameInfo.Instance.sex = "girl";
            }
            GameEvent.DoMsgTipEvent("修改头像成功!");
            GameEvent.DoPicEvent(GameInfo.Instance.UserIcon.ToString());
        }

       //加入开房结果
       public void JoinRoomRqs(SocketModel socketModel)
       {
            Debug.Log("收到加入房间返回结果"+ socketModel.GetCommand());
            if (socketModel.GetCommand() == Helper_ErrCode.ErrCommand_Hall_JoinSucceed_加入房间成功)
            {
                GameInfo.Instance.roomId = socketModel.GetData()[0];
                GameInfo.Instance.positon = socketModel.GetData()[1];
                GameInfo.Instance.myGold = socketModel.GetData()[2];
                GameInfo.Instance.maxRound = socketModel.GetData()[3];
                GameInfo.Instance.maxPoint = socketModel.GetData()[4];
                GameInfo.Instance.isZjDouble = socketModel.GetData()[5];
                GameInfo.Instance.canQiangGang= socketModel.GetData()[6];
                GameInfo.Instance.isDaiGen = socketModel.GetData()[7];
                GameInfo.Instance.isZiMoHu = socketModel.GetData()[8];
                GameInfo.Instance.roomPassWord= socketModel.GetData()[9];


            List<String> list = socketModel.GetMessage();
                for(int i = 0; i <list.Count; i++)
                {
                    if ((i+1) % 4== 0)
                    {
                        GameInfo.Instance.addNewPlayer(int.Parse(list[i-3]), list[i-2], list[i-1], int.Parse(list[i]));
                        Debug.Log("有玩家="+i);
                    }
                }
                Debug.Log("加入房间成功");
                GameEvent.DoSenceEvent ("game");
            }
            else {
                switch (socketModel.GetCommand())
                {
                    case Helper_ErrCode.ErrCommand_Hall_WrongPW_密码不正确:
                        GameEvent.DoMsgEvent("密码不正确!");
                        break;
                    case Helper_ErrCode.ErrCommand_Hall_RooMInvalid_房间不存在:
                        GameEvent.DoMsgEvent("房间不存在!");
                        break;
                    case Helper_ErrCode.ErrCommand_Hall_RoomFull_房间人满:
                        GameEvent.DoMsgEvent("房间人满!");
                        break;
                    case Helper_ErrCode.ErrCommand_Hall_UserExit_玩家已在房间:
                        GameEvent.DoMsgEvent("玩家已存在!");
                        break;
                }
            }
        }

        //返回开房结果
        public void CreatRoomRqs(SocketModel socketModel)
        {
            if (socketModel.GetCommand() == 11)
            {

                List<int> list = socketModel.GetData();
                GameInfo.Instance.roomId = list[0];
                GameInfo.Instance.positon = list[1];
                GameInfo.Instance.myGold = list[2];
                GameInfo.Instance.maxRound = list[3];
                GameInfo.Instance.maxPoint = list[4];
                GameInfo.Instance.isZjDouble = list[5];
                GameInfo.Instance.canQiangGang = list[6];
                GameInfo.Instance.isDaiGen = list[7];
                GameInfo.Instance.isZiMoHu = list[8];
                GameInfo.Instance.roomPassWord = list[9];
                int fk = list[10];
                //更新钻石
                int fk2 = GameInfo.Instance.UserFK - fk;
                GameInfo.Instance.setUserFk(fk2);
                GameEvent.DoSenceEvent("game");
                Debug.Log("开房成功,房间ID=" + GameInfo.Instance.roomId);
                Debug.Log("开房成功,我的位置=" + GameInfo.Instance.positon);
                Debug.Log("开房成功,我的金币=" + GameInfo.Instance.myGold);
                Debug.Log("开房成功,最大圈数=" + GameInfo.Instance.maxRound);
                Debug.Log("开房成功,最大番数=" + GameInfo.Instance.maxPoint);
            }
            else
            { 
                Debug.Log("开房失败 socketModel.GetComman="+ socketModel.GetCommand());
                GameEvent.DoMsgEvent("开房失败!");
            }
        }
}
