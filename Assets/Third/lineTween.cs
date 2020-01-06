using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class lineTween : MonoBehaviour
{

    public float _delayTime = .5f;

    public float[] _timeArr;//每一段要播放的时间长度
    private LineRenderer _lr;//需要被划线的物体

    private Vector3[] _lrPosArr;//该线需要被逐一绘制的点

    public GameObject[] objs;
    private List<Vector3> mlist;
    private GameObject initclone;

    private void Awake()
    {
        //mlist = new List<Vector3>();
        //for (int i = 0; i < objs.Length; i++)
        //{
        //    mlist.Add(objs[i].transform.position);
        //}
    }
    private void Start()
    {
        Init();
        StartCoroutine(DrawLine());
    }
    private IEnumerator DrawLine()
    {
        yield return new WaitForSeconds(_delayTime);
        //遍历所有点进行设置
        for (int k = 1, kmax = _lrPosArr.Length; k < kmax; k++)
        {
            Vector3 perPos = _lrPosArr[k - 1];//得到划线的前一个点
            Vector3 targetPos = _lrPosArr[k];//得到当前需要差值到达的点
            _lr.positionCount = k + 1;//扩充数组的数量
            _lr.SetPosition(k, perPos);//设置当前点坐标
            //声明临时变量,开始进行差值
            float curTime = Time.time;
            Vector3 midPos;
            while (Time.time < curTime + _timeArr[k - 1])
            {
                midPos = Vector3.Lerp(perPos, targetPos, (Time.time - curTime) / _timeArr[k - 1]);
                _lr.SetPosition(k, midPos);//设置射线
                yield return 0;
            }
            _lr.SetPosition(k, targetPos);//做完之后开始做下一个点,将位置设置到正确的位置
        }
    }

    void Init()
    {
        _lr = this.GetComponent<LineRenderer>();
        //_lr.positionCount = mlist.Count;//设置线段数
        //for (int i = 0; i < mlist.Count; i++)
        //{
        //    _lr.SetPosition(i, mlist[i]);//设置线段顶点坐标
        //}
        _lrPosArr = new Vector3[_lr.positionCount];

        for (int i = 0, max = _lr.positionCount; i < max; i++)
        {
            //得到具体的相对坐标值
            _lrPosArr[i] = _lr.GetPosition(i);
        }
        _lr.positionCount = 1;
    }
}
