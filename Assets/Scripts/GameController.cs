using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject menuCamera;
    public GameObject menuPanel;
    private int pontos;
    public Text txtPontos;


    public Estado estado { get; private set; }

    public GameObject obstaculo;
    public float espera;
    public float tempoDestruicao;

    public static GameController instancia = null;

    private void atualizarPontos(int x) {
        pontos = x;
        txtPontos.text = "" + x;
    }

    public void incrementarPontos(int x) {
        atualizarPontos(pontos + x);
    }

    void Awake() {
        if (instancia == null) {
            instancia = this;
        }
        else if (instancia != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

	void Start () {
        estado = Estado.AguardoComecar;
	}
	
	IEnumerator GerarObstaculos() {
        while (GameController.instancia.estado == Estado.Jogando) {
            Vector3 pos = new Vector3(7.7f, Random.Range(1f, 5f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.identity) as GameObject;
            Destroy(obj, tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
	}

    public void PlayerComecou() {
        estado = Estado.Jogando;
        menuCamera.SetActive(false);
        menuPanel.SetActive(false);
        atualizarPontos(0);
        StartCoroutine(GerarObstaculos());
    }

    public void PlayerMorreu() {
        estado = Estado.GameOver;
    }
}


