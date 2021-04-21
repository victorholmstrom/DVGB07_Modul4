
namespace DVGB07_Modul4
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.StoreTab = new System.Windows.Forms.TabPage();
            this.WarehouseTab = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.StoreTab);
            this.tabControl1.Controls.Add(this.WarehouseTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(774, 421);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // StoreTab
            // 
            this.StoreTab.Location = new System.Drawing.Point(4, 22);
            this.StoreTab.Name = "StoreTab";
            this.StoreTab.Padding = new System.Windows.Forms.Padding(3);
            this.StoreTab.Size = new System.Drawing.Size(766, 395);
            this.StoreTab.TabIndex = 0;
            this.StoreTab.Text = "Shop view";
            this.StoreTab.UseVisualStyleBackColor = true;
            // 
            // WarehouseTab
            // 
            this.WarehouseTab.Location = new System.Drawing.Point(4, 22);
            this.WarehouseTab.Name = "WarehouseTab";
            this.WarehouseTab.Padding = new System.Windows.Forms.Padding(3);
            this.WarehouseTab.Size = new System.Drawing.Size(766, 395);
            this.WarehouseTab.TabIndex = 1;
            this.WarehouseTab.Text = "Warehouse View";
            this.WarehouseTab.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(774, 421);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(790, 460);
            this.Name = "MainForm";
            this.Text = "Business System";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage StoreTab;
        private System.Windows.Forms.TabPage WarehouseTab;
    }
}

