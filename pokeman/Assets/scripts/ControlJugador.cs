using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody2D rb;

    private float moveX;
    private float moveY;
    private Vector2 entrada;

    public Animator animator;
    private SpriteRenderer spriteRenderer;
  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Esto bloquea que gire, por más que choque
        rb.gravityScale = 0f;
        rb.freezeRotation = true; 
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        //entrada = new Vector2(moveX, moveY).normalized;

        if (moveX != 0)
        {   
            entrada = new Vector2(moveX, 0);
        }

        else if (moveY != 0)
        {
            entrada = new Vector2(0, moveY);
        }

        else
        {
            entrada = Vector2.zero;
        }


        Move();
        UpdateAnimations();
        
    }

    private void Move()
    {
        if (Time.timeScale == 1f)
        {
            animator.SetFloat("MoveX", entrada.x);
            animator.SetFloat("MoveY", entrada.y);
        }

    }

    private void UpdateAnimations()
    {
        // Giro de imagen
        if (entrada.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (entrada.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (entrada.y > 0)
        {
            spriteRenderer.flipY = false;
        }
        else if (entrada.y < 0)
        {
            spriteRenderer.flipY = true;
        }

        // para que quede bien el Idle del Pj

            if (moveY == 0)
        {
            spriteRenderer.flipY = false;
        }

        // Animación (se mueve solo si hay entrada)
        if (entrada.magnitude > 0.1f) 
        {
            animator.speed = 1f;
        }
        
        else 
        {
            animator.speed = 0f;
        }


    }



    void FixedUpdate()
    {
        // USO DE 'velocity' PARA EVITAR EL ERROR ROJO
        rb.velocity = entrada * velocidad;
        
        // Refuerzo para que no rote ni un grado
        rb.rotation = 0f;
        rb.angularVelocity = 0f;
    }
}