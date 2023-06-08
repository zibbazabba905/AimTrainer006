using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargetScripts
{
    public class TargetSpawn : MonoBehaviour
    {
        public Transform PointA;
        public Transform PointB;


        public Vector3 GetSpawnPoint()
        {
            Vector3 StartPosition = PointA.position;
            Vector3 EndPosition = PointB.position;
            float Distance = Vector3.Distance(StartPosition, EndPosition);
            float randomDistance = Random.Range(0, Distance);
            Vector3 SpawnPosition = Vector3.Lerp(StartPosition, EndPosition, randomDistance / Distance);
            return SpawnPosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(PointA.position, PointB.position);
            Gizmos.DrawSphere(PointA.position, 0.2f);
            Gizmos.DrawSphere(PointB.position, 0.2f);
        }
    }
}
