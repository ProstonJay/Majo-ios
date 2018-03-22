using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour {

    public Button helpBtn;
    public Button setBtn;
    public Button emailBtn;
    public Button zhanjiBtn;
    public Button qiutBtn;
    public Button fenxiangBtn;

    public GameObject qiutPanel;
    public GameObject usePanel;
    public GameObject helpPanel;
    public GameObject fenxiangPanel;
    public GameObject mailPanel;
    public GameObject battlePanel;

    // Update is called once per frame
    void Update()
    {

    }
    // Use this for initialization
    void Start () {
        helpBtn.onClick.AddListener(helpPressed);
        setBtn.onClick.AddListener(setPressed);
        emailBtn.onClick.AddListener(emailPressed);
        zhanjiBtn.onClick.AddListener(zhanjiPressed);
        qiutBtn.onClick.AddListener(qiutPressed);
        fenxiangBtn.onClick.AddListener(PressedFenXiang);

        qiutPanel.transform.gameObject.SetActive(false);
        usePanel.transform.gameObject.SetActive(false);
    }

    //分享
    public void PressedFenXiang()
    {
        fenxiangPanel.SetActive(true);
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }


    //帮助
    public void helpPressed()
    {
        helpPanel.SetActive(true);
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
    //设置
    public void setPressed()
    {
        usePanel.GetComponent<InfoPanel>().setAvtie();
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
    //邮件
    public void emailPressed()
    {
        mailPanel.SetActive(true);
        mailPanel.GetComponent<MailPanel>().showMail();
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
    //战绩
    public void zhanjiPressed()
    {
        if(battlePanel==null)
        {
            GameObject battlePanel = Instantiate(Resources.Load("Prefab/GameObject_Hall_ZhanJi")) as GameObject;
            battlePanel.transform.SetParent(this.transform, false);
            battlePanel.GetComponent<Panel_ZhanJi>().showBattle();
            AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
        }
     

    }
    //退出游戏
    public void qiutPressed()
    {
        qiutPanel.transform.gameObject.SetActive(true);
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
}
