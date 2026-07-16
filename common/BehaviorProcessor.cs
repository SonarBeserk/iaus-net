using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiniteAxisUtility
{
    public class BehaviorProcessor
    {
        private readonly Dictionary<string, IBehavior> _behaviors;
        private Type _lastBehavior;

        public BehaviorProcessor()
        {
            _behaviors = new Dictionary<string, IBehavior>();
        }

        public BehaviorProcessor(List<IBehavior> behaviors) : this()
        {
            foreach (var behavior in behaviors)
            {
                _behaviors.Add(behavior.GetType().Name, behavior);
            }
        }

        public BehaviorProcessor(BehaviorSet behaviorSet) : this(behaviorSet.Behaviors)
        {
        }

        public void AddBehavior(IBehavior behavior)
        {
            _behaviors.Add(behavior.GetType().Name, behavior);
        }

        public void RemoveBehavior(IBehavior behavior)
        {
            _behaviors.Remove(behavior.GetType().Name);
        }

        public void ClearBehaviors()
        {
            _behaviors.Clear();
        }

        public void AddBehaviorsFromSet(BehaviorSet behaviorSet)
        {
            foreach (var behavior in behaviorSet.Behaviors)
            {
                _behaviors.Add(behavior.GetType().Name, behavior);
            }
        }
        
        public void RemoveBehaviorsFromSet(BehaviorSet behaviorSet)
        {
            foreach (var behavior in behaviorSet.Behaviors)
            {
                _behaviors.Remove(behavior.GetType().Name);
            }
        }

        public IBehavior FindNextBehavior()
        {
            double bestScore = 0.0;
            IBehavior bestBehavior = null;

            // Attempt to sort and run higher category behaviors first
            var behaviors = _behaviors.Values.OrderBy(b => b.Category);

            foreach (var behavior in behaviors)
            {
                double bonus = 0.0;

                // Momentum bonus encourages the same action to continue
                if (_lastBehavior != null && behavior.GetType() == _lastBehavior)
                {
                    bonus = 1.25;
                }

                var finalScore = behavior.Score(bonus, bestScore);

                // Score can't possibly win so we skip the behavior
                if (finalScore <= 0)
                {
                    continue;
                }

                // Score is lower than the best score
                if (finalScore < bestScore)
                {
                    continue;
                }

                bestScore = finalScore;
                bestBehavior = behavior;
            }

            if (bestBehavior == null)
            {
                throw new InvalidOperationException("No valid behaviors detected");
            }

            // Update last behavior
            _lastBehavior = bestBehavior.GetType();
            return bestBehavior;
        }
    }
}
