using System.Collections.Generic;
using UnityEngine;

namespace MinMax
{
    public class AIHandler : MonoBehaviour
    {
        public Node BestChild = null;
        
        public int MinMax(Node node, int depth, bool maximizingPlayer, bool firstCall)
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
                    maxHeuristicValue = Mathf.Max(maxHeuristicValue, MinMax(child, depth - 1, false, false));

                    if (firstCall)
                    {
                        if (bestChildHeuristicValue == 0)
                        {
                            bestChildHeuristicValue = maxHeuristicValue;
                            BestChild = child;
                        }
                        else if (bestChildHeuristicValue < maxHeuristicValue)
                        {
                            bestChildHeuristicValue = maxHeuristicValue;
                            BestChild = child;
                        }
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
                    minHeuristicValue = Mathf.Min(minHeuristicValue, MinMax(child, depth - 1, true, false));
                }
                
                return minHeuristicValue;
            }
        }
    }
}
