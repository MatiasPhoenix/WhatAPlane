using System.Collections;
using UnityEngine;

public class ExplosionScriptBullet : MonoBehaviour
{
    [Header("Explosion Particles")]
    public GameObject gameObjectExplosionParticle;
    public Rigidbody rigidbodyExplosionParticle;
    public ParticleSystem particleEvent;



    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Player" || collider.tag == "Player1" || collider.tag == "Player2" || collider.tag == "Player3" || collider.tag == "Player4")
        {
            // Debug.Log("Personaggio colpito");
            StartCoroutine(DestroyObject());
        }
        else if (collider.tag == "ObstacleInmortal")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyObject()
    {
        particleEvent.Play();
        rigidbodyExplosionParticle.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameObjectExplosionParticle.SetActive(true);
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        yield break;
    }
}
