using UnityEngine;

public class TestCreatePlayer : MonoBehaviour
{
    public GameObject playerPrehub;
    bool exsistPlayer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    GameObject player ;
    public void OnButtonClick()
    {
        if (!exsistPlayer)
        {
            player = Instantiate(playerPrehub);
            exsistPlayer = true;
        }
        else
        {
            Destroy(player);
            exsistPlayer = false;
        }
        // ここにボタンがクリックされたときの処理を追加
    }
}
