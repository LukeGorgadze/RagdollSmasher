using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReferenceManager : Singleton<ReferenceManager>
{
    public Player player;
    public Transform Pl;
    public Transform movePosTrans;
    public GameObject WinScene;
    public GameObject LoseScene;
    public List<GameObject> zombieList;
    public TextMeshProUGUI zombieText;

    public void addZombie(GameObject zom)
    {
        zombieList.Add(zom);
        zombieText.text = "ZOMBIES : " + zombieList.Count;
    }

    public void removeZombie(GameObject zom)
    {
        zombieList.Remove(zom);
        zombieText.text = "ZOMBIES : " + zombieList.Count;
    }
}
