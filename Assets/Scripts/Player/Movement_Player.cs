using UnityEngine;
using System.Collections.Generic;
public class Movement_Player : Information_Player
{
    Rigidbody rb;
    GameObject player;
    float moveSpeed;
    public GameObject jumpSencor;
    private List<KeyCode> pressedKeys = new List<KeyCode>();
    private bool isMoving = false;

    public static bool isGrounded;  
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

        int moveX = 0;
        int moveZ = 0;

        if (Input.GetKey(KeyCode.W)) moveZ += 1;
        if (Input.GetKey(KeyCode.S)) moveZ -= 1;
        if (Input.GetKey(KeyCode.A)) moveX -= 1;
        if (Input.GetKey(KeyCode.D)) moveX += 1;

        Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;
        isMoving = direction != Vector3.zero;

        if (isMoving&&isGrounded)
        {
            rb.rotation = Quaternion.LookRotation(direction);
        }


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
        GameObject sensor = Instantiate(jumpSencor,this.transform);
        Destroy(sensor,0.3f);
    }
    public void LookAtEnemy(GameObject enemy)
    {
        Debug.Log("LookAtEnemyが呼び出されたよん");
        Vector3 relativePos = enemy.transform.position - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        player.transform.rotation = rotation;
    }
}
