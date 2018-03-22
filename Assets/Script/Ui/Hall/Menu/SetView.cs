using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetView : MonoBehaviour {

    public Button closeBtn;
    public Button yinyueBtn;
    public Button yinxiaoBtn;
    public Button yuyinBtn;

    public Toggle blueToggle;
    public Toggle redToggle;

    public Toggle ptToggle;
    public Toggle scToggle;

    // Use this for initialization
    void Start () {
        closeBtn.onClick.AddListener(closePressed);
        yinyueBtn.onClick.AddListener(yinyuePressed);
        yinxiaoBtn.onClick.AddListener(yinxiaoPressed);
        yuyinBtn.onClick.AddListener(yuyinPressed);

        blueToggle.onValueChanged.AddListener((bool value) => OnColorClick(blueToggle, value));
        redToggle.onValueChanged.AddListener((bool value) => OnColorClick(redToggle, value));

        ptToggle.onValueChanged.AddListener((bool value) => OnLgClick(ptToggle, value));
        scToggle.onValueChanged.AddListener((bool value) => OnLgClick(scToggle, value));
    }

    //初始化
    public void tryinit()
    {
        //如果有缓存,初始化
        if (PlayerPrefs.HasKey("ok"))
        {
            Debug.Log("有缓存");
            if (PlayerPrefs.GetString("TableColor") == "blue")
            {
                blueToggle.isOn = true;
            }
            else if (PlayerPrefs.GetString("TableColor") == "green")
            {
                redToggle.isOn = true;
            }
            //
            switch (PlayerPrefs.GetString("Language"))
            {
                case "pt":
                    ptToggle.isOn = true;
                    break;
                case "sc":
                    scToggle.isOn = true;
                    break;
                default:
                    break;
            }
            Debug.Log("PlayerPrefs.GetString(music)="+ PlayerPrefs.GetString("music"));
            if (PlayerPrefs.GetString("music") == "off")
            {
                Debug.Log("关音乐");
                Sprite sp = Resources.Load("Sprite/setting/btn_guang", typeof(Sprite)) as Sprite;
                yinyueBtn.GetComponent<Image>().sprite = sp;
            }
            else
            {
                Debug.Log("开音乐");
                Sprite sp = Resources.Load("Sprite/setting/btn_kai", typeof(Sprite)) as Sprite;
                yinyueBtn.GetComponent<Image>().sprite = sp;
            }

            if (PlayerPrefs.GetString("sound") == "off")
            {    
                Sprite sp = Resources.Load("Sprite/setting/btn_guang", typeof(Sprite)) as Sprite;
                yinxiaoBtn.GetComponent<Image>().sprite = sp;
            }
            else
            {
                Sprite sp = Resources.Load("Sprite/setting/btn_kai", typeof(Sprite)) as Sprite;
                yinxiaoBtn.GetComponent<Image>().sprite = sp;
            }

            if (PlayerPrefs.GetString("vioce") == "off")
            {
                Sprite sp = Resources.Load("Sprite/setting/btn_guang", typeof(Sprite)) as Sprite;
                yuyinBtn.GetComponent<Image>().sprite = sp;
            }
            else
            {
                Sprite sp = Resources.Load("Sprite/setting/btn_kai", typeof(Sprite)) as Sprite;
                yuyinBtn.GetComponent<Image>().sprite = sp;
            }

        }
        else {
            Debug.Log("没有缓存");
        }
    }

    //桌布
    public void OnColorClick(Toggle toggle, bool ison)
    {
        //Debug.Log(toggle.name  + (ison ? "On" : "Off"));
        if (ison)
        {
            AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
            switch (toggle.name)
            {
                case "blue":
                    PlayerPrefs.SetString("TableColor", "blue");
                    RoomEvent.DoChangeTableColor();
                    break;
                case "green":
                    PlayerPrefs.SetString("TableColor", "green");
                    RoomEvent.DoChangeTableColor();
                    break;
                default:
                    break;
            }
        }

    }
    //语言
    public void OnLgClick(Toggle toggle, bool ison)
    {
        //Debug.Log(toggle.name + (ison ? "On" : "Off"));
        if (ison)
        {
            AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
            switch (toggle.name)
            {
                case "pt":
                    PlayerPrefs.SetString("Language", "pt");
                    break;
                case "sc":
                    PlayerPrefs.SetString("Language", "sc");
                    break;
                default:
                    break;
            }
        }
    }

    //音乐
    public void yinyuePressed()
    {
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
        if (PlayerPrefs.GetString("music") == "on")
        {
            PlayerPrefs.SetString("music", "off");

            Sprite sp = Resources.Load("Sprite/setting/btn_guang", typeof(Sprite)) as Sprite;
            yinyueBtn.GetComponent<Image>().sprite = sp;

            AudioMgr.Instance.BGMStop();
        }
        else
        {
            PlayerPrefs.SetString("music", "on");

            Sprite sp = Resources.Load("Sprite/setting/btn_kai", typeof(Sprite)) as Sprite;
            yinyueBtn.GetComponent<Image>().sprite = sp;

            AudioMgr.Instance.BGMPlay("Bg/bgm", 0.05f);
        }
    }

    //音效
    public void yinxiaoPressed()
    {
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
        if (PlayerPrefs.GetString("sound") == "on")
        {
            PlayerPrefs.SetString("sound", "off");

            Sprite sp = Resources.Load("Sprite/setting/btn_guang", typeof(Sprite)) as Sprite;
            yinxiaoBtn.GetComponent<Image>().sprite = sp;
        }
        else
        {
            PlayerPrefs.SetString("sound", "on");

            Sprite sp = Resources.Load("Sprite/setting/btn_kai", typeof(Sprite)) as Sprite;
            yinxiaoBtn.GetComponent<Image>().sprite = sp;
        }
    }


    //语音
    public void yuyinPressed()
    {
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
        if (PlayerPrefs.GetString("vioce") == "on")
        {
            PlayerPrefs.SetString("vioce", "off");

            Sprite sp = Resources.Load("Sprite/setting/btn_guang", typeof(Sprite)) as Sprite;
            yuyinBtn.GetComponent<Image>().sprite = sp;
        }
        else
        {
            PlayerPrefs.SetString("vioce", "on");

            Sprite sp = Resources.Load("Sprite/setting/btn_kai", typeof(Sprite)) as Sprite;
            yuyinBtn.GetComponent<Image>().sprite = sp;
        }
    }


    //关闭
    public void closePressed()
    {
        this.transform.gameObject.SetActive(false);
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }

    private int tagKey = 0;
    //调用接口//
    public void setAvtie()
    {
        this.transform.gameObject.SetActive(true);
        tagKey = 1;
    }

    // Update is called once per frame
    void Update () {
        if (tagKey > 0)
        {
            tryinit();
            tagKey = 0;
        }
    }
}
