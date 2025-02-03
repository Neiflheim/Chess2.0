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
        
        public int MinMaxAlphaBeta(Node node, int depth, bool maximizingPlayer)
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
    }
}
