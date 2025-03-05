using UnityEngine;

public class AudienceController : MonoBehaviour
{
    [SerializeField] private Animator[] animator;

    public void SetGlad()
    {
        foreach (var anim in animator)
        {
            anim.SetTrigger("Glad");
        }
    }

    public void SetAngry()
    {
        foreach (var anim in animator)
        {
            anim.SetTrigger("Angry");
        }
    }
}
