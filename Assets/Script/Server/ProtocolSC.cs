

public class ProtocolSC  {

    //登录请求
    public const int Sub_Cmd_LoginRqs = 1;
    //创建房间请求
    public const int Sub_Cmd_CreatRoomRqs = 2;
    //加入房间请求
    public const int Sub_Cmd_JoinRoomRqs = 3;
    //有其他玩家进入房间
    public const int Sub_Cmd_PLayerEnterRoom = 4;
    //房间人满开局
    public const int Sub_Cmd_PUSH_GAMESTART= 5;
    //服务器发牌
    public const int Sub_Cmd_PUSH_GIVECARD= 6;
    //出牌
    public const int Sub_Cmd_GAME_CHUPAI = 7;
    //准备完成
    public const int Sub_Cmd_GAME_READY = 8;
    //摸牌
    public const int Sub_Cmd_GAME_MOPAI = 9;

    //过牌
    public const int Sub_Cmd_GAME_GUOPAI = 10;
    //自摸
    public const int Sub_Cmd_GAME_ZIMO= 11;
    //碰牌
    public const int Sub_Cmd_GAME_PENGPAI = 12;
    //直杠牌  
    public const int Sub_Cmd_GAME_GANGPAI = 13;
    //吃胡
    public const int Sub_Cmd_GAME_HUPAI = 14;

    //明杠牌  
    public const int Sub_Cmd_GAME_MINGGANG = 15;
    //暗杠牌  
    public const int Sub_Cmd_GAME_ANGANG = 16;

    //抢杠
    public const int Sub_Cmd_GAME_QIANGGANG = 17;
    //小结算
    public const int Sub_Cmd_GAME_END = 18;


    //改名字
    public const int Sub_Cmd_HALL_CHANGENAME = 19;
    //改头像
    public const int Sub_Cmd_GAME_CHANGEICON = 20;
    //总结算
    public const int Sub_Cmd_GAME_OVER = 21;
    //流局
    public const int Sub_Cmd_GAME_LIUJU= 22;


    //聊天
    public const int Sub_Cmd_GAME_CHAT = 23;
    //分享
    public const int Sub_Cmd_GAME_FENXIANG = 24;
    //战绩
    public const int Sub_Cmd_HALL_ZHANJI_战绩 = 25;


    //心跳
    public const int Sub_Cmd_GAME_HEARTBEAT_心跳 = 30;
    //重连
    public const int Sub_Cmd_GAME_REJOINROOM_房间重连 = 31;
    //断线
    public const int Sub_Cmd_GAME_OFFLINE_断线 = 32;
    //上线
    public const int Sub_Cmd_GAME_ONLINE_上线 = 33;
    //发起解散
    public const int Sub_Cmd_GAME_INITIATE_DISMISS_发起解散 = 34;
    //发起解散
    public const int Sub_Cmd_GAME_VOTE_DISMISS_投票解散 = 35;
}
