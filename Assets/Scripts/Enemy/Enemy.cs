using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject scoreUITextGo;
    [SerializeField] GameObject Explosion;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        scoreUITextGo = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x,position.y - speed *Time.deltaTime);
        transform.position = position;
        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0,0));

        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "PlayerShipTag")||(col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();
            scoreUITextGo.GetComponent<GameScore>().Score +=100;
            Destroy(gameObject);
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    } 
}