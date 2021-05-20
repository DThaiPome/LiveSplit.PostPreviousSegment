using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LiveSplit
{
    public class PostPreviousSegment : IComponent
    {
        private struct PPSState
        {
            public bool bettingOpen { get; set; }
            public bool runActive { get; set; }

            public PPSState(bool bettingOpen, bool runActive)
            {
                this.bettingOpen = bettingOpen;
                this.runActive = runActive;
            }
        }

        private PPSState state;

        private string baseApiUrl;

        private PostPreviousSegmentSettings settings;

        public PostPreviousSegment()
        {
            this.state = new PPSState(false, false);
            this.settings = new PostPreviousSegmentSettings();
            this.baseApiUrl = this.settings.baseApiUrl;
        }

        public string ComponentName => "PostToSplitBetBot";

        public float HorizontalWidth => 0f;

        public float MinimumHeight => 0f;

        public float VerticalHeight => 0f;

        public float MinimumWidth => 0f;

        public float PaddingTop => 0f;

        public float PaddingBottom => 0f;

        public float PaddingLeft => 0f;

        public float PaddingRight => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;

        public void Dispose()
        {
            return;
        }

        public void DrawHorizontal(System.Drawing.Graphics g, LiveSplitState state, float height, System.Drawing.Region clipRegion)
        {
            return;
        }

        public void DrawVertical(System.Drawing.Graphics g, LiveSplitState state, float width, System.Drawing.Region clipRegion)
        {
            return;
        }

        public System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return this.settings.GetSettings(document);
        }

        public System.Windows.Forms.Control GetSettingsControl(LayoutMode mode)
        {
            return this.settings;
        }

        public void SetSettings(System.Xml.XmlNode settings)
        {
            this.settings.SetSettings(settings);
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            this.state = new PPSState(false, false); // DOES THIS EVEN HAPPEN??
        }
    }
}
