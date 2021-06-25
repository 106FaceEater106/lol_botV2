using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

//using LeagueBot.LCU;
using LeagueBot;
using LeagueBot.AI;
using LeagueBot.DEBUG;
using LeagueBot.Patterns;
using LeagueBot.Patterns.Actions;
using LeagueBot.Constants;

using LCU;
using LCU.Helper;

using InputManager;

namespace LeagueBot {
    public class Bot {

        public event EventHandler<EndGameData> GameEndEvent;

        private Pattern pattern;
        //private Pattern currentPattern = null;

        public AvailableGameType GameType;
        public bool working;
        public bool isReady = false;
        public bool stopAsap = false;

        public Pattern nextPattern = null;
        public Thread workThread { get; private set; } = null;
        public PatternAction currentAction { get; private set; } = null;
        public baseAI ai = null;

        public bool needRestart = false;

        public Bot() {
            GameEndEvent += DBGV2.onGameEnd;

        }

        public void init() {
            isReady = clientLCU.init(BotConf.FilePath);
            Thread.Sleep(500);
            BotConst.accountId = clientLCU.getAccountId();
            DBGV2.log($"Set account id to: {BotConst.accountId}");
        }

        public string getVersion() {
            Version v = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            return v.ToString();
        }
        #region Controll

        public void FFandSTOP() {
            stopAsap = true;
            if (ai != null) {
                ai.endGameASAP = true;
            }
        }

        public void Start() {
            if(workThread != null) {
                return; // hard stop cuz i am bad at code :)
            }
            workThread = new Thread(this._start);
            workThread.Name = "workThread";
            workThread.Start();
        }

        [Obsolete("no stop msg u stopid shit")]
        public void stop(string mgs, MessageLevel lvl = MessageLevel.Info) {
            DBG.log(mgs, lvl);
            stop();
        }

        public void stop(bool killThread = true) {
            currentAction?.stop();
            pattern?.stop();
            if(killThread) {
                workThread?.Interrupt();
                workThread = null;
            }
            currentAction = null;
            nextPattern = null;
        }

        private void _start() { // TODO: Fix name
            
            Start:
            
            try {
                AvailableGameType gameType = AvailableGameType.TFT;
                working = true;
                switch (gameType) {
                    case AvailableGameType.TFT:
                        ApplyPattern(new StartTFTPattern(this));
                        break;
                    case AvailableGameType.TEST:
                        ApplyPattern(new memTestPattern(this));
                        break;
                    default:
                        throw new NotImplementedException("NO default game mode set");
                        //break;
                }
            } catch (ThreadAbortException) {
            } catch (ThreadInterruptedException) {
            } catch(Exception e) {
                DBGV2.log($"Unknown error: {e}", MessageLevel.Critical);
            }

            DBGV2.log($"Done({needRestart})");

            if(needRestart) {
                needRestart = false;
                goto Start;
            }
            working = false;
        }

        public void reset() {
            nextPattern = new StartTFTPattern(this);
            stop(false);

        }

        public void ApplyPattern(Pattern p, int i = 0) {
            pattern = p;
            do {
                pattern.ExecuteV2();
                pattern = nextPattern;
                nextPattern = null;

                if(stopAsap) {
                    stopAsap = false;
                    return;
                }

            } while(pattern != null);
            DBGV2.log("All patterns done!");
        }
        
        public void setCurrentAction(PatternAction action, Pattern sender) {
            DBGV2.log($"set action to: {action.GetType()}");
            if(sender != pattern) {
                string zombie = sender.GetType().Name;
                string live = pattern.GetType().Name;
                DBGV2.log(
                    $"ZOMBIE PATTER: {live} is active pattern but {zombie} is trying to change active action",
                    MessageLevel.Critical
                );
                stop();
            } else {
                currentAction = action;
            }
        }

        /*
        [Obsolete("Move to Bot.stop")]
        public void Abort(String stop_reson = "unknown reson", MessageLevel lvl = MessageLevel.Info) {
            if (pattern != null) pattern.Dispose();
            working = false;
            DBG.log("BOT STOPED: " + stop_reson, lvl ,"BOT");
            if(stop_reson == "unknown reson") {
                DBG.log($"{Environment.StackTrace}");
            }
        }
        */
        #endregion

        #region Mouse
        public void RightClick(Point point) {
            Mouse.Move(point.X, point.Y);
            Mouse.PressButton(Mouse.MouseKeys.Right, 150);
        }

        public void LeftClick(Point point, bool NoPress = false) {
            Mouse.Move(point.X, point.Y);
            if (!NoPress) {
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);
            }
        }
        #endregion

        #region EVENTS
        public void invoke_tft_game_end(object s, EndGameData data) {
            GameEndEvent?.Invoke(s, data);
        }
        #endregion
    }
}
