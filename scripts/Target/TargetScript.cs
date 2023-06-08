using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargetScripts
{
    public class TargetScript : MonoBehaviour
    {
        public CharStatsSO CharStats;
        public DrillManager Manager;
        public Transform LeftSide;
        public Transform RightSide;


        private float MinDistance = 0.1f;
        private CharacterController CharacterController;

        private bool MovingLeft;
        private float MovementSpeed;

        private void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            RandomizeDirection();
            RandomizeSpeed();
        }
        public void OnHit()
        {
            Manager.DestroyTarget(this);
            DrillManager.Instance.TargetHit();
        }

        private void Update()
        {
            MoveTo(MovingLeft ? LeftSide : RightSide);
        }

        private void MoveTo(Transform Side)
        {
            float Distance = Vector3.Distance(Side.position, transform.position);
            if(Distance <= MinDistance)
            {
                MovingLeft = !MovingLeft;
            }
            Vector3 Direction = (Side.position - transform.position).normalized;
            CharacterController.Move(MovementSpeed * Time.deltaTime * Direction);
        }
        private void RandomizeDirection()
        {
            MovingLeft = (Random.Range(0f, 1f) > 0.5f);
        }
        private void RandomizeSpeed()
        {
            int choice = Random.Range(1, 4);

            if (choice == 1)
            {
                MovementSpeed = 0;
            }
            else if (choice == 2)
            {
                MovementSpeed = CharStats.BaseMoveSpeed;
            }
            else
            {
                MovementSpeed = CharStats.SprintSpeed;
            }
        }
    }
}
