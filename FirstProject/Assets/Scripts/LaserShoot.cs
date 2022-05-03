using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    [SerializeField]
    private float _shootSpeed = 20f;

    // Start is called before the first frame update
    void Update() {
        LaserMovement();
        DestroyLaser();
    }

    private void LaserMovement() {
        transform.Translate(Vector3.up * Time.deltaTime * _shootSpeed);
    }

    private void DestroyLaser() {
        if ( transform.position.y > 8f ) {
            if ( this.transform.parent != null ) {
                Destroy(this.transform.parent.gameObject);
            } 
            Destroy(this.gameObject);
        }
    }
}
