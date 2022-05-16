using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    [SerializeField]
    private float _shootSpeed = 20f;

    private bool _isEnemyLaser = false;

    private int _damage = 1;

    // Start is called before the first frame update
    void Update() {
        LaserMovement();
        DestroyLaser();
    }

    private void LaserMovement() {
        if (_isEnemyLaser) {
            transform.Translate(Vector3.down * Time.deltaTime * _shootSpeed);
        } else {
            transform.Translate(Vector3.up * Time.deltaTime * _shootSpeed);
        }
    }

    private void DestroyLaser() {
        if (_isEnemyLaser) {
            if ( transform.position.y > 8f ) {
                if ( this.transform.parent != null ) {
                    Destroy(this.transform.parent.gameObject);
                } 
                Destroy(this.gameObject);
            }
        } else {
            if ( transform.position.y < -8f ) {
                if ( this.transform.parent != null ) {
                    Destroy(this.transform.parent.gameObject);
                } 
                Destroy(this.gameObject);
            }
        }
    }

    public void AssignEnemyLaser() {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && _isEnemyLaser == true) {
            Player player = other.GetComponent<Player>();

            if (player != null) {
                player.Damage(_damage);
            }
        }
    }

    public void setDamage(int arg_damage) {
        _damage = arg_damage;
    }
}
