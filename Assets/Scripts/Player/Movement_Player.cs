using UnityEngine;
using System.Collections.Generic;
public class Movement_Player : Information_Player
{
    Rigidbody rb;
    GameObject player;
    float moveSpeed;
    private List<KeyCode> pressedKeys = new List<KeyCode>();
    private bool isMoving = false;
    private bool isGrounded;  
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
        if (Input.GetKeyDown(KeyCode.W)&&!pressedKeys.Contains(KeyCode.W)) { pressedKeys.Add(KeyCode.W); CalculateDirection(); }
        if (Input.GetKeyDown(KeyCode.S)&&!pressedKeys.Contains(KeyCode.S)) { pressedKeys.Add(KeyCode.S); CalculateDirection(); }
        if (Input.GetKeyDown(KeyCode.A)&&!pressedKeys.Contains(KeyCode.A)) { pressedKeys.Add(KeyCode.A); CalculateDirection(); }
        if (Input.GetKeyDown(KeyCode.D)&&!pressedKeys.Contains(KeyCode.D)) { pressedKeys.Add(KeyCode.D); CalculateDirection(); }

        // キーを離したらリストから削除
        if (pressedKeys.Contains(KeyCode.W)&&Input.GetKeyUp(KeyCode.W)) { pressedKeys.Remove(KeyCode.W); CalculateDirection(); }
        if (pressedKeys.Contains(KeyCode.S)&&Input.GetKeyUp(KeyCode.S)) { pressedKeys.Remove(KeyCode.S); CalculateDirection(); }
        if (pressedKeys.Contains(KeyCode.A)&&Input.GetKeyUp(KeyCode.A)) { pressedKeys.Remove(KeyCode.A); CalculateDirection(); }
        if (pressedKeys.Contains(KeyCode.D)&&Input.GetKeyUp(KeyCode.D)) { pressedKeys.Remove(KeyCode.D); CalculateDirection(); }

        //リストに存在している＝キーが押されている  

        // ジャンプ処理
        if ((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Mouse4)) && isGrounded)
        {
            Jump();
        }

        //シフトで移動速度を変更
        moveSpeed = (Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.Mouse3)) ? playerDashSpeed : playerSpeed;
        
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + transform.forward * moveSpeed);
            
        }
    }

    //プレイヤーが向く方向を決定する関数
    //押されたキーの状況が変化するたび呼ばれる
     void CalculateDirection()
    {
        

        if (pressedKeys.Count >= 1)
        {
            //押されているキーをベクトル表現する
            int registZ = 0,registX = 0;
            if(pressedKeys.Contains(KeyCode.W)){registZ+=1;}
            if(pressedKeys.Contains(KeyCode.S)){registZ-=1;}
            if(pressedKeys.Contains(KeyCode.A)){registX-=1;}
            if(pressedKeys.Contains(KeyCode.D)){registX+=1;}
            //方向が０でない場合、プレイヤーの向きを変更し、移動開始する
            if(new Vector3(registX, 0, registZ) !=Vector3.zero)
            {
                rb.rotation = Quaternion.LookRotation(new Vector3(registX, 0, registZ));
                isMoving = true;
            }
            else
            {

                isMoving = false;
            }
            


        }
        else if (pressedKeys.Count == 0)
        {
            isMoving = false;
        }
    }
    public void StopMoving()
    {
        isMoving = false;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;  // 地面に接している場合
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;  // 地面から離れた場合
        }
    }

    void Jump()
    {
        // 上方向に力を加える
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    public void LookAtEnemy(GameObject enemy)
    {
        Debug.Log("LookAtEnemyが呼び出されたよん");
        Vector3 relativePos = enemy.transform.position - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        player.transform.rotation = rotation;
    }
}
