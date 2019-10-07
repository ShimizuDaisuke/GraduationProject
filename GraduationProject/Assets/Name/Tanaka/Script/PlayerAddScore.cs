using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddScore : MonoBehaviour
{
    //スコア
    [SerializeField]
    private int m_score = 0;
    //消しカスのクラス
    private EraserDust m_eraserDust = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_score);
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        //消しカスに当たったら
        if (collider.gameObject.tag == "EraserDust")
        {
            //消しカスのクラスを取得
            m_eraserDust = collider.gameObject.GetComponent<EraserDust>();

            // ノーマルの消しカスによってスコアを増やす
            if (m_eraserDust.IsEraserDustKind == EraserDust.EraserDustKIND.NORMAL)
            {
                // スコアを増やす
                m_score += m_eraserDust.Point;
                //衝突した消しカスを消す
                Destroy(collider.gameObject);
            }

           
        }
    }
}
