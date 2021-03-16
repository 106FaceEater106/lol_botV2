using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Leaf.xNet;
using Newtonsoft.Json;

using LCU;
using LCU.Champs.Helper;

namespace LCU.Champs {
    public static class Champions {

        public static Dictionary<string, Champion> champs;
        public static string version;
        public static bool loadDone = false;
        public static bool loadOk = false;


        public static Task init() {
            Task t = new Task(loadChamps);
            t.Start();
            return t;
        }

        public static Champion[] UIslectChamps(int limit = -99) {
            SelectForm form = new SelectForm(limit);
            DialogResult res = form.ShowDialog();
            return (res == DialogResult.OK ? form.getList<Champion>() : null);
        }

        public static void loadChamps() {
            string ver = clientLCU.GetVersion();
            string dataPath = $@"https://ddragon.leagueoflegends.com/cdn/{ver}/data/en_US/champion.json";
            using (HttpRequest req = new HttpRequest()) {
                HttpResponse res = req.Get(dataPath);

                if(res.StatusCode != HttpStatusCode.OK) {
                    throw new Exception("Failt do get champions list");
                }

                DDchamlist data = JsonConvert.DeserializeObject<DDchamlist>(res.ToString());

                champs = data.data;
                loadDone = true;
                loadOk = champs.Count > 1;
            }

        }

    }
}
