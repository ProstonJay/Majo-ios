using cn.sharesdk.unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DaJieSuan : MonoBehaviour {

    public GameObject jieSuanPanel1;
    public GameObject jieSuanPanel2;
    public GameObject jieSuanPanel3;
    public GameObject jieSuanPanel4;

    public Button leaveBtn;
    public Button Btn_FenXiang;
    public Text Text_timeStamp;
    //分享
    public ShareSDK ssdk;

    //结算
    private List<PlayerData> plaerlist;

    // Use this for initialization
    void Start () {
        leaveBtn.onClick.AddListener(leavePressed);
        Btn_FenXiang.onClick.AddListener(clickPresss);
        //截屏分享
        ssdk = GetComponent<ShareSDK>();
        ssdk.shareHandler += ShareResultHandler;  
    }

    void clickPresss()
    {
        string pstr = GameInfo.Instance.roomId.ToString();
        ScreenCapture.CaptureScreenshot(pstr+".png");
        StartCoroutine(savePic());
    }

    private IEnumerator savePic()
    {
        yield return new WaitForSeconds(1f);
        string pstr = GameInfo.Instance.roomId.ToString();
        string path = Application.persistentDataPath + "/"+pstr+ ".png";
        ShareContent content = new ShareContent();
        //content.SetTitle("【九尾麻将馆】");
        //content.SetText("房间号：1001 ，封顶32番，自摸翻倍 密码：234423");
        //content.SetComment("快来人，三缺一啦");
        content.SetImagePath(path);
        content.SetShareType(ContentType.Image);
        //ssdk.ShowPlatformList(null, content, 100, 100);
        ssdk.ShowShareContentEditor(PlatformType.WeChat, content);

    }

    void ShareResultHandler(int repID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            GameEvent.DoMsgTipEvent("分享成功");
        }
        //失败  
        else if (state == ResponseState.Fail)
        {
            GameEvent.DoMsgTipEvent("分享失败");
        }
        //关闭  
        else if (state == ResponseState.Cancel)
        {
            GameEvent.DoMsgTipEvent("分享取消");
        }
    }

    // Update is called once per frame
    void Update () {
     
    }

    //
    public void showGameOver()
    {
        List<PlayerData> plaerlist = GameInfo.Instance.jieSuanEndData;
        //最高放炮次数
        int fangpao = 0;
        int paoshou = 0; ;
        //赢的最多的金币
        int gold = 0;
        int dayingjia = 0;
        //解析数据
        for (int i = 0; i < plaerlist.Count; i++)
        {
            PlayerData pd = plaerlist[i];
            if (i == 0)
            {
                if(pd.getFangPaoCunt()> fangpao)
                {
                    fangpao = pd.getFangPaoCunt();
                    paoshou = 11;
                }
                if (pd.getWinGold() > gold)
                {
                    gold = pd.getWinGold();
                    dayingjia = 11;
                }
                jieSuanPanel1.GetComponent<DaJiePreble>().setData(pd);
                //时间戳   
                Text_timeStamp.text = pd.getDataStamp();
            }
            else if (i == 1)
            {
                if (pd.getFangPaoCunt() > fangpao)
                {
                      gold = pd.getWinGold();
                    paoshou = 12;
                }
                if (pd.getWinGold() > gold)
                {
                    gold = pd.getWinGold();
                    dayingjia = 12;
                }
               jieSuanPanel2.GetComponent<DaJiePreble>().setData(pd);
            }
            else if (i == 2)
            {
                if (pd.getFangPaoCunt() > fangpao)
                {
                    fangpao = pd.getFangPaoCunt();
                    paoshou = 13;
                }
                if (pd.getWinGold() > gold)
                {
                    gold = pd.getWinGold();
                    dayingjia = 13;
                }
                jieSuanPanel3.GetComponent<DaJiePreble>().setData(pd);
            }
            else if (i == 3)
            {
                if (pd.getFangPaoCunt() > fangpao)
                {
                    fangpao = pd.getFangPaoCunt();
                    paoshou = 14;
                }
                if (pd.getWinGold() > gold)
                {
                    gold = pd.getWinGold();
                    dayingjia = 14;
                }
                jieSuanPanel4.GetComponent<DaJiePreble>().setData(pd);
            }

        }
        switch (paoshou)
        {
            case 11:
                jieSuanPanel1.GetComponent<DaJiePreble>().setPaoShou();
                break;
            case 12:
                jieSuanPanel2.GetComponent<DaJiePreble>().setPaoShou();
                break;
            case 13:
                jieSuanPanel3.GetComponent<DaJiePreble>().setPaoShou();
                break;
            case 14:
                jieSuanPanel4.GetComponent<DaJiePreble>().setPaoShou();
                break;
        }
        switch (dayingjia)
        {
            case 11:
                jieSuanPanel1.GetComponent<DaJiePreble>().setDaYingJia();
                break;
            case 12:
                jieSuanPanel2.GetComponent<DaJiePreble>().setDaYingJia();
                break;
            case 13:
                jieSuanPanel3.GetComponent<DaJiePreble>().setDaYingJia();
                break;
            case 14:
                jieSuanPanel4.GetComponent<DaJiePreble>().setDaYingJia();
                break;
        }
        Debug.Log("paoshou" + paoshou + "dayingjia" + dayingjia);
        GameInfo.Instance.jieSuanEndData.Clear();
        GameInfo.Instance.jieSuanEndData = null;
    }
    //离开房间
    public void leavePressed()
    {
        GameInfo.Instance.resetAll();
        Destroy(this.transform.gameObject);
        SceneManager.LoadScene("hall");
    }
}
