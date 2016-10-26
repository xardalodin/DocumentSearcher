using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FromSearchWindowsDocs.frontend
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // make a list out of the search terms
            List<string> terms = backend.SearchDocX.StringSplitComma(tbSearch.Text);

            //ones documnets loaded in list 
            List<string> res = new List<string>();

            //look 
            foreach (ListViewItem l in listView1.Items)
            {
                string temp = l.Text;
                if (backend.SearchDocX.DoesDocXContainTheseWords(temp,terms))
                {
                    // if true it meas add to listview2                   
                    res.Add(temp);
                }

            }
                                          
            PopulateListView(res);

        }

        private void PopulateListView(List<string> List)
        {
            List<ListViewItem> listviewCoulums = new List<ListViewItem>();

            foreach (var l in List)
            {
                ListViewItem temp = new ListViewItem();
                temp.Text = l;
                listviewCoulums.Add(temp);
            }
            listView2.Items.Clear();
            listView2.Items.AddRange(listviewCoulums.ToArray());
             
        }

        private void btAddDocs_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Multiselect = true;
                open.ShowDialog();

                List<ListViewItem> listviewCoulums = new List<ListViewItem>();

                foreach (string s in open.FileNames)
                {
                    ListViewItem temp = new ListViewItem();
                    temp.Text = s;
                    listviewCoulums.Add(temp);
                }

                listView1.Items.Clear();
                listView1.Items.AddRange(listviewCoulums.ToArray());
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("pick some files");
            }
        }

        private void DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                ListViewItem test = listView2.SelectedItems[0];
                System.Diagnostics.Process.Start("WINWORD.EXE", test.Text);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
