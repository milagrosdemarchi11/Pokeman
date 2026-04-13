using UnityEngine;

public class EnemigoMovimiento : MonoBehaviour
{
    public float velocidad = 2f;
    private Vector2 direccion;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CambiarDireccion();
    }

    void FixedUpdate()
    {
        rb.velocity = direccion * velocidad;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CambiarDireccion();
    }

    void CambiarDireccion()
    {
        int random = Random.Range(0, 4);

        if (random == 0) direccion = Vector2.up;
        if (random == 1) direccion = Vector2.down;
        if (random == 2) direccion = Vector2.left;
        if (random == 3) direccion = Vector2.right;
    }
}