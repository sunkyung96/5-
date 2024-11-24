using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public int life;
    public int score;
    public int maxPower;
    public int power;
    public float speed;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjet;

    public GameManager gameManager;
    public ObjectManager objectManager;
    public bool isHit;

    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    void Fire()
    {
        if (!Input.GetButton("space"))
            return;

        if(curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                GameObject bullet = objectManager.MakeObj("PlayerBulletA");
                bullet.transform.position = transform.position;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = objectManager.MakeObj("PlayerBulletA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                
                GameObject bulletL = objectManager.MakeObj("PlayerBulletA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bullet1 = objectManager.MakeObj("PlayerBulletA");
                bullet1.transform.position = transform.position + Vector3.right * 0.2f;

                GameObject bullet2 = objectManager.MakeObj("PlayerBulletA");
                bullet2.transform.position = transform.position + Vector3.left * 0.2f;

                GameObject bulletC = objectManager.MakeObj("PlayerBulletA");
                bulletC.transform.position = transform.position;

                Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
                rigid1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid2.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject bullet3 = objectManager.MakeObj("PlayerBulletA");
                bullet3.transform.position = transform.position + Vector3.right * 0.4f;

                GameObject bullet4 = objectManager.MakeObj("PlayerBulletA");
                bullet4.transform.position = transform.position + Vector3.right * 0.2f;

                GameObject bullet5 = objectManager.MakeObj("PlayerBulletA");
                bullet5.transform.position = transform.position;

                GameObject bullet6 = objectManager.MakeObj("PlayerBulletA");
                bullet6.transform.position = transform.position + Vector3.left * 0.2f;

                GameObject bullet7 = objectManager.MakeObj("PlayerBulletA");
                bullet7.transform.position = transform.position + Vector3.left * 0.4f;

                Rigidbody2D rigid3 = bullet3.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid4 = bullet4.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid5 = bullet5.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid6 = bullet6.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid7 = bullet7.GetComponent<Rigidbody2D>();
                rigid3.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid4.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid5.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid6.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid7.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;


        }

        

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (isHit)
                return;

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);

            if(life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Power":
                    if (power == maxPower)
                        score += 100;
                    else
                        power++;
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
