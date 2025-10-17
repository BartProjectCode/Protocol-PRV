using UnityEngine;

public class Death : MonoBehaviour
{
        [SerializeField] private StrengthAndWeaknesses type;
        private void OnCollisionEnter(Collision other)
        {
                if (other.gameObject.CompareTag(type.strengthAgainst))
                {
                        Destroy(other.gameObject);
                        GetComponent<EntityMovement>().attackMode = false;
                }
        }
}

    