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

    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _laserShootAudio;
                     private AudioSource _audioSource;
    [SerializeField] private GameObject _laserPrefab;     
    [SerializeField] private bool _stopShoot = false;       
                     private float _shootTimer = 5f;         
    

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

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            Debug.LogError("The AudioSource is null, EnemyBehavior.cs");
        } else {
            _audioSource.clip = _laserShootAudio;
        }
        StartCoroutine( enemyShoot() );
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
            _audioSource.Play();
            OnDeathAnimation();
            //Destroy(this.gameObject);
        }
    }

    IEnumerator enemyShoot() {
        while ( _stopShoot == false ) {
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            LaserShoot[] lasers = enemyLaser.GetComponentsInChildren<LaserShoot>();
            foreach (LaserShoot laser in lasers)
            {
                laser.setDamage(_attackDamage);
                laser.AssignEnemyLaser();
            }
            _shootTimer = Random.Range(0.1f, 2f);
            yield return new WaitForSeconds(_shootTimer);
        }
    }

    private void OnDeathAnimation() {
        _audioSource.clip = _explosionSound;
        _audioSource.Play();
        isDead = true;
        _stopShoot = true;
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
