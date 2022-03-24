using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cell : MonoBehaviour
{
    public int number;
    public Image image;
        
    [SerializeField] GameManager _gameManager;
    public void OnClicked()
    {
        if (!GameManager.isMyTurn)
        {
            Debug.Log("Not your turn");
            return;
        }

        if(image.sprite != null)
        {
            Debug.Log($"Cell {number} is clicked and is already assigned");
            return;
        }
        Debug.Log("Cell is empty and clicked");
        _gameManager.CellClicked(this);
    }
}
