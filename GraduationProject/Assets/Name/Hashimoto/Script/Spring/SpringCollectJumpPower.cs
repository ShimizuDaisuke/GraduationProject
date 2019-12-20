//=======================================================================================
//! @file   SpringCollectJumpPower.cs
//!
//! @brief  「ばねがジャンプパワーを溜める」動き
//!
//! @author 橋本奉武
//!
//! @date   12月13日
//!
//! @note  とあるボタンを押してから離したまでに掛かった最低時間：0.084(s)
//!        1フレーム経過時間：0.016(s)
//=======================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCollectJumpPower : MonoBehaviour
{
    // ばねの状態
    public enum StateKind
    {
        ERR = -1,   // 例外

        NONE,       // 何もしない
        SAMLL,      // 縮む
        BIG,        // 伸びる
        STOP,       // ジャンプパワーを溜め込む
        JUMP,       // ジャンプ
        
        MAX,        // この列挙型の最大数
    }

    // <列挙型> ばねによる今の状態
    private StateKind Enum_NowStateKind;

    // <列挙型> ばねによる前の状態
    private StateKind Enum_OnceStateKind;

    // < スクリプト > ばねのサイズ
    private SpringScale Script_SpringScale;

    // プレイヤーの前の位置
    private Vector3 OncePosition;

    // < テスト > グルグル回る回る
    [SerializeField]
    private GameObject rotationobj;

    //ジョイスティッククラス
    [SerializeField]
    private Joystick m_joystick = default;

    // テスト
    public GameObject testobj;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {   
        // スクリプト「ばねのサイズ」を取得する
        Script_SpringScale = GetComponent<SpringScale>();

        // <テスト> ばねを縮める
        Enum_NowStateKind = Enum_OnceStateKind = StateKind.SAMLL;

        // プレイヤーの前の位置を取得する
        OncePosition = transform.position;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
       
        // ジョイスティックの値
        Vector2 d = new Vector2(m_joystick.Horizontal, m_joystick.Vertical);

        // ジョイスティックが向いている方向                            意図的角度↓
        float joystick_degree = (-Mathf.Atan2(d.y, d.x)) * Mathf.Rad2Deg - 90.0f;

        // 「ジョイスティックの真ん中にあるオブジェクトが中点から動いた距離」の割合          最大距離↓
        float joystick_lengthrate = Vector2.Distance(Vector2.zero, new Vector2(d.x, d.y))  /   1.0f    ;

        // ジョイスティックの真ん中にあるオブジェクトが中点から動いた距離の割合

        // グルグル回る回る                                                      意図的角度↓
        rotationobj.transform.rotation = Quaternion.Euler(0, joystick_degree, -(60.0f -  60.0f * joystick_lengthrate + 30.0f));

        // < テスト > ---------------------------------------------------------------------------------------------------------
        float l = 2.0f; float angle1 = (-joystick_degree-180.0f) * Mathf.Deg2Rad; float angle2 = (60.0f - 60.0f * joystick_lengthrate + 30.0f) * Mathf.Deg2Rad;
        // 何かの値
        Vector3 degreepos = new Vector3(l * Mathf.Cos(angle2) * Mathf.Cos(angle1),
                                        l * Mathf.Sin(angle2), 
                                        l * Mathf.Cos(angle2) * Mathf.Sin(angle1));


        // オブジェクトを動かす
        if (testobj != null)  testobj.transform.position = transform.position + degreepos;

        //return;


        // スクリプト「ばねのサイズ」が存在していない場合、何もしない
        if (Script_SpringScale == null) return;

        // ばねの状態
        switch (Enum_NowStateKind)
        {
            // ばねが「縮む」もしくは「伸びる」場合
            case StateKind.SAMLL:
            case StateKind.BIG:
            {
                // ばねを拡大するか(true)、縮小するか(false)決める
                bool state = (Enum_NowStateKind == StateKind.BIG) ? true : false;

                // 拡大・縮小する
                Script_SpringScale.ChangeSpringScale(state);

                // グルグル回る回る
                //rotationobj.transform.rotation = Quaternion.Euler(a, 0, -a);


                // 最大まで拡大もしくは縮小した場合
                if (Script_SpringScale.IsSpring_MaxBigSmall == true)
                {
                    // 最大まで拡大もしくは縮小した情報をリセットする
                    Script_SpringScale.IsSpring_MaxBigSmall = false;

                    // ばねの状態が変わる前に、ばねの前の状態を更新する
                    Enum_OnceStateKind = Enum_NowStateKind;

                    // ばねの状態を変える (最大まで拡大 → ジャンプ、   最小まで縮小 → 溜める)
                    Enum_NowStateKind = (Enum_NowStateKind == StateKind.BIG) ? StateKind.JUMP : StateKind.STOP;

                    break;
                }

                // 常にばねの前の状態を更新する
                Enum_OnceStateKind = Enum_NowStateKind;

                break;
            }

            // ばねがジャンプパワーを溜め込む場合
            case StateKind.STOP:
            {   
                // < テスト >  ばねを伸ばす
                Enum_NowStateKind = StateKind.BIG;
                
                // 常にばねの前の状態を更新する
                Enum_OnceStateKind = Enum_NowStateKind;

                break;
            }


            // ばねがジャンプする場合
            case StateKind.JUMP:
            {
                // 初めてばねがジャンプし始める場合
                if(Enum_OnceStateKind != Enum_NowStateKind)
                {
                   // プレイヤーが向いてる方向
                   Vector3 PlayerBoneDegree = rotationobj.transform.localEulerAngles;
                        
                    // プレイヤーが向いてる方向を正規化する
                    PlayerBoneDegree.Normalize();

                    // 上空へ力を加える                    テスト↓
                    GetComponent<Rigidbody>().AddForce(degreepos * 300.0f);

                }

                    // 現在のばねの高さが前のばねの高さより低い場合
                    if (transform.position.y <= OncePosition.y)
                {
                    //　ばねから飛ばすレイを作成する 
                    Ray ray = new Ray(transform.position, -transform.up);

                    // レイの長さ  テスト↓
                    float distance = 0.9f;

                    // レイに当たったオブジェクト入れる箱
                    RaycastHit hit;

                    // レイの可視化
                    Debug.DrawLine(ray.origin, ray.origin + -transform.up * distance, Color.red);

                    // ばねの状態が変わる前に、ばねの前の状態を更新する
                    Enum_OnceStateKind = Enum_NowStateKind;

                    // もしレイが他のオブジェクトに当たった場合、ばねを縮める
                    if ((Physics.Raycast(ray, out hit, distance))) { Enum_NowStateKind = StateKind.SAMLL; }

                    break;
                }

                // 常にばねの前の状態を更新する
                Enum_OnceStateKind = Enum_NowStateKind;

                break;
            }
        }        

        // 常にばねの前の位置を更新する
        OncePosition = transform.position;

    }
}
