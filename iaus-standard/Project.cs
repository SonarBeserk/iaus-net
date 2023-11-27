using System.Collections.Generic;

namespace InfiniteAxisUtility
{
    public class Project
    {
        public Project(string name = "Untitled")
        {
            Name = name;
            Behaviors = new List<Behavior>();
            BehaviorSets = new List<BehaviorSet>();
        }

        public string Name
        {
            get;
            set;
        }

        public List<Behavior> Behaviors
        {
            get;
            set;
        }

        public List<BehaviorSet> BehaviorSets
        {
            get;
            set;
        }
    }
}
