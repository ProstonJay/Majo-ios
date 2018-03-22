using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;//注意要用到这个dll
using System;

[ProtoContract]
public class Player {

    [ProtoMember(1)]
    private int Gid;
    [ProtoMember(2)]
    private int PlyerIcon;
    [ProtoMember(3)]
    private String PlyerName;
    [ProtoMember(4)]
    private int PlyerDenFen;

    public Player()
    {

    }
    public Player(int gid, int pi, string pn, int pd)
    {
        this.Gid = gid;
        this.PlyerIcon = pi;
        this.PlyerName = pn;
        this.PlyerDenFen = pd;
    }

    //Gid
    public int GetGid()
    {
        return Gid;
    }
    public void SetGid(int value)
    {
        this.Gid = value;
    }

    //ICon
    public int GetPlyerIcon()
    {
        return PlyerIcon;
    }
    public void SetPlyerIcon(int value)
    {
        this.PlyerIcon = value;
    }

    //Name
    public String GetPlyerName()
    {
        return PlyerName;
    }
    public void SetPlyerName(String value)
    {
        this.PlyerName = value;
    }

    //gold
    public int GetPlyerDenFen()
    {
        return PlyerDenFen;
    }
    public void SetPlyerDenFen(int value)
    {
        this.PlyerDenFen = value;
    }
}
