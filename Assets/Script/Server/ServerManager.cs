using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager  {

    private static ServerManager instance;//静态实例
    public LoginServer loginser;
    public HallServer hallser;
    public RoomServer roomser;

    public static ServerManager GetInstance()
    {
     //惰性实例化，如果不调用Getinstance，就是不实例化的，不给你分配内存。
     //当你调用的时候再去实例化，这样比较优化的节省内存。
        if (instance == null)
        {
            instance = new ServerManager();
        }
        return instance;
    }


    //为了防止其他类去调用这个类生成这个实例
    //所以把这个构造函数私有
    private ServerManager() {
        loginser = new LoginServer();
        hallser = new HallServer();
        roomser = new RoomServer();
    }

    //解析服务器数据
    public void ReceiveMsg(SocketModel socketModel)
    {
        Debug.Log("收到消息主协议=" + socketModel.GetMainCmd()+ "子协议 = " + socketModel.GetSubCmd());
        switch (socketModel.GetMainCmd())
        {
            case ProtocolMC.Main_Cmd_LOGIN:
                loginser.handlerSubCmd(socketModel);
            break;
            case ProtocolMC.Main_Cmd_HALL:
                hallser.handlerSubCmd(socketModel);
                break;
            case ProtocolMC.Main_Cmd_ROOM:
                roomser.handlerSubCmd(socketModel);
                break;
        }
    }

}
