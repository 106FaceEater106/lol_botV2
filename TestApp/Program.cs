using System;
using System.Threading;
using System.Windows;

using System.Text.Json;

using LeagueBotV3;
using LeagueBotV3.AI;
using LeagueBotV3.Pattern;
using LeagueBotV3.Pattern.Action;

using LCU;
using LCU.Helper;


namespace testApp {
    class Program {
        static void Main(string[] args) {
            while(true) {
                LeagueBotV3.Windows.MoveMouse(100,100);
                Thread.Sleep(10000);
                LeagueBotV3.Windows.MoveMouse(1000, 1000);
                Thread.Sleep(10000);

            }
        }
    }
}
