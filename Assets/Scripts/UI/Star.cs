using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIの星を制御するクラス
/// stateがtrueなら黄色、falseなら黒色になる
/// </summary>
public class Star : MonoBehaviour
{
    public bool State { get; private set; }
    private Image image;

    public void SetState(bool state)
    {
        State = state;
        
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (State)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.black;
        }
    }
}
