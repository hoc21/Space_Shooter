using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
    [SerializeField]private  Joystick Joystick;
    [SerializeField] float moveSpeed =  5f;
    [SerializeField] float Speed = 40f;
    [SerializeField] GameObject bulletPrefab; // Prefab của đạn
    [SerializeField] float bulletSpeed = 10f; // Tốc độ đạn
    Fire fire;

    [SerializeField] GameObject GameManagerGo;
    [SerializeField] GameObject Explosion;

    [SerializeField] Text LiveUIText;
    const int MaxLives = 3;
    int lives;
    Vector2 min;
    Vector2 max;
    private Rigidbody2D Rigidbody2D;
    private float posX = 11.0f;
    private float posY = 4.25f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    public void Init()
    {
        lives = MaxLives;
        LiveUIText.text = lives.ToString();
        transform.position = new Vector2(0,0);     
        gameObject.SetActive (true);
    } 
    void Start()
    {
        Rigidbody2D =GetComponent<Rigidbody2D>();
        fire = FindObjectOfType<Fire>();
        Camera maincamera = Camera.main;
        min = maincamera.ViewportToWorldPoint(new Vector2(0,0));
        max = maincamera.ViewportToWorldPoint(new Vector2(0,0));
    }

    void Update()
    {
        Move();
        Shoot();
        moveJoystick();
    }

    public void Move()
    {
        //Giới hạn palyer ko đi ra khỏi màn hình
        if(transform.position.x > posX)
        {
            transform.position = new Vector2(posX,transform.position.y);
        }
        if(transform.position.x < -posX)
        {
            transform.position = new Vector2(-posX,transform.position.y);
        }
        if(transform.position.y > posY)
        {
            transform.position = new Vector2(transform.position.x,posY);
        }
        if(transform.position.y < -posY)
        {
            transform.position = new Vector2(transform.position.x,-posY);
        }

        {
        // Di chuyển trái
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        // Di chuyển phải
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }  
        // Di chuyển xuống
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        // Di chuyển lên
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            fire.Up();
        }
        else 
        {
            fire.Down();
        }
        }
    }

    public void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<AudioSource>().Play();
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation); // Tạo đạn
            Vector3 shootDir = transform.right; // Xác định hướng bắn của đạn
            bullet.GetComponent<BulletController>().Shoot(shootDir, bulletSpeed); // Gọi hàm Shoot() trong script của đạn để bắn nó
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "EnemyShipTag")||(col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            lives--;
            LiveUIText.text = lives.ToString();
            if(lives == 0)
            {
                GameManagerGo.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
    void moveJoystick()
    {
        float moveX = Joystick.Horizontal;    // Lấy giá trị di chuyển theo trục x của joystick
        float moveY = Joystick.Vertical;      // Lấy giá trị di chuyển theo trục y của joystick

        Vector2 movement = new Vector2(moveX, moveY);    // Vector di chuyển của player

        Rigidbody2D.velocity = movement * Speed;     // Thiết lập vận tốc của player dựa trên giá trị joystick
        
    }
}
