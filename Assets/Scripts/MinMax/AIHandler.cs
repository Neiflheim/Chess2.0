using System.Collections.Generic;
using UnityEngine;

namespace MinMax
{
    public class AIHandler : MonoBehaviour
    {
        public Node bestChild = null;
        
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
                    int heuristicValue = MinMax(child, depth - 1, false);

                    if (heuristicValue > maxHeuristicValue)
                    {
                        maxHeuristicValue = heuristicValue;
                        bestChild = child;
                    }
                    
                    // maxHeuristicValue = Mathf.Max(maxHeuristicValue, MinMax(child, depth - 1, false));
                }
                
                return maxHeuristicValue;
            }
            else
            {
                int maxHeuristicValue = int.MaxValue;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    int heuristicValue = MinMax(child, depth - 1, true);

                    if (heuristicValue < maxHeuristicValue)
                    {
                        maxHeuristicValue = heuristicValue;
                        bestChild = child;
                    }
                    
                    // heuristicValue = Mathf.Min(heuristicValue, MinMax(child, depth - 1, true));
                }
                
                return maxHeuristicValue;
            }
        }
    }
}
