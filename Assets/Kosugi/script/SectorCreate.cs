using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SectorType
{
    Main,   //直接的な視界
    Sub     //間接的な視界
}

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SectorCreate : MonoBehaviour
{
    [Range(1, 5)]
    public float _radius = 3.0f;
    [Range(1, 360)]
    public int _degree = 1;
    [Range(3, 30)]
    public int _triangleNum = 10;
    [SerializeField, Header("プレイヤーが範囲に入ったか")]
    private bool isPlayerSearch = false;

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
        m.mesh = CreateMesh();

        if (isDebugMeshUpdate)
            DebugMeshUpdate();
    }

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

    void DebugMeshUpdate()
    {
        Destroy(GetComponent<MeshCollider>());
        gameObject.AddComponent<MeshCollider>();

        isDebugMeshUpdate = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (mSectorType == SectorType.Main && col.gameObject.tag == "Player")
        {
            isPlayerSearch = true;
            print("Player Enter");
        }
        else if (mSectorType == SectorType.Sub && col.gameObject.tag == "Torch")
            print("Torch Enter");
    }

    public bool GetSearchFlag()
    {
        return isPlayerSearch;
    }

    private void OnDrawGizmos()
    {
        if (CreateMesh() == null) return;

        Mesh mesh = CreateMesh();
        mesh.RecalculateNormals();

        Gizmos.color = mColor;
        Gizmos.DrawMesh(mesh,transform.position,transform.rotation,Vector3.one);
    }
}
