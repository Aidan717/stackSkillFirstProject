using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] 
    private float _moveSpeed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
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
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            _nextFire = Time.time + _fireRate;
        }
    }
}
