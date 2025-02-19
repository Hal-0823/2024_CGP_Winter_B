using UnityEngine;

public class SpeedChangeBull : BullController
{
    private float t = 0;

    protected override void Move()
    {
        t += Time.deltaTime;

        transform.position += direction * Speed * Mathf.Max(0.5f, Mathf.Abs(Mathf.Sin(t/2 * Mathf.PI))) * Time.deltaTime;
    }
}
