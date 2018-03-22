using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class setPanel : MonoBehaviour {

    public Button loginBtn;
    public Button closeBtn;
    public Button jiesanBtn;
    public Text Text_RoomInfo;


    // Use this for initialization
    void Start () {
        loginBtn.onClick.AddListener(loginBtnPressed);
        closeBtn.onClick.AddListener(closeBtntPressed);
        jiesanBtn.onClick.AddListener(jiesanBtnPressed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //解散房间
    public void jiesanBtnPressed()
    {
        if (GameInfo.Instance.VoteDisFlag > 0)
        {
            GameEvent.DoMsgTipEvent("等待其它玩家投票！");
             closeBtntPressed();
            return;
        }
        SocketModel ReadyRequset = new SocketModel();
        ReadyRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        ReadyRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_INITIATE_DISMISS_发起解散);
        ReadyRequset.SetCommand(0);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        ReadyRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(ReadyRequset);//发送这条消息给服务器

        GameEvent.DoMsgTipEvent("已提交解散申请！");
        GameInfo.Instance.VoteDisFlag = 1;
        closeBtntPressed();
    }


    public void loginBtnPressed()
    {
        //NettySocket.Instance.Closed();
        //SceneManager.LoadScene("login");
        Application.Quit();
    }


    public void closeBtntPressed()
    {
        this.transform.gameObject.SetActive(false);
    }

    public void showRoomInfo()
    {
        string roomiD = GameInfo.Instance.roomId.ToString();
        string pw = "无密码";
        if (GameInfo.Instance.roomPassWord > 0)
        {
            pw = GameInfo.Instance.roomPassWord.ToString();
        }
        string maxPoint = GameInfo.Instance.maxPoint.ToString();
        string maxRound = GameInfo.Instance.maxRound.ToString();
        string zjDouble = "";
        if (GameInfo.Instance.isZjDouble == 1)
        {
            zjDouble = "庄翻倍";
        }
        string isQiangGang = "";
        if (GameInfo.Instance.canQiangGang == 1)
        {
            isQiangGang = "可抢杠";
        }
        string isDaiGen = "";
        if (GameInfo.Instance.isDaiGen == 1)
        {
            isDaiGen = "带根";
        }
        string isZimoHu = "";
        if (GameInfo.Instance.isZiMoHu == 1)
        {
            isZimoHu = "自摸胡";
        }
        Text_RoomInfo.text = "【房号:" +roomiD+ " 密码:" + pw + "】" + maxRound + "局," + maxPoint + "封顶," + zjDouble + "," + isQiangGang + "," + isDaiGen + "," + isZimoHu;
        
    }
}
