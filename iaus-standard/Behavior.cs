using System;
using System.Collections.Generic;

namespace InfiniteAxisUtility
{
    /// <summary>
    /// Category represents the priority of the consideration
    /// </summary>
    public enum Category
    {
        /// <summary>
        /// CategoryUnknown is the default value for category and is considered invalid
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        CategoryUnknown = 0,

        /// <summary>
        /// CategoryIdle is for idle behaviors and is normal priority
        /// </summary>
        CategoryIdle = 1,

        /// <summary>
        /// CategorySurvival is for behaviors important for survival (eating, sleeping)
        /// that can be done when not in danger
        /// </summary>
        CategorySurvival = 2,

        /// <summary>
        /// CategoryCombat is for behaviors used for combat or dangerous situations that are not emergencies
        /// </summary>
        CategoryCombat = 3,

        /// <summary>
        /// CategoryEmergency is for behaviors that have to dealt with immediately. On fire, trapped, ect..
        /// </summary>
        CategoryEmergency = 4,
    }

    /// <summary>
    /// The IBehavior interface represents an action that can be taken if it scores high enough
    /// </summary>
    public interface IBehavior: IComparable<IBehavior>
    {
        /// <summary>
        /// The name of the behavior
        /// </summary>
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// The list of considerations to score
        /// </summary>
        public List<IConsideration> Considerations { get; set; }

        /// <summary>
        /// The type of consideration used to provide a bonus to the score based on priority
        /// Also known as the weight in some implementations
        /// </summary>
        public Category Category
        {
            get;
            set;
        }
    
        /// <summary>
        /// Called when the behavior is triggered
        /// </summary>
        public void Run();

        /// <summary>
        /// Score processes the considerations, and uses a compensation factor to return the same results as calculating the geometric mean
        /// </summary>
        /// <returns>the final score</returns>
        public double Score(double bonus, double min);
    }

    /// <summary>
    /// Behavior represents an action that can be taken if it scores high enough
    /// This class implements the IBehavior interface and provides a consistent structure to build on
    /// </summary>
    public abstract class Behavior: IBehavior
    {
        /// <summary>
        /// The human readable name shown for the behavior
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The purpose and use of this behavior
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The list of considerations to score
        /// </summary>
        public List<IConsideration> Considerations { get; set; }

        /// <summary>
        /// The type of consideration used to provide a bonus to the score based on priority
        /// Also known as the weight in some implementations
        /// </summary>
        public Category Category { get; set; }

        protected Behavior(string name, Category category = Category.CategoryIdle, string description = "")
        {
            Name = name;
            Description = description;
            Considerations = new List<IConsideration>();
            Category = category;
        }

        /// <summary>
        /// Called when the behavior is triggered
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Score processes the considerations, and uses a compensation factor to return the same results as calculating the geometric mean
        /// </summary>
        /// <returns>the final score</returns>
        public double Score(double bonus, double min)
        {
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            double finalScore = (double)Category + bonus;

            // ReSharper disable once ArrangeRedundantParentheses
            // ReSharper disable once RedundantCast
            // Compensate for multiple considerations being multiplied together
            double compensationFactor = 1.0 - (1.0 / (double)Considerations.Count);

            foreach (var consideration in Considerations)
            {
                var score = consideration.Calculate();
                // Compensate for number of considerations
                double modification = (1.0 - score) * compensationFactor;
                score += (modification * score);

                finalScore *= score;

                // Behavior can't win so we stop processing conditions
                if (finalScore < min)
                {
                    break;
                }
            }

            return finalScore;
        }

        public int CompareTo(IBehavior other)
        {
            // Sort above null behavior
            if (other == null)
            {
                return 1;
            }

            // Sort above lower category behaviors
            if (Category > other.Category)
            {
                return 1;
            }

            // Sort below higher category behaviors 
            if (Category < other.Category)
            {
                return -1;
            }

            return 0;
        }
    }

    /// <summary>
    /// BehaviorSet represents a set of behaviors that are typically run together
    /// This can be use for archetypes (guard, shopkeeper, monster, ect) or to package behaviors together
    /// </summary>
    public class BehaviorSet
    {
        public readonly string Name;
        public readonly List<IBehavior> Behaviors;

        public BehaviorSet(string name, List<IBehavior> behaviors)
        {
            Name = name;
            Behaviors = behaviors;
        }
    }
}