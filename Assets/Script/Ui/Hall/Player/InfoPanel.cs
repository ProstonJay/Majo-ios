using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public Button closeBtn;
    public Button changePicBtn;
    public Button changeNameBtn;

    public Text idText;
    public Text nameText;

    public Image iconImage;
    public Image sexImage;

    public GameObject picPanel;
    public GameObject namePanel;

    private string newName;
    private string newIcon;

    void Awake()
    {
        GameEvent.StringEvent += ReName;
        GameEvent.PicEvent += RePic;
    }

    private void RePic(string value)
    {
        newIcon = value;

    }

    private void ReName(string value)
    {
        newName = value;

    }

    private int tagKey;

    // Use this for initialization
    void Start () {
        closeBtn.onClick.AddListener(closePressed);
        changePicBtn.onClick.AddListener(changePicPressed);
        changeNameBtn.onClick.AddListener(changeNamePressed);
        //
        picPanel.gameObject.SetActive(false);
        namePanel.gameObject.SetActive(false);
    }

    public void setAvtie()
    {
        this.transform.gameObject.SetActive(true);
        tagKey = 1;
    }

    public void showUserPanel()
    {
        //
        idText.text = GameInfo.Instance.UserID.ToString();
        nameText.text = GameInfo.Instance.UserName;
        Sprite sp = Resources.Load("Sprite/head/head_" + GameInfo.Instance.UserIcon, typeof(Sprite)) as Sprite;
        iconImage.sprite = sp;

        string str;
        if (GameInfo.Instance.UserIcon > 4)
        {
            str = "nan";
        }
        else
        {
            str = "nv";
        }
        Sprite sp1 = Resources.Load("Sprite/head/" + str , typeof(Sprite)) as Sprite;
        sexImage.sprite = sp1;

    }

    //改头像
    public void changePicPressed()
    {
        picPanel.gameObject.SetActive(true);
        picPanel.GetComponent<PicPanel>().showPicBox();
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }
    //改名
    public void changeNamePressed()
    {
        namePanel.gameObject.SetActive(true);
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
        if (newName!="")
        {
            nameText.text = newName;
            namePanel.GetComponent<NamePanel>().resetNameTxt();
            namePanel.gameObject.SetActive(false);
            newName = "";
        }

        if (newIcon != "")
        {
            Sprite sp = Resources.Load("Sprite/head/head_" + newIcon, typeof(Sprite)) as Sprite;
            iconImage.sprite = sp;

            string str;
            if (GameInfo.Instance.UserIcon > 4)
            {
                str = "nan";
            }
            else
            {
                str = "nv";
            }
            Sprite sp1 = Resources.Load("Sprite/head/" + str, typeof(Sprite)) as Sprite;
            sexImage.sprite = sp1;

            newIcon = "";

            picPanel.gameObject.SetActive(false);
        }
        if (tagKey > 0)
        {
            showUserPanel();
            tagKey = 0;
        }
    }
}
