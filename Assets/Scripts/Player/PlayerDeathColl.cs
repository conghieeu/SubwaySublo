using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubwaySublo.Player
{
    public class PlayerDeathColl : MonoBehaviour
    {
        [SerializeField] PlayerCtrl playerCtrl;

        [Space]
        [SerializeField] string targetTag;
        [SerializeField] Vector3 boxSize;

        bool isDeath;

        public Vector3 BoxSize { get => boxSize; set => boxSize = value; }
        public bool IsDeath { get => isDeath; set => isDeath = value; }

        void Start()
        {
            playerCtrl = GetComponentInParent<PlayerCtrl>();
        }

        void Update()
        {
            // CHECK THE DEATH
            if (BoxCastHit(targetTag)) isDeath = true;
        }

        protected bool BoxCastHit(string tag)
        {
            RaycastHit[] boxcastHits = Physics.BoxCastAll(transform.position, BoxSize / 2f, transform.forward, Quaternion.identity, 0f);
            foreach (RaycastHit hit in boxcastHits)
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    return true;
                }
            }
            return false;
        }

        // HIỆN BOXCAST KHI TRỎ VÀO
        protected void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, BoxSize);
        }

    }
}