using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_JieSuan  {

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

    //类型 加分======1.直杠 2.明刚 3.暗杠  4.直杠接炮 5.明杠接炮 6.暗杠接炮 7.直杠开花 8.明刚开花 9 暗杠开花  10.抢杠 11.吃胡 12.自摸
    //类型 扣分======1.被直杠 2.被明刚 3.被暗杠  4.直杠点炮 5.明杠点炮   6.暗杠点炮  7.被直杠开花 8.被明刚开花 9 被暗杠开花  10.被抢杠11.被吃胡 12.被自摸
    public const int jieSuan_type_PaiXing_牌型 = 0;
    public const int jieSuan_type_zhiGang_直杠 = 1;
    public const int jieSuan_type_beiZhiGang_被直杠 = 2;
    public const int jieSuan_type_mingGang_明杠 = 3;
    public const int jieSuan_type_beiMingGang_被明杠 = 4;
    public const int jieSuan_type_anGang_暗杠 = 5;
    public const int jieSuan_type_beiAnGang_被暗杠 = 6;
    public const int jieSuan_type_zhiGangPao_直杠接炮 = 7;
    public const int jieSuan_type_beiZhiGangPao_直杠点炮 = 8;
    public const int jieSuan_type_mingGangPao_明杠接炮 = 9;
    public const int jieSuan_type_beiMingGangPao_明杠点炮 = 10;
    public const int jieSuan_type_anGangPao_暗杠接炮 = 11;
    public const int jieSuan_type_beiAnGangPao_暗杠点炮 = 12;
    public const int jieSuan_type_zhiGangKaiHua_直杠开花 = 13;
    public const int jieSuan_type_beiZhiGangKaiHua_被直杠开花 = 14;
    public const int jieSuan_type_mingGangKaiHua_明杠开花 = 15;
    public const int jieSuan_type_beiMingGangKaiHua_被明杠开花 = 16;
    public const int jieSuan_type_anGangKaiHua_暗杠开花 = 17;
    public const int jieSuan_type_beiAnGangKaiHua_被暗杠开花 = 18;
    public const int jieSuan_type_qiangGang_抢杠 = 19;
    public const int jieSuan_type_beiQiangGang_被抢杠 = 20;
    public const int jieSuan_type_chiHu_吃胡 = 21;
    public const int jieSuan_type_beiChiHu_被吃胡 = 22;
    public const int jieSuan_type_ziMo_自摸 = 23;
    public const int jieSuan_type_beiZiMo_被自摸 = 24;
    //
    public const int jieSuan_type_menQing_门清 = 25;
    public const int jieSuan_type_queMen_缺门 = 26;
    public const int jieSuan_type_jinGouD_金钩钓 = 27;
    public const int jieSuan_type_duanyj_断幺九 = 28;
    public const int jieSuan_type_zhiGangSP_直杠杠上炮 = 29;
    public const int jieSuan_type_mingGangSP_明杠杠上炮 = 30;
    public const int jieSuan_type_anGangSP_暗杠杠上炮 = 31;
    public const int jieSuan_type_qiangGangHu_抢杠胡 = 32;


    public static string getTypeToStr(int type,int paixing,int zj)
    {
        string str = "";
        switch (type)
        {
            case jieSuan_type_PaiXing_牌型:
                str = Helper_JieSuan.getPaiXingToStr(paixing);
                break;
            case jieSuan_type_zhiGang_直杠:
                str = "直杠(+1根)";
                break;
            case jieSuan_type_beiZhiGang_被直杠:
                str = "被直杠";
                break;
            case jieSuan_type_mingGang_明杠:
                str = "明杠(+1根)";
                break;
            case jieSuan_type_beiMingGang_被明杠:
                str = "被明杠";
                break;
            case jieSuan_type_anGang_暗杠:
                str = "暗杠(+2根)";
                break;
            case jieSuan_type_beiAnGang_被暗杠:
                str = "被暗杠";
                break;
            case jieSuan_type_zhiGangPao_直杠接炮:
                str = "直杠接炮" + getZjToStr(zj);
                break;
            case jieSuan_type_beiZhiGangPao_直杠点炮:
                str = "直杠点炮" + getZjToStr(zj);
                break;
            case jieSuan_type_mingGangPao_明杠接炮:
                str = "明杠接炮" + getZjToStr(zj);
                break;
            case jieSuan_type_beiMingGangPao_明杠点炮:
                str = "明杠点炮" + getZjToStr(zj);
                break;
            case jieSuan_type_anGangPao_暗杠接炮:
                str = "暗杠接炮" + getZjToStr(zj);
                break;
            case jieSuan_type_beiAnGangPao_暗杠点炮:
                str = "暗杠点炮" + getZjToStr(zj);
                break;
            case jieSuan_type_zhiGangKaiHua_直杠开花:
                str = "直杠开花" + getZjToStr(zj);
                break;
            case jieSuan_type_beiZhiGangKaiHua_被直杠开花:
                str = "被直杠开花" + getZjToStr(zj);
                break;
            case jieSuan_type_mingGangKaiHua_明杠开花:
                str = "明杠开花" + getZjToStr(zj);
                break;
            case jieSuan_type_beiMingGangKaiHua_被明杠开花:
                str = "被明杠开花" + getZjToStr(zj);
                break;
            case jieSuan_type_anGangKaiHua_暗杠开花:
                str = "暗杠开花" + getZjToStr(zj);
                break;
            case jieSuan_type_beiAnGangKaiHua_被暗杠开花:
                str = "被暗杠开花" + getZjToStr(zj);
                break;
            case jieSuan_type_qiangGang_抢杠:
                str = "抢杠" + getZjToStr(zj);
                break;
            case jieSuan_type_beiQiangGang_被抢杠:
                str = "被抢杠" + getZjToStr(zj);
                break;
            case jieSuan_type_chiHu_吃胡:
                str = "吃胡" + getZjToStr(zj);
                break;
            case jieSuan_type_beiChiHu_被吃胡:
                str = "点炮" + getZjToStr(zj);
                break;
            case jieSuan_type_ziMo_自摸:
                str = "自摸" + getZjToStr(zj);
                break;
            case jieSuan_type_beiZiMo_被自摸:
                str = "被自摸" + getZjToStr(zj);
                break;
            case jieSuan_type_menQing_门清:
                str = "门清";
                break;
            case jieSuan_type_queMen_缺门:
                str = "缺门";
                break;
            case jieSuan_type_jinGouD_金钩钓:
                str = "金钩钓";
                break;
            case jieSuan_type_duanyj_断幺九:
                str = "断幺九";
                break;
            case jieSuan_type_zhiGangSP_直杠杠上炮:
                str = "直杠杠上炮";
                break;
            case jieSuan_type_mingGangSP_明杠杠上炮:
                str = "明杠杠上炮";
                break;
            case jieSuan_type_anGangSP_暗杠杠上炮:
                str = "暗杠杠上炮";
                break;
            case jieSuan_type_qiangGangHu_抢杠胡:
                str = "抢杠胡";
                break;

        }
        return str;
    }

    public static string getPaiXingToStr(int paixing)
    {
        string str = "";
        switch (paixing)
        {
            case HuPai_PaiXing__PingHu_平胡:
                str = "平胡";
                break;
            case HuPai_PaiXing__DaDuiZi_大对子:
                str = "大对子";
                break;
            case HuPai_PaiXing__QingYiSe_清一色:
                str = "清一色";
                break;
            case HuPai_PaiXing__XiaoQiDui_小七对:
                str = "小七对";
                break;
            case HuPai_PaiXing__QingDaDui_清大对:
                str = "清大对";
                break;
            case HuPai_PaiXing__LongQiDui_龙七对:
                str = "龙七对";
                break;
            case HuPai_PaiXing__QingQiDui_清七对:
                str = "清七对"; 
                break;
            case HuPai_PaiXing__ShuangLongQiDui_双龙七对:
                str = "双龙七对";
                break;
            case HuPai_PaiXing__QingLongQiDui_清龙七对:
                str = "清龙七对";
                break;
            case HuPai_PaiXing__ShuangQingLongQiDui_双清龙七对:
                str = "双清龙七对";
                break;
        }
        return str;
    }

    public static string getZjToStr(int zj)
    {
        string str = "";
        if (zj == 1)
        {
            str = "(庄)";
        }
        return str;
    }
}
