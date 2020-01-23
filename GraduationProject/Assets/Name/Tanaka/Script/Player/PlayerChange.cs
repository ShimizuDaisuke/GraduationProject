//=======================================================================================
//! @file   PlayerChange.cs
//! @brief  //プレイヤーのモデル変える処理
//! @author 田中歩夢
//! @date   01月22日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのモデル変えるクラス
public class PlayerChange : MonoBehaviour
{ 
    //メッシュ
    //消しゴム
    [SerializeField]
    private Mesh m_eraserMesh = default;
    //鉄
    [SerializeField]
    private Mesh m_ironMesh = default;

    //マテリアル
    //消しゴム
    [SerializeField]
    private Material m_eraserMaterial = default;

    //鉄
    [SerializeField]
    private Material[] m_ironMaterial = new Material[2];

    //プレイヤーの属性
    private PlayerType m_playerType = default;

    //プレイヤーヒット
    private PlayerHit m_playerHit = default;

    //メッシュフィルター
    private MeshFilter m_meshFilter = default;

    //メッシュレンダラー
    private MeshRenderer m_meshRenderer = default;

    //ボックスコライダー
    private BoxCollider m_boxCol = default;

    //プレイヤーオブジェクト
    private GameObject m_playerObj = null;

    //消しゴムのカバーオブジェクト
    private GameObject m_coverObj = null;

    // Start is called before the first frame update
    void Start()
    {
        m_playerType = GetComponent<PlayerType>();
        m_playerHit = GetComponent<PlayerHit>();
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_boxCol = GetComponent<BoxCollider>();

        //プレイヤーオブジェクトを探す
        m_playerObj = GameObject.FindGameObjectWithTag("Player");

        // 消しゴムのカバーオブジェクトを探す
        m_coverObj = GameObject.FindGameObjectWithTag("EraserDustCover");
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButton(0))
        //{
        //    ChangeModel();
        //}
    }


    //モデル変更
    public void ChangeModel()
    {
        if(m_playerType.IsPlayerType == PlayerType.Type.ERASER)
        {
            m_meshFilter.mesh = m_eraserMesh;
            m_meshRenderer.material = m_eraserMaterial;
            transform.localScale = new Vector3(13, 13, 13);
            m_boxCol.size = new Vector3(0.08f, 0.08f, 0.08f);

            if(m_playerHit.FixCover)
            {
                m_coverObj.transform.position = m_playerObj.transform.position;
            }

        }
        if (m_playerType.IsPlayerType == PlayerType.Type.IRON)
        {
            m_meshFilter.mesh = m_ironMesh;
            m_meshRenderer.materials = m_ironMaterial;
            transform.localScale = new Vector3(58, 58, 58);
            m_boxCol.size = new Vector3(0.04f, 0.02f, 0.01f);

            if (m_playerHit.FixCover)
            {
                m_coverObj.transform.position = new Vector3(0.0f, 100.0f, 0.0f);
            }
        }
    }

}
