using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public float speed = 200f; // Bewegungsgeschwindigkeit des Gegners
    public float nextWaypointDistance = 3f; // Abstand zum nächsten Wegpunkt
    private int currentWaypoint = 0; // Aktueller Wegpunkt auf dem Pfad
    //private bool reachedEndOfPath = false; // Flag, um festzustellen, ob das Ende des Pfads erreicht wurde

    private Transform target; // Das Ziel, auf das der Gegner zuläuft
    private Path path; // Der Pfad, den der Gegner folgt
    private Seeker seeker; // Das Objekt, das den Pfad findet
    public Player playerController;

    private Rigidbody2D rb; // Rigidbody2D-Komponente des Gegners

    private void Start()
    {
        seeker = GetComponent<Seeker>(); // Initialisierung des Seekers
        rb = GetComponent<Rigidbody2D>(); // Initialisierung des Rigidbody2D

        // Suchen des Spielers als Ziel
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerSwitcher playerManager = GameObject.Find("PlayerSwitcher").GetComponent<PlayerSwitcher>();
        
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<Player>();
        }

        if (playerController != null && !playerController.GetIsDead())
        {
            target = playerManager.playerClass.position;
        }

        InvokeRepeating("UpdatePath", 0f, 0.5f); // Aktualisierung des Pfads alle 0,5 Sekunden
    }


    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (target != null)
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete); // Erstellung eines neuen Pfads
            }
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p; // Speichern des Pfads
            currentWaypoint = 0; // Zurücksetzen des aktuellen Wegpunkts
        }
    }

    private void FixedUpdate()
    {
        if (path == null) return; // Überprüfen, ob ein Pfad vorhanden ist

        if (currentWaypoint >= path.vectorPath.Count)
        {
            //reachedEndOfPath = true; // Flag setzen, wenn das Ende des Pfads erreicht wurde
            return;
        }
        else
        {
            //reachedEndOfPath = false; // Zurücksetzen des Flags, wenn es nicht das Ende des Pfads ist
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; // Richtung zum nächsten Wegpunkt
        Vector2 force = direction * speed * Time.deltaTime; // Berechnung der Bewegungskraft

        rb.AddForce(force); // Bewegen des Gegners

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]); // Berechnung der Entfernung zum nächsten Wegpunkt

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++; // Erhöhen des aktuellen Wegpunkts, wenn der nächste erreicht wurde
        }
    }
    
    public void SetTarget(Transform t)
    {
        target = t;
    }

    public Transform GetTarget()
    {
        return target;
    }
}
