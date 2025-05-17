using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRL_Image_Creator.Windows
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            textListView.View = View.Details;
            textListView.Columns.Add("Description", 100);
            textListView.Columns.Add("Font Name", 100);
            textListView.Columns.Add("Font Size", 100);
            textListView.Columns.Add("Font Style", 100);
            textListView.Columns.Add("Text Color", 100);
            textListView.Columns.Add("Background Color", 100);
            textListView.Columns.Add("Properties", 100);
            textListView.Scrollable = true;
            

            AddTestDataRows();
            textListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        void AddTestDataRows()
        {
            string[] row1 = { "Description1", "FontName1", "12", "FontStyle1", "#FF0000", "#00FF00", "Property1" };
            string[] row2 = { "Description2", "FontName2", "24", "FontStyle2", "#FF0000", "#00FF00", "Property2" };
            string[] row3 = { "Description3", "FontName3", "36", "FontStyle3", "#FF0000", "#00FF00", "Property3" };

            ListViewItem listViewItem1 = new(row1);
            ListViewItem listViewItem2 = new(row2);
            ListViewItem listViewItem3 = new(row3);

            textListView.Items.Add(listViewItem1);
            textListView.Items.Add(listViewItem2);
            textListView.Items.Add(listViewItem3);
        }
    }
}
