using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10.0f;
    public float camSpeed = 10.0f;

    private Rigidbody2D rb;
    private Transform cam;
    private Animator anim;

    private Vector3 camPos;
    private Vector3 prevDir = Vector2.down;

    private float animSpeed = 1.0f;
    private float fastAnimSpeed = 2.0f;

    private void Start()
    {
        float vol = PlayerPrefs.GetFloat("masterVolume");
        AudioListener.volume = vol;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = Camera.main.transform;
        camPos = cam.position;
        cam.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float speed = Speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 1.5f;
            anim.speed = fastAnimSpeed;
        }
        else
        {
            anim.speed = animSpeed;
        }
        int animX = 0;
        int animY = 0;
        if (horizontal > 0) animX = 1;
        else if (horizontal < 0) animX = -1;
        if (vertical > 0) animY = 1;
        else if (vertical < 0) animY = -1;

        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("Walking", true);
            anim.SetInteger("dX", animX);
            anim.SetInteger("dY", animY);
            prevDir = new Vector2(animX, animY);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetInteger("dX", (int)prevDir.x);
            anim.SetInteger("dY", (int)prevDir.y);
        }

        rb.velocity = new Vector2(horizontal, vertical) * speed;
        camPos = Vector3.Lerp(cam.position, rb.position + (rb.velocity.normalized * 1.5f), camSpeed * Time.deltaTime);
        camPos.z = -10;
        cam.position = camPos;
    }

}
