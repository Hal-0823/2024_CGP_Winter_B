using UnityEngine;

public class SpeedChangeBull : BullController
{
    private float t = 0;
    private float orginalSpeed;

    public override void Initialize(Vector3 start, Vector3 end, bool isSpawn)
    {
        base.Initialize(start, end);
        orginalSpeed = Speed;
    }

    protected override void Move()
    {
        t += Time.deltaTime;
        Speed =  orginalSpeed * Mathf.Max(0.5f, Mathf.Abs(Mathf.Sin(t/2 * Mathf.PI)));
        base.Move();
    }
}
