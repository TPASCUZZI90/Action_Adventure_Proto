using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Material lightAttackMat;
    public Material heavyAttackMat;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private float lightAttackDmg = 25f;
    [SerializeField] private float heavyAttackDmg = 50f;
    [SerializeField] private LayerMask ennemyLayer;
    [SerializeField] private CharacterController playerCharacterController;
    [SerializeField] private PlayerControl playerControlScript;
    // Update is called once per frame
    private void Start()
    {
        playerCharacterController = GetComponent<CharacterController>();
        playerControlScript = GetComponent<PlayerControl>();
    }

    void Update()
    {
        Attacks();
        float heavyAttackAxis = Input.GetAxis("Fire2");
    }

    void Attacks()
    {
        float heavyAttackAxis = Input.GetAxis("Fire2");
        if (playerCharacterController.isGrounded)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //playerControlScript.AlignWithCamera();
                LightAttack();
            }

            if (heavyAttackAxis > 0)
            {
                //playerControlScript.AlignWithCamera();
                HeavyAttack();
            }
        }        
    }

    void LightAttack()
    {                
        Collider[] attackedObjectsColliders = Physics.OverlapSphere(transform.position, attackRange, ennemyLayer);
        foreach (Collider col in attackedObjectsColliders)
        {
            Vector3 directionToObject = (col.transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, directionToObject) < 0) continue;

            float objectAngle = Vector3.Angle(transform.forward, directionToObject);

            if(objectAngle <= attackAngle / 2)
            {
                GameObject attackedObj = col.gameObject;
                attackedObj.GetComponent<Renderer>().material = lightAttackMat;
                if (attackedObj.CompareTag("LivingBeing"))
                {
                    attackedObj.GetComponent<LivingBeing>().TakeDamage(lightAttackDmg);
                }
            }                
        }
               
    }

    void HeavyAttack()
    {
        Collider[] attackedObjectsColliders = Physics.OverlapSphere(transform.position, attackRange, ennemyLayer);
        foreach (Collider col in attackedObjectsColliders)
        {
            Vector3 directionToObject = (col.transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, directionToObject) < 0) continue;

            float objectAngle = Vector3.Angle(transform.forward, directionToObject);

            if (objectAngle <= attackAngle / 2)
            {
                GameObject attackedObj = col.gameObject;
                attackedObj.GetComponent<Renderer>().material = heavyAttackMat;
                if (attackedObj.CompareTag("LivingBeing"))
                {
                    attackedObj.GetComponent<LivingBeing>().TakeDamage(heavyAttackDmg);
                }
            }
        }
    }
}
