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
                float heuristicValue = -Mathf.Infinity;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    int childHeuristicValue = MinMax(child, depth - 1, false);
                    if (childHeuristicValue > heuristicValue)
                    {
                        heuristicValue = childHeuristicValue;
                    }
                }
                
                return Mathf.RoundToInt(heuristicValue);
            }
            else
            {
                float heuristicValue = Mathf.Infinity;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    int childHeuristicValue = child.HeuristicValue();
                    if (childHeuristicValue < heuristicValue)
                    {
                        heuristicValue = childHeuristicValue;
                    }
                }
                
                return Mathf.RoundToInt(heuristicValue);
            }
        }
    }
}
