using UnityEngine;

public class SplitBullController : BullController
{
    public BullController ChildBull;
    
    [Range(1,5), Tooltip("どの程度の進行位置で分裂するか（3で中間）")]
    public int SplitPoint = 3;
    [Tooltip("分裂する際の広がり")]
    public float SplitAngle = 60f;
    public int SplitNum = 2;
    private Vector3 splitPosition;
    private bool canSplit = false;

    public override void Initialize(Vector3 start, Vector3 end, bool isSpawn = true)
    {
        base.Initialize(start, end, isSpawn);
        GenerateSplitPosition(start, end, SplitPoint);
        canSplit = true;
    }

    private void GenerateSplitPosition(Vector3 Start, Vector3 end, int splitPoint)
    {
        splitPosition = Vector3.Lerp(Start, end, splitPoint/6.0f);
    }

    private void Split()
    {
        float angleStep = SplitAngle / (SplitNum - 1) ;
        float startAngle = -SplitAngle/2;

        for (int i=0; i<SplitNum; i++)
        {
            float angle = startAngle + (i * angleStep);
            Vector3 spawnAngle = Quaternion.Euler(0, angle, 0) * transform.forward;
            BullController bull = Instantiate(ChildBull);
            bull.Initialize(this.transform.position, this.transform.position + spawnAngle, false);
        }

        Destroy(this.gameObject);
    }

    protected override void Move()
    {
        base.Move();

        if (!canSplit) return;

        if (Vector3.Distance(transform.position, splitPosition) < 0.1f)
        {
            Split();
        }
    }
}
