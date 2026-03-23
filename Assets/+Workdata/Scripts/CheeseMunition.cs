using System.Collections;
using UnityEngine;

namespace _Workdata.Scripts
{
    public class CheeseMunition : MonoBehaviour
    {
        [SerializeField]private Rigidbody2D cheeseRb;
        [SerializeField]private Rigidbody2D slingRb;
        [SerializeField]private bool isDragging;
        private readonly float _maxDragDistance = 2.2f;
    
        [SerializeField]private SpriteRenderer sr;
        [SerializeField]private Sprite[] sprites;
    
        [SerializeField]private LineRenderer[] lineRenderers;
        [SerializeField]private Transform[] slingPositions;
        [SerializeField]private Transform defaultPosition;
    
        [SerializeField]private GameObject nextCheesePrefab;
        [SerializeField]private bool lastCheese;
    
        void Start()
        {
            lineRenderers[0].positionCount = 2;
            lineRenderers[1].positionCount = 2;
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
                if(Vector3.Distance(mousePos, slingRb.position) > _maxDragDistance)
                {
                    cheeseRb.position = slingRb.position + (mousePos - slingRb.position).normalized * _maxDragDistance;
                    
                    SetSling(slingRb.position + (mousePos - slingRb.position).normalized * 2.6f);
                }
                else
                {
                    cheeseRb.position = mousePos;
                    
                    SetSling(mousePos + (mousePos - slingRb.position).normalized * 0.4f);
                }
                
            }
            else
            {
                SetSling(defaultPosition.position);
            }
        }
    
        
        private void OnHolding()
        {
            isDragging = true;
            cheeseRb.bodyType = RigidbodyType2D.Kinematic;
        }
        
        private void OnStopHolding()
        {
            isDragging = false;
            cheeseRb.bodyType =  RigidbodyType2D.Dynamic;
            StartCoroutine(nameof(StartFlying));
        }
    
        private IEnumerator StartFlying()
        {
            yield return new WaitForSeconds(0.2f);
            
            //TODO:GetComponent<SlingJoint2D>().enabled = false;
    
            // cheese can rotate again
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
            
            StartCoroutine(nameof(DeleteBirds));
        }
    
        private IEnumerator DeleteBirds()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
            
            if(lastCheese)
            {
                /*TODO:GameManager gm = FindObjectOfType<GameManager>();
    
                if(gm.gameIsOver == false)
                {
                    gm.GameOver();
                }//*/
            }
        }
    }
}
