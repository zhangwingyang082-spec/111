using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float playerMoveSpeed;
    public float playerJumpSpeed;
    public bool isGround;
    public LayerMask Ground;
    public Rigidbody2D playerRB;
    public Collider2D playerColl;
    private float orignalX;
    // Start is called before the first frame update
    void Start()
    {
        playerColl = GetComponent<Collider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        orignalX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    void PlayerMove()
    {
        float horizontalNum = Input.GetAxis("Horizontal");
        float faceNum = Input.GetAxisRaw("Horizontal");
        playerRB.velocity = new Vector2(playerMoveSpeed * horizontalNum,playerRB.velocity.y);
        if(faceNum!=0)
        {
            transform.localScale = new Vector3(faceNum * orignalX, transform.localScale.y, transform.localScale.z);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButton("Jump"))
        { 
             playerRB.velocity = new Vector2(playerRB.velocity.x, playerJumpSpeed);
        }
    }
}
