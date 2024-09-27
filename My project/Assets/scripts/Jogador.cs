using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
    public float velocidadeDoJogador;
    public Animator oAnimador;
    public int quantidadeDeMoedas;

    public Text textoDasMoedasAtuais;

    public GameObject painelDeGameOver;
    public Text textoDePontuacao;
    public Text textoDeHighScore;
    // Start is called before the first frame update
    void Start()
    {
        quantidadeDeMoedas = 0;
        textoDasMoedasAtuais.text = "X" + quantidadeDeMoedas;
        oAnimador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarJogador();
    }

    public void MovimentarJogador()
    {
        //verifica se o computador recebeu um comando do teclado ou controle
        float comandosDoTeclado = Input.GetAxisRaw("Horizontal") * velocidadeDoJogador * Time.deltaTime;

        transform.position = new Vector3((transform.position.x + comandosDoTeclado), transform.position.y,transform.position.z);

        if(comandosDoTeclado > 0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if(comandosDoTeclado < -0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if(comandosDoTeclado == 0)
        {
            oAnimador.Play("Player Parado");
        }
        else
        {
            oAnimador.Play("Player Andando");
        }
    }

    public void AumentarQuantidadeDeMoedas()
    {
        quantidadeDeMoedas += 1;
        textoDasMoedasAtuais.text = "X" + quantidadeDeMoedas;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        painelDeGameOver.SetActive(true);
        textoDePontuacao.text = "PONTUAÇÃO: " + quantidadeDeMoedas;

        if(quantidadeDeMoedas > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", quantidadeDeMoedas);
        }

        textoDeHighScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore");
        Debug.Log("Game Over");
    }
}
