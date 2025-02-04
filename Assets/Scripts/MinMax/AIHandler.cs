using System.Collections.Generic;
using UnityEngine;

namespace MinMax
{
    public class AIHandler : MonoBehaviour
    {
        public int MinMax(Node node, int depth, bool maximizingPlayer)
        {
            if (depth == 0 || node.IsTerminal())
            {
                return node.HeuristicValue();
            }
            
            if (maximizingPlayer)
            {
                int maxHeuristicValue = int.MinValue;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    maxHeuristicValue = Mathf.Max(maxHeuristicValue, MinMax(child, depth - 1, false));
                }
                
                return maxHeuristicValue;
            }
            else
            {
                int minHeuristicValue = int.MaxValue;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    minHeuristicValue = Mathf.Min(minHeuristicValue, MinMax(child, depth - 1, true));
                }
                
                return minHeuristicValue;
            }
        }
        
        public int MinMaxAlphaBeta(Node node, int depth, bool maximizingPlayer, int alpha, int beta)
        {
            if (depth == 0 || node.IsTerminal())
            {
                return node.HeuristicValue();
            }
            
            if (maximizingPlayer)
            {
                int maxHeuristicValue = int.MinValue;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    maxHeuristicValue = Mathf.Max(maxHeuristicValue, MinMaxAlphaBeta(child, depth - 1, false, alpha, beta));
                    
                    if (maxHeuristicValue >= beta)
                    {
                        return maxHeuristicValue;
                    }
                    alpha = Mathf.Max(alpha, maxHeuristicValue);
                }
                
                return maxHeuristicValue;
            }
            else
            {
                int minHeuristicValue = int.MaxValue;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    minHeuristicValue = Mathf.Min(minHeuristicValue, MinMaxAlphaBeta(child, depth - 1, true, alpha, beta));
                    
                    if (minHeuristicValue <= alpha)
                    {
                        return minHeuristicValue;
                    }
                    beta = Mathf.Min(beta, minHeuristicValue);
                }
                
                return minHeuristicValue;
            }
        }
    }
}
