﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AttackableController : MonoBehaviour {

    // TODO: Needs check whether public or protected
    public int hp;
    public int atk;
    public int def;
    public float range;
    public float speed;
    public float cd; // cooldown
    public bool isAlive;

    // Use this for initialization
    public virtual void Start () {
        this.isAlive = true;
    }

    // Update is called once per frame
    public virtual void Update () {
        if (this.cd > 0) {
            this.cd -= Time.deltaTime;
        }

        // updating Damage Text Visual effect
        for (int i = 0; i < this.transform.childCount; i++) {
            Transform child = this.transform.GetChild(i);
            if (child.name.Equals("Damage Text")) {
                child.transform.position = child.transform.position + new Vector3(0, 0.1f, 0);
                // TODO: this rotation should be wrong, wait for test.
                child.transform.rotation = Camera.main.transform.rotation;
            }
        }

        if (hp <= 0) {
            isAlive = false;
        }
    }

    //abstract protected void OnCollisionEnter(Collision collision);

    /// not decided return type
    public void Hurt (int damage) {
        if (isAlive) {
            int effectiveDamage = (damage - this.def);
            if (effectiveDamage <= 1)
                effectiveDamage = 1;

            this.hp -= effectiveDamage;
            if (this.hp <= 0) {
                this.isAlive = false;
                die();
            }

            GameObject damageText = new GameObject();
            damageText.name = "Damage Text";
            TextMesh textMesh = damageText.AddComponent<TextMesh>();
            textMesh.text = effectiveDamage.ToString();
            textMesh.fontSize = 7;
            textMesh.anchor = TextAnchor.MiddleCenter;
            damageText.transform.SetParent(this.transform);
            damageText.transform.localPosition = new Vector3(0, 0, 0);
            GameObject.Destroy(damageText, 2f);
        }
    }

    /// not decided return/param type
    abstract protected void die ();

}
