namespace IB2Toolset
{
    partial class WeatherEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.lbxWeatherTypeListItems = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lbxWeatherTypeLists = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSortWeathers = new System.Windows.Forms.Button();
            this.btnDuplicateWeather = new System.Windows.Forms.Button();
            this.btnAddWeather = new System.Windows.Forms.Button();
            this.btnRemoveWeather = new System.Windows.Forms.Button();
            this.lbxWeathers = new System.Windows.Forms.ListBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(884, 449);
            this.splitContainer1.SplitterDistance = 582;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.button8);
            this.groupBox3.Controls.Add(this.lbxWeatherTypeListItems);
            this.groupBox3.Location = new System.Drawing.Point(378, 11);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(177, 426);
            this.groupBox3.TabIndex = 92;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "List of WeatherTypeListItems";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(8, 14);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 23);
            this.button5.TabIndex = 91;
            this.button5.Text = "Sort";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnSortWeatherTypeListItems_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(108, 14);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(61, 23);
            this.button6.TabIndex = 87;
            this.button6.Text = "Duplicate";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnDuplicateWeatherTypeListItem_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(8, 37);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(161, 23);
            this.button7.TabIndex = 85;
            this.button7.Text = "Add";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btnAddWeatherTypeListItem_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(48, 14);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(60, 23);
            this.button8.TabIndex = 86;
            this.button8.Text = "Remove";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.btnRemoveWeatherTypeListItem_Click);
            // 
            // lbxWeatherTypeListItems
            // 
            this.lbxWeatherTypeListItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxWeatherTypeListItems.FormattingEnabled = true;
            this.lbxWeatherTypeListItems.Location = new System.Drawing.Point(10, 61);
            this.lbxWeatherTypeListItems.Name = "lbxWeatherTypeListItems";
            this.lbxWeatherTypeListItems.Size = new System.Drawing.Size(157, 329);
            this.lbxWeatherTypeListItems.TabIndex = 82;
            this.lbxWeatherTypeListItems.SelectedIndexChanged += new System.EventHandler(this.lbxWeatherTypeListItems_SelectedIndexChanged_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.lbxWeatherTypeLists);
            this.groupBox2.Location = new System.Drawing.Point(195, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(177, 426);
            this.groupBox2.TabIndex = 92;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List of WeatherTypeLists";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 23);
            this.button1.TabIndex = 91;
            this.button1.Text = "Sort";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSortWeatherTypeLists_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(108, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 23);
            this.button2.TabIndex = 87;
            this.button2.Text = "Duplicate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnDuplicateWeatherTypeList_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 37);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(161, 23);
            this.button3.TabIndex = 85;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnAddWeatherTypeList_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(48, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 23);
            this.button4.TabIndex = 86;
            this.button4.Text = "Remove";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnRemoveWeatherTypeList_Click);
            // 
            // lbxWeatherTypeLists
            // 
            this.lbxWeatherTypeLists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxWeatherTypeLists.FormattingEnabled = true;
            this.lbxWeatherTypeLists.Location = new System.Drawing.Point(10, 61);
            this.lbxWeatherTypeLists.Name = "lbxWeatherTypeLists";
            this.lbxWeatherTypeLists.Size = new System.Drawing.Size(157, 329);
            this.lbxWeatherTypeLists.TabIndex = 82;
            this.lbxWeatherTypeLists.SelectedIndexChanged += new System.EventHandler(this.lbxWeatherTypeLists_SelectedIndexChanged_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnSortWeathers);
            this.groupBox1.Controls.Add(this.btnDuplicateWeather);
            this.groupBox1.Controls.Add(this.btnAddWeather);
            this.groupBox1.Controls.Add(this.btnRemoveWeather);
            this.groupBox1.Controls.Add(this.lbxWeathers);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 426);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of Weathers";
            // 
            // btnSortWeathers
            // 
            this.btnSortWeathers.Location = new System.Drawing.Point(8, 14);
            this.btnSortWeathers.Name = "btnSortWeathers";
            this.btnSortWeathers.Size = new System.Drawing.Size(40, 23);
            this.btnSortWeathers.TabIndex = 91;
            this.btnSortWeathers.Text = "Sort";
            this.btnSortWeathers.UseVisualStyleBackColor = true;
            this.btnSortWeathers.Click += new System.EventHandler(this.btnSortWeathers_Click);
            // 
            // btnDuplicateWeather
            // 
            this.btnDuplicateWeather.Location = new System.Drawing.Point(108, 14);
            this.btnDuplicateWeather.Name = "btnDuplicateWeather";
            this.btnDuplicateWeather.Size = new System.Drawing.Size(61, 23);
            this.btnDuplicateWeather.TabIndex = 87;
            this.btnDuplicateWeather.Text = "Duplicate";
            this.btnDuplicateWeather.UseVisualStyleBackColor = true;
            this.btnDuplicateWeather.Click += new System.EventHandler(this.btnDuplicateWeather_Click);
            // 
            // btnAddWeather
            // 
            this.btnAddWeather.Location = new System.Drawing.Point(8, 37);
            this.btnAddWeather.Name = "btnAddWeather";
            this.btnAddWeather.Size = new System.Drawing.Size(161, 23);
            this.btnAddWeather.TabIndex = 85;
            this.btnAddWeather.Text = "Add";
            this.btnAddWeather.UseVisualStyleBackColor = true;
            this.btnAddWeather.Click += new System.EventHandler(this.btnAddWeather_Click);
            // 
            // btnRemoveWeather
            // 
            this.btnRemoveWeather.Location = new System.Drawing.Point(48, 14);
            this.btnRemoveWeather.Name = "btnRemoveWeather";
            this.btnRemoveWeather.Size = new System.Drawing.Size(60, 23);
            this.btnRemoveWeather.TabIndex = 86;
            this.btnRemoveWeather.Text = "Remove";
            this.btnRemoveWeather.UseVisualStyleBackColor = true;
            this.btnRemoveWeather.Click += new System.EventHandler(this.btnRemoveWeather_Click);
            // 
            // lbxWeathers
            // 
            this.lbxWeathers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxWeathers.FormattingEnabled = true;
            this.lbxWeathers.Location = new System.Drawing.Point(10, 61);
            this.lbxWeathers.Name = "lbxWeathers";
            this.lbxWeathers.Size = new System.Drawing.Size(157, 329);
            this.lbxWeathers.TabIndex = 82;
            this.lbxWeathers.SelectedIndexChanged += new System.EventHandler(this.lbxWeathers_SelectedIndexChanged_1);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(298, 449);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            this.propertyGrid1.Click += new System.EventHandler(this.propertyGrid1_Click);
            // 
            // WeatherEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 14f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 449);
            this.Controls.Add(this.splitContainer1);
            this.Name = "WeatherEditor";
            this.Text = "WeatherEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WeatherEditor_FormClosing);
            this.Load += new System.EventHandler(this.WeatherEditor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDuplicateWeather;
        private System.Windows.Forms.Button btnAddWeather;
        private System.Windows.Forms.Button btnRemoveWeather;
        private System.Windows.Forms.ListBox lbxWeathers;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button btnSortWeathers;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ListBox lbxWeatherTypeListItems;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox lbxWeatherTypeLists;
    }
}