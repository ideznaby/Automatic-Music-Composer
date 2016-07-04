namespace musiccomposerv0._5
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.feels = new System.Windows.Forms.ListBox();
            this.feelingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.musiccomposerDataSet1 = new musiccomposerv0._5.musiccomposerDataSet1();
            this.genre = new System.Windows.Forms.ComboBox();
            this.genresBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.instruments = new System.Windows.Forms.ListBox();
            this.instrumentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.musiccomposerDataSet = new musiccomposerv0._5.musiccomposerDataSet();
            this.instrumentsTableAdapter = new musiccomposerv0._5.musiccomposerDataSetTableAdapters.instrumentsTableAdapter();
            this.musiccomposerDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.feelingsTableAdapter = new musiccomposerv0._5.musiccomposerDataSet1TableAdapters.feelingsTableAdapter();
            this.instrumentsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.genresTableAdapter = new musiccomposerv0._5.musiccomposerDataSet1TableAdapters.genresTableAdapter();
            this.selectedsongs = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.choruspath = new System.Windows.Forms.TextBox();
            this.choruschannel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.feelingsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musiccomposerDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.genresBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.instrumentsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musiccomposerDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musiccomposerDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.instrumentsBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(413, 322);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // feels
            // 
            this.feels.DataSource = this.feelingsBindingSource;
            this.feels.DisplayMember = "feelname";
            this.feels.FormattingEnabled = true;
            this.feels.Location = new System.Drawing.Point(29, 12);
            this.feels.Name = "feels";
            this.feels.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.feels.Size = new System.Drawing.Size(366, 160);
            this.feels.TabIndex = 13;
            // 
            // feelingsBindingSource
            // 
            this.feelingsBindingSource.DataMember = "feelings";
            this.feelingsBindingSource.DataSource = this.musiccomposerDataSet1;
            // 
            // musiccomposerDataSet1
            // 
            this.musiccomposerDataSet1.DataSetName = "musiccomposerDataSet1";
            this.musiccomposerDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // genre
            // 
            this.genre.DataSource = this.genresBindingSource;
            this.genre.DisplayMember = "genrename";
            this.genre.FormattingEnabled = true;
            this.genre.Location = new System.Drawing.Point(432, 77);
            this.genre.Name = "genre";
            this.genre.Size = new System.Drawing.Size(121, 21);
            this.genre.TabIndex = 14;
            // 
            // genresBindingSource
            // 
            this.genresBindingSource.DataMember = "genres";
            this.genresBindingSource.DataSource = this.musiccomposerDataSet1;
            // 
            // instruments
            // 
            this.instruments.DataSource = this.instrumentsBindingSource;
            this.instruments.DisplayMember = "name";
            this.instruments.FormattingEnabled = true;
            this.instruments.Location = new System.Drawing.Point(729, 9);
            this.instruments.Name = "instruments";
            this.instruments.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.instruments.Size = new System.Drawing.Size(274, 615);
            this.instruments.TabIndex = 15;
            // 
            // instrumentsBindingSource
            // 
            this.instrumentsBindingSource.DataMember = "instruments";
            this.instrumentsBindingSource.DataSource = this.musiccomposerDataSet;
            // 
            // musiccomposerDataSet
            // 
            this.musiccomposerDataSet.DataSetName = "musiccomposerDataSet";
            this.musiccomposerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // instrumentsTableAdapter
            // 
            this.instrumentsTableAdapter.ClearBeforeFill = true;
            // 
            // musiccomposerDataSetBindingSource
            // 
            this.musiccomposerDataSetBindingSource.DataSource = this.musiccomposerDataSet;
            this.musiccomposerDataSetBindingSource.Position = 0;
            // 
            // feelingsTableAdapter
            // 
            this.feelingsTableAdapter.ClearBeforeFill = true;
            // 
            // instrumentsBindingSource1
            // 
            this.instrumentsBindingSource1.DataMember = "instruments";
            this.instrumentsBindingSource1.DataSource = this.musiccomposerDataSetBindingSource;
            // 
            // genresTableAdapter
            // 
            this.genresTableAdapter.ClearBeforeFill = true;
            // 
            // selectedsongs
            // 
            this.selectedsongs.FormattingEnabled = true;
            this.selectedsongs.Location = new System.Drawing.Point(53, 208);
            this.selectedsongs.Name = "selectedsongs";
            this.selectedsongs.Size = new System.Drawing.Size(629, 95);
            this.selectedsongs.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "selected songs";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(546, 455);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "chorus finder";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // choruspath
            // 
            this.choruspath.Location = new System.Drawing.Point(305, 455);
            this.choruspath.Name = "choruspath";
            this.choruspath.Size = new System.Drawing.Size(152, 20);
            this.choruspath.TabIndex = 19;
            // 
            // choruschannel
            // 
            this.choruschannel.Location = new System.Drawing.Point(305, 506);
            this.choruschannel.Name = "choruschannel";
            this.choruschannel.Size = new System.Drawing.Size(152, 20);
            this.choruschannel.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 455);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "path :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 509);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "channel :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 636);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.choruschannel);
            this.Controls.Add(this.choruspath);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectedsongs);
            this.Controls.Add(this.instruments);
            this.Controls.Add(this.genre);
            this.Controls.Add(this.feels);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.feelingsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musiccomposerDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.genresBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.instrumentsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musiccomposerDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musiccomposerDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.instrumentsBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox feels;
        private System.Windows.Forms.ComboBox genre;
        private System.Windows.Forms.ListBox instruments;
        private musiccomposerDataSet musiccomposerDataSet;
        private System.Windows.Forms.BindingSource instrumentsBindingSource;
        private musiccomposerDataSetTableAdapters.instrumentsTableAdapter instrumentsTableAdapter;
        private System.Windows.Forms.BindingSource musiccomposerDataSetBindingSource;
        private musiccomposerDataSet1 musiccomposerDataSet1;
        private System.Windows.Forms.BindingSource feelingsBindingSource;
        private musiccomposerDataSet1TableAdapters.feelingsTableAdapter feelingsTableAdapter;
        private System.Windows.Forms.BindingSource instrumentsBindingSource1;
        private System.Windows.Forms.BindingSource genresBindingSource;
        private musiccomposerDataSet1TableAdapters.genresTableAdapter genresTableAdapter;
        private System.Windows.Forms.ListBox selectedsongs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox choruspath;
        private System.Windows.Forms.TextBox choruschannel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

