using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed;
    Vector2 _direction;
    [SerializeField]bool isReadly;

    void Awake()
    {
        speed = 5f;
        isReadly = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        isReadly = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(isReadly)
        {
            Vector2 position = transform.position;
            position +=_direction *speed *Time.deltaTime;
            transform.position = position;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

            if((transform.position.x <min.x) || (transform.position.x >max.x) ||
              (transform.position.y <min.y) || (transform.position.y >max.y))
              {
                Destroy(gameObject);
              }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "PlayerShipTag"))
        {
            Destroy(gameObject);
        }
    }
}
