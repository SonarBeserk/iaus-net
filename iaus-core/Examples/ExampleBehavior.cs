using System;

namespace InfiniteAxisUtility.Examples
{
    /// <summary>
    /// ExampleBehavior is an example of a behavior that prints to stdout
    /// </summary>
    public class ExampleBehavior: Behavior, IBehavior
    {
        public ExampleBehavior() : base("Example", Category.CategorySurvival)
        {
            // Consideration with a score of 0.25
            Considerations.Add(new ExampleConsideration(1.0, 4.0));
        }

        public override void Run()
        {
            Console.WriteLine("Behavior Run");
        }
    }
}
