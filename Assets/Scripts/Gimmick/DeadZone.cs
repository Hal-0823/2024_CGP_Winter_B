using UnityEngine;

/// <summary>
/// Playerが触れるとゲームオーバーになる
/// </summary>
public class DeadZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.I.isFallen = true;
            Debug.Log("GAME OVER");
        }
    }
}
