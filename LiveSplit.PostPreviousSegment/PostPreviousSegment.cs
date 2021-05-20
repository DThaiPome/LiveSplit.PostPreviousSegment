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

        public bool onSplitAdded;

        public PostPreviousSegment()
        {
            this.state = new PPSState(false, false);
            this.settings = new PostPreviousSegmentSettings();
            this.baseApiUrl = this.settings.baseApiUrl;
            this.onSplitAdded = false;
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
                Console.WriteLine("Switched!");
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
                TimeSpan? thirtySec = new TimeSpan(0, 0, 30);
                if (prevSplit < 0)
                {
                    return new PPSState(currentSec < thirtySec, true);
                } else
                {
                    TimeSpan? prevSplitSec = method == TimingMethod.RealTime ? 
                        state.Run[prevSplit].SplitTime.RealTime : 
                        state.Run[prevSplit].SplitTime.GameTime;
                    TimeSpan? currentSplitSec = currentSec - prevSplitSec;
                    return new PPSState(currentSplitSec < thirtySec, true);
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
            int prevSplitIndex = state.CurrentSplitIndex;
            ISegment prevSeg = state.Run[prevSplitIndex]; // TODO: I think this breaks after the run finishes
            TimeSpan? prevSplitTime = method == TimingMethod.RealTime ?
                prevSeg.SplitTime.RealTime :
                prevSeg.SplitTime.GameTime;
            TimeSpan? pbSplitTime = method == TimingMethod.RealTime ?
                prevSeg.PersonalBestSplitTime.RealTime :
                prevSeg.PersonalBestSplitTime.GameTime;
            TimeSpan? bestSplitTime = method == TimingMethod.RealTime ?
                prevSeg.BestSegmentTime.RealTime :
                prevSeg.BestSegmentTime.GameTime;

            if (pbSplitTime == null || bestSplitTime == null)
            {
                Console.WriteLine("No best time found...");
            } else
            {
                if (prevSplitTime < bestSplitTime)
                {
                    Console.WriteLine("Gold!");
                }
                else if (prevSplitTime < pbSplitTime - new TimeSpan(0, 0, 0, 0, 100))
                {
                    Console.WriteLine("Ahead!");
                } else if (TimeAbs(prevSplitTime - pbSplitTime) < new TimeSpan(0, 0, 0, 0, 100))
                {
                    Console.WriteLine("Tie!");
                } else
                {
                    Console.WriteLine("Behind!");
                }
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
    }
}
