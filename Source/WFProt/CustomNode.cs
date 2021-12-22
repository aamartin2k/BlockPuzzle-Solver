using ControlTreeView;
using System;
using System.Windows.Forms;

namespace WFProt
{
    class BPNode : NodeControl
    {
        // Controles internos

        private Label lbText;
        private Button button;
        private TextBox tBox;

        private bool Editing = false;

        public BPNode(string text)
           : base()
        {
            InitializeComponent();

            lbText.Text = text;
            tBox.Text = text;
        }

        internal Action<int> MakeCurrent;
        internal Action<int, string> Rename;

        internal int Index { get; set; }


        private void InitializeComponent()
        {
            this.lbText = new System.Windows.Forms.Label();
            this.tBox = new System.Windows.Forms.TextBox();
            this.button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.BackColor = System.Drawing.Color.Transparent;
            this.lbText.Location = new System.Drawing.Point(3, 3);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(80, 20);
            this.lbText.TabIndex = 0;
            this.lbText.Text = "lbText";
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbText.DoubleClick += new System.EventHandler(this.LbText_DoubleClick);
            // 
            // tBox
            // 
            this.tBox.Location = new System.Drawing.Point(3, 3);
            this.tBox.Name = "tBox";
            this.tBox.Size = new System.Drawing.Size(75, 20);
            this.tBox.TabIndex = 1;
            this.tBox.Text = "tBox";
            this.tBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBox.KeyUp += TBox_KeyUp;
            this.tBox.KeyDown += TBox_KeyDown;
            this.tBox.LostFocus += TBox_LostFocus;
            this.tBox.Visible = false;
            // 
            // button1
            // 
            this.button.Location = new System.Drawing.Point(3, 23);
            this.button.Name = "button1";
            this.button.Size = new System.Drawing.Size(80, 21);
            this.button.TabIndex = 0;
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += Button1_Click;
            // 
            // BPNode
            // 
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Controls.Add(this.button);
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.tBox);
            this.Name = "BPNode";
            this.Size = new System.Drawing.Size(86, 48);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Event Handlers

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!this.tBox.Visible)
            {
                MakeCurrent(Index);
            }
        }


        private void TBox_LostFocus(object sender, EventArgs e)
        {
            if (!Editing)
            {
                // down text
                this.tBox.Visible = false;
                // up label
                this.lbText.Visible = true;
                // update Text
                this.lbText.Text = this.tBox.Text;
                // Call out
                Rename(Index, this.lbText.Text);
            }

            Editing = false;
            Console.WriteLine("TBox_LostFocus");
        }

        private void TBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Aceptar edit con Enter
            if (e.KeyCode == Keys.Enter)
            {
                this.lbText.Text = this.tBox.Text;
                // Call out
                Rename(Index, this.lbText.Text);
            }
        }

        private void TBox_KeyUp(object sender, KeyEventArgs e)
        {
            // Condiciones de procesar
            // Enter o ESC terminar edicion
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                Editing = true;

                // down text
                this.tBox.Visible = false;

                // up label
                this.lbText.Visible = true;
            }
        }

        private void LbText_DoubleClick(object sender, EventArgs e)
        {

            this.lbText.Visible = false;

            this.tBox.Text = this.lbText.Text;
            this.tBox.Visible = true;

            Console.WriteLine("LbText_DoubleClick");
        }




    }
}
