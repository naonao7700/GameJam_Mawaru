using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    //ゲームマネージャ
    [SerializeField] private GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject, 0.01f);

            //プレイヤーにダメージを与える
            game.OnPlayerDamage(100);
        }
    }
}
