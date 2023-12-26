using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  private void Awake()
  { 
    if (GameManager.instance != null)
    {
        Destroy(gameObject);
        return;
    }

    instance = this; 
    SceneManager.sceneLoaded += LoadState; 
    DontDestroyOnLoad(gameObject);
  }

  //Resources
  public List<Sprite> playerSprites;
  public List<Sprite> weaponSprites;
  public List<int> weaponPrices;
  public List<int> expTable;

  //References
  public Player player;
  public Weapon weapon;
  public FloatingTextManager floatingTextManager;
  public RectTransform hitpointBar;
  public Animator deathMenuAnim;

  //Logic
  public int glims = 0;
  public int experience = 0;

  //Floating text
  public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
  {
    floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
  }

  //Upgrade weapon
  public bool TryUpgradeWeapon()
  {
    //check if max level
    if (weaponPrices.Count <= weapon.weaponLevel)
        return false;

    if (glims >= weaponPrices[weapon.weaponLevel])
    {
        glims -= weaponPrices[weapon.weaponLevel];
        weapon.UpgradeWeapon();
        return true;
    }

    return false;
  }

  // Hitpoint Bar
  public void OnHitpointChange()
  {
      float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
      hitpointBar.localScale = new Vector3(1, ratio, 1);
  }

  //Experience System
  public int GetCurrentLevel()
  {
      int r = 0;
      int add = 0;

      while(experience >= add)
      {   
          add += expTable[r];
          r++;

          if (r == expTable.Count) //Max level
            return r;
      }

      return r;
  }

  public int GetExpToLevel(int level)
  {
      int r = 0;
      int exp = 0;

      while(r < level)
      {
          exp += expTable[r];
          r++;
      }

      return exp;
  }

  public void GainExp(int exp)
  {
      int currentLevel = GetCurrentLevel();
      experience += exp;
      if (currentLevel < GetCurrentLevel())
        OnLevelUp();
  }

  public void OnLevelUp()
  {
      Debug.Log("Level Up!");
      player.OnLevelUp();
  }

  //Death menu & respawn
  public void Respawn()
  {
    deathMenuAnim.SetTrigger("hide");
    UnityEngine.SceneManagement.SceneManager.LoadScene("Dungeon1");
    player.Respawn();
  }

  //Save state
  /*
   *INT preferedSkin
   *INT glims
   *INT experience
   *INT weaponLevel
   */
  public void SaveState()
  { 
    string s = "";
    
    s += "0" + "|";
    s += glims.ToString() + "|";
    s += experience.ToString() + "|";
    s += weapon.weaponLevel.ToString();
    
    PlayerPrefs.SetString("SaveState", s); 
    
    Debug.Log("SaveState");
  }

  public void LoadState(Scene s, LoadSceneMode mode)
  {    
    if (!PlayerPrefs.HasKey("SaveState"))
        return;
    
    string[] data = PlayerPrefs.GetString("SaveState").Split('|');
    
    glims = int.Parse(data[1]);
    experience = int.Parse(data[2]);
    if(GetCurrentLevel() != 1)
      player.SetLevel(GetCurrentLevel());

    weapon.SetWeaponLevel(int.Parse(data[3]));
    
    player.transform.position = GameObject.Find("SpawnPoint").transform.position;

    Debug.Log("LoadState");  
  }
}
