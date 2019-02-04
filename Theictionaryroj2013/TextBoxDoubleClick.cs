using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheDictionaryTrainingSetProj2013
{
    public partial class TextBoxDoubleClick : TextBox
    {
        DataGridView dataGridfield;
        public TabControl MaintabControl;
        public TextBoxDoubleClick()
        {
            
            InitializeComponent();
            this.DoubleClick += TextBoxDoubleClick_DoubleClick;
        }

        public TextBoxDoubleClick( DataGridView dataGrid): this()
        {
            dataGridfield = dataGrid;
        }
        void TextBoxDoubleClick_DoubleClick(object sender, EventArgs e)
        {
            //var win = (Form)dataGridfield.TopLevelControl;
            DataGridViewRow ro = new DataGridViewRow();
            DataGridViewRow tempro = new DataGridViewRow();

            switch (MaintabControl.SelectedIndex)
            {


                case 0:
                    dataGridfield = (DataGridView)MaintabControl.SelectedTab.Controls[0];
                                       
                    ro.CreateCells(dataGridfield);

                    ro.Cells[dataGridfield.Columns["Word"].Index].Value = Text;
                    dataGridfield.Rows.Add(ro);
                    break;
                


                case 1:
                    break;
                


                case 2:
                    break;
                


                case 3:
                    dataGridfield = (DataGridView)MaintabControl.SelectedTab.Controls["WordRelatioWordDB_dataGridView1"];

                    ro.CreateCells(dataGridfield);
                    tempro.CreateCells(dataGridfield);

                    if (dataGridfield.SelectedRows.Count < 1)
                    {
                        tempro = dataGridfield.Rows[dataGridfield.Rows.Count - 1];
                    } else
                    {
                        tempro=dataGridfield.SelectedRows[0];
                    }

                    if (((ComboBox)MaintabControl.SelectedTab.Controls["panel1"].Controls["WordRelatioWordDB_comboBox1"]).SelectedIndex == 0)
                    {
                        if (((CheckBox)MaintabControl.SelectedTab.Controls["panel1"].Controls["WordRelatioWordDB_modifyCheckBox1"]).Checked == true)
                        {
                            tempro.Cells[0].Value=Text;
                        }
                        else
                        {
                            ro.Cells[dataGridfield.Columns[0].Index].Value = Text;
                            dataGridfield.Rows.Add(ro);
                        }
                    }
                    else if (((ComboBox)MaintabControl.SelectedTab.Controls["panel1"].Controls["WordRelatioWordDB_comboBox1"]).SelectedIndex == 1)
                    {
                        if (((CheckBox)MaintabControl.SelectedTab.Controls["panel1"].Controls["WordRelatioWordDB_modifyCheckBox1"]).Checked == true)
                        {
                            tempro.Cells[2].Value = Text;
                        }
                        else
                        {
                            ro.Cells[dataGridfield.Columns[2].Index].Value = Text;
                            dataGridfield.Rows.Add(ro);
                        }
                    }
                    else if (((ComboBox)MaintabControl.SelectedTab.Controls["panel1"].Controls["WordRelatioWordDB_comboBox1"]).SelectedIndex == 2)
                    {
                        if (((CheckBox)MaintabControl.SelectedTab.Controls["panel1"].Controls["WordRelatioWordDB_modifyCheckBox1"]).Checked == true)
                        {
                            tempro.Cells[4].Value += (", " + Text);
                        }
                        else
                        {
                            ro.Cells[dataGridfield.Columns[4].Index].Value = Text;
                            dataGridfield.Rows.Add(ro);
                        }
                    }
                    break;

            }

            

            this.Parent.Controls.Remove(this);
            //throw new NotImplementedException();
        }

    }
}
