using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject[] objs;
    private List<Vector3> mlist;
    private GameObject initclone;

    private void Awake()
    {
        mlist = new List<Vector3>();
        for (int i = 0; i < objs.Length; i++)
        {
            mlist.Add(objs[i].transform.position);
        }

        OnClick();
    }

    public void OnClick()
    {
        GameObject go = Resources.Load("testid") as GameObject;

        for (int i = 0; i < mlist.Count - 1; i++)
        {
            initclone = GameObject.Instantiate(go);
            initclone.GetComponent<move>().SetLine(mlist[i], mlist[i + 1]);
        }
    }
}
