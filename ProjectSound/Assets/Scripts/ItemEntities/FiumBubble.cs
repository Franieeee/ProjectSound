﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiumBubble : ItemEntity {

    public float damage = 10f;

    public float impulse; 

    [SerializeField]
    private Vector3 movementForce;

    private float cooldown = 0.001f;

    

    #region Unity
    protected override void Awake() {
        base.Awake();
        this.audioSource = this.GetComponent<AudioSource>();
    }

    private new void FixedUpdate() {
        base.FixedUpdate();
        if(!this.floating) {
            this.cooldown -= Time.deltaTime;
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if(!this.floating && this.cooldown < 0) {
            this.Damage(other);
            this.PlaySound();
        }
    }

    private void OnCollisionStay(Collision other) {
        this.OnCollisionEnter(other);
    }
    #endregion

    public override void Move(float move) {
        if(!this.floating) {
            this.rb.AddForce(move * Vector3.right, ForceMode.Impulse);
        }
    }

    public override void Use(int direction, Vector3 position) {
        this.transform.position = position;
        this.floating = false;
        this.Move(direction * impulse);
    }

    private void Damage(Collision other) {
        if(other.gameObject == GameManager.instance.player.gameObject) {
            return;
        }
        var entity = other.gameObject.GetComponent<Entity>();
        if(entity != null) {
            entity.addHealth(-this.damage);
        }
        GameObject.Destroy(this.gameObject);
    }
}
