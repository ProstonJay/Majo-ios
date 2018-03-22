using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class taohua : MonoBehaviour {

    private int tm;
    private int posX;
    private int detm;

    void Start()
    {
        detm = Random.Range(1, 8);
        Invoke("goDown", detm);
    }

    public void goDown()
    {
        tm = Random.Range(5, 8);
        posX = Random.Range(-500, 500);
        //设置动画
        Tweener tweener = transform.DOLocalMove(new Vector3(posX, -720, 0), tm);
        //动画播放一般是先块后慢，设置动画播放的曲线
        tweener.SetEase(Ease.InSine);//枚举类型，先后退在返回  动画曲线
        tweener.SetLoops(0);//设置循环的次数 默认是0 可以设置2
        tweener.OnComplete(OnTweenComplete);//动画播放完成之后执行的方法 参数1：方法名称； 动画函数
    }


    void Update()
    {

    }
    void OnTweenComplete()
    {
        Vector3 locpV3 = this.transform.localPosition;
        locpV3.y = 370;
        locpV3.x = Random.Range(-500, 500);
        this.transform.localPosition = locpV3;

        detm = Random.Range(1, 8);
        Invoke("goDown", detm);
    }

}
