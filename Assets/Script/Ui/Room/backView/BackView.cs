using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackView : MonoBehaviour
{

    public Text img_roomId;
    public Image img_tableColor;

    private string tablePath = "";

    private int startFlag;
    private int changeTableColor;

    void Awake()
    {
        GameEvent.GameStartEvent += GameStart;
        //
        RoomEvent.ChangeTableColorEvent += ChangeTableColorEvent;
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log("ddddddddddddddddddddddddddddd");
    //    GameObject ani = Instantiate(Resources.Load("Prefab/GameObject_number")) as GameObject;
    //    //ani.GetComponent<Image>().SetNativeSize();
    //    ani.transform.SetParent(this.transform, false);
    //    ani.transform.localPosition = new Vector3(280, -140, 0);
    //    ani.GetComponent<Number>().showNumber(2000);
    //    ani.GetComponent<GameAnimator_Move>().goMove();

    //    GameObject ani1 = Instantiate(Resources.Load("Prefab/GameObject_number")) as GameObject;
    //    //ani.GetComponent<Image>().SetNativeSize();
    //    ani1.transform.SetParent(this.transform, false);
    //    ani1.transform.localPosition = new Vector3(430, 30, 0);
    //    ani1.GetComponent<Number>().showNumber(2000);
    //    ani1.GetComponent<GameAnimator_Move>().goMove();

    //    GameObject ani2 = Instantiate(Resources.Load("Prefab/GameObject_number")) as GameObject;
    //    //ani.GetComponent<Image>().SetNativeSize();
    //    ani2.transform.SetParent(this.transform, false);
    //    ani2.transform.localPosition = new Vector3(190, 240, 0);
    //    ani2.GetComponent<Number>().showNumber(2000);
    //    ani2.GetComponent<GameAnimator_Move>().goMove();

    //    GameObject ani3 = Instantiate(Resources.Load("Prefab/GameObject_number")) as GameObject;
    //    //ani.GetComponent<Image>().SetNativeSize();
    //    ani3.transform.SetParent(this.transform, false);
    //    ani3.transform.localPosition = new Vector3(-430, 30, 0);
    //    ani3.GetComponent<Number>().showNumber(2000);
    //    ani3.GetComponent<GameAnimator_Move>().goMove();
    //}

    //换桌布
    private void ChangeTableColorEvent()
    {
        changeTableColor = 1;
    }

    //开局
    private void GameStart(int value)
    {
        if (value ==1)
        {
            startFlag = 1;
        }

    }

    // Use this for initialization
    void Start () {
        changeTable();

    }

    private void changeTable()
    {
        if (PlayerPrefs.GetString("TableColor") == "blue")
        {
            tablePath = "Sprite/table/background_2";
        }
        else if (PlayerPrefs.GetString("TableColor") == "green")
        {
            tablePath = "Sprite/table/background_1";
        }
    }

    private void upDateRound()
    {
        img_roomId.text = "第 " + GameInfo.Instance.round + " 局";
    }
	
	// Update is called once per frame
	void Update () {
        if (tablePath!="")
        {

            Sprite sp = Resources.Load(tablePath, typeof(Sprite)) as Sprite;
            img_tableColor.sprite = sp;
            tablePath = "";
        }
        if (startFlag == 1)
        {
            upDateRound();
            startFlag = 0;
        }

        if (changeTableColor == 1)
        {
            changeTable();
            changeTableColor = 0;
        }
    }

    //重置数据
    public void resetView()
    {
        img_roomId.text = "";
    }

    //重连
    public void ReJoinRoom()
    {
        changeTable();
        upDateRound();
    }

}
