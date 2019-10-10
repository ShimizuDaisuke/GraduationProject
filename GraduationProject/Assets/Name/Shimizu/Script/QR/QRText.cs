using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRText : MonoBehaviour
{
    [SerializeField]
    private Text qRText;

    public void IncreaseTime()
    {
        qRText.text = "制限時間が増えました";
    }

    public void PowerUp()
    {
        qRText.text = "攻撃力が上昇しました";
    }

}
