using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ProtoContract]
public class MailData {

    //mid
    [ProtoMember(1)]
    private int mid;
    //日期
    [ProtoMember(2)]
    private String dataTime;
    //用户GID
    [ProtoMember(3)]
    private int gid;
    //钻石
    [ProtoMember(4)]
    private int fk;
    //类型 1= 被赠送方， 2=赠送方
    [ProtoMember(5)]
    private int sType;

    public MailData()
    {

    }

    public MailData(int mid, string datatm, int gid, int fk, int sType)
    {
        this.mid = mid;
        this.dataTime = datatm;
        this.mid = gid;
        this.mid = fk;
        this.mid = sType;
    }

    //mid
    public int getMid()
    {
        return mid;
    }
    public void setMid(int value)
    {
        this.mid = value;
    }

    //日期
    public String getDataTime()
    {
        return dataTime;
    }
    public void setDataTime(String value)
    {
        this.dataTime = value;
    }

    //用户GID
    public int getGid()
    {
        return gid;
    }
    public void setGid(int value)
    {
        this.gid = value;
    }

    //钻石
    public int getFk()
    {
        return fk;
    }
    public void setFk(int value)
    {
        this.fk = value;
    }

    //类型 1= 被赠送方， 2=赠送方
    public int getStype()
    {
        return sType;
    }
    public void setStype(int value)
    {
        this.sType = value;
    }
}
