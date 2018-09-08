using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSpriteToPlayer : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    public bool baseSpriteLookingLeft = true;
    public Transform player;

    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (transform.position.x >= player.position.x)
            sprite.flipX = !baseSpriteLookingLeft;
        else
            sprite.flipX = baseSpriteLookingLeft;
    }
}
