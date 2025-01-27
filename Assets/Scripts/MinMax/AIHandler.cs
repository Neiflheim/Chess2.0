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
                foreach (Node child in node.Children())
                {
                    int childHeuristicValue = child.HeuristicValue();
                    if (childHeuristicValue > heuristicValue)
                    {
                        heuristicValue = MinMax(child, depth - 1, false);
                    }
                }
                
                return Mathf.RoundToInt(heuristicValue);
            }
            else
            {
                float heuristicValue = Mathf.Infinity;
                foreach (Node child in node.Children())
                {
                    int childHeuristicValue = child.HeuristicValue();
                    if (childHeuristicValue < heuristicValue)
                    {
                        heuristicValue = MinMax(child, depth - 1, true);
                    }
                }
                
                return Mathf.RoundToInt(heuristicValue);
            }
        }
    }
}
