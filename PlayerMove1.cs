using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove1 : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 600.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }


        //좌우 이동
        float key = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = 0.6f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            key = -0.6f;
        }

        //플레이어 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        if (speedx < this.maxWalkSpeed)//스피드 제한
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        if (key != 0)
        {
            transform.localScale = new Vector3((float)key, 0.6f, 0.6f);
        }

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene1");
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
      
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            if (rigid2D.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                AttackMonster(collision.transform);
            }
            
        }
        if (collision.gameObject.tag == "Finish")
        {
          

        }
    }


    void AttackMonster(Transform monster)
    {
        // Reaction Force
        rigid2D.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // Enemy Die
        MonsterMove monsterMove = monster.GetComponent<MonsterMove>();
        monsterMove.OnDamaged();
    }

}
