using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOver;
    [SerializeField] private Text _restartText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImage;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager == null ) {
            Debug.LogError("GameManager is null");
        }
    }

    public void UpdateScore(int arg_score) {
        _scoreText.text = "Score: " + arg_score.ToString();
    }

    public void UpdateLives(int arg_currentLives) {
        _livesImage.sprite = _liveSprites[arg_currentLives];
        if ( arg_currentLives == 0 ) {
            GameOverSequence();
        }
    }

    private void GameOverSequence() {
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(ActivateGameOver());
    }

    IEnumerator ActivateGameOver() {
        while(true) {
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
