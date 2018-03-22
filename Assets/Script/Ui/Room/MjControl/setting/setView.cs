using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class setView : MonoBehaviour {

    public Button setBtn;
    public GameObject setPanel;

    public Button settingBtn;
    public GameObject settingPanel;
    
    public GameObject DismissPanel;

    //
    private string disName = "";
    private int voteRlt;

    void Awake()
    {
        RoomEvent.InitiateDisMissEvent += InitiateDisMissEvent;
        RoomEvent.VoteDisResultsEvent += VoteDisResultsEvent;
    }

    private void VoteDisResultsEvent(int comd)
    {
        this.voteRlt = comd;
    }


    private void InitiateDisMissEvent(string pName)
    {
        this.disName = pName;
        GameInfo.Instance.VoteDisFlag = 1;
    }

    // Use this for initialization
    void Start () {
        setBtn.onClick.AddListener(setPressed);
        settingBtn.onClick.AddListener(settingPressed);
 
    }
	
	// Update is called once per frame
	void Update () {
        if (this.disName != "")
        {
            DismissPanel.SetActive(true);
            DismissPanel.GetComponent<DismissPanel>().ShowPanel(this.disName);
            this.disName = "";
        }
        if (this.voteRlt >0)
        {
            if (this.voteRlt == 26)
            {
                GameEvent.DoMsgTipEvent("有玩家不同意解散！");
                GameInfo.Instance.VoteDisFlag = 0;
            }
            else if (this.voteRlt == 27)
            {
                GameEvent.DoMsgTipEvent("房间已解散，请及时保存结算数据！");
                StartCoroutine(ShowGameOver());
     
            }
            DismissPanel.SetActive(false);
            this.voteRlt = 0;
        }
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1);
        if (GameInfo.Instance.jieSuanEndData != null)
        {
            GameEvent.DoZongJieSuan();
        }
        else
        {
            SceneManager.LoadScene("hall");
        }
        GameInfo.Instance.resetAll();
    }

    public void settingPressed()
    {
        settingPanel.GetComponent<SetView>().setAvtie();
    }

    public void setPressed()
    {
        setPanel.SetActive(true);
        setPanel.GetComponent<setPanel>().showRoomInfo();
    }


}
