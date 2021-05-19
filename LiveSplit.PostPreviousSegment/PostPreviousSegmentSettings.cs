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

namespace LiveSplit.PostPreviousSegment
{
    public partial class PostPreviousSegmentSettings : UserControl
    {
        public string baseApiUrl { get; private set; }

        public PostPreviousSegmentSettings()
        {
            this.baseApiUrl = "";
            InitializeComponent();
        }

        private void baseApiUrlChanged(object sender, EventArgs e)
        {
            this.baseApiUrl = this.baseApiUrlInput.Text;
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
