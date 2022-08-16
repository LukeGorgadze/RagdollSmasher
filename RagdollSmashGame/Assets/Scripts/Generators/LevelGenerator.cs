using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerType
    {
        public string name;
        public Color color;

    }
    [System.Serializable]
    public struct GroundType
    {
        public string name;
        public Color color;

    }

    public MeshRenderer groundRend;
    public PlayerType[] playerTypes;
    public GroundType[] groundTypes;
    private void Awake()
    {

        Generate();

    }

    void Generate()
    {
        int plLen = playerTypes.Length;
        ReferenceManager.instance.player.setMyCol(playerTypes[Random.Range(0, plLen)].color);

        int grLen = groundTypes.Length;
        groundRend.materials[1].color = groundTypes[Random.Range(0, grLen)].color;
    }
}
