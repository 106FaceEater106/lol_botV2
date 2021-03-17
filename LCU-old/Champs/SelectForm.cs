using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCU.Champs {
    public partial class SelectForm : Form {

        private List<string> outList = new List<string>();

        public SelectForm(int limit) {
            InitializeComponent();
            allChampsList.Items.Clear();
            foreach (string s in Champions.champs.Keys) {
                allChampsList.Items.Add(s);
            }
        }

        private void SelectForm_Load(object sender, EventArgs e) {
        }

        public T[] getList<T>() {
            List<T> ret = new List<T>();
            foreach (string s in selectedChampList.Items) {
                if (typeof(T) == typeof(string)) {
                    ret.Add((dynamic)s);
                } else if (typeof(T) == typeof(int)) {
                    Champion c = Champions.champs[s];
                    ret.Add((dynamic)c.iKey);
                } else if (typeof(T) == typeof(Champion)) {
                    Champion c = Champions.champs[s];
                    ret.Add((dynamic)c);
                } else {
                    throw new Exception($"get list cant return {typeof(T)}");
                }
            }

            return ret.ToArray();
        }

        [Obsolete]
        public Champion[] getList() {
            List<Champion> ret = new List<Champion>();
            foreach(string s in selectedChampList.Items) {
                ret.Add(Champions.champs[s]);
            }
            return ret.ToArray();

        }

        private void searchBox_TextChanged(object sender, EventArgs e) {
            allChampsList.Items.Clear();
            foreach(string s in Champions.champs.Keys) {
                if((s.ToUpper()).Contains(textBox1.Text.ToUpper())) {
                    allChampsList.Items.Add(s);
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e) {
            int index = allChampsList.SelectedIndex;
            if(index == -1) {
                return;
            }

            selectedChampList.Items.Add(allChampsList.Items[index]);

        }

        private void removeButton_Click(object sender, EventArgs e) {
            int index = selectedChampList.SelectedIndex;
            if(index == -1) {
                return;
            }

            selectedChampList.Items.RemoveAt(index);

        }

        private void okButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }
    }
}
