using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 direction; // Hướng bay của đạn
    private float speed; // Tốc độ bay của đạn
    private float distanceBeforeDestroy = 15f; // Khoảng cách tối đa đạn bay trước khi bị phá hủy

    public void Shoot(Vector3 dir, float sp){
        direction = dir;
        speed = sp;
    }

    void Update(){
        transform.position += direction * speed * Time.deltaTime; // Di chuyển đạn
        if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("PlayerShipTag").transform.position) > distanceBeforeDestroy){
            Destroy(gameObject); // Phá hủy đạn nếu nó bay quá xa
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "EnemyShipTag"))
        {
            Destroy(gameObject);
        }
    }
}
