using LiveSplit.Model;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

// TODO: Add setting to disable HTTP requests

namespace LiveSplit.UI.Components
{
    public partial class PostPreviousSegmentSettings : UserControl
    {
        public string baseApiUrl { get; private set; }
        public ComparisonOption betComparison { get; private set; }

        public ComparisonOption splitComparison { get; private set; }

        public PostPreviousSegmentSettings()
        {
            this.baseApiUrl = "";
            this.betComparison = ComparisonOption.CurrentComparison;
            this.splitComparison = ComparisonOption.CurrentComparison;

            InitializeComponent();

            this.betComparisonInput.SelectedIndex = 0;
            this.splitComparisonInput.SelectedIndex = 0;
        }

        private void baseApiUrlChanged(object sender, EventArgs e)
        {
            this.baseApiUrl = this.baseApiUrlInput.Text;
        }

        private void betComparisonInputChanged(object sender, EventArgs e)
        {
            string selected = this.betComparisonInput.SelectedItem.ToString();
            switch (selected)
            {
                case "Current Comparison":
                    this.betComparison = ComparisonOption.CurrentComparison;
                    break;
                case "Game Time":
                    this.betComparison = ComparisonOption.GameTime;
                    break;
                case "Real Time":
                    this.betComparison = ComparisonOption.RealTime;
                    break;
            }
        }

        private void splitComparisonInputChanged(object sender, EventArgs e)
        {
            string selected = this.splitComparisonInput.SelectedItem.ToString();
            switch(selected)
            {
                case "Current Comparison":
                    this.splitComparison = ComparisonOption.CurrentComparison;
                    break;
                case "Game Time":
                    this.splitComparison = ComparisonOption.GameTime;
                    break;
                case "Real Time":
                    this.splitComparison = ComparisonOption.RealTime;
                    break;
            }
        }

        private void PostPreviousSegmentSettings_Load(object sender, EventArgs e)
        {

        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            this.baseApiUrl = SettingsHelper.ParseString(element["baseApiUrl"]);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "baseApiUrl", baseApiUrl);
        }
    }
}
