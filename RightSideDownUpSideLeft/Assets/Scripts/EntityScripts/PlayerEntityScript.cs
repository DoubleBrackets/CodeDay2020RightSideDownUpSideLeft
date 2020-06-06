using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntityScript : EntityScript
{
    public static PlayerEntityScript playerEntityScript;

    public GameObject respawnPoint;

    public bool isInvulnerable = false;

    protected override void OnEntityDeath()
    {
        if (isInvulnerable)
            health = 1;
        else
            StartCoroutine(PlayerDeath());
    }

    private void Awake()
    {
        CreateEntity();
        playerEntityScript = this;
    }

    public void SetRespawnPoint(GameObject obj)
    {
        respawnPoint = obj;
    }

    IEnumerator PlayerDeath()
    {
        isInvulnerable = true;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRen= gameObject.GetComponent<SpriteRenderer>();
        Collider2D coll = gameObject.GetComponent<Collider2D>();

        coll.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;        
        spriteRen.enabled = false;
        RotationScript.rotationScript.IncrementCanRotate();

        ParticleManager.particleManager.PlayParticle("DeathParticle");

        yield return new WaitForSeconds(1.5f);

        gameObject.transform.position = respawnPoint.transform.position;

        RotationScript.rotationScript.SetRotation(respawnPoint.transform.localRotation.eulerAngles.z);

        yield return new WaitForSeconds(0.5f);

        ParticleManager.particleManager.PlayParticle("RespawnParticle");
        yield return new WaitForSeconds(0.25f);
        spriteRen.enabled = true;
        coll.enabled = true;

        isInvulnerable = false;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        RotationScript.rotationScript.DecrementCanRotate();
        health = 1;
    }
}
