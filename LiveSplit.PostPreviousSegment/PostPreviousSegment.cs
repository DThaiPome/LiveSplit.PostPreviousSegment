using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

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

        private PostPreviousSegmentSettings settings;

        public bool onSplitAdded;

        private HttpClient client;

        public PostPreviousSegment()
        {
            this.state = new PPSState(false, false);
            this.settings = new PostPreviousSegmentSettings();
            this.onSplitAdded = false;
            this.client = new HttpClient();
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
            if (!this.onSplitAdded)
            {
                state.OnSplit += this.OnSplit;
                this.onSplitAdded = true;
            }

            PPSState prevState = this.state;
            this.state = GetTargetState(state);
            if (prevState.bettingOpen != this.state.bettingOpen)
            {
                string b = $"{this.state.bettingOpen}".ToLower();
                HttpPost($"SetBettingOpen?open={b}");
            }
        }

        private PPSState GetTargetState(LiveSplitState state)
        {
            TimingMethod method = GetBetTimingMethod(state);
            if (state.CurrentPhase == TimerPhase.NotRunning || state.CurrentPhase == TimerPhase.Ended)
            {
                return new PPSState(false, false);
            } else
            {
                int prevSplit = state.CurrentSplitIndex - 1;
                TimeSpan? currentSec = state.CurrentTime[method];
                TimeSpan? durationSec = new TimeSpan(0, 0, this.settings.betDuration);
                if (prevSplit < 0)
                {
                    return new PPSState(currentSec < durationSec, true);
                } else
                {
                    TimeSpan? prevSplitSec = method == TimingMethod.RealTime ? 
                        state.Run[prevSplit].SplitTime.RealTime : 
                        state.Run[prevSplit].SplitTime.GameTime;
                    TimeSpan? currentSplitSec = currentSec - prevSplitSec;
                    return new PPSState(currentSplitSec < durationSec, true);
                }
            }
        }

        private TimingMethod GetBetTimingMethod(LiveSplitState state)
        {
            return ComparisonOptionToMethod(this.settings.betComparison, state);
        }

        private TimingMethod GetSplitTimingMethod(LiveSplitState state)
        {
            return ComparisonOptionToMethod(this.settings.splitComparison, state);
        }

        private static TimingMethod ComparisonOptionToMethod(ComparisonOption opt, LiveSplitState state)
        {
            switch (opt)
            {
                case ComparisonOption.GameTime:
                    return TimingMethod.GameTime;
                case ComparisonOption.RealTime:
                    return TimingMethod.RealTime;
                default:
                    return state.CurrentTimingMethod;
            }
        }

        public void OnSplit(object sender, EventArgs e)
        {
            Console.WriteLine("Split!");

            LiveSplitState state = null;
            try
            {
                state = (LiveSplitState)sender;
            } catch(InvalidCastException err)
            {
                return;
            }

            TimingMethod method = GetSplitTimingMethod(state);
            int prevSplitIndex = state.CurrentSplitIndex - 1;
            ISegment prevSeg = state.Run[prevSplitIndex];
            TimeSpan? prevSplitTime = method == TimingMethod.RealTime ?
                prevSeg.SplitTime.RealTime :
                prevSeg.SplitTime.GameTime;
            TimeSpan? pbSplitTime = method == TimingMethod.RealTime ? // TODO: might have to do the same subtraction as above with this ^ do further testing
                prevSeg.PersonalBestSplitTime.RealTime :
                prevSeg.PersonalBestSplitTime.GameTime;
            if (prevSplitIndex > 0)
            {
                prevSplitTime -= method == TimingMethod.RealTime ? // I have to do this to actually get the segment time
                    state.Run[prevSplitIndex - 1].SplitTime.RealTime :
                    state.Run[prevSplitIndex - 1].SplitTime.GameTime;
                pbSplitTime -= method == TimingMethod.RealTime ?
                    state.Run[prevSplitIndex - 1].PersonalBestSplitTime.RealTime :
                    state.Run[prevSplitIndex - 1].PersonalBestSplitTime.GameTime;
            }
            TimeSpan? bestSplitTime = method == TimingMethod.RealTime ?
                prevSeg.BestSegmentTime.RealTime :
                prevSeg.BestSegmentTime.GameTime;

            string comp = "";

            if (pbSplitTime == null || bestSplitTime == null)
            {
                Console.WriteLine("No best time found..."); // TODO - End split with no rewards - make that an input to the API
            } else
            {
                if (prevSplitTime < bestSplitTime)
                {
                    comp = "gold";
                }
                else if (prevSplitTime < pbSplitTime - new TimeSpan(0, 0, 0, 0, 100))
                {
                    comp = "ahead";
                } else if (TimeAbs(prevSplitTime - pbSplitTime) < new TimeSpan(0, 0, 0, 0, 100))
                {
                    comp = "tied";
                } else
                {
                    comp = "behind";
                }
            }
            if (comp != "")
            {
                HttpPost($"OnSplit?result={comp}");
            }
        }

        private static TimeSpan? TimeAbs(TimeSpan? time)
        {
            if (time < new TimeSpan(0, 0, 0))
            {
                return -time;
            } else
            {
                return time;
            }
        }

        private class EmptyPostBody { }

        private async void HttpPost(string apiUrl)
        {
            if (this.settings.enableApiCalls)
            {
                HttpContent data = new StringContent("{}", Encoding.UTF8, "application/json");
                string url = this.settings.baseApiUrl + apiUrl;
                HttpResponseMessage response = await client.PostAsync(url, data);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
