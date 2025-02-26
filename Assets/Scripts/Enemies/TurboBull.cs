using UnityEngine;
using TMPro;

public class TurboBull : BullController
{
    [Header("Charge Settings")]
    [SerializeField] private float chargeTime = 1.5f;   // 溜める時間
    private bool isCharging = false;

    [Header("Warning Settings")]
    [SerializeField] private LineRenderer WarningLine;  // 警告ライン
    private LineRenderer warningLine;
    private float currentChargeTime = 0f;

    public override void Initialize(Vector3 start, Vector3 end, bool isSpawn)
    {
        base.Initialize(start, end);
        ShowWarning(true);
    }

    private void Update()
    {
        if (isCharging)
        {
            base.Move();
        }
        else
        {
            ChargePrepare();
        }
    }

    private void ChargePrepare()
    {
        // 溜め時間が経過したら突進開始
        if (currentChargeTime < chargeTime)
        {
            currentChargeTime += Time.deltaTime;
            // 溜め中の処理（エフェクトやアニメーション）
        }
        else
        {
            // 溜め完了後、突進を開始
            isCharging = true;
            currentChargeTime = 0f;
            ShowWarning(false);
        }
    }

    private void ShowWarning(bool show)
    {
        if (show)
        {
            // 警告ラインの生成
            warningLine = Instantiate(WarningLine, transform.position, transform.rotation*Quaternion.Euler(90, 90, 0));
        }
        else
        {
            // 非表示
            Destroy(warningLine.gameObject);
        }
    }

    private void StopCharge()
    {
        isCharging = false;
        currentChargeTime = 0f;
        ShowWarning(false); // 警告を非表示にする
        // 突進終了後の処理（リセットなど）
    }
}
