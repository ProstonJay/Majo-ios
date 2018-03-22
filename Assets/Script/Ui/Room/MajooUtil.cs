using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MajooUtil  {
    public const int HuPai_PaiXing__LiuJu_流局 = 11;
    public const int HuPai_PaiXing__QiangGang_抢杠 = 12;

    public const int HuPai_PaiXing__PingHu_平胡 = 1;
    public const int HuPai_PaiXing__DaDuiZi_大对子 = 2;
    public const int HuPai_PaiXing__QingYiSe_清一色 = 3;
    public const int HuPai_PaiXing__XiaoQiDui_小七对 = 4;
    public const int HuPai_PaiXing__QingDaDui_清大对 = 5;
    public const int HuPai_PaiXing__LongQiDui_龙七对 = 6;
    public const int HuPai_PaiXing__QingQiDui_清七对 = 7;
    public const int HuPai_PaiXing__ShuangLongQiDui_双龙七对 = 8;
    public const int HuPai_PaiXing__QingLongQiDui_清龙七对 = 9;
    public const int HuPai_PaiXing__ShuangQingLongQiDui_双清龙七对 = 10;

    //出牌配音路径
    public static string getChuPaiViocePath(string mjid)
    {
        string str = PlayerPrefs.GetString("Language") + "/" + GameInfo.Instance.sex + "/" + mjid;
        return str;
    }

    //碰牌配音路径
    public static string getPengPaiViocePath()
    {
        int vid = UnityEngine.Random.Range(1, 3);
        string str = PlayerPrefs.GetString("Language") + "/" + GameInfo.Instance.sex + "/peng" + vid.ToString();
        return str;
    }

    //自摸配音路径
    public static string getZiMoViocePath()
    {
        int vid = UnityEngine.Random.Range(1, 3);
        string str = PlayerPrefs.GetString("Language") + "/" + GameInfo.Instance.sex + "/zimo" + vid.ToString();
        return str;
    }

    //吃胡配音路径
    public static string getChiHuViocePath()
    {
        string str = PlayerPrefs.GetString("Language") + "/" + GameInfo.Instance.sex + "/hu";
        return str;
    }

    //暗杠配音路径
    public static string getAnGangViocePath()
    {
        string str = PlayerPrefs.GetString("Language") + "/" + GameInfo.Instance.sex + "/" + "anGang";
        return str;
    }

    //明杠配音路径
    public static string getMingGangViocePath()
    {
        string str = PlayerPrefs.GetString("Language") + "/" + GameInfo.Instance.sex + "/" + "mingGang";
        return str;
    }

    //直杠配音路径
    public static string getZhiGangViocePath()
    {
        string str = PlayerPrefs.GetString("Language") + "/" + GameInfo.Instance.sex + "/" + "zhiGang";
        return str;
    }

    //是否能碰牌
    public static bool CanPengPai(int mj,List<int> list)
    {
        bool boolen = false;
        int count = 0;
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i] == mj)
            {
                count++;
            }
        }
        if (count == 2)
        {
            boolen = true;
        }
        return boolen;
    }

    //是否能杠牌
    public static bool CanGangPai(int mj, List<int> list)
    {
        bool boolen = false;
        int count = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == mj)
            {
                count++;
            }
        }
        if (count == 3)
        {
            boolen = true;
        }
        return boolen;
    }

    public static string getChatText(int value)
    {
        string str = "";
        switch (value)
        {
            case 1:
                str = "快点吧，等到花儿都谢了！";
                break;
            case 2:
                str = "又断线了，网络怎么这么差！";
                break;
            case 3:
                str = "不要走，决战到天亮！";
                break;
            case 4:
                str = "你的牌打的太好了！";
                break;
            case 5:
                str = "跟你合作，真是太愉快了！";
                break;
            case 6:
                str = "不要吵了,好好打牌吧！";
                break;
            case 7:
                str = "打牌速度点嘛!";
                break;
            case 8:
                str = "别催，让我想想！";
                break;
            case 9:
                str = "输家不开口赢家不许走了！";
                break;
        }
        return str;
    }

    public static string getPlayerSex(string pos)
    {
        string str = "";
        int picon = 0;
        switch (pos)
        {
            case "bot":
                picon = GameInfo.Instance.UserIcon;
                break;
            case "right":
                picon = int.Parse(GameInfo.Instance.rightIcon);
                break;
            case "top":
                picon = int.Parse(GameInfo.Instance.topIcon);
                break;
            case "left":
                picon = int.Parse(GameInfo.Instance.leftIcon);
                break;
        }
        if (picon > 4)
        {
            str = "boy";
        }
        else
        {
            str = "girl";
        }
        return str;
    }
}
