using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathColl : MonoBehaviour
{
    [SerializeField] PlayerCtrl playerCtrl;

    [Space]
    [SerializeField] string targetTag; // Tag của đối tượng muốn phát hiện
    [SerializeField] Vector3 boxSize;

    public Vector3 BoxSize { get => boxSize; set => boxSize = value; }

    void Start()
    {
        playerCtrl = GetComponentInParent<PlayerCtrl>();
    }

    void Update()
    {
        playerDeath();
    }

    private void playerDeath()
    {
        if (BoxCastHit(targetTag))
        {
            print("Player Death");
            playerCtrl.SetDeathState();
        }
    }

    protected bool BoxCastHit(string tag)
    {
        // Sử dụng Boxcast
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

    // Hàm được gọi tự động khi bạn chọn đối tượng trong Scene view
    protected void OnDrawGizmosSelected()
    {
        // Vẽ Boxcast
        Gizmos.DrawWireCube(transform.position, BoxSize);
    }

}