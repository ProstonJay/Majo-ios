using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HallView : MonoBehaviour
{

    public Button CreatBtn;
    public Button JoinBtn;
    public Text text_fk;
    public Text text_name;
    public Image iocn;

    private GameObject CreatWindow;

    private GameObject JoinWindow;

    private  string SenceName = "";
    private string newName = "";
    private string picId = "";
    private int newFk;

    // Use this for initialization
    void Awake()
    {
        GameEvent.SenceEvent += DoSenceEvent;
        GameEvent.StringEvent += reNameEvent;
        GameEvent.PicEvent += rePicEvent;
        GameEvent.UpdateFkEvent += UpdateFkEvent;
    }
    // Use this for initialization
    void Start()
    {
        CreatBtn.onClick.AddListener(CreatPressed);
        JoinBtn.onClick.AddListener(JoinPressed);

        text_fk.text = " "+GameInfo.Instance.UserFK;
        text_name.text =  GameInfo.Instance.UserName;
        Sprite sp = Resources.Load("Sprite/head/head_" + GameInfo.Instance.UserIcon, typeof(Sprite)) as Sprite;
        iocn.sprite = sp;
        //
        AudioMgr.Instance.BGMPlay("Bg/bgm",0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (SenceName != "")
        {
            SceneManager.LoadScene(SenceName);
        }

        if (newName != "")
        {
            text_name.text = newName;
            newName = "";
        }

        if (picId != "")
        {
            Sprite sp = Resources.Load("Sprite/head/head_" + picId, typeof(Sprite)) as Sprite;
            iocn.sprite = sp;

            picId = "";
        }
        if (newFk >0)
        {
            text_fk.text = " " + GameInfo.Instance.UserFK;

            newFk = 0;
        }
    }
    public void CreatPressed()
    {
        if (CreatWindow == null)
        {
            CreatWindow = Instantiate(Resources.Load("Prefab/GameObject_Hall_CreatRoom")) as GameObject;
            CreatWindow.transform.SetParent(this.transform, false);
            AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
        }
    }
    public void JoinPressed()
    {
        if (JoinWindow == null)
        {
            JoinWindow = Instantiate(Resources.Load("Prefab/GameObject_Hall_JoinRoom")) as GameObject;
            JoinWindow.transform.SetParent(this.transform, false);
            AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
        }
    }

    void DoSenceEvent(string str)
    {
        SenceName = str; 
     
    }

    void reNameEvent(string str)
    {
        newName = str;
    }

    void rePicEvent(string str)
    {
        picId = str;
    }
    void UpdateFkEvent()
    {
        newFk = 1;
    }
}
