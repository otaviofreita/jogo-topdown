using UnityEngine;
using UnityEngine.UI;


public class Jogador : MonoBehaviour
{
    [Header("Atributos")]
    public float velocidade = 5f;
    public int vidaMaxima = 100;
    public int vidaAtual;
    public int pontuacao;

    [Header("UI")]
    public Slider barraVidaUI;
    public TMPro.TextMeshProUGUI textoPontuacao;

    [Header("Referências")]
    public Transform corpo; // corpo do tanque, sem o canhão

    private Rigidbody2D rb;
    private Vector2 direcao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vidaAtual = vidaMaxima;
        AtualizarHUD();
    }

    void Update()
    {
        // Movimento
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        direcao = new Vector2(moveX, moveY).normalized;

        // Rotação do corpo
        if (direcao.sqrMagnitude > 0.01f && corpo != null)
        {
            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            corpo.rotation = Quaternion.Euler(0f, 0f, angulo - 90f);
        }

        AtualizarHUD();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);
    }

    public void LevarDano(int dano)
    {
        if (vidaAtual <= 0) return;

        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        AtualizarHUD();

        if (vidaAtual == 0)
            Morrer();
    }

    void Morrer()
    {
        Destroy(gameObject, 0.5f); // espera meio segundo antes de sumir
    }

    public void AdicionarPontuacao(int pontos)
    {
        pontuacao += pontos;
        AtualizarHUD();
        Debug.Log($"Pontuação atual: {pontuacao}");
    }

    void AtualizarHUD()
    {
        if (barraVidaUI != null)
            barraVidaUI.value = (float)vidaAtual / vidaMaxima;

        if (textoPontuacao != null)
            textoPontuacao.text = $"Pontos: {pontuacao}";
    }
}
