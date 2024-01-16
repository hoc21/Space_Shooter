using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    [SerializeField] Vector2 speed;
    Vector2 offset;
    Material material;
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        offset = speed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
