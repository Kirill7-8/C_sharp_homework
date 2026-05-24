namespace Example;

public class BalanceDP
    {
        private Dictionary<AVLNode, Dictionary<int, DPState>> memo;

        public BalanceDP(AVLNode root)
        {
            memo = new Dictionary<AVLNode, Dictionary<int, DPState>>();
        }
        
        public Dictionary<int, DPState> Process(AVLNode node)
        {
            if (node == null)
                return new Dictionary<int, DPState>() { { 0, new DPState(0, 0, new List<int>()) } };

            if (memo.ContainsKey(node))
                return memo[node];

            var leftStates = Process(node.Left);
            var rightStates = Process(node.Right);

            int leftSize = SubtreeSize(node.Left);
            int rightSize = SubtreeSize(node.Right);
            var leftDelState = new DPState(0, leftSize, GetAllNodes(node.Left));
            var rightDelState = new DPState(0, rightSize, GetAllNodes(node.Right));

            var leftOptions = new List<DPState>(leftStates.Values) { leftDelState };
            var rightOptions = new List<DPState>(rightStates.Values) { rightDelState };
            
            var currentStates = new Dictionary<int, DPState>();
            foreach (var l in leftOptions)
            {
                foreach (var r in rightOptions)
                {
                    if (Math.Abs(l.KeptCount - r.KeptCount) <= 1)
                    {
                        int totalKept = l.KeptCount + r.KeptCount + 1;
                        int totalRemovals = l.Removals + r.Removals;
                        List<int> totalDeleted = new List<int>();
                        totalDeleted.AddRange(l.DeletedNodes);
                        totalDeleted.AddRange(r.DeletedNodes);
                        if (!currentStates.ContainsKey(totalKept) || totalRemovals < currentStates[totalKept].Removals)
                            currentStates[totalKept] = new DPState(totalKept, totalRemovals, totalDeleted);
                    }
                }
            }

            int currSubtreeSize = SubtreeSize(node);
            var deleteState = new DPState(0, currSubtreeSize, GetAllNodes(node));

            if (!currentStates.ContainsKey(0) || deleteState.Removals < currentStates[0].Removals)
                currentStates[0] = deleteState;

            memo[node] = currentStates;
            return currentStates;
        }

        private int SubtreeSize(AVLNode node)
        {
            return node == null ? 0 : node.Count;
        }

        private List<int> GetAllNodes(AVLNode node)
        {
            List<int> nodes = new List<int>();
            Collect(node, nodes);
            return nodes;
        }

        private void Collect(AVLNode node, List<int> list)
        {
            if (node == null) 
                return;
            list.Add(node.Value);
            Collect(node.Left, list);
            Collect(node.Right, list);
        }
    }