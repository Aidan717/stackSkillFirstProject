using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private int _attackDamage = 1;

    private Player _player;

    [SerializeField]
    private int _scoreValue = 10;

    private void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {

            Player player = other.transform.GetComponent<Player>();

            if (player != null) {
                player.Damage(_attackDamage);
            }

            Destroy(this.gameObject);
            
        } else if (other.tag == "Laser") {
            Destroy(other.gameObject);

            if (_player != null) {
                _player.AddScore(_scoreValue);
            }

            Destroy(this.gameObject);
        }
    }

    private void Move() {
        transform.Translate(Vector3.down * Time.deltaTime * _moveSpeed);

        if(transform.position.y < -5.5f) {
            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }
}
