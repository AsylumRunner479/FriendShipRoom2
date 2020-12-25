using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool isAlien;
    Renderer[] visuals;
    public int health = 100;
    public Text[] text;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
        //sync health
    }
    public void Damage(int damage)
    {
        health -= damage;
    }
    IEnumerator Respawn()
    {
        SetRenderers(false);
        health = 100;
        GetComponent<CharacterController>().enabled = false;
        Transform spawn = SpawnManager.instance.GetRandomSpawn();

        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
        yield return new WaitForSeconds(1);
        GetComponent<CharacterController>().enabled = true;
        SetRenderers(true);
    }
    public void PlayerRole(int Role)
    {
        if (Role == 0)
        {
            isAlien = true;
            text[0].text = "You Are An Alien";
            text[1].text = "Kill All Humans";
        }
        else
        {
            isAlien = false;
            text[0].text = "You Are A Human";
            text[1].text = "Kill All Aliens";
        }
    }
    void SetRenderers(bool state)
    {
        foreach (var renderer in visuals)
        {
            renderer.enabled = state;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        visuals = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (isAlien == true && GameManager.InGame == true)
            {
                GameManager.AlienCount -= 1;
            }
            else if (GameManager.InGame == true)
            {
                GameManager.HumanCount -= 1;
            }
            else
            {
                StartCoroutine(Respawn());
            }
            
        }
    }
}
