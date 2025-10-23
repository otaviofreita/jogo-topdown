using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Atributos")]
    public float velocidade = 5f;
    public int vida = 100;
    public int pontuacao;

    [Header("UI")]
    public Slider barraVidaUI;
    public TextMeshProUGUI textoPontuacao;

    private Rigidbody2D rb;
    private Vector2 direcao;
    private float anguloAlvo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AtualizarHUD();
    }

    void Update()
    {
        // Captura direção de movimento
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        direcao = new Vector2(moveX, moveY).normalized;

        // Gira o corpo do tanque na direção do movimento (sem afetar o canhão)
        if (direcao.sqrMagnitude > 0.01f)
        {
            anguloAlvo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, anguloAlvo), 0.2f);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);
    }

    public void LevarDano(int dano)
    {
        vida -= dano;
        AtualizarHUD();

        if (vida <= 0)
            Destroy(gameObject);
    }

    public void AdicionarPontuacao(int pontos)
    {
        pontuacao += pontos;
        AtualizarHUD();
    }

    void AtualizarHUD()
    {
        if (barraVidaUI) barraVidaUI.value = vida;
        if (textoPontuacao) textoPontuacao.text = pontuacao.ToString();
    }
}
