using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Collectable
{   
    public Sprite emptyChest;
    public int glimsAmount = 5;
    protected override void OnCollect()
    {   
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ShowText("+", 15, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
            Debug.Log("Gain " + glimsAmount + " Glims!");
        }
    
    }
}
