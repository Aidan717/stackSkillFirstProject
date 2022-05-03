using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _PowerUpMoveSpeed = 5f;

    [SerializeField] //0 = Triple shot, 1 = Speed, 2 = Shield
    private int _powerupId;


    // Update is called once per frame
    void Update()
    {
        PowerUpMove();
    }

    private void PowerUpMove()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _PowerUpMoveSpeed);
        if (transform.position.y < -6f) {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if ( other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();
            if ( player != null ) {
                switch(_powerupId) {
                    case 0:
                        player.ActivateTriple();
                        break;
                    case 1:
                        player.ActivateSpeed();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default:
                        //if it's not in the case
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
