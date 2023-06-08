using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TargetScripts;

public class DrillManager : MonoBehaviour
{
    public static DrillManager Instance { get; set; }

    public TargetSpawn TargetSpawn;
    public List<TargetScript> Targets = new List<TargetScript>();
    public int MaxTargets;
    public int ConsecutiveHitMax;
    private int Level = 1;

    private int ConsecutiveHit = 0;

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            DecreaseLevel();
        if (Input.GetKeyDown(KeyCode.E))
            IncreaseLevel();

        if (Targets.Count < MaxTargets)
        {
            SpawnTarget();
        }
    }

    public void SpawnTarget()
    {
        Vector3 SpawnPoint = TargetSpawn.GetSpawnPoint();
        GameObject TargetClone = Instantiate(LevelManager.Instance.TargetPrefab, SpawnPoint, Quaternion.identity);
        TargetScript TS = TargetClone.GetComponent<TargetScript>();
        TS.Manager = this;
        TS.LeftSide = TargetSpawn.PointA;
        TS.RightSide = TargetSpawn.PointB;
        Targets.Add(TS);
    }

    public void DestroyTarget(TargetScript Target)
    {
        Targets.Remove(Target);
        Destroy(Target.gameObject);
    }

    public void ResetTargets()
    {
        //start at end of list and remove from there
        int targetCount = Targets.Count;
        for (int i = targetCount - 1; i >= 0; i--)
        {
            DestroyTarget(Targets[i]);
        }
        Targets.Clear();

        TargetSpawn.transform.position = 10 * Level * Vector3.forward;
    }

    public void IncreaseLevel()
    {
        if (Level < 20)
        {
            Level++;
            ResetTargets();
        }
    }

    public void DecreaseLevel()
    {
        if (Level > 1)
        {
            Level--;
            ResetTargets();
        }
    }

    public void TargetHit()
    {
        ConsecutiveHit++;
        if (ConsecutiveHit >= ConsecutiveHitMax)
        {
            IncreaseLevel();
            ConsecutiveHit = 0;
        }
    }
    public void TargetMiss()
    {
        ConsecutiveHit = 0;
    }
}

