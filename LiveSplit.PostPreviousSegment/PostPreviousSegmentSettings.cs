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
        public bool enableApiCalls { get; private set; }
        public int betDuration { get; private set; }

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
            this.betComparison = this.GetComparison(this.betComparisonInput);
        }

        private void splitComparisonInputChanged(object sender, EventArgs e)
        {
            this.splitComparison = this.GetComparison(this.splitComparisonInput);
        }

        private ComparisonOption GetComparison(ComboBox input)
        {
            string selected = input.SelectedItem.ToString();
            switch (selected)
            {
                case "Current Comparison":
                    return ComparisonOption.CurrentComparison;
                case "Game Time":
                    return ComparisonOption.GameTime;
                case "Real Time":
                    return ComparisonOption.RealTime;
            }
            return (ComparisonOption)(-1);
        }

        private void PostPreviousSegmentSettings_Load(object sender, EventArgs e)
        {
            return;
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            this.baseApiUrl = SettingsHelper.ParseString(element["baseApiUrl"]);
            this.baseApiUrlInput.Text = this.baseApiUrl;
            this.betComparisonInput.SelectedItem = this.betComparisonInput.Items[SettingsHelper.ParseInt(element["betComparison"])];
            this.betComparison = this.GetComparison(this.betComparisonInput);
            this.splitComparisonInput.SelectedItem = this.betComparisonInput.Items[SettingsHelper.ParseInt(element["splitComparison"])];
            this.splitComparison = this.GetComparison(this.splitComparisonInput);
            this.enableApiCallsInput.Checked = SettingsHelper.ParseBool(element["enableApiCalls"]);
            this.betDurationInput.Text = SettingsHelper.ParseInt(element["betDuration"]).ToString();
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
            return SettingsHelper.CreateSetting(document, parent, "baseApiUrl", baseApiUrl) ^
                SettingsHelper.CreateSetting(document, parent, "betComparison", this.betComparisonInput.SelectedIndex) ^
                SettingsHelper.CreateSetting(document, parent, "splitComparison", this.splitComparisonInput.SelectedIndex) ^
                SettingsHelper.CreateSetting(document, parent, "enableApiCalls", enableApiCalls) ^
                SettingsHelper.CreateSetting(document, parent, "betDuration", betDuration);
        }

        private void enableApiCalls_CheckedChanged(object sender, EventArgs e)
        {
            this.enableApiCalls = this.enableApiCallsInput.Checked;
        }

        private void betDurationInput_TextChanged(object sender, EventArgs e)
        {
            string durStr = this.betDurationInput.Text;
            if (int.TryParse(durStr, out int r))
            {
                this.betDuration = r;
            } else
            {
                this.betDurationInput.Text = this.betDuration.ToString();
            }
        }
    }
}
