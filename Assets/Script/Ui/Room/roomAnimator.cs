using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class roomAnimator : MonoBehaviour
{

    public GameObject myNum;
    public GameObject rightNum;
    public GameObject topNum;
    public GameObject leftNum;

    public int duijukaishi;

    public Image imgHuType;

    //碰牌
    private string pengPos = "";
    //自摸
    private string zimoPos = "";
    //杠牌
    private string gangPos = "";
    //吃胡
    private string chihuPos = "";
    //流局
    private int liuju;

    //
    private List<PlayerData> plist = new List<PlayerData>();


    //自摸胡类型
    private int zimohuType;
    //点炮类型
    private int ChihuType;
    //
    private int gangType;
    private string gangPostion;
    private int fangGangPos;

    void Awake()
    {
        GameEvent.GameStartEvent += GameStart;
        RoomEvent.PengPaiEvent += PengPaiEvent;
        RoomEvent.ZiMoEvent += ZiMoEvent;
        RoomEvent.ZhiGangEvent += ZhiGangEvent;
        RoomEvent.AnGangEvent += AnGangEvent;
        RoomEvent.MingGangEvent += MingGangEvent;
        RoomEvent.ChiHuEvent += ChiHuEvent;
        RoomEvent.LiuJuEvent += LiuJuEvent;
    }

    //流局动画
    private void LiuJuEvent(List<PlayerData> list)
    {
        liuju = 1;
        this.plist = list;
    }

    //开局动画
    private void GameStart(int type)
    {
        if (type == 1)
        {
            duijukaishi = 1;
        }

    }

    //碰牌动画
    private void PengPaiEvent(string pos, int mj)
    {
        if (pos != "")
        {
            this.pengPos = pos;
        }

    }

    //直杠动画
    private void ZhiGangEvent(string pos, int mj, int fangGangPos)
    {
        if (pos != "")
        {
            this.gangPos = pos;
            this.gangType = 1;
            this.fangGangPos = fangGangPos;
        }

    }

    //明杠动画
    private void MingGangEvent(string pos, int mj)
    {
        if (pos != "")
        {
            this.gangPos = pos;
            this.gangType = 2;

        }

    }

    //直杠动画
    private void AnGangEvent(string pos, int mj)
    {
        if (pos != "")
        {
            this.gangPos = pos;
            this.gangType = 3;
        }

    }

    //自摸动画
    private void ZiMoEvent(string pos, int mj, List<PlayerData> list)
    {
        this.zimoPos = pos;
        this.plist = list;
        if(plist!=null)
        {
            zimohuType = plist[0].getHupai();
        }

    }


    //吃胡动画
    private void ChiHuEvent(string pos, int mj, int fangpao, List<PlayerData> list)
    {
        this.chihuPos = pos;
        this.plist = list;
        if (plist != null)
        {
            ChihuType = plist[0].getHupai();
        }
    }


    // Use this for initialization
    void Start () {

    }


    // Update is called once per frame
    void Update()
    {
        //开局
        if (duijukaishi == 1)
        {
            playStartAnimator();
            duijukaishi = 0;
        }

        //碰牌
        if (pengPos != "")
        {
            playPengAnimator(pengPos);
            pengPos = "";
        }

        //杠牌
        if (gangPos != "")
        {
            playGangAnimator(gangPos);
            gangPos = "";
        }

        //自摸
        if (zimoPos != "")
        {
            playZiMoAnimator(zimoPos);
            zimoPos = "";
        }

        //吃胡
        if (chihuPos != "")
        {
            playChiHuAnimator(chihuPos);
            chihuPos = "";
        }
        //流局
        if (liuju >0)
        {
            playLiuJuAnimator();
            liuju = 0;
        }
    }

    //流局动画
    private void playLiuJuAnimator()
    {
        StartCoroutine(showHuNumber());
    }

        //吃胡动画
    private void playChiHuAnimator(string pos)
    {
        string path = "";
        if (this.ChihuType == 1 || this.ChihuType == 2 || this.ChihuType == 3)
        {
            path = "Image_Animator_Action_GSP";
        }
        else
        {
            path = "Image_Animator_Action_Hu";
        }
        GameObject ani = Instantiate(Resources.Load("Prefab/"+ path)) as GameObject;
        ani.transform.SetParent(this.transform, false);
        gangPostion = pos;
        switch (pos)
        {
            case "bot":
                ani.transform.localPosition = new Vector3(280, -140, 0);
                break;
            case "right":
                ani.transform.localPosition = new Vector3(430, 30, 0);
                break;
            case "top":
                ani.transform.localPosition = new Vector3(190, 240, 0);
                break;
            case "left":
                ani.transform.localPosition = new Vector3(-430, 30, 0);
                break;
        }
        ani.GetComponent<GameAnimator_Move>().goMove(true);
        StartCoroutine(showHuNumber());
        AudioMgr.Instance.SoundPlay(MajooUtil.getChiHuViocePath(), 1, 0);

    }

    //自摸动画
    private void playZiMoAnimator(string pos)
    {
        string path = "";
        if (this.zimohuType == 12)
        {
            path = "Image_Animator_Action_QGH";
        }
        else if(this.zimohuType==1|| this.zimohuType == 2 || this.zimohuType == 3)
        {
            path = "Image_Animator_Action_GSKH";
        }
        else
        {
            path = "Image_Animator_Action_Zimo";
        }
        GameObject ani = Instantiate(Resources.Load("Prefab/"+ path)) as GameObject;
        ani.transform.SetParent(this.transform, false);
        switch (pos)
        {
            case "bot":
                ani.transform.localPosition = new Vector3(280, -140, 0);
                break;
            case "right":
                ani.transform.localPosition = new Vector3(430, 30, 0);
                break;
            case "top":
                ani.transform.localPosition = new Vector3(240, 200, 0);
                break;
            case "left":
                ani.transform.localPosition = new Vector3(-430, 30, 0);
                break;
        }
        ani.GetComponent<GameAnimator_Move>().goMove(true);
        StartCoroutine(showHuNumber());
        if (this.zimohuType == 12)//抢杠胡
        {
            AudioMgr.Instance.SoundPlay(MajooUtil.getChiHuViocePath(), 1, 0);
        }
        else
        {
            AudioMgr.Instance.SoundPlay(MajooUtil.getZiMoViocePath(), 1, 0);
        }
        zimohuType = 0;
    }

    //碰牌动画
    private void playPengAnimator(string pos)
    {
        GameObject ani = Instantiate(Resources.Load("Prefab/Image_Animator_Action_Peng")) as GameObject;
        ani.transform.SetParent(this.transform, false);
        switch (pos)
        {
            case "bot":
                ani.transform.localPosition = new Vector3(280, -140, 0);
                break;
            case "right":
                ani.transform.localPosition = new Vector3(430, 30, 0);
                break;
            case "top":
                ani.transform.localPosition = new Vector3(190, 240, 0);
                break;
            case "left":
                ani.transform.localPosition = new Vector3(-430, 30, 0);
                break;
        }
        ani.GetComponent<GameAnimator_Move>().goMove(true);
    }

    //杠牌动画
    private void playGangAnimator(string pos)
    {
        string path1 = "";
        string path2 = "";
        if (this.gangType == 3)
        {
            path1 = "Image_Animator_XiaLu";
            path2 = "Image_Animator_Action_XiaYu";
        }
        else
        {
            path1 = "Image_Animator_longjuanfeng";
            path2 = "Image_Animator_Action_GuaFeng";
        }
        GameObject longjuanfeng = Instantiate(Resources.Load("Prefab/"+ path1)) as GameObject;
        longjuanfeng.transform.SetParent(this.transform, false);
        longjuanfeng.transform.SetSiblingIndex(0);
        gangPostion = pos;
        switch (pos)
        {
            case "bot":
                longjuanfeng.transform.localPosition = new Vector3(20, -30, 0);
                break;
            case "right":
                longjuanfeng.transform.localPosition = new Vector3(260, 150, 0);
                break;
            case "top":
                longjuanfeng.transform.localPosition = new Vector3(20, 270, 0);
                break;
            case "left":
                longjuanfeng.transform.localPosition = new Vector3(-230, 150, 0);
                break;
        }
        //
        GameObject ani = Instantiate(Resources.Load("Prefab/"+ path2)) as GameObject;
        ani.transform.SetParent(this.transform, false);
        switch (pos)
        {
            case "bot":
                ani.transform.localPosition = new Vector3(280, -140, 0);
                break;
            case "right":
                ani.transform.localPosition = new Vector3(430, 30, 0);
                break;
            case "top":
                ani.transform.localPosition = new Vector3(190, 240, 0);
                break;
            case "left":
                ani.transform.localPosition = new Vector3(-430, 30, 0);
                break;
        }
        ani.GetComponent<GameAnimator_Move>().goMove(true);
        Invoke("showNumer", 2);
    }

    //开局动画
    private void playStartAnimator()
    {
        GameObject ani = Instantiate(Resources.Load("Prefab/Image_Animator_duijukaishi")) as GameObject;
        ani.transform.SetParent(this.transform, false);
        ani.transform.localPosition = new Vector3(0, 20, 0);
    }

    private IEnumerator showHuNumber()
    {
        yield return new WaitForSeconds(2.5f);
        int hutype = 0;
        for (int i = 0; i < this.plist.Count; i++)
        {
            PlayerData pd = plist[i];
            hutype=pd.getHupai();
            string pos = "";
            if (pd.getWinGold() != 0)
            {
                if (pd.getUserId() == GameInfo.Instance.positon)//位置是自己
                {
                    pos = "bot";
                }
                else
                {
                    pos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, pd.getUserId());
                }
                if (hutype != 11)//如果是 10 就是流局 ，没数值
                {
                    switch (pos)
                    {
                        case "bot":
                            myNum.GetComponent<Number>().showNumber(pd.getWinGold());
                            break;
                        case "right":
                            rightNum.GetComponent<Number>().showNumber(pd.getWinGold());
                            break;
                        case "top":
                            topNum.GetComponent<Number>().showNumber(pd.getWinGold());
                            break;
                        case "left":
                            leftNum.GetComponent<Number>().showNumber(pd.getWinGold());
                            break;
                    }
                }

            }
        }
        //
        //Debug.Log("胡牌的类型="+ hutype);
        switch (hutype)
        {
            //    case MajooUtil.HuPai_PaiXing__DaDuiZi_大对子:
            //    case MajooUtil.HuPai_PaiXing__QingDaDui_清大对:
            //        this.imgHuType.transform.gameObject.SetActive(true);
            //        Sprite sp1 = Resources.Load("Texture/game/xiaojiesuan/HuType_2", typeof(Sprite)) as Sprite;
            //        this.imgHuType.sprite = sp1;
            //        break;
            //    case MajooUtil.HuPai_PaiXing__QingYiSe_清一色:
            //        this.imgHuType.transform.gameObject.SetActive(true);
            //        Sprite sp2 = Resources.Load("Texture/game/xiaojiesuan/HuType_" + hutype, typeof(Sprite)) as Sprite;
            //        this.imgHuType.sprite = sp2;
            //        break;
            //    case MajooUtil.HuPai_PaiXing__XiaoQiDui_小七对:
            //    case MajooUtil.HuPai_PaiXing__QingQiDui_清七对:
            //        this.imgHuType.transform.gameObject.SetActive(true);
            //        Sprite sp3 = Resources.Load("Texture/game/xiaojiesuan/HuType_4", typeof(Sprite)) as Sprite;
            //        this.imgHuType.sprite = sp3;
            //        break;
            //    case MajooUtil.HuPai_PaiXing__LongQiDui_龙七对:
            //    case MajooUtil.HuPai_PaiXing__QingLongQiDui_清龙七对:
            //        this.imgHuType.transform.gameObject.SetActive(true);
            //        Sprite sp4 = Resources.Load("Texture/game/xiaojiesuan/HuType_6", typeof(Sprite)) as Sprite;
            //        this.imgHuType.sprite = sp4;
            //        break;
            //    case MajooUtil.HuPai_PaiXing__ShuangLongQiDui_双龙七对:
            //    case MajooUtil.HuPai_PaiXing__ShuangQingLongQiDui_双清龙七对:
            //        this.imgHuType.transform.gameObject.SetActive(true);
            //        Sprite sp5 = Resources.Load("Texture/game/xiaojiesuan/HuType_8" , typeof(Sprite)) as Sprite;
            //        this.imgHuType.sprite = sp5;
            //        break;
            case MajooUtil.HuPai_PaiXing__LiuJu_流局:
                    this.imgHuType.transform.gameObject.SetActive(true);
                    Sprite sp6 = Resources.Load("Sprite/table/HuType_" + hutype, typeof(Sprite)) as Sprite;
                    this.imgHuType.sprite = sp6;
                    break;
        //    case MajooUtil.HuPai_PaiXing__QiangGang_抢杠:
        //        this.imgHuType.transform.gameObject.SetActive(true);
        //        Sprite sp7 = Resources.Load("Texture/game/xiaojiesuan/HuType_" + hutype, typeof(Sprite)) as Sprite;
        //        this.imgHuType.sprite = sp7;
        //        break;
        }

        StartCoroutine(hideImgHuType());
    }

    private IEnumerator hideImgHuType()
    {
        yield return new WaitForSeconds(2);
        this.imgHuType.transform.gameObject.SetActive(false);
        GameInfo.Instance.isEndStat = 1;
    }

    private void showNumer()
    {
        switch (this.gangType)
        {
            case 1://直杠
                        switch (gangPostion)
                        {
                            case "bot":
                                myNum.GetComponent<Number>().showNumber(200);
                                break;
                            case "right":
                                rightNum.GetComponent<Number>().showNumber(200);
                                break;
                            case "top":
                                topNum.GetComponent<Number>().showNumber(200);
                                break;
                            case "left":
                                leftNum.GetComponent<Number>().showNumber(200);
                                break;
                        }
                        if (fangGangPos == GameInfo.Instance.positon)
                        {
                            myNum.GetComponent<Number>().showNumber(-200);
                         }
                        else
                        {
                            string fg = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, fangGangPos);
                            switch (fg)
                            {
                                case "right":
                                    rightNum.GetComponent<Number>().showNumber(-200);
                                    break;
                                case "top":
                                    topNum.GetComponent<Number>().showNumber(-200);
                                    break;
                                case "left":
                                    leftNum.GetComponent<Number>().showNumber(-200);
                                    break;
                            }
                        }

                break;
            case 2://明杠
                        switch (gangPostion)
                        {
                            case "bot":
                                myNum.GetComponent<Number>().showNumber(300);
                                rightNum.GetComponent<Number>().showNumber(-100);
                                topNum.GetComponent<Number>().showNumber(-100);
                                leftNum.GetComponent<Number>().showNumber(-100);
                            break;
                            case "right":
                                myNum.GetComponent<Number>().showNumber(-100);
                                rightNum.GetComponent<Number>().showNumber(300);
                                topNum.GetComponent<Number>().showNumber(-100);
                                leftNum.GetComponent<Number>().showNumber(-100);
                            break;
                            case "top":
                                myNum.GetComponent<Number>().showNumber(-100);
                                rightNum.GetComponent<Number>().showNumber(-100);
                                topNum.GetComponent<Number>().showNumber(300);
                                leftNum.GetComponent<Number>().showNumber(-100);
                            break;
                            case "left":
                                myNum.GetComponent<Number>().showNumber(-100);
                                rightNum.GetComponent<Number>().showNumber(-100);
                                topNum.GetComponent<Number>().showNumber(-100);
                                leftNum.GetComponent<Number>().showNumber(300);
                            break;
                        }
                break;
            case 3://暗杠
                        switch (gangPostion)
                        {
                            case "bot":
                                myNum.GetComponent<Number>().showNumber(600);
                                rightNum.GetComponent<Number>().showNumber(-200);
                                topNum.GetComponent<Number>().showNumber(-200);
                                leftNum.GetComponent<Number>().showNumber(-200);
                            break;
                            case "right":
                                myNum.GetComponent<Number>().showNumber(-200);
                                rightNum.GetComponent<Number>().showNumber(600);
                                topNum.GetComponent<Number>().showNumber(-200);
                                leftNum.GetComponent<Number>().showNumber(-200);
                            break;
                            case "top":
                                myNum.GetComponent<Number>().showNumber(-200);
                                rightNum.GetComponent<Number>().showNumber(-200);
                                topNum.GetComponent<Number>().showNumber(600);
                                leftNum.GetComponent<Number>().showNumber(-200);
                            break;
                            case "left":
                                myNum.GetComponent<Number>().showNumber(-200);
                                rightNum.GetComponent<Number>().showNumber(-200);
                                topNum.GetComponent<Number>().showNumber(-200);
                                leftNum.GetComponent<Number>().showNumber(600);
                            break;
                        }
                break;
        }
        gangType = 0;
        gangPostion = "";
    }
}
