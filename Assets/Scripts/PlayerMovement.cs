using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var yMove = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
        var xMove = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + xMove, transform.position.y + yMove);

        if (transform.position.y <= -18)
        {
            print("GameOver");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
