using UnityEngine;

public class MoveBlockX : MonoBehaviour
{
    public float MoveSpeed;
    Rigidbody2D rigid;
    float way = 0;
    public static bool Button = false;
    public string layerName;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D rayHit_left = Physics2D.Raycast(rigid.position, new Vector3(-1, 0, 0), (float)0.52, LayerMask.GetMask(layerName));
        RaycastHit2D rayHit_right = Physics2D.Raycast(rigid.position, new Vector3(1, 0, 0), (float)0.52, LayerMask.GetMask(layerName));
        if (rayHit_right.collider == null && Button == true)
        {
            way = MoveSpeed;
            Button = false;
        }
        else{
            way = MoveSpeed * -1;
        }


        transform.position = new Vector2(transform.position.x + way * Time.deltaTime,transform.position.y);
        //rigid.linearVelocity = new Vector2(way * Time.deltaTime, rigid.linearVelocity.y); //way 방향으로 이동
    }
    

}
