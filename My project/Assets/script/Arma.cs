using UnityEngine;

public class Arma : MonoBehaviour
{
    public Transform saidaDoTiro;
    public GameObject balaPrefab;
    public float intervaloDisparo = 0.25f;
    public float velocidadeBala = 10f;

    private float proximoDisparo;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Mira do mouse
        Vector3 posMouse = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direcao = posMouse - transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        // Disparo
        if (Input.GetMouseButton(0) && Time.time > proximoDisparo)
        {
            proximoDisparo = Time.time + intervaloDisparo;
            GameObject bala = Instantiate(balaPrefab, saidaDoTiro.position, saidaDoTiro.rotation);
            bala.GetComponent<Rigidbody2D>().linearVelocity = saidaDoTiro.right * velocidadeBala;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 1f);
    }
}
