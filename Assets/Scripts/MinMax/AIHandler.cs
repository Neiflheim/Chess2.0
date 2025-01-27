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
                int heuristicValue = int.MinValue;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    heuristicValue = Mathf.Max(heuristicValue, MinMax(child, depth - 1, false));
                }
                
                return heuristicValue;
            }
            else
            {
                int heuristicValue = int.MaxValue;
                
                List<Node> nodeChildren = node.Children();
                foreach (Node child in nodeChildren)
                {
                    heuristicValue = Mathf.Min(heuristicValue, MinMax(child, depth - 1, true));
                }
                
                return heuristicValue;
            }
        }
    }
}
