using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private int _attackDamage = 1;

                     private Player _player;
    [SerializeField] private bool isDead = false;

    [SerializeField] private int _scoreValue = 10;

    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider2D _collider;

    private void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if ( _player == null) {
            Debug.LogError("_Player is null, EnemyBehavior.cs");
        }

        anim = GetComponent<Animator>();
        if ( anim == null) {
            Debug.LogError("anim is NULL." );
        }
        _collider = GetComponent<BoxCollider2D>();
        if ( _collider == null) {
            Debug.LogError("_collider is NULL." );
        }
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
            OnDeathAnimation();
            //Destroy(this.gameObject);
            
        } else if (other.tag == "Laser") {

            

            Destroy(other.gameObject);

            if (_player != null) {
                _player.AddScore(_scoreValue);
            }
            OnDeathAnimation();
            //Destroy(this.gameObject);
        }
    }

    private void OnDeathAnimation() {
        isDead = true;
        _collider.enabled = false;
        anim.SetTrigger("OnEnemyDeath");
        Destroy(this.gameObject, 2.5f);
    }

    private void Move() {
        transform.Translate(Vector3.down * Time.deltaTime * _moveSpeed);

        if(transform.position.y < -5.5f && !isDead ) {
            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }
}
