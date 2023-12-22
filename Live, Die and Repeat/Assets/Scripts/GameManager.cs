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
  public List<int> weaponPrice;
  public List<int> xpTable;

  //References
  public Player player;
  public FloatingTextManager floatingTextManager;

  //Logic
  public int glims;
  public int experience;

  //Floating text
  public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
  {
    floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
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
    s += "0";
    
    PlayerPrefs.SetString("SaveState", s); 
    
    Debug.Log("SaveState");
  }

  public void LoadState(Scene s, LoadSceneMode mode)
  {    
    if (!PlayerPrefs.HasKey("SaveState"))
        return;
    
    string[] data = PlayerPrefs.GetString("SaveState").Split('|');
    
    //Change player skin
    glims = int.Parse(data[1]);
    experience = int.Parse(data[2]);
    //Change weapon level
    
    Debug.Log("LoadState");  
  }
}
