using System;
using UnityEngine;

public class MapAttributes : MonoBehaviour
{
    public static MapAttributes instance { get; private set; }
    [SerializeField] private Transform[] spawnPositions;
    private int posIndex = 0;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    public Transform getSpawn()
    {
        return spawnPositions[Mathf.Clamp(posIndex++, 0, spawnPositions.Length-1)];
    }
}
