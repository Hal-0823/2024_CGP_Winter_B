public interface IGamePhase
{
    void EnterState(PhaseManager manager);
    void UpdateState(PhaseManager manager);
}