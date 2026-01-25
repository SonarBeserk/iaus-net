using System;
using System.Collections.Generic;

namespace InfiniteAxisUtility
{
    public class BehaviorProcessor
    {
        private readonly List<IBehavior> _behaviors;
        private Type _lastBehavior;

        public BehaviorProcessor(List<IBehavior> behaviors)
        {
            _behaviors = behaviors;
        }

        public BehaviorProcessor(BehaviorSet behaviorSet)
        {
            _behaviors = behaviorSet.Behaviors;
        }

        public IBehavior FindNextBehavior()
        {
            double bestScore = 0.0;
            IBehavior bestBehavior = null;

            // Attempt to sort and run higher category behaviors first
            _behaviors.Sort();

            foreach (var behavior in _behaviors)
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
