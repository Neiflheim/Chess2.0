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
                int bestChildHeuristicValue = 0;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    maxHeuristicValue = Mathf.Max(maxHeuristicValue, MinMax(child, depth - 1, false));

                    if (bestChildHeuristicValue == 0)
                    {
                        bestChildHeuristicValue = maxHeuristicValue;
                        bestChild = child;
                    }

                    if (maxHeuristicValue != bestChildHeuristicValue)
                    {
                        bestChildHeuristicValue = maxHeuristicValue;
                        bestChild = child;
                    }
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
