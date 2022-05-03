using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShot;
    [SerializeField] private GameObject _shield;
                     private SpawnManager _spawnManager;
    [SerializeField] private float _moveSpeed = 5f;
                     private float _speedMultiplier = 2f;
    [SerializeField] private float _fireRate = 0.5f;
                     private float _nextFire = -1f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private bool isTriple = false;
    [SerializeField] private bool isShield = false;


    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
         if (_spawnManager == null) {
            Debug.LogError("SpawnManager is NULL.");
        }
    }

    // Update is called once per frame
    void Update() {
        MovePlayer();
        RestrictPlayer();
        Shoot();
    }

    private void MovePlayer() {

        var deltaX = Input.GetAxisRaw("Horizontal");
        var deltaY = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(deltaX, deltaY, 0);
        transform.Translate(direction * Time.deltaTime * _moveSpeed);           
    }

    private void RestrictPlayer() {
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 2), 0);
        
        if (transform.position.x >= 11.2f) {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        } else if (transform.position.x <= -11.2f) {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }
    }

    private void Shoot() {
        if ( Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire) {
            if ( isTriple ) {
                Instantiate(_tripleShot, transform.position, Quaternion.identity);
                _nextFire = Time.time + _fireRate;
            } else {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
                _nextFire = Time.time + _fireRate;
            }
        }
    }

    public void Damage(int arg_damage) {
        if ( !isShield ) {
            _lives = _lives - arg_damage;
        } else {
            DeactivateShield();
        }

        if ( _lives <= 0 ) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    #region "TripleBoostActivateAndDeactivate"
    public void ActivateTriple() {
        isTriple = true;

        StartCoroutine(disableTriple());
    }
    IEnumerator disableTriple() {
        yield return new WaitForSeconds(5f);
        isTriple = false;
    }
    #endregion

    #region "SpeedBoostActivateAndDeactivate"
    public void ActivateSpeed() {
        _moveSpeed *= _speedMultiplier;
        StartCoroutine(disableSpeed());
    }

    IEnumerator disableSpeed()
    {
        yield return new WaitForSeconds(5f);
        _moveSpeed /= _speedMultiplier;
    }
    #endregion

    #region "ShieldBoostActivateAndDeactivate"
    public void ActivateShield() {
        isShield = true;
        _shield.SetActive(true);
    }

    public void DeactivateShield() {
        isShield = false;
        _shield.SetActive(false);
    }
    #endregion
}
