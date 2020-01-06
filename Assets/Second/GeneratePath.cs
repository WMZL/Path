using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 路径管理器
/// </summary>
public class GeneratePath : MonoBehaviour
{
    /// <summary>
    /// 路径点列表
    /// </summary>
    private List<Vector3> m_CurPathList = new List<Vector3>();
    /// <summary>
    /// 路标父物体
    /// </summary>
    private GameObject m_LuBiaoParent;

    public GameObject[] objs;
    private GameObject initclone;

    void Awake()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            m_CurPathList.Add(objs[i].transform.position);
        }
        m_LuBiaoParent = new GameObject("m_LuBiaoParent");

        OnButtonLuJingBianJiClick();
    }

    /// <summary>
    /// 点击后设置路线
    /// </summary>
    public void OnButtonLuJingBianJiClick()
    {
        StartCoroutine(TestGen());
    }

    IEnumerator TestGen()
    {
        for (int i = 0; i < m_CurPathList.Count - 1; i++)
        {
            yield return new WaitForSeconds(1f);
            creatRoadForList(m_CurPathList[i], m_CurPathList[i + 1]);
        }
    }

    #region Demo
    public GameObject InspectionRoad;

    public Material mat;
    /// <summary>
    /// 通过list生成道路
    /// </summary>
    /// <param name="postions"></param>
    public void creatRoadForList(Vector3 start, Vector3 end, string name = "testPos")
    {
        InspectionRoad.SetActive(true);

        float z = Vector3.Distance(start, end);
        GameObject obj = creatGameObjct(Vector3.zero, new Vector3(0, 0, z), 3, mat);
        //obj.transform.DOFade(0,2);
        obj.transform.position = start;
        obj.transform.name = name;
        obj.transform.rotation = Quaternion.LookRotation(end - start);
        obj.transform.SetParent(InspectionRoad.transform);
    }

    /// <summary>
    /// 生成两点mesh
    /// </summary>
    /// <param name="begin">开始点</param>
    /// <param name="end">终点</param>
    /// <param name="r">半径</param>
    /// <param name="_mat">材质球</param>
    /// <returns>返回物体</returns>
    GameObject creatGameObjct(Vector3 begin, Vector3 end, float r, Material _mat)
    {
        Vector3[] Pos = new Vector3[4];
        int[] Triangles = new int[6];
        Vector2[] uvs = new Vector2[4];
        Pos[0] = begin + new Vector3(r, 0, 0);
        Pos[1] = begin + new Vector3(-r, 0, 0);
        Pos[2] = end + new Vector3(r, 0, 0);
        Pos[3] = end + new Vector3(-r, 0, 0);
        Triangles[0] = 0;
        Triangles[1] = 1;
        Triangles[2] = 3;
        Triangles[3] = 0;
        Triangles[4] = 3;
        Triangles[5] = 2;
        GameObject go = new GameObject();
        MeshFilter filter = go.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        filter.sharedMesh = mesh;
        mesh.vertices = Pos;
        mesh.triangles = Triangles;
        for (int i = 0; i < Pos.Length; i++)
        {
            uvs[i] = new Vector2(Pos[i].x, Pos[i].z) / (2 * r);

        }
        Vector2[] newUV = uvs;
        mesh.uv = newUV;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = _mat;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;//不投影
        renderer.receiveShadows = false;
        return go;

    }
    #endregion
}
