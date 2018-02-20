using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SectorType
{
    Main,   //直接的な視野
    Sub     //間接的な視野
}

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class ViewField : MonoBehaviour
{
    [Range(1, 5)]
    public float _radius = 3.0f;
    [Range(1, 360)]
    public int _degree = 1;
    [Range(3, 30)]
    public int _triangleNum = 10;

    MeshFilter m;

    [SerializeField, Header("Gizmoの色")]
    private Color mColor;

    public bool isDebugMeshUpdate = false;

    [SerializeField]
    private SectorType mSectorType;

    void Start()
    {
        m = this.GetComponent<MeshFilter>();
        m.mesh = CreateMesh();

        gameObject.AddComponent<MeshCollider>();
    }

    void Update()
    {
        // メッシュ
        m.mesh = CreateMesh();

        if (isDebugMeshUpdate)
            DebugMeshUpdate();

        //自分 -> Eyes -> sekizui1 -> center -> 14!Root 視界に入った人の方を向く
        GameObject neck = transform.parent.parent.gameObject;
    }

    /// <summary>
    /// 扇形のメッシュを生成
    /// </summary>
    /// <returns></returns>
    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        //頂点座標計算
        Vector3[] vertices = new Vector3[2 + _triangleNum];
        Vector2[] uv = new Vector2[2 + _triangleNum];

        vertices[0] = new Vector3(0f, 0f, 0f);
        uv[0] = new Vector2(0.5f, 0.5f);

        float deltaRad = Mathf.Deg2Rad * (_degree / (float)_triangleNum);
        for (int i = 1; i < 2 + _triangleNum; i++)
        {
            float mValue = 0;
            if (_degree <= 180)
            {
                mValue = 90 - (_degree / 2);
            }
            else if (_degree > 180)
            {
                mValue = (180 - _degree) / 2;
            }

            float x = Mathf.Cos(deltaRad * (i - 1) + (Mathf.Deg2Rad * mValue));
            float y = Mathf.Sin(deltaRad * (i - 1) + (Mathf.Deg2Rad * mValue));
            vertices[i] = new Vector3(
                x * _radius,
                0,
                y * _radius);

            uv[i] = new Vector2(x * 0.5f + 0.5f, y * 0.5f + 0.5f);
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        //三角形を構成する頂点のindexを，順に設定していく
        int[] triangles = new int[3 * _triangleNum];
        for (int i = 0; i < _triangleNum; i++)
        {
            triangles[(i * 3)] = 0;
            triangles[(i * 3) + 1] = i + 1;
            triangles[(i * 3) + 2] = i + 2;
        }
        mesh.triangles = triangles;

        mesh.name = "Sector";

        return mesh;
    }

    /// <summary>
    /// メッシュの更新(デバッグ用)
    /// </summary>
    void DebugMeshUpdate()
    {
        Destroy(GetComponent<MeshCollider>());
        gameObject.AddComponent<MeshCollider>();

        isDebugMeshUpdate = false;
    }

    // 広い範囲の視界に入った場合
    private void OnTriggerEnter(Collider col)
    {
        if (mSectorType == SectorType.Sub && col.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<SectorManager>().isFind = true;
        }

        //if (mSectorType == SectorType.Sub && col.gameObject.tag == "Torch")
        //    print("Torch Enter");
    }
    // 狭い範囲の視界に入った場合
    private void OnTriggerStay(Collider col)
    {
        if (mSectorType == SectorType.Main && col.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<SectorManager>().isFind = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<SectorManager>().isFind = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (CreateMesh() == null) return;

        Mesh mesh = CreateMesh();
        mesh.RecalculateNormals();

        Gizmos.color = mColor;
        Gizmos.DrawMesh(mesh, transform.position, transform.rotation, transform.lossyScale);
    }
}