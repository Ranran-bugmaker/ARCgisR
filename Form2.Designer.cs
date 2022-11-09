
namespace _10._12.arcgis1
{
    partial class Form2
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
            this.comboBoxLayerName = new System.Windows.Forms.ComboBox();
            this.comboBoxSelectMethod = new System.Windows.Forms.ComboBox();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.wheretxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonGetUniqeValue = new System.Windows.Forms.Button();
            this.listBoxValues = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxLayerName
            // 
            this.comboBoxLayerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayerName.FormattingEnabled = true;
            this.comboBoxLayerName.Location = new System.Drawing.Point(207, 25);
            this.comboBoxLayerName.Name = "comboBoxLayerName";
            this.comboBoxLayerName.Size = new System.Drawing.Size(537, 23);
            this.comboBoxLayerName.TabIndex = 0;
            this.comboBoxLayerName.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayerName_SelectedIndexChanged);
            // 
            // comboBoxSelectMethod
            // 
            this.comboBoxSelectMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectMethod.FormattingEnabled = true;
            this.comboBoxSelectMethod.Items.AddRange(new object[] {
            "新建选择集",
            "添加到当前选择集",
            "从当前选择集中删除",
            "从当前选择集中选择"});
            this.comboBoxSelectMethod.Location = new System.Drawing.Point(207, 69);
            this.comboBoxSelectMethod.Name = "comboBoxSelectMethod";
            this.comboBoxSelectMethod.Size = new System.Drawing.Size(537, 23);
            this.comboBoxSelectMethod.TabIndex = 1;
            // 
            // listBoxFields
            // 
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.ItemHeight = 15;
            this.listBoxFields.Location = new System.Drawing.Point(34, 143);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(182, 244);
            this.listBoxFields.TabIndex = 2;
            this.listBoxFields.SelectedIndexChanged += new System.EventHandler(this.listBoxFields_SelectedIndexChanged);
            this.listBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseDoubleClick);
            // 
            // wheretxt
            // 
            this.wheretxt.Location = new System.Drawing.Point(34, 393);
            this.wheretxt.Multiline = true;
            this.wheretxt.Name = "wheretxt";
            this.wheretxt.Size = new System.Drawing.Size(523, 127);
            this.wheretxt.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 526);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(267, 526);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(728, 526);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "列表显示";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(624, 526);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "开始查询";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonGetUniqeValue
            // 
            this.buttonGetUniqeValue.Location = new System.Drawing.Point(749, 231);
            this.buttonGetUniqeValue.Name = "buttonGetUniqeValue";
            this.buttonGetUniqeValue.Size = new System.Drawing.Size(75, 23);
            this.buttonGetUniqeValue.TabIndex = 8;
            this.buttonGetUniqeValue.Text = "获取唯一值";
            this.buttonGetUniqeValue.UseVisualStyleBackColor = true;
            this.buttonGetUniqeValue.Click += new System.EventHandler(this.buttonGetUniqeValue_Click);
            // 
            // listBoxValues
            // 
            this.listBoxValues.FormattingEnabled = true;
            this.listBoxValues.ItemHeight = 15;
            this.listBoxValues.Location = new System.Drawing.Point(538, 143);
            this.listBoxValues.Name = "listBoxValues";
            this.listBoxValues.Size = new System.Drawing.Size(205, 244);
            this.listBoxValues.TabIndex = 9;
            this.listBoxValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxValues_MouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(862, 625);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.comboBoxLayerName);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.buttonGetUniqeValue);
            this.tabPage1.Controls.Add(this.listBoxValues);
            this.tabPage1.Controls.Add(this.comboBoxSelectMethod);
            this.tabPage1.Controls.Add(this.listBoxFields);
            this.tabPage1.Controls.Add(this.wheretxt);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(854, 596);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "矩形",
            "圆形",
            "多边形",
            "无"});
            this.comboBox1.Location = new System.Drawing.Point(622, 414);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(181, 23);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(624, 487);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(179, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "设定空间范围";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(854, 598);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(_10._12.arcgis1.Form1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(848, 592);
            this.dataGridView1.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 625);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form2";
            this.Text = "属性查询";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.ComboBox comboBoxLayerName;
        private System.Windows.Forms.ComboBox comboBoxSelectMethod;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.TextBox wheretxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonGetUniqeValue;
        private System.Windows.Forms.ListBox listBoxValues;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}