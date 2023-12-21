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
            Debug.Log("Gain " + glimsAmount + " Glims!");
        }
    
    }
}
