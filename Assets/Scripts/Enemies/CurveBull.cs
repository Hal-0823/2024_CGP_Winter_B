using UnityEngine;
using System.Collections.Generic;

public class CurveBull : BullController
{
    public List<Vector3> PathPoints = new List<Vector3>(); // 進むべき経路
    private int currentPointIndex = 0; // 次に向かうポイントのインデックス
    public int Divisions = 64; // 目的地までの分割数
    public float CurveAmount = 4f; // カーブの強さ
    public Vector3 CenterPosition; // ステージ中央の座標

    public override void Initialize(Vector3 start, Vector3 end)
    {
        base.Initialize(start, end);
        GenerateCurvePath(start, end, Divisions, CurveAmount);
    }

    private void GenerateCurvePath(Vector3 start, Vector3 end, int divisions, float curveAmount)
    {
        PathPoints.Clear();
        
        for (int i=0; i<=divisions; i++)
        {
            float t = (float)i / divisions; // 0.0 〜 1.0 の補間値
            Vector3 point = Vector3.Lerp(start, end, t); // 直線上の点を計算

            // カーブを加える (Sin波を使って左右に振る)
            float curveOffset = Mathf.Sin(t * Mathf.PI) * curveAmount; // 中央で最大、両端で0
            Vector3 curveDirection = Vector3.Cross((end - start).normalized, Vector3.up); // 直線の法線ベクトル
            Vector3 centerDirection = CenterPosition - transform.position; // ステージ中央へのベクトル
            if (Vector3.Dot(centerDirection, curveDirection) < Vector3.Dot(centerDirection, -curveDirection))
            {
                curveDirection *= -1;
            }
            point += curveDirection * curveOffset; // 曲げた位置にずらす

            PathPoints.Add(point);
        }
    }

    protected override void Move()
    {
        if (currentPointIndex >= PathPoints.Count) return; // ゴールに到達

        // 現在のターゲット地点に向かって進む
        Vector3 target = PathPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(this.transform.position, target, Speed * Time.deltaTime);

        //　闘牛を進行方向に向ける
        transform.rotation = Quaternion.LookRotation(target - transform.position);

        // ターゲットに到達したら次のポイントへ
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentPointIndex++;
        }
    }
}
