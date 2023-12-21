using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Collidable : MonoBehaviour
{
  public ContactFilter2D filter;
  private TilemapCollider2D tilemapCollider;
  private Collider2D[] hits = new Collider2D[10];

  protected virtual void Start(){
    tilemapCollider = GetComponent<TilemapCollider2D>();
  }

  protected virtual void Update(){
    //Collision work
    tilemapCollider.OverlapCollider(filter, hits);
    for (int i = 0; i < hits.Length; i++)
    {
        if (hits[i] == null)
            continue;

        OnCollide(hits[i]);

        //Clean up the array
        hits[i] = null;
    }
  }

  protected virtual void OnCollide(Collider2D coll)
  { 
    Debug.Log(coll.name);    
  }
}
