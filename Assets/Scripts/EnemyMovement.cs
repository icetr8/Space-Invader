﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    public float changeTimer;
    float maxTimer;
    public int hp;
    public bool directionSwitch;
    public GameObject particleEffect;
    Rigidbody rig;
    public MapLimits Limits;
	// Use this for initialization
	void Start () {
        maxTimer = changeTimer;
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        switchTimer();
        if (transform.position.x == Limits.maximumX || transform.position.x == Limits.minimumX) switchDirection(directionSwitch);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Limits.minimumX, Limits.maximumX),
            Mathf.Clamp(transform.position.y, Limits.minimumY, Limits.maximumY), 0.0f);
	}
    
    void Movement ()
    {
        if(directionSwitch == true)
            rig.velocity = new Vector3(speed * Time.deltaTime, -speed * Time.deltaTime, 0);
        else
            rig.velocity = new Vector3(-speed * Time.deltaTime, -speed * Time.deltaTime, 0);
    }

    void switchTimer()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            switchDirection(directionSwitch);
            changeTimer = maxTimer;
            
                
              
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "friendlyBullet")
        {
            Destroy(col.gameObject);
            Instantiate(particleEffect, transform.position, transform.rotation);
            hp--;
            if (hp <= 0)
                Destroy(gameObject);

        }
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerCharacter>().hp--;
            Instantiate(particleEffect, transform.position, transform.rotation);
            hp--;
            if (hp <= 0)
                Destroy(gameObject);
        }
    }

    bool switchDirection(bool dir)
    {
        if (dir)
            directionSwitch = false;
        else
            directionSwitch = true;
        return directionSwitch;
    }
}
