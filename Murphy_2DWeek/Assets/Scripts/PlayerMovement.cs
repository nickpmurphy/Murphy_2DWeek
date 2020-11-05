using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Use vector, apply the vector to a rigidbody

    Rigidbody2D rB2D;
    public float runSpeed;
    public float jumpSpeed;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public int count;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
      rB2D = GetComponent<Rigidbody2D>();
      SetText();

    }

    void SetText()
    {
      scoreText.text = "Your Score: " + count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetButtonDown("Jump"))
      {
        int levelMask = LayerMask.GetMask("Level");
        if(Physics2D.BoxCast(transform.position, new Vector2(1f, .1f), 0f, Vector2.down, .01f, levelMask))
        {
          Jump();
        }
    }

    SetText();
    }
    private void FixedUpdate ()
    {
      float horizontalInput = Input.GetAxis("Horizontal");
      rB2D.velocity = new Vector2(horizontalInput * runSpeed * Time.deltaTime, rB2D.velocity.y);

      if (rB2D.velocity.x> 0f)
      {
        spriteRenderer.flipX = false;
      }else
      if (rB2D.velocity.x < 0f) {
        spriteRenderer.flipX = true;
      }

      if (Mathf.Abs(horizontalInput) > 0f)
      {
        animator.SetBool("IsRunning", true);
      }
      else
        animator.SetBool("IsRunning", false);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.CompareTag("Pick Up"))
      {
        count += 1;
      }
    }

    void Jump()
    {
      rB2D.velocity = new Vector2(rB2D.velocity.x, jumpSpeed);

    }
}
