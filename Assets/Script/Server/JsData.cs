using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;//注意要用到这个dll
[ProtoContract]
public class JsData {

    //类型
    [ProtoMember(1)]
    private int Jtype;
    //番数
    [ProtoMember(2)]
    private int funCount;
    //数量
    [ProtoMember(3)]
    private int gold;
    //牌型
    [ProtoMember(4)]
    private int paiXing;
    //庄家
    [ProtoMember(5)]
    private int zj;

    public JsData()
    {

    }

    public JsData(int jtype, int fc,int gd,int px,int zj)
    {
        this.Jtype = jtype;
        this.funCount = fc;
        this.gold = gd;
        this.paiXing = px;
        this.zj = zj;
    }

    public int getZj()
    {
        return zj;
    }
    public void setZj(int value)
    {
        this.zj = value;
    }

    public int getPaiXing()
    {
        return paiXing;
    }
    public void setPaiXing(int value)
    {
        this.paiXing = value;
    }

    public int getJtype()
    {
        return Jtype;
    }
    public void setJtype(int value)
    {
        this.Jtype = value;
    }

    public int getFunCount()
    {
        return funCount;
    }
    public void setFunCount(int value)
    {
        this.funCount = value;
    }

    public int getGold()
    {
        return gold;
    }
    public void setGold(int value)
    {
        this.gold = value;
    }
}
