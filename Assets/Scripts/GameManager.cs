using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Protocol
    /*
     * This is a proposal only
     Message = playerID [space] positionID

     int that represents position of the player's step.
     [0][1][2]
     [3][4][5]
     [6][7][8]
     */
    #endregion
    public static bool isMyTurn = false;
    int _playerNumber = 0;
    int _player1Port = 40000;
    int _player2Port = 40001;
    [SerializeField] Sprite _x;
    [SerializeField] Sprite _o;
    [SerializeField] List<Cell> _cells;
    [SerializeField] NetworkManager _networkManager;
    public void AssignPlayer(int index)
    {
        if (index == 1)
        {
            _playerNumber = index;
            _networkManager.ListeningPort = _player1Port;
            _networkManager.SendingPort = _player2Port;
            isMyTurn = true;
        }
        else if (index == 2)
        {
            _playerNumber = index;
            _networkManager.ListeningPort = _player2Port;
            _networkManager.SendingPort = _player1Port;
            isMyTurn = false;
        }
        _networkManager.StartUDP();
        //ask from server to get your player number 1/2 
    }

    public void GotNetworkMessage(string message)
    {
        Debug.Log("got network message: " + message);
        var messageInt = int.Parse(message);
        { 
            EnemyCellClicked(_cells[messageInt]);
        }
    }
    public void PositionClicked(int position)
    {
        //update the other player about the shape
        _networkManager.SendMessage($"{position}");// your job to finish it
    }
    //for debug purpouses only
    public void CellClicked(Cell cell)
    {
            if (_playerNumber == 1)
        {
            cell.image.sprite = _x;
            PositionClicked(cell.number);
        }
        else if (_playerNumber == 2)
            {
            cell.image.sprite = _o;
            PositionClicked(cell.number);
        }
        else
            {
                throw new System.Exception("Player is not assigned correctly");
            }
        isMyTurn = false;
        //send second player that you played and what cell
    }
    public void EnemyCellClicked(Cell cell)
    {
        if (_playerNumber == 1)
        {
            cell.image.sprite = _o;
        }
        else if (_playerNumber == 2)
        {
            cell.image.sprite = _x;
        }
        else
        {
            throw new System.Exception("Player is not assigned correctly");
        }
        isMyTurn = true;
    }
}
