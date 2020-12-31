using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviourPunCallbacks
{
    public Transform gunTransform;
    public ParticleSystem bullet;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                photonView.RPC("RPC_Shoot", RpcTarget.All);
                anim.SetTrigger("Shoot");
            }
        }
        
    }
    [PunRPC]
    void RPC_Shoot()
    {
        bullet.Play();
        Ray ray = new Ray(gunTransform.position, gunTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {

            //hit
            //check enemy hit
            var enemyHealth = hit.collider.GetComponentInParent<Health>();
            if (enemyHealth)
            {
                enemyHealth.Damage(20);
            }
        }
    }
}
