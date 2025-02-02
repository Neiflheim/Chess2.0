using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Queen", menuName = "Piece/Queen")]
    public class Queen : Piece
    {
        private Vector2Int _basDroite  = new Vector2Int(1,1);
        private Vector2Int _basGauche  = new Vector2Int(1,-1);
        private Vector2Int _hautdroite  = new Vector2Int(-1,1);
        private Vector2Int _hautGauche  = new Vector2Int(-1,-1);
        
        private Vector2Int _testDirection  = new Vector2Int(0,0);

        private bool _movementBasDroite = true;
        private bool _movementBasGauche = true;
        private bool _movementHautDroite = true;
        private bool _movementHautGauche = true;
        
        public override List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position, bool firstCall)
        {
            _movementBasDroite = true;
            _movementBasGauche = true;
            _movementHautDroite = true;
            _movementHautGauche = true;
            
            List<Vector2Int> movements = new List<Vector2Int>();
            List<Vector2Int> movementsToRemove = new List<Vector2Int>();

            int i;
            int j;
            
            for (i = position.x + 1; i <= 7; i++) 
            { 
                if (pieces[i, position.y] == null)
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    continue;
                } 
                if (pieces[i, position.y].IsWhite != IsWhite)
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    break;
                }
                else 
                { 
                    break;
                }
            } 
            for (i = position.x - 1; i >= 0; i--) 
            { 
                if (pieces[i, position.y] == null) 
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    continue;
                } 
                if (pieces[i, position.y].IsWhite != IsWhite) 
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    break;
                }
                else 
                { 
                    break;
                }
            }
            
            for (j = position.y + 1; j <= 7; j++) 
            { 
                if (pieces[position.x, j] == null)
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    continue;
                } 
                if (pieces[position.x, j].IsWhite != IsWhite) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    break;
                }
                else 
                { 
                    break;
                }
            } 
            for (j = position.y - 1; j >= 0; j--) 
            { 
                if (pieces[position.x, j] == null) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    continue;
                } 
                if (pieces[position.x, j].IsWhite != IsWhite) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    break;
                }
                else 
                { 
                    break;
                }
            }
            
            _testDirection = position;
            while (_movementBasDroite)
            {
                _testDirection += _basDroite;
                
                if (_testDirection.x < 0 || _testDirection.x > 7 || _testDirection.y < 0 || _testDirection.y > 7)
                {
                    _movementBasDroite = false;
                    break;
                }
                if (pieces[_testDirection.x, _testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y)); 
                    continue;
                } 
                if (pieces[_testDirection.x, _testDirection.y].IsWhite != IsWhite)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y));
                    _movementBasDroite = false;
                }
                else
                {
                    _movementBasDroite = false;
                }
            }
            
            _testDirection = position;
            while (_movementBasGauche)
            {
                _testDirection += _basGauche;
                
                if (_testDirection.x < 0 || _testDirection.x > 7 || _testDirection.y < 0 || _testDirection.y > 7)
                {
                    _movementBasGauche = false;
                    break;
                }
                if (pieces[_testDirection.x, _testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y)); 
                    continue;
                } 
                if (pieces[_testDirection.x, _testDirection.y].IsWhite != IsWhite)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y));
                    _movementBasGauche = false;
                }
                else
                {
                    _movementBasGauche = false;
                }
            }
            
            _testDirection = position;
            while (_movementHautDroite)
            {
                _testDirection += _hautdroite;
                
                if (_testDirection.x < 0 || _testDirection.x > 7 || _testDirection.y < 0 || _testDirection.y > 7)
                {
                    _movementHautDroite = false;
                    break;
                }
                if (pieces[_testDirection.x, _testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y)); 
                    continue;
                } 
                if (pieces[_testDirection.x, _testDirection.y].IsWhite != IsWhite)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y));
                    _movementHautDroite = false;
                }
                else
                {
                    _movementHautDroite = false;
                }
            }
            
            _testDirection = position;
            while (_movementHautGauche)
            {
                _testDirection += _hautGauche;
                
                if (_testDirection.x < 0 || _testDirection.x > 7 || _testDirection.y < 0 || _testDirection.y > 7)
                {
                    _movementHautGauche = false;
                    break;
                }
                if (pieces[_testDirection.x, _testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y)); 
                    continue;
                } 
                if (pieces[_testDirection.x, _testDirection.y].IsWhite != IsWhite)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y));
                    _movementHautGauche = false;
                }
                else
                {
                    _movementHautGauche = false;
                }
            }
            
            if (firstCall)
            {
                foreach (Vector2Int movement in movements)
                {
                    if (!CanPlayThisMovement(pieces, this, position, movement))
                    {
                        movementsToRemove.Add(movement);
                    }
                }
            
                foreach (Vector2Int movement in movementsToRemove)
                {
                    if (movements.Contains(movement))
                    {
                        movements.Remove(movement);
                    }
                }
            }
            
            return movements;
        }
    }
}