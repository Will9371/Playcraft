﻿using System.Collections.Generic;
using UnityEngine;

namespace ZMD
{
    public class MoveRBsInList : MonoBehaviour
    {
        [SerializeField] RBsInTriggerList followerList;
        
        List<Rigidbody> followers => followerList.rbs;
        Vector3 step => transform.position - priorPosition;
        
        Vector3 priorPosition;

        void Update()
        {
            for (int i = followers.Count - 1; i >= 0; i--)
            {
                if (!followers[i])
                    followers.Remove(followers[i]);
                else
                    followers[i].MovePosition(followers[i].position + step);
            }
            
            priorPosition = transform.position;
        }
    }
}