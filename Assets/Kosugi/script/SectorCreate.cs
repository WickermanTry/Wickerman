using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SectorType
{
    Main,
    Sub
}

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SectorCreate : MonoBehaviour
{
    [Range(1, 5)]
    public float radius = 3.0f;
    [Range(1, 360)]
    public int mDegree = 1;
    [Range(3, 30)]
    public int triangleNum = 10;
    [SerializeField, Header("プレイヤーが視線に入ったか")]
    private bool mPlayerSearch = false;

    MeshFilter m;

    public bool debugMeshUpdate = false;

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

        if (debugMeshUpdate)
            DebugMeshUpdate();
    }

    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        //頂点座標計算
        Vector3[] vertices = new Vector3[2 + triangleNum];
        Vector2[] uv = new Vector2[2 + triangleNum];

        vertices[0] = new Vector3(0f, 0f, 0f);
        uv[0] = new Vector2(0.5f, 0.5f);

        float deltaRad = Mathf.Deg2Rad * (mDegree / (float)triangleNum);
        for (int i = 1; i < 2 + triangleNum; i++)
        {
            float mValue = 0;
            if (mDegree <= 180)
            {
                mValue = 90 - (mDegree / 2);
            }
            else if (mDegree > 180)
            {
                mValue = (180 - mDegree) / 2;
            }

            float x = Mathf.Cos(deltaRad * (i - 1) + (Mathf.Deg2Rad * mValue));
            float y = Mathf.Sin(deltaRad * (i - 1) + (Mathf.Deg2Rad * mValue));
            vertices[i] = new Vector3(
                x * radius,
                0,
                y * radius);

            uv[i] = new Vector2(x * 0.5f + 0.5f, y * 0.5f + 0.5f);
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        //三角形を構成する頂点のindexを，順に設定していく
        int[] triangles = new int[3 * triangleNum];
        for (int i = 0; i < triangleNum; i++)
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

        debugMeshUpdate = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (mSectorType == SectorType.Main && col.gameObject.tag == "Player")
        {
            mPlayerSearch = true;
            print("Player Enter");
        }
        else if (mSectorType == SectorType.Sub && col.gameObject.tag == "Torch")
            print("Torch Enter");
    }

    public bool GetSearchFlag()
    {
        return mPlayerSearch;
    }
}
