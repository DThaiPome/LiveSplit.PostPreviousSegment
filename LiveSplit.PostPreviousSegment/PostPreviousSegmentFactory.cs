using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit
{
    public class PostPreviousSegmentFactory : IComponentFactory
    {
        public string ComponentName => "PostToSplitBetBot";

        public string Description => "Make calls to the SplitBetBot API";

        public ComponentCategory Category => ComponentCategory.Other;

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => new Version("0.0.1");

        public IComponent Create(LiveSplitState state)
        {
            return new PostPreviousSegment();
        }
    }
}
