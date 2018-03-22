using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;//注意要用到这个dll
using System;

[ProtoContract]
public class BattleData  {

    [ProtoMember(1)]
    private int Bid;
    [ProtoMember(2)]
    private String DataTime;
    [ProtoMember(3)]
    private String RoomRule;
    [ProtoMember(4)]
    private List<Player> PlayerList;

    public BattleData()
    {

    }
    public BattleData(int bid, string dt,string rl, List<Player> pl)
    {
        this.Bid = bid;
        this.DataTime = dt;
        this.RoomRule = rl;
        this.PlayerList = pl;
    }
    //mid
    public int GetBid()
    {
        return Bid;
    }
    public void SetBid(int value)
    {
        this.Bid = value;
    }

    //日期
    public String GetDataTime()
    {
        return DataTime;
    }
    public void SetDataTime(String value)
    {
        this.DataTime = value;
    }
    //规则
    public String GetRoomRule()
    {
        return RoomRule;
    }
    public void SetRoomRule(String value)
    {
        this.RoomRule = value;
    }
    //玩家数据
    public List<Player> GetPlayerList()
    {
        return PlayerList;
    }
    public void SetPlayerList(List<Player> value)
    {
        this.PlayerList = value;
    }
}
