using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace AI
{
    [RequireComponent(typeof(Rigidbody2D),typeof(CapsuleCollider2D))]
    public abstract class Enemy : MonoBehaviour
    {   
        [Header("Setting Ragdoll")]
        [SerializeField] private GameObject[] bodyPart;
        [HideInInspector]  public CapsuleCollider2D cCollider;
        [HideInInspector] public Rigidbody2D rb;

        private List<CapsuleCollider2D> _ragdollCollider = new List<CapsuleCollider2D>();
        private List<Rigidbody2D> _ragdollRb = new List<Rigidbody2D>();

        private void Awake()
        {
            cCollider = GetComponent<CapsuleCollider2D>();
            rb = GetComponent<Rigidbody2D>();

            foreach(GameObject obj in bodyPart)
            {
                CapsuleCollider2D cc = obj.GetComponent<CapsuleCollider2D>();
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

                if(cc != null) _ragdollCollider.Add(cc);
                if(rb != null) _ragdollRb.Add(rb);
            }
        }

        /// <summary>
        /// mematikan effect ragdoll 
        /// </summary>
        public void DisableRagdoll()
        {
            rb.gravityScale = 1;

            cCollider.enabled = true;

            foreach(CapsuleCollider2D cc in _ragdollCollider) cc.enabled = false;
            foreach (Rigidbody2D rb in _ragdollRb) rb.isKinematic = true;
        }

        /// <summary>
        /// menjalankan effect ragdoll
        /// </summary>
        public void EnableRagdoll()
        {
            rb.gravityScale = 0;
            cCollider.enabled = false;
            foreach (CapsuleCollider2D cc in _ragdollCollider) cc.enabled = true;
            foreach (Rigidbody2D rb in _ragdollRb) rb.isKinematic = false;
        }

  
        /// <summary>
        /// di gunakan jika dibutuhkan efect phisic ketika musuh mati
        /// </summary>
        public void DeathUsingRagdoll()
        {
            EnableRagdoll();
            Destroy(gameObject,3);
        }
    }
}