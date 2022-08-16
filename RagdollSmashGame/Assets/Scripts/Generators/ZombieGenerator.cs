using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct ZombieType
    {
        public string name;
        public Color color;

    }
    public Transform spawnPoint;
    public Transform zombieParent;
    public Zombie zombie;
    [Range(1, 100)]
    public int ZombieCount;
    public ZombieType[] zombieTypes;
    public bool randomAmount;

    int typeLen;
    private void Awake()
    {

        spawnZombies();

    }

    void spawnZombies()
    {
        typeLen = zombieTypes.Length;

        if (randomAmount)
            ZombieCount = Random.Range(0, 100);

        for (int i = 0; i < ZombieCount; i++)
        {
            Zombie zz = Instantiate(zombie, spawnPoint.position, Quaternion.identity, zombieParent);
            ZombieType type = zombieTypes[Random.Range(0, typeLen)];
            zz.color = type.color;

            ReferenceManager.instance.addZombie(zz.gameObject);
        }
    }
}
