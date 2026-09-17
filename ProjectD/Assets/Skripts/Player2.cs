using UnityEngine;


public class Player2 : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public Transform P1SpawnPoint;
    public Transform P2SpawnPoint;
    public GameObject Player1object;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        // 점프

        if (Input.GetKeyDown(KeyCode.W) && !anim.GetBool("isJumping"))
        { // 점프버튼 누르기 && 애니메이션(isjumping)이 true가 아닌지 
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //점프
            anim.SetBool("isJumping", true); //점프 애니메이션
        }
        // keyup->move stop
        if (Input.GetButtonUp("Horizontal2"))
        {
            rigid.linearVelocity = new Vector2(0, rigid.linearVelocity.y);
        }

        // 좌우반전
        if (Input.GetButton("Horizontal2")){
             spriteRenderer.flipX = Input.GetAxisRaw("Horizontal2") == -1;
         }

        // 애니메이션
        if (rigid.linearVelocity.normalized.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    void FixedUpdate()
    {
        // Move Key Control
        float h = Input.GetAxisRaw("Horizontal2");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);



        // Max Speed
        if (rigid.linearVelocity.x > maxSpeed)
        {
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        } // Right Max Speed

        else if (rigid.linearVelocity.x < maxSpeed * (-1))
        {
            rigid.linearVelocity = new Vector2(maxSpeed * (-1), rigid.linearVelocity.y);
        } // Left Max Speed



        // 래래이케스트 
        RaycastHit2D rayHit0 = Physics2D.Raycast(transform.position, Vector2.down, 1.05f, LayerMask.GetMask("Platform") | LayerMask.GetMask("MovePlatform") | LayerMask.GetMask("PlatformStone") | LayerMask.GetMask("PlatformGold") | LayerMask.GetMask("PlatformCU"));//실제 레이 쏘기 
        if (rayHit0.collider != null)
        { // 래래이가 닿았다면
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
        // 래래이캐스트(밟으면 안되는 바닥)
        RaycastHit2D rayHit1 = Physics2D.Raycast(transform.position, Vector2.down, 1.05f, LayerMask.GetMask("PlatformStone") | LayerMask.GetMask("PlatformGold") | LayerMask.GetMask("DeadZone"));

        Debug.DrawRay(transform.position, Vector2.down * 1.05f, Color.blue);  // 파란색으로 그리기

        //// 사망처리
        // 바닥 밟았을때
        if (rayHit1.collider != null) {
            Death();
        }
        // 거리 멀어졌을때
        if (Camera.playerDis >= 50){
            Death();
        }
    }
    public void Death()
    {
        Player1.score -= 20;
        this.rigid.position = P2SpawnPoint.position;
        Player1object.transform.position = P1SpawnPoint.position;
    }
}
