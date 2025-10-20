using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Grabber Params")] public GameObject ground; //le sol

    [Header("Private Vars")] public GameObject activeEntity; //entité actuellement controlée
    private StrengthAndWeaknesses activeStrAndWeak;
    private StrengthAndWeaknesses hitStrAndWeak;
    public Transform target;
    private Vector3 newDest;
    public float speed;

    void Update()
    {
        if (Camera.main == null) return;

        if (activeEntity != null)
        {
            float z = Camera.main.WorldToScreenPoint(activeEntity.transform.position).z;
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            
            if (Input.GetMouseButtonDown(1))
            {
                if (CastRay(out RaycastHit hit) && hit.collider != null)
                {
                    if (hit.collider.CompareTag("Fire") || hit.collider.CompareTag("Wind") ||
                        hit.collider.CompareTag("Water") || hit.collider.CompareTag("Earth"))
                    {
                        target = hit.collider.transform;
                    }

                    else
                    {
                        target = null;
                        newDest = new Vector3(worldPos.x, ground.transform.position.y, worldPos.z);
                    }
                    
                }
            }

            if (target == null)
            {
                activeEntity.transform.position =
                    Vector3.MoveTowards(activeEntity.transform.position, newDest, speed * Time.deltaTime);
            }
            else
            {
                activeEntity.transform.position =
                    Vector3.MoveTowards(activeEntity.transform.position, target.transform.position, (speed * 2f) * Time.deltaTime);
                // pour cause de scripts private, l'entity qui est possédée retourne à sa newDest Précédente quand elle n'a plus de cible
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (CastRay(out RaycastHit hit) && hit.collider != null)
            {
                if (hit.collider.CompareTag("Fire") || hit.collider.CompareTag("Wind") ||
                    hit.collider.CompareTag("Water") || hit.collider.CompareTag("Earth"))
                {
                    GameObject hitGO = hit.collider.gameObject;

                    if (activeEntity == null)
                    {
                        Debug.Log("Control active");
                        activeEntity = hitGO;
                        activeStrAndWeak = hitGO.GetComponent<StrengthAndWeaknesses>();
                        activeEntity.GetComponent<EntityMovement>().enabled = false;
                        newDest = activeEntity.transform.position;
                    }

                    else if (activeEntity != null && hitGO == activeEntity)
                    {
                        Debug.Log("No control active");
                        newDest = activeEntity.transform.position;
                        activeEntity.GetComponent<EntityMovement>().enabled = true;
                        activeEntity = null;
                    }
                }
            }
        }

        
    }

    private bool CastRay(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }
}