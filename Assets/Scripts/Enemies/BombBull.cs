using UnityEngine;

public class BombBull : MonoBehaviour
{
    public ExplosionAction explosionAction;
    public float Limit = 3f;

    void Start()
    {
        ParticleSystem effect = GetComponent<ParticleSystem>();
        effect.Stop();

        var main = effect.main;
        main.duration = Limit * 2f;

        effect.Play();
    }

    private void OnParticleSystemStopped()
    {
        BullController bull = GetComponent<BullController>();
        explosionAction.Execute(bull, null);
    }
}