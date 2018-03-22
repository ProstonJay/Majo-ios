using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicPanel : MonoBehaviour {

    public Button closeBtn;

    public Button saveBtn;

    public Toggle picToggle1;
    public Toggle picToggle2;
    public Toggle picToggle3;
    public Toggle picToggle4;
    public Toggle picToggle5;
    public Toggle picToggle6;
    public Toggle picToggle7;
    public Toggle picToggle8;


    // Use this for initialization
    void Start () {
        closeBtn.onClick.AddListener(closePressed);
        saveBtn.onClick.AddListener(savePressed);

        picToggle1.onValueChanged.AddListener((bool value) => OnPicClick(picToggle1, value));
        picToggle2.onValueChanged.AddListener((bool value) => OnPicClick(picToggle2, value));
        picToggle3.onValueChanged.AddListener((bool value) => OnPicClick(picToggle3, value));
        picToggle4.onValueChanged.AddListener((bool value) => OnPicClick(picToggle4, value));
        picToggle5.onValueChanged.AddListener((bool value) => OnPicClick(picToggle5, value));
        picToggle6.onValueChanged.AddListener((bool value) => OnPicClick(picToggle6, value));
        picToggle7.onValueChanged.AddListener((bool value) => OnPicClick(picToggle7, value));
        picToggle8.onValueChanged.AddListener((bool value) => OnPicClick(picToggle8, value));

        setUserIcon();

        
    }

    private void setUserIcon() {
        switch (GameInfo.Instance.UserIcon)
        {
            case 1:
                picToggle1.isOn = true;
                break;
            case 2:
                picToggle2.isOn = true;
                break;
            case 3:
                picToggle3.isOn = true;
                break;
            case 4:
                picToggle4.isOn = true;
                break;
            case 5:
                picToggle5.isOn = true;
                break;
            case 6:
                picToggle6.isOn = true;
                break;
            case 7:
                picToggle7.isOn = true;
                break;
            case 8:
                picToggle8.isOn = true;
                break;
            default:
                break;
        }
    }

    private int selectPicId;
    //
    public void OnPicClick(Toggle toggle, bool ison)
    {
        //Debug.Log(toggle.name  + (ison ? "On" : "Off"));
        if (ison)
        {
            switch (toggle.name)
            {
                case "head_1":
                    selectPicId = 1;
                    break;
                case "head_2":
                    selectPicId =2;
                    break;
                case "head_3":
                    selectPicId = 3;
                    break;
                case "head_4":
                    selectPicId = 4;
                    break;
                case "head_5":
                    selectPicId = 5;
                    break;
                case "head_6":
                    selectPicId = 6;
                    break;
                case "head_7":
                    selectPicId = 7;
                    break;
                case "head_8":
                    selectPicId = 8;
                    break;
                default:
                    break;
            }
            AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
        }

    }

    public void showPicBox()
    {
        setUserIcon();
    }

    //保存
    public void savePressed()
    {
        //
        if (selectPicId == GameInfo.Instance.UserIcon)
        {
            GameEvent.DoMsgTipEvent("头像没有更改!");
            return;
        }
        SocketModel rePicRequset = new SocketModel();
        rePicRequset.SetMainCmd(ProtocolMC.Main_Cmd_HALL);
        rePicRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_CHANGEICON);
        rePicRequset.SetCommand(0);
        List<string> message = new List<string>();
        message.Add(selectPicId.ToString());
        rePicRequset.SetMessage(message);
        NettySocket.Instance.SendMsg(rePicRequset);//发送这条消息给服务器

        //
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }


    //关闭
    public void closePressed()
    {
        this.transform.gameObject.SetActive(false);
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
