namespace ListUsbSerials
{
    partial class Application
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Application));
            this.portList = new System.Windows.Forms.ListView();
            this.Device = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Serial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Manufacturer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.showDisconnected = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // portList
            // 
            this.portList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.portList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Device,
            this.VID,
            this.PID,
            this.Serial,
            this.Manufacturer,
            this.Description});
            this.portList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portList.FullRowSelect = true;
            this.portList.GridLines = true;
            this.portList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.portList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.portList.Location = new System.Drawing.Point(3, 3);
            this.portList.MultiSelect = false;
            this.portList.Name = "portList";
            this.portList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.portList.Size = new System.Drawing.Size(647, 376);
            this.portList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.portList.TabIndex = 0;
            this.portList.UseCompatibleStateImageBehavior = false;
            this.portList.View = System.Windows.Forms.View.Details;
            this.portList.SelectedIndexChanged += new System.EventHandler(this.PortList_SelectedIndexChanged);
            this.portList.DoubleClick += new System.EventHandler(this.PortList_DoubleClick);
            this.portList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PortList_MouseClick);
            // 
            // Device
            // 
            this.Device.Text = "Device";
            this.Device.Width = 75;
            // 
            // VID
            // 
            this.VID.Text = "VID";
            this.VID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VID.Width = 41;
            // 
            // PID
            // 
            this.PID.Text = "PID";
            this.PID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PID.Width = 38;
            // 
            // Serial
            // 
            this.Serial.Text = "Serial";
            this.Serial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Serial.Width = 122;
            // 
            // Manufacturer
            // 
            this.Manufacturer.Text = "Manufacturer";
            this.Manufacturer.Width = 147;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 220;
            // 
            // showDisconnected
            // 
            this.showDisconnected.AutoSize = true;
            this.showDisconnected.Location = new System.Drawing.Point(12, 3);
            this.showDisconnected.Name = "showDisconnected";
            this.showDisconnected.Size = new System.Drawing.Size(160, 17);
            this.showDisconnected.TabIndex = 2;
            this.showDisconnected.Text = "Show disconnected devices";
            this.showDisconnected.UseVisualStyleBackColor = true;
            this.showDisconnected.CheckedChanged += new System.EventHandler(this.RefreshNeeded);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.portList);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.showDisconnected);
            this.splitContainer1.Size = new System.Drawing.Size(653, 416);
            this.splitContainer1.SplitterDistance = 382;
            this.splitContainer1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(514, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Configure Terminal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.TerminalSetup_Click);
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 416);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Application";
            this.Text = "ListUSBSerials";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView portList;
        private System.Windows.Forms.ColumnHeader Device;
        private System.Windows.Forms.ColumnHeader VID;
        private System.Windows.Forms.ColumnHeader PID;
        private System.Windows.Forms.ColumnHeader Serial;
        private System.Windows.Forms.ColumnHeader Manufacturer;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.CheckBox showDisconnected;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
    }
}

