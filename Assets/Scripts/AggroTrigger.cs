using UnityEngine;

public class AggroTrigger : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private string _strength;
    private string _weakness;

    private void Start()
    {
        _strength = parent.GetComponent<StrengthAndWeaknesses>().strengthAgainst;
        _weakness = parent.GetComponent<StrengthAndWeaknesses>().weaknessAgainst;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag(_strength))
        {
            if (!parent.GetComponent<EntityMovement>().attackMode)
            {
                parent.GetComponent<EntityMovement>().target = other.gameObject;
                parent.GetComponent<EntityMovement>().attackMode = true;
            }
            
        }

        if (other.gameObject.CompareTag(_weakness))
        {
            if (!parent.GetComponent<EntityMovement>().fleeMode)
            {
                parent.GetComponent<EntityMovement>().predator = other.gameObject;
                parent.GetComponent<EntityMovement>().fleeMode = true;
            }
        }
    }
}
