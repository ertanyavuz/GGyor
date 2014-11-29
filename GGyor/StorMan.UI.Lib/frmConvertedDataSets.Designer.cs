namespace ElektrostilXmlEditor
{
    partial class frmConvertedDataSets
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvDataSetList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvTransforms = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnDuzenle = new System.Windows.Forms.Button();
            this.btnKaldir = new System.Windows.Forms.Button();
            this.btnKategoriDegisimleri = new System.Windows.Forms.Button();
            this.btnGoruntule = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDataSetList
            // 
            this.lvDataSetList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvDataSetList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDataSetList.FullRowSelect = true;
            this.lvDataSetList.GridLines = true;
            this.lvDataSetList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDataSetList.HideSelection = false;
            this.lvDataSetList.Location = new System.Drawing.Point(3, 16);
            this.lvDataSetList.MultiSelect = false;
            this.lvDataSetList.Name = "lvDataSetList";
            this.lvDataSetList.Size = new System.Drawing.Size(278, 235);
            this.lvDataSetList.TabIndex = 0;
            this.lvDataSetList.UseCompatibleStateImageBehavior = false;
            this.lvDataSetList.View = System.Windows.Forms.View.Details;
            this.lvDataSetList.SelectedIndexChanged += new System.EventHandler(this.lvDataSetList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Liste Adı";
            this.columnHeader2.Width = 173;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvDataSetList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 254);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ürün Listeleri";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvTransforms);
            this.groupBox2.Location = new System.Drawing.Point(302, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 254);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dönüşümler";
            // 
            // lvTransforms
            // 
            this.lvTransforms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvTransforms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTransforms.FullRowSelect = true;
            this.lvTransforms.GridLines = true;
            this.lvTransforms.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTransforms.HideSelection = false;
            this.lvTransforms.Location = new System.Drawing.Point(3, 16);
            this.lvTransforms.MultiSelect = false;
            this.lvTransforms.Name = "lvTransforms";
            this.lvTransforms.Size = new System.Drawing.Size(278, 235);
            this.lvTransforms.TabIndex = 0;
            this.lvTransforms.UseCompatibleStateImageBehavior = false;
            this.lvTransforms.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "No";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Dönüşüm Adı";
            this.columnHeader4.Width = 173;
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(592, 51);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(75, 23);
            this.btnEkle.TabIndex = 3;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnDuzenle
            // 
            this.btnDuzenle.Location = new System.Drawing.Point(592, 80);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(75, 23);
            this.btnDuzenle.TabIndex = 4;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.UseVisualStyleBackColor = true;
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);
            // 
            // btnKaldir
            // 
            this.btnKaldir.Location = new System.Drawing.Point(592, 109);
            this.btnKaldir.Name = "btnKaldir";
            this.btnKaldir.Size = new System.Drawing.Size(75, 23);
            this.btnKaldir.TabIndex = 5;
            this.btnKaldir.Text = "Kaldır";
            this.btnKaldir.UseVisualStyleBackColor = true;
            this.btnKaldir.Click += new System.EventHandler(this.btnKaldir_Click);
            // 
            // btnKategoriDegisimleri
            // 
            this.btnKategoriDegisimleri.Location = new System.Drawing.Point(305, 272);
            this.btnKategoriDegisimleri.Name = "btnKategoriDegisimleri";
            this.btnKategoriDegisimleri.Size = new System.Drawing.Size(118, 23);
            this.btnKategoriDegisimleri.TabIndex = 6;
            this.btnKategoriDegisimleri.Text = "Kategori Değişimleri";
            this.btnKategoriDegisimleri.UseVisualStyleBackColor = true;
            this.btnKategoriDegisimleri.Click += new System.EventHandler(this.btnKategoriDegisimleri_Click);
            // 
            // btnGoruntule
            // 
            this.btnGoruntule.Location = new System.Drawing.Point(592, 138);
            this.btnGoruntule.Name = "btnGoruntule";
            this.btnGoruntule.Size = new System.Drawing.Size(75, 23);
            this.btnGoruntule.TabIndex = 7;
            this.btnGoruntule.Text = "Görüntüle";
            this.btnGoruntule.UseVisualStyleBackColor = true;
            this.btnGoruntule.Click += new System.EventHandler(this.btnGoruntule_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(316, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmConvertedDataSets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 470);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGoruntule);
            this.Controls.Add(this.btnKategoriDegisimleri);
            this.Controls.Add(this.btnKaldir);
            this.Controls.Add(this.btnDuzenle);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmConvertedDataSets";
            this.Text = "Ürün Listelerim";
            this.Load += new System.EventHandler(this.frmConvertedDataSets_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvDataSetList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvTransforms;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnDuzenle;
        private System.Windows.Forms.Button btnKaldir;
        private System.Windows.Forms.Button btnKategoriDegisimleri;
        private System.Windows.Forms.Button btnGoruntule;
        private System.Windows.Forms.Button button1;

    }
}