namespace MyFace
{
    partial class frmMyFace
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgPrincipal = new System.Windows.Forms.PictureBox();
            this.imgResult = new System.Windows.Forms.PictureBox();
            this.txtCaminho = new System.Windows.Forms.TextBox();
            this.btnCapt = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.imagemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webCAMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ligarWebCamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssPixelCompativel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssCompativel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsssInCompativel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssIncompativel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTaxaPixel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnWebCam = new System.Windows.Forms.Button();
            this.picResultTrain = new System.Windows.Forms.PictureBox();
            this.btnTrain = new System.Windows.Forms.Button();
            this.imgCompativel = new System.Windows.Forms.PictureBox();
            this.imgIncompativel = new System.Windows.Forms.PictureBox();
            this.desligarWebCamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limparToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.imgPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgResult)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResultTrain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCompativel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIncompativel)).BeginInit();
            this.SuspendLayout();
            // 
            // imgPrincipal
            // 
            this.imgPrincipal.BackColor = System.Drawing.SystemColors.Window;
            this.imgPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgPrincipal.Location = new System.Drawing.Point(17, 88);
            this.imgPrincipal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.imgPrincipal.Name = "imgPrincipal";
            this.imgPrincipal.Size = new System.Drawing.Size(738, 544);
            this.imgPrincipal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgPrincipal.TabIndex = 0;
            this.imgPrincipal.TabStop = false;
            // 
            // imgResult
            // 
            this.imgResult.BackColor = System.Drawing.SystemColors.Window;
            this.imgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgResult.Location = new System.Drawing.Point(997, 93);
            this.imgResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.imgResult.Name = "imgResult";
            this.imgResult.Size = new System.Drawing.Size(219, 264);
            this.imgResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgResult.TabIndex = 1;
            this.imgResult.TabStop = false;
            // 
            // txtCaminho
            // 
            this.txtCaminho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCaminho.Location = new System.Drawing.Point(17, 45);
            this.txtCaminho.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCaminho.Name = "txtCaminho";
            this.txtCaminho.ReadOnly = true;
            this.txtCaminho.Size = new System.Drawing.Size(855, 31);
            this.txtCaminho.TabIndex = 3;
            this.txtCaminho.Click += new System.EventHandler(this.txtCaminho_Click);
            // 
            // btnCapt
            // 
            this.btnCapt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapt.Location = new System.Drawing.Point(993, 45);
            this.btnCapt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCapt.Name = "btnCapt";
            this.btnCapt.Size = new System.Drawing.Size(107, 38);
            this.btnCapt.TabIndex = 4;
            this.btnCapt.Text = "CAPTURTAR";
            this.btnCapt.UseVisualStyleBackColor = true;
            this.btnCapt.Click += new System.EventHandler(this.btnCapt_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imagemToolStripMenuItem,
            this.webCAMToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1233, 35);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // imagemToolStripMenuItem
            // 
            this.imagemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.limparToolStripMenuItem});
            this.imagemToolStripMenuItem.Name = "imagemToolStripMenuItem";
            this.imagemToolStripMenuItem.Size = new System.Drawing.Size(94, 29);
            this.imagemToolStripMenuItem.Text = "Imagem";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.abrirToolStripMenuItem.Text = "Abrir";
            // 
            // webCAMToolStripMenuItem
            // 
            this.webCAMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ligarWebCamToolStripMenuItem,
            this.desligarWebCamToolStripMenuItem});
            this.webCAMToolStripMenuItem.Name = "webCAMToolStripMenuItem";
            this.webCAMToolStripMenuItem.Size = new System.Drawing.Size(125, 29);
            this.webCAMToolStripMenuItem.Text = "Ferramentas";
            // 
            // ligarWebCamToolStripMenuItem
            // 
            this.ligarWebCamToolStripMenuItem.Name = "ligarWebCamToolStripMenuItem";
            this.ligarWebCamToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.ligarWebCamToolStripMenuItem.Text = "Ligar WebCam";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssPixelCompativel,
            this.tssCompativel,
            this.tsssInCompativel,
            this.tssIncompativel,
            this.toolStripStatusLabel1,
            this.tssTaxaPixel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 673);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 20, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1233, 32);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssPixelCompativel
            // 
            this.tssPixelCompativel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tssPixelCompativel.Name = "tssPixelCompativel";
            this.tssPixelCompativel.Size = new System.Drawing.Size(159, 25);
            this.tssPixelCompativel.Text = "Pixel Compativel:";
            // 
            // tssCompativel
            // 
            this.tssCompativel.Name = "tssCompativel";
            this.tssCompativel.Size = new System.Drawing.Size(22, 25);
            this.tssCompativel.Text = "0";
            // 
            // tsssInCompativel
            // 
            this.tsssInCompativel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tsssInCompativel.Name = "tsssInCompativel";
            this.tsssInCompativel.Size = new System.Drawing.Size(174, 25);
            this.tsssInCompativel.Text = "Pixel Incompativel:";
            // 
            // tssIncompativel
            // 
            this.tssIncompativel.Name = "tssIncompativel";
            this.tssIncompativel.Size = new System.Drawing.Size(22, 25);
            this.tssIncompativel.Text = "0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(128, 25);
            this.toolStripStatusLabel1.Text = "Taxa de Pixel:";
            // 
            // tssTaxaPixel
            // 
            this.tssTaxaPixel.Name = "tssTaxaPixel";
            this.tssTaxaPixel.Size = new System.Drawing.Size(22, 25);
            this.tssTaxaPixel.Text = "0";
            // 
            // btnWebCam
            // 
            this.btnWebCam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWebCam.Location = new System.Drawing.Point(883, 45);
            this.btnWebCam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnWebCam.Name = "btnWebCam";
            this.btnWebCam.Size = new System.Drawing.Size(104, 38);
            this.btnWebCam.TabIndex = 7;
            this.btnWebCam.Text = "WEBCAM";
            this.btnWebCam.UseVisualStyleBackColor = true;
            this.btnWebCam.Click += new System.EventHandler(this.btnWebCam_Click);
            // 
            // picResultTrain
            // 
            this.picResultTrain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.picResultTrain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picResultTrain.Location = new System.Drawing.Point(769, 93);
            this.picResultTrain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picResultTrain.Name = "picResultTrain";
            this.picResultTrain.Size = new System.Drawing.Size(219, 264);
            this.picResultTrain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picResultTrain.TabIndex = 8;
            this.picResultTrain.TabStop = false;
            // 
            // btnTrain
            // 
            this.btnTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrain.Location = new System.Drawing.Point(1109, 45);
            this.btnTrain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(107, 38);
            this.btnTrain.TabIndex = 9;
            this.btnTrain.Text = "TRAIN";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.button1_Click);
            // 
            // imgCompativel
            // 
            this.imgCompativel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.imgCompativel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgCompativel.Location = new System.Drawing.Point(769, 368);
            this.imgCompativel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.imgCompativel.Name = "imgCompativel";
            this.imgCompativel.Size = new System.Drawing.Size(219, 264);
            this.imgCompativel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCompativel.TabIndex = 10;
            this.imgCompativel.TabStop = false;
            // 
            // imgIncompativel
            // 
            this.imgIncompativel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.imgIncompativel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgIncompativel.Location = new System.Drawing.Point(997, 368);
            this.imgIncompativel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.imgIncompativel.Name = "imgIncompativel";
            this.imgIncompativel.Size = new System.Drawing.Size(219, 264);
            this.imgIncompativel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgIncompativel.TabIndex = 11;
            this.imgIncompativel.TabStop = false;
            // 
            // desligarWebCamToolStripMenuItem
            // 
            this.desligarWebCamToolStripMenuItem.Name = "desligarWebCamToolStripMenuItem";
            this.desligarWebCamToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.desligarWebCamToolStripMenuItem.Text = "Desligar WebCam";
            // 
            // limparToolStripMenuItem
            // 
            this.limparToolStripMenuItem.Name = "limparToolStripMenuItem";
            this.limparToolStripMenuItem.Size = new System.Drawing.Size(287, 34);
            this.limparToolStripMenuItem.Text = "Limpar TrainedImages";
            this.limparToolStripMenuItem.Click += new System.EventHandler(this.limparToolStripMenuItem_Click);
            // 
            // frmMyFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 705);
            this.Controls.Add(this.imgIncompativel);
            this.Controls.Add(this.imgCompativel);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.picResultTrain);
            this.Controls.Add(this.btnWebCam);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCapt);
            this.Controls.Add(this.txtCaminho);
            this.Controls.Add(this.imgResult);
            this.Controls.Add(this.imgPrincipal);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(22, 649);
            this.Name = "frmMyFace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My FACE [DEV]";
            ((System.ComponentModel.ISupportInitialize)(this.imgPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgResult)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResultTrain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCompativel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIncompativel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox imgPrincipal;
        private PictureBox pictureBox2;
        private TextBox txtCaminho;
        private Button btnCapt;
        private PictureBox imgCap;
        private PictureBox imgResult;
        private MenuStrip menuStrip1;
        private StatusStrip statusStrip1;
        private Button btnWebCam;
        private ToolStripMenuItem imagemToolStripMenuItem;
        private ToolStripMenuItem webCAMToolStripMenuItem;
        private ToolStripMenuItem abrirToolStripMenuItem;
        private ToolStripMenuItem ligarWebCamToolStripMenuItem;
        private PictureBox pictureBox1;
        private Button btnTrain;
        private PictureBox picResultTrain;
        private PictureBox picSalvo;
        private PictureBox imgCompativel;
        private PictureBox imgIncompativel;
        private ToolStripStatusLabel tssPixelCompativel;
        private ToolStripStatusLabel tssCompativel;
        private ToolStripStatusLabel tsssInCompativel;
        private ToolStripStatusLabel tssIncompativel;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel tssTaxaPixel;
        private ToolStripMenuItem limparToolStripMenuItem;
        private ToolStripMenuItem desligarWebCamToolStripMenuItem;
    }
}