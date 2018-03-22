using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XiaoJieSuan : MonoBehaviour {

    public GameObject GO_MyInfo;
    public GameObject GO_RightInfo;
    public GameObject GO_TopInfo;
    public GameObject GO_LeftInfo;

    public Button nextGameBtn;
    //结算
    private List<PlayerData> plaerlist;

    public void setJieSuanData( )
    {
        List<PlayerData> plaerlist = GameInfo.Instance.jieSuanRoundData;
        for (int i = 0; i < plaerlist.Count; i++)
        {
            PlayerData pd = plaerlist[i];
            string pos = "";
            if (pd.getUserId() == GameInfo.Instance.positon) { pos = "bot"; } else {  pos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, pd.getUserId()); }
            switch (pos)
            {
                case "bot":
                    GO_MyInfo.GetComponent<JieSuanPanel>().initData(pd,1);
                    break;
                case "right":
                    GO_RightInfo.GetComponent<JieSuanPanel>().initData(pd);
                    break;
                case "top":
                    GO_TopInfo.GetComponent<JieSuanPanel>().initData(pd);
                    break;
                case "left":
                    GO_LeftInfo.GetComponent<JieSuanPanel>().initData(pd);
                    break;
            }

        }
        GameInfo.Instance.jieSuanRoundData.Clear();
        GameInfo.Instance.jieSuanRoundData = null;
    }

    // Use this for initialization
    void Start () {
        nextGameBtn.onClick.AddListener(nextGamePressed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextGamePressed()
    {
        if (GameInfo.Instance.jieSuanEndData!=null)
        {
            GameEvent.DoZongJieSuan();
            Destroy(this.transform.gameObject);
        }
        else
        {
            //重置数据
            GameInfo.Instance.reset();
            //重置界面
            GameEvent.DoReSetRoom();
            Destroy(this.transform.gameObject);
        }
    }
}
