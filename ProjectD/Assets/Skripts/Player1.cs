using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class Player1 : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public Transform P1SpawnPoint;
    public Transform P2SpawnPoint;
    public GameObject Player2object;
    public TextMeshProUGUI ScoreTextUI;

    public static int score = 0;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        // 점프
        if (Input.GetKeyDown(KeyCode.UpArrow) && !anim.GetBool("isJumping"))
        { // 점프버튼 누르기 && 애니메이션(isjumping)이 true가 아닌지 
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //점프
            anim.SetBool("isJumping", true); // 점프 애니메이션
        }
        // keyup->move stop
        if (Input.GetButtonUp("Horizontal1"))
        {
            rigid.linearVelocity = new Vector2(0, rigid.linearVelocity.y);
        }

        // 좌우반전
        if (Input.GetButton("Horizontal1"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal1") == -1;
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

        //텍스트(점수, 거리) 표시
        // ScoreTextUI.text = ("| SCORE : " + score + " |      | DISTANCE : " + (int)Camera.playerDis);
        ScoreTextUI.text = "| SCORE : <color=#FFFFFF>" + score.ToString("D3") + "</color> |      | DISTANCE : <color=#FFFFFF>" + ((int)Camera.playerDis).ToString("D2") + "</color>";
    }

    void FixedUpdate()
    {
        // Move Key Control
        float h = Input.GetAxisRaw("Horizontal1");
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



        //레이케스트(점프)
        RaycastHit2D rayHit0 = Physics2D.Raycast(transform.position, Vector2.down, 1.05f, LayerMask.GetMask("Platform") | LayerMask.GetMask("MovePlatform") | LayerMask.GetMask("PlatformStone") | LayerMask.GetMask("PlatformGold") | LayerMask.GetMask("PlatformCU"));
        if (rayHit0.collider != null)
        { // 레이가 닿았다면
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        // 레이캐스트(밟으면 안되는 바닥)
        RaycastHit2D rayHit1 = Physics2D.Raycast(transform.position, Vector2.down, 1.05f, LayerMask.GetMask("PlatformCU") | LayerMask.GetMask("PlatformGold") | LayerMask.GetMask("DeadZone"));

        // Ray 그리기 (Debug.DrawRay)
        Debug.DrawRay(transform.position, Vector2.down * 1.05f, Color.red);  // 빨간색으로 그리기

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
        score -= 20;
        this.rigid.position = P1SpawnPoint.position;
        Player2object.transform.position = P2SpawnPoint.position;
    }  
}
