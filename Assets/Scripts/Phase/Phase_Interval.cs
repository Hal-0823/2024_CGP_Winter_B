using UnityEngine;

public class Phase_Interval : PhaseBase
{
    public float Interval = 3f;

    public override void UpdateState(PhaseManager manager)
    {
        if (manager.GameTime - phaseStartTime > Interval)
        {
            manager.NextState();
        }
    }

    public override void SpawnEnemy(PhaseManager manager){}
}