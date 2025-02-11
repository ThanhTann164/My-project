using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	PlayerControls controls;
	float direction = 0;
	public float speed = 700;
	public float jumpForce = 15f;
	public Animator animator;
	public Rigidbody2D playerRB;
	bool facingRight = true;

	public Transform groundCheck;
	public LayerMask groundLayer;
	private bool isGrounded = true;
	private int jumpCount = 0;
	private int maxJumps = 2; // Cho phép nhảy tối đa 2 lần

	public void OnEnable()
	{
		playerRB = GetComponent<Rigidbody2D>();

		controls = new PlayerControls();
		controls.Enable();

		controls.Land.Move.performed += ctx =>
		{
			direction = ctx.ReadValue<float>();
		};

		controls.Land.Jump.performed += ctx =>
		{
			Debug.Log("Jump pressed! isGrounded: " + isGrounded + ", jumpCount: " + jumpCount);
			if (jumpCount < maxJumps)
			{
				playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, jumpForce);
				jumpCount++;
				Debug.Log("Jump thành công! jumpCount: " + jumpCount);
			}
		};
	}

	void Start()
	{
		if (playerRB != null)
		{
			playerRB.freezeRotation = true;
		}
	}

	void Update()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);

		if (isGrounded)
		{
			jumpCount = 0; // Reset số lần nhảy khi chạm đất
		}

		if (playerRB != null)
		{
			playerRB.linearVelocity = new Vector2(direction * speed * Time.deltaTime, playerRB.linearVelocity.y);
		}

		if (animator != null)
		{
			animator.SetFloat("speed", Mathf.Abs(direction));
		}

		if ((facingRight && direction < 0) || (!facingRight && direction > 0))
		{
			Flip();
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3(facingRight ? 1 : -1, transform.localScale.y, transform.localScale.z);
	}

	void OnDisable()
	{
		if (controls != null)
		{
			controls.Disable();
		}
	}

	void OnDestroy()
	{
		if (controls != null)
		{
			controls.Disable();
		}
	}
}
