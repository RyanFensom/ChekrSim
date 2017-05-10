using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GGL;

public class PiecePicker : MonoBehaviour
{
    public float pieceHeight = 5f;
    public float rayDistance = 1000f;
    public LayerMask selectionIgnoreLayer;

    private Piece selectedPiece;
    private CheckerBoard board;

    // Use this for initialization
    void Start()
    {
        board = FindObjectOfType<CheckerBoard>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckSelection();
        MoveSelection();
    }


    void MoveSelection()
    {
        // check if we have a piece selected
        if (selectedPiece != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance, 0.1f, 0.1f, Color.yellow, Color.red);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, ~selectionIgnoreLayer))
            {
                // Obtain the hit spot
                GizmosGL.color = Color.red;
                GizmosGL.AddSphere(hit.point, 0.5f);

                // Move the piece to position
                Vector3 piecePos = hit.point + Vector3.up * pieceHeight;
            }
        }
    }

    void CheckSelection()
    {
        // Create a ray from camera to mouse position to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance);


        RaycastHit hit;
        // check if player hits mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                // Set the selected piece to be the hit object
                selectedPiece = hit.collider.GetComponent<Piece>();
                // if the user did not hit a piece
                if (selectedPiece == null)
                {
                    // Display log message
                    Debug.Log("cannot pick up object: " + hit.collider.name);
                }
            }
        }
    }
}
