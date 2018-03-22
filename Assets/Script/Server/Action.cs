using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;//注意要用到这个dll
[ProtoContract]
public class Action  {

    [ProtoMember(1)]
    private int actionType;
    [ProtoMember(2)]
    private List<int> actionData;//打出的牌

    public Action()
    {

    }

    public Action(int aType, List<int> adata)
    {
        this.actionType = aType;
        this.actionData = adata;
    }

    public int getActionType()
    {
        return actionType;
    }
    public void setActionType(int atype)
    {
        this.actionType = atype;
    }

    public List<int> getActionData()
    {
        return actionData;
    }


    public void setActionData(List<int> list)
    {
        this.actionData = list;
    }

    public int getValue()
    {
        return actionData[0];
    }
}
