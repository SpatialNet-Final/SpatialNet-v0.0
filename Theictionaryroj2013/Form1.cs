using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace TheDictionaryTrainingSetProj2013
{
    public partial class MainForm : Form
    {
        private Dictionary<string, WordsIntuitiveCloud> dictionary = new Dictionary<string, WordsIntuitiveCloud>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void listView1_DragLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.Bisque; 
        }

        private void textBox1_DragLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.Black;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WordsIntuitiveCloud_DataGridView.Rows.Add(new object[] { "err", true, true, false, false, false, true, false });
            // DataGridViewRow err = DataGridViewRow(); err.Cells.
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            var txtb = new TextBoxDoubleClick();
            txtb.MaintabControl = MaintabControl  ;
            //var dgv = (DataGridView)MaintabControl.SelectedTab.Controls[0];
            //if(dgv.RowTemplate == WordsIntuitiveCloud_DataGridView.RowTemplate)
            //{
            //txtb.dataGridfield = dgv; //WordsIntuitiveCloud_DataGridView;
            //}



            Random er = new Random();
            txtb.Text = er.NextDouble().ToString();
            ConceptNet_flowLayoutPanel.Controls.Add(txtb);
        }

        private void toolStripMenuItem_Past_Click(object sender, EventArgs e)
        {
            DataGridViewCell vc = WordsIntuitiveCloud_DataGridView.CurrentCell;
            try
            {
                ((TextBox)vc.Value).Text = (string)Clipboard.GetData("");
            }

            catch (Exception ee) { }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int tv = (int)((trackBar1.Value * 1.00 / trackBar1.Maximum) * 255);
            int err = (int)((trackBar1.Value * 1.00 / trackBar1.Maximum) * 100);
            textBox1.Text = err.ToString();
            DataGridViewCell vc = WordsIntuitiveCloud_DataGridView.CurrentCell;
            DataGridViewCellStyle cs = new DataGridViewCellStyle();
            vc.ToolTipText = err.ToString();
            cs.BackColor = System.Drawing.Color.FromArgb(tv, tv, tv);
            vc.Style = cs;
        }

        private void DGView_Dictionary_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            string err;
            try
            {
                err = WordsIntuitiveCloud_DataGridView.CurrentCell.ToolTipText;

                double er = (int.Parse(err) * 1.00 * trackBar1.Maximum) / 100;

                trackBar1.Value = (int)Math.Round(er);
            }
            catch (Exception ne)
            {
                trackBar1.Value = trackBar1.Maximum;
            }


        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            int err = (int)((trackBar1.Value * 1.00 / trackBar1.Maximum) * 100);
            textBox1.Text = err.ToString();

            // DataGridViewCell vc = DGView_Dictionary.CurrentCell;
            //vc.ToolTipText = err.ToString();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9 || e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) return; // e.Handled = true;
            int er = int.Parse(textBox1.Text);
            if (er > 100) er = 100;
            er = (int)(er * 1.00 * trackBar1.Maximum / 100);
            trackBar1.Value = er;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            /* try 
             {
                 bool cond1 = textBox1.Text.Length > 3;
                 bool cond2 = int.Parse(textBox1.Text) > 100;
                 bool cond3 = !(e.KeyData == Keys.Decimal);
                 if (cond1 && cond2 && cond3)
                     e.SuppressKeyPress = true;
             }
             catch(Exception ee){}*/

        }

        private void DGView_Dictionary_Click(object sender, EventArgs e)
        {
            //DataGridViewCell vc = WordsIntuitiveCloud_DataGridView.CurrentCell;
            //if (vc.ValueType == typeof(CheckBox)) ((CheckBox)vc.Value).Checked = !((CheckBox)vc.Value).Checked;
        }

        private void DGView_Dictionary_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /* DataGridViewCell vc = DGView_Dictionary.CurrentCell;

             if (vc.ColumnIndex>0)
             {
                 DataGridViewCheckBoxCell ckdc = (DataGridViewCheckBoxCell)vc;
                 ckdc.Value = !(bool)ckdc.Value;
             }
             */

        }

        private void Update_button_Click(object sender, EventArgs e)
        {
            WordsIntuitiveCloud_DataGridView.EndEdit();
            for (int i = 0; i < WordsIntuitiveCloud_DataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < WordsIntuitiveCloud_DataGridView.Columns.Count; j++)
                {
                    TextBox txt = new TextBox();
                    if (j == 0)
                    {
                        DataGridViewTextBoxCell vc = WordsIntuitiveCloud_DataGridView.Rows[i].Cells[j] as DataGridViewTextBoxCell;
                        txt.Text = vc.FormattedValue.ToString();
                    }
                    else
                    {
                        DataGridViewCheckBoxCell vc = WordsIntuitiveCloud_DataGridView.Rows[i].Cells[j] as DataGridViewCheckBoxCell;
                        txt.Text = vc.FormattedValue.ToString();

                    }
                    ConceptNet_flowLayoutPanel.Controls.Add(txt);
                }


            }
        }

        private void Lemmabutton_Click(object sender, EventArgs e)
        {
            lemmatextBoxTarget.Text = Utilities.Lemmatizations(LemmatextBox.Text);

            //var er = NWord2Vec.Model.Load(;
            /*var er2 = new Word2vec.Tools.Word2VecTextReader();
            var vocab = er2.Read("");
            vocab.*/
            // Donkey.Word2Vec.Util.        }
            //KPI_ML.NET.
            // ServiceStack..
            //NServiceKit.Text.
        }


        private async void conceptnetbutton2_Click(object sender, EventArgs e)
        {
            List<string> strng = InternetGetLibrary.ConceptNetGetAsync(LemmatextBox.Text, "", 3);
            //strng.Wait();
            //List<string> err = strng.Result;
            foreach (string st in strng)
            {
                TextBoxDoubleClick txtB = new TextBoxDoubleClick();
                txtB.Text = st;
                ConceptNet_flowLayoutPanel.Controls.Add(txtB);
            }
        }

        private void WordNetbutton_Click(object sender, EventArgs e)
        {
            var strngList = LocalGetLibrary.WordnetGetAsync(LemmatextBox.Text, 3);
            ConceptNet_flowLayoutPanel.Controls.Clear();

            foreach (var stnr in strngList)
            {
                TextBoxDoubleClick txtb = new TextBoxDoubleClick();
                txtb.MaintabControl = MaintabControl;
                txtb.Text = stnr;
                ConceptNet_flowLayoutPanel.Controls.Add(txtb);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var strngList = LocalGetLibrary.Glove_Wiki_6b_GetAsync(LemmatextBox.Text, 300);
            ConceptNet_flowLayoutPanel.Controls.Clear();

            foreach (var stnr in strngList)
            {
                TextBoxDoubleClick txtb = new TextBoxDoubleClick();
                txtb.MaintabControl = MaintabControl;
                txtb.Text = stnr;
                ConceptNet_flowLayoutPanel.Controls.Add(txtb);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var strngList = LocalGetLibrary.Glove_CommonCrawl_42b_GetAsync(LemmatextBox.Text, 300);
            ConceptNet_flowLayoutPanel.Controls.Clear();

            foreach (var stnr in strngList)
            {
                TextBoxDoubleClick txtb = new TextBoxDoubleClick();
                txtb.MaintabControl = MaintabControl;
                txtb.Text = stnr;
                ConceptNet_flowLayoutPanel.Controls.Add(txtb);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var strngList = LocalGetLibrary.Glove_Twitter_GetAsync(LemmatextBox.Text, 300);
            ConceptNet_flowLayoutPanel.Controls.Clear();

            foreach (var stnr in strngList)
            {
                TextBoxDoubleClick txtb = new TextBoxDoubleClick();
                txtb.MaintabControl = MaintabControl;
                txtb.Text = stnr;
                ConceptNet_flowLayoutPanel.Controls.Add(txtb);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MaincomboBox.SelectedIndex = 0;

            for (int i = 0; i < 3; i++)
            { 
                WordRelatioWordDB_comboBox1.Items.Add(WordRelatioWordDB_dataGridView1.Columns[i*2].HeaderText);
            }
            WordRelatioWordDB_comboBox1.SelectedIndex = 0;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MaincomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            if (MaincomboBox.SelectedIndex == 4)
            {
                while (i < 4)
                {
                    ((Control)MaintabControl.TabPages[i]).Enabled = true;
                    ++i;
                }

            }

            else
            {

                while (i < MaintabControl.TabCount)
                {
                    if (i == MaincomboBox.SelectedIndex)
                    {
                        ((Control)MaintabControl.TabPages[MaincomboBox.SelectedIndex]).Enabled = true;
                        MaintabControl.SelectedTab = MaintabControl.TabPages[i];
                    }


                    else
                    {
                        ((Control)MaintabControl.TabPages[i]).Enabled = false;
                    }
                    ++i;

                }
            }

        }

        private void MaintabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true; 
            }
        }

        private void MaintabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = MaintabControl.TabPages[e.Index];

            if (!page.Enabled)
            {
                using (SolidBrush brush = new SolidBrush(SystemColors.GrayText))
                {
                    e.Graphics.DrawString(page.Text, page.Font, brush, e.Bounds);
                }
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(page.ForeColor))
                {
                    e.Graphics.DrawString(page.Text, page.Font, brush, e.Bounds);
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Utilities.Glove_Wiki_6b_MLNET("");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Utilities.Glove_Wiki_6b_MLNET("");

            var wrdlst = LocalGetLibrary.ContextAlgoritmFin(LemmatextBox.Text,wordNnetDepth:5,wordnetonly:true);
            foreach(string er in wrdlst)
            {
                TextBoxDoubleClick txtb = new TextBoxDoubleClick();
                txtb.MaintabControl = MaintabControl;
                txtb.Text = er;
                ConceptNet_flowLayoutPanel.Controls.Add(txtb);
            }

            
        }

        int numclick = 0;
        private void chart_wordnet_button_Click(object sender, EventArgs e)
        {
           var words = LocalGetLibrary.ContextAlgoritmFin(LemmatextBox.Text, wordNnetDepth: 5, wordnetonly: true);
           // var arr = Utilities.PlotListString_PCA(words);
            var ponts= new Accord.Math.Random.ZigguratUniformOneGenerator(12);
            //if (numclick==0)
            //{
            //    Chart_WordNet.Titles.Add( "[WordNet] " + LemmatextBox.Text.ToUpper() + ", " + "Rel: IsA & Similar");
            //}
            //else
            //{
                Chart_WordNet.Titles[0].Text = "[WordNet] " + LemmatextBox.Text.ToUpper() + ", " + "Rel: IsA & Similar";
            //}


            Chart_WordNet.Series[0].Points.Clear() ;
            Chart_WordNet.Annotations.Clear();


            for (int ii=0; ii< words.Count; ii++)
            {
                var  pointInst=ponts.Generate(2);
                //var pnt = new System.Drawing.PointF((float)pointInst[0], (float)pointInst[1]);
                
                Chart_WordNet.Series[0].Points.AddXY(pointInst[0], pointInst[1]);
                

                var txtanno = new TextAnnotation();
                txtanno.Text = words[ii];
                txtanno.AnchorDataPoint = Chart_WordNet.Series[0].Points[ii];
                //txtanno.Alignment = ContentAlignment.BottomCenter;


                txtanno.ForeColor = Color.DarkOrange;
                txtanno.Font = new Font("Arial", 9); ;
                txtanno.LineWidth = 2;


                Chart_WordNet.Annotations.Add(txtanno);                               
            }


            var words_conceptnet = LocalGetLibrary.ContextAlgoritmFin(LemmatextBox.Text, conceptnetonly: true, conceptNnetDepth:4);
            // var arr = Utilities.PlotListString_PCA(words);
            var ponts_conceptnet = new Accord.Math.Random.ZigguratUniformOneGenerator(52);

            //if (numclick == 0)
            //{
            //    chart_Conceptnet.Titles.Add( "[ConceptNet] " + LemmatextBox.Text.ToUpper() + ", " + "Rel: IsA & Synonem & Similar");
            //    numclick++;
            //}
            //else
            //{
                chart_Conceptnet.Titles[0].Text = "[ConceptNet] " + LemmatextBox.Text.ToUpper() + ", " + "Rel: IsA & Synonem & Similar";
            chart_Conceptnet.Series[0].Points.Clear();
            chart_Conceptnet.Annotations.Clear();
            //}
            for (int ii = 0; ii < words_conceptnet.Count; ii++)
            {
                var pointInst = ponts_conceptnet.Generate(2);
                //var pnt = new System.Drawing.PointF((float)pointInst[0], (float)pointInst[1]);

                chart_Conceptnet.Series[0].Points.AddXY(pointInst[0], pointInst[1]);


                var txtanno = new TextAnnotation();
                txtanno.Text = words_conceptnet[ii];
                txtanno.AnchorDataPoint = chart_Conceptnet.Series[0].Points[ii];
                //txtanno.Alignment = ContentAlignment.BottomCenter;


                txtanno.ForeColor = Color.DarkGreen;
                txtanno.Font = new Font("Arial", 9); ;
                txtanno.LineWidth = 2;


                chart_Conceptnet.Annotations.Add(txtanno);
            }


            

            //chart_all.Series[1]= 

        }

        int numclick2 = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            var words_all = LocalGetLibrary.ContextAlgoritmFin(LemmatextBox.Text, wordNnetDepth: 5, gloveonly: true);
            // var arr = Utilities.PlotListString_PCA(words);
            var ponts_all = new Accord.Math.Random.ZigguratUniformOneGenerator(152);

            //if (numclick2==0)
            //{
            //    chart_all.Titles.Add("[Glove] " + LemmatextBox.Text.ToUpper() + ", " + "Rel: IsA & Similar");
            //    numclick2++;
            //}
            //else
            //{
                chart_all.Titles[0].Text="[Glove] " + LemmatextBox.Text.ToUpper() + ", " + "Rel: IsA & Similar";
            chart_all.Series[0].Points.Clear();
            chart_all.Annotations.Clear();
            //}

            for (int ii = 0; ii < words_all.Count; ii++)
            {
                var pointInst = ponts_all.Generate(2);
                //var pnt = new System.Drawing.PointF((float)pointInst[0], (float)pointInst[1]);

                chart_all.Series[0].Points.AddXY(pointInst[0], pointInst[1]);


                var txtanno = new TextAnnotation();
                txtanno.Text = words_all[ii];
                txtanno.AnchorDataPoint = chart_all.Series[0].Points[ii];
                //txtanno.Alignment = ContentAlignment.BottomCenter;


                txtanno.ForeColor = Color.Gray;
                txtanno.Font = new Font("Arial", 9); ;
                txtanno.LineWidth = 2;


                chart_all.Annotations.Add(txtanno);
            }

        }

        // private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        // {

        //}

    }
}
