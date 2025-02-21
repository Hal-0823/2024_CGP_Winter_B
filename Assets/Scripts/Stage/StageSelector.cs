using UnityEngine;

/// <summary>
/// ステージ選択画面を処理するクラス
/// </summary>
public class StageSelector : MonoBehaviour
{
    public GameObject[] StageObj;
    private int selectId;
    private float rotationSpeed;

    private void Start()
    {
        selectId = 0;
        rotationSpeed = 20.0f;

        for (int i=0; i<StageObj.Length; i++)
        {
            if (i == selectId)
            {
                StageObj[i].SetActive(true);
            }
            else
            {
                StageObj[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        RotateStageObj();
    }

    private void RotateStageObj()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void NextStage()
    {
        StageObj[selectId].SetActive(false);

        if (selectId + 1 >= StageObj.Length)
        {
            selectId = 0;
        }
        else
        {
            selectId++;
        }

        StageObj[selectId].SetActive(true);
    }

    public void BackStage()
    {
        StageObj[selectId].SetActive(false);

        if (selectId - 1 < 0)
        {
            selectId = StageObj.Length - 1;
        }
        else
        {
            selectId--;
        }

        StageObj[selectId].SetActive(true);
    }
}
