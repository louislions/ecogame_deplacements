using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent agent;
    public float PlayerSpeed = 50f;
    public float PlayerAcceleration = 1f;
    public Camera mainCamera;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Keyboard movements 
        /* 
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * PlayerSpeed;
        Vector3 moveAmount = velocity * Time.deltaTime;
        transform.position += moveAmount; 
        */

        //Screentouch movement

        if (Input.touchCount > 0)
        {
            //on récupère la position en pixel du toucher de l'utilisateur sur l'écran
            Touch touch = Input.touches[0];
            Vector2 touchPixelPosition = touch.position;

            //on transforme cette position en rayon perpendiculaire au plan de la camera
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(touchPixelPosition.x, touchPixelPosition.y, 0f));
            RaycastHit hit;
            Vector3 newTargetPos = new Vector3();
            //si ce rayon rencontre un obstable on récupère la position de l'impact et le jouer ce déplace vers cette position
            if (Physics.Raycast(ray, out hit))
            {
                newTargetPos = hit.point;

                // mouvement automatiques avec un NavMeshAgent
                agent.SetDestination(newTargetPos);
                agent.speed = PlayerSpeed;
                agent.acceleration = PlayerAcceleration;

                /* mouvements manuels
                Vector3 mouvement = newTargetPos - transform.position;
                Vector3 direction = mouvement.normalized;
                Vector3 velocity = direction * PlayerSpeed;
                Vector3 moveAmount = velocity * Time.deltaTime;

                transform.position += moveAmount;
                */
            }
        }
    }
}