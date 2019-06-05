using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private string piece = "Piece";
    [SerializeField] private string blackTile = "BlackTile";
    [SerializeField] private string whiteTile = "WhiteTile";
    [SerializeField] private string blueTile = "BlueTile";
    [SerializeField] private string redTile = "RedTile";
    [SerializeField] private string determineTileType = "";
    [SerializeField] private bool resetPossible = true;
    [SerializeField] private bool tileSelectable = false;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material pieceMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material selectingMaterial;

    private Transform _Piece;
    private Transform _HighlightPiece;
    private Transform _Tile;
    private Transform _HighlightTile;

    // Update is called once per frame
    void Update()
    {
        if (resetPossible)
        {
            resetPiece();
        }
        highlightPiece();
        if (tileSelectable)
        {
            highlightTile();
        }
        resetTile();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(piece))
                {
                    selectPiece();
                }
                else if ((hit.transform.CompareTag(blackTile) || hit.transform.CompareTag(whiteTile) || hit.transform.CompareTag(redTile) || hit.transform.CompareTag(blueTile))  && tileSelectable)
                {
                    selectTile();
                }
            }
        }
        if (_Tile != null && _Piece != null)
        {
            movePiece();
        }
    }

    public void resetTile()
    {
        if(_HighlightTile != null)
        {
            Renderer tileRenderer = _HighlightTile.transform.GetComponent<Renderer>();
            if (_HighlightTile.CompareTag(whiteTile))
            {
                tileRenderer.material = whiteMaterial;
            }
            else if (_HighlightTile.CompareTag(blueTile))
            {
                tileRenderer.material = blueMaterial;
            }
            else if (_HighlightTile.CompareTag(redTile))
            {
                tileRenderer.material = redMaterial;
            }
            else if (_HighlightTile.CompareTag(blackTile))
            {
                tileRenderer.material = blackMaterial;
            }
            _HighlightTile = null;
        }
    }

    public void highlightTile()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Transform selectedObject = hit.transform;
            if (selectedObject.CompareTag(blackTile) || selectedObject.CompareTag(whiteTile) || selectedObject.CompareTag(redTile) || selectedObject.CompareTag(blueTile))
            {
                Renderer selectedObjectRenderer = selectedObject.GetComponent<Renderer>();
                selectedObjectRenderer.material = highlightMaterial;
                _HighlightTile = selectedObject;
            }
            else
            {
                Debug.Log("selected object in highlight tile is null");
            }
        }
    }

    public void resetPiece()
    {
        if(_HighlightPiece != null && resetPossible)
        {
            Renderer pieceRenderer = _HighlightPiece.transform.GetComponent<Renderer>();
            pieceRenderer.material = pieceMaterial;
            _HighlightPiece = null;
        }
    }

    public void highlightPiece()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Transform selectedObject = hit.transform;
            if (selectedObject.CompareTag(piece))
            {
                Renderer pieceHighlighter = selectedObject.transform.GetComponent<Renderer>();
                pieceHighlighter.material = highlightMaterial;
                _HighlightPiece = selectedObject;
            }
            else
            {
                Debug.Log("selected object in highlight piece is null");
            }
        }
    }

    public void selectPiece()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Transform selectedObject = hit.transform;
            Renderer pieceRenderer = selectedObject.transform.GetComponent<Renderer>();
            pieceRenderer.material = selectingMaterial;
            resetPossible = false;
            tileSelectable = true;

            _Piece = selectedObject;
        }
    }

    public void selectTile()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if ((hit.transform.CompareTag(blackTile) || hit.transform.CompareTag(whiteTile) || hit.transform.CompareTag(redTile) || hit.transform.CompareTag(blueTile)) && tileSelectable)
            {
                Transform selectedObject = hit.transform;
                if (selectedObject != null)
                {
                    _Tile = selectedObject;
                }
                else
                {
                    Debug.Log("selected object in selected tile is null");
                }
            }
        }
    }

    public void movePiece()
    {
        var position = _Tile.position;
        position.y += 1.5f;

        _Piece.position = position;
        _HighlightPiece = null;
        _Piece = null;
        _Tile = null;
        resetPossible = true;
        tileSelectable = false;
    }
}
