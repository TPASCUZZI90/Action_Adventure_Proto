using System.Collections;
using UnityEngine;

public class LivingBeing : MonoBehaviour
{
    public float healthPoint = 100;
    public string name;
    [SerializeField] private Material deathMat;

    public virtual void TakeDamage(float dmgPoints)
    {
        healthPoint -= dmgPoints;

        Debug.Log(name + " takes " + dmgPoints + " damage");

        if (healthPoint <= 0)
        {
            StartCoroutine(Die());
        }
    }

    protected virtual IEnumerator Die()
    {
        gameObject.GetComponent<Renderer>().material = deathMat;
        yield return new WaitForSeconds(1f);        
        Destroy(gameObject);
        StopCoroutine(Die());
        Debug.Log(name + " dies");
    }

}
