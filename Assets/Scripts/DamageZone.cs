using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LivingBeing>() != null)
        {
            Debug.Log($"{other.name}est entré en contact avec {gameObject.name}");
            StartCoroutine(LogWhileInContact(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LivingBeing>() != null)
        {
            Debug.Log($"{other.name} a quitté le contact avec {gameObject.name}");
            StopAllCoroutines(); // Arrête les logs quand il n'y a plus de contact
        }
    }

    private System.Collections.IEnumerator LogWhileInContact(Collider other)
    {
        while (other != null && other.GetComponent<LivingBeing>() != null)
        {
            other.gameObject.GetComponent<LivingBeing>().TakeDamage(10f);
            yield return new WaitForSeconds(0.5f); // Répète le log toutes les 0.5s
        }
    }
}
