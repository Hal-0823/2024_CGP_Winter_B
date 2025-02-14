using UnityEngine;
using System.Collections.Generic;
public class Movement_Player : Information_Player
{
    Rigidbody rb;
    GameObject player;
    float moveSpeed;
    private List<KeyCode> pressedKeys = new List<KeyCode>();
    private bool isAnyKeyPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = this.gameObject;
        moveSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        // キーを押した順にリストに追加
        if (Input.GetKeyDown(KeyCode.W) && !pressedKeys.Contains(KeyCode.W)) { pressedKeys.Add(KeyCode.W); isAnyKeyPressed = true; PicUpLastKeyList(); }
        if (Input.GetKeyDown(KeyCode.S) && !pressedKeys.Contains(KeyCode.S)) { pressedKeys.Add(KeyCode.S); isAnyKeyPressed = true; PicUpLastKeyList(); }
        if (Input.GetKeyDown(KeyCode.A) && !pressedKeys.Contains(KeyCode.A)) { pressedKeys.Add(KeyCode.A); isAnyKeyPressed = true; PicUpLastKeyList(); }
        if (Input.GetKeyDown(KeyCode.D) && !pressedKeys.Contains(KeyCode.D)) { pressedKeys.Add(KeyCode.D); isAnyKeyPressed = true; PicUpLastKeyList(); }

        // キーを離したらリストから削除
        if (Input.GetKeyUp(KeyCode.W)) { pressedKeys.Remove(KeyCode.W); PicUpLastKeyList(); }
        if (Input.GetKeyUp(KeyCode.S)) { pressedKeys.Remove(KeyCode.S); PicUpLastKeyList(); }
        if (Input.GetKeyUp(KeyCode.A)) { pressedKeys.Remove(KeyCode.A); PicUpLastKeyList(); }
        if (Input.GetKeyUp(KeyCode.D)) { pressedKeys.Remove(KeyCode.D); PicUpLastKeyList(); }

        //リストに存在している＝キーが押されている


        // 追加分：キーの組み合わせによる移動方向の変更
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)){rb.rotation = Quaternion.Euler(0, 45, 0);}
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)){rb.rotation = Quaternion.Euler(0, -45, 0);}
        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)){rb.rotation = Quaternion.Euler(0, 135, 0);}  
        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)){rb.rotation = Quaternion.Euler(0, -135, 0);}      

        //シフトで移動速度を変更
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = playerDashSpeed;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = playerSpeed;
        }
        
    }

    void FixedUpdate()
    {
        if (isAnyKeyPressed)
        {
            rb.MovePosition(rb.position + transform.forward * moveSpeed);
            
        }
    }

    void PicUpLastKeyList()
    {
        // 押されているキーの中で最後のものを取得（リストの末尾）
        if (pressedKeys.Count > 0)
        {
            KeyCode lastPressedKey = pressedKeys[pressedKeys.Count - 1]; // 最後に押したキーを取得
            switch (lastPressedKey)
            {
                case KeyCode.W: rb.rotation = Quaternion.Euler(0, 0, 0); break;
                case KeyCode.S: rb.rotation = Quaternion.Euler(0, 180, 0); break;
                case KeyCode.A: rb.rotation = Quaternion.Euler(0, -90, 0); break;
                case KeyCode.D: rb.rotation = Quaternion.Euler(0, 90, 0); break;
            }
        }
        else
        {
            // 何も押されていないから停止
            StopMoving();
        }
    }
    public void StopMoving()
    {
        isAnyKeyPressed = false;
    }
    public void LookAtEnemy(GameObject enemy)
    {
        Debug.Log("LookAtEnemyが呼び出されたよん");
        Vector3 relativePos = enemy.transform.position - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        player.transform.rotation = rotation;
    }
}
