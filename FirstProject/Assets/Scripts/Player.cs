using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] 
    private float _moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RestrictPlayer();
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
}
