using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource[] sounds;

    //リザルトメインのスクリプト
    private ClearManagement clearManager;

    //破棄しないように設定したオブジェクト
    [SerializeField]
    private GameObject ClearObject = default;

    // Use this for initialization
    void Start()
    {
        sounds = GetComponents<AudioSource>();

        ///リザルトメインのスクリプトの割り当て
        clearManager = ClearObject.GetComponent<ClearManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clearManager.IsPlayerClear == true)
        {
            //ゲームクリアした時
            sounds[0].Play();
        }
        else
        {
            //ゲームオーバーした時
            sounds[1].Play();
        }
    }

}
