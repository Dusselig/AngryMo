using System.Collections;
using UnityEngine;

namespace _Workdata.Scripts
{
    public class CheeseMunition : MonoBehaviour
    {
        [SerializeField]private Rigidbody2D cheeseRb;
        [SerializeField]private Rigidbody2D slingRb;
        [SerializeField]private bool isDragging;
        [SerializeField]private float maxDragDistance = 3.3f;
    
        [SerializeField]private LineRenderer[] lineRenderers;
        [SerializeField]private Transform[] slingPositions;
        [SerializeField]private Transform defaultPosition;
    
        [SerializeField]private GameObject nextCheesePrefab;
        [SerializeField]private bool lastCheese;
    
        void Start()
        {
            lineRenderers[0].SetPosition(0, slingPositions[0].position);
            lineRenderers[1].SetPosition(0, slingPositions[1].position);
            SetSling(defaultPosition.position);
        }
        
        private void SetSling(Vector2 position)
        {
            
            lineRenderers[0].SetPosition(1, position);
            lineRenderers[1].SetPosition(1, position);
        }
    
        void Update()
        {
            if(isDragging)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
                // if distance is bigger then maxDragDistance
                if(Vector3.Distance(mousePos, slingRb.position) > maxDragDistance)
                {
                    cheeseRb.position = slingRb.position + (mousePos - slingRb.position).normalized * maxDragDistance;
                    
                    SetSling(slingRb.position + (mousePos - slingRb.position).normalized * (maxDragDistance +3f));
                }
                else
                {
                    cheeseRb.position = mousePos;
                    
                    SetSling(mousePos + (mousePos - slingRb.position).normalized * 2.2f);
                }
                
            }
            else
            {
                SetSling(defaultPosition.position);
            }
        }
    
        
        private void OnMouseDown()
        {
            isDragging = true;
            cheeseRb.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log("mouseDown");
        }
        
        private void OnMouseUp()
        {
            isDragging = false;
            cheeseRb.bodyType =  RigidbodyType2D.Dynamic;
            StartCoroutine(nameof(StartFlying));
            Debug.Log("mouseUp");
        }
    
        private IEnumerator StartFlying()
        {
            yield return new WaitForSeconds(0.2f);
            
            GetComponent<SpringJoint2D>().enabled = false;
            
            cheeseRb.constraints = RigidbodyConstraints2D.None;
            
            enabled = false;
            
            yield return new WaitForSeconds(1.5f);
            
            if(nextCheesePrefab != null)
            {
                nextCheesePrefab.SetActive(true);
            }
            else
            {
                lastCheese = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            StartCoroutine(nameof(DeleteCheese));
        }
    
        private IEnumerator DeleteCheese()
        {
            yield return new WaitForSeconds(10);
            Destroy(gameObject);
            
            if(lastCheese)
            {
                if (GameManager.Instance == false)
                {
                    GameManager.Instance.GameIsOver();
                }
            }
        }
    }
}
