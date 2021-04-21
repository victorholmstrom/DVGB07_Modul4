
namespace DVGB07_Modul4
{
    partial class AddArticleForm
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
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.newFieldButton = new System.Windows.Forms.Button();
            this.deleteFieldButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(173, 333);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(70, 24);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(97, 333);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(70, 24);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // newFieldButton
            // 
            this.newFieldButton.Location = new System.Drawing.Point(168, 129);
            this.newFieldButton.Name = "newFieldButton";
            this.newFieldButton.Size = new System.Drawing.Size(75, 23);
            this.newFieldButton.TabIndex = 14;
            this.newFieldButton.Text = "New field";
            this.newFieldButton.UseVisualStyleBackColor = true;
            this.newFieldButton.Click += new System.EventHandler(this.newFieldButtonClick);
            // 
            // deleteFieldButton
            // 
            this.deleteFieldButton.Enabled = false;
            this.deleteFieldButton.Location = new System.Drawing.Point(87, 129);
            this.deleteFieldButton.Name = "deleteFieldButton";
            this.deleteFieldButton.Size = new System.Drawing.Size(75, 23);
            this.deleteFieldButton.TabIndex = 15;
            this.deleteFieldButton.Text = "Delete field";
            this.deleteFieldButton.UseVisualStyleBackColor = true;
            this.deleteFieldButton.Click += new System.EventHandler(this.deleteFieldButtonClick);
            // 
            // AddArticleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 369);
            this.Controls.Add(this.deleteFieldButton);
            this.Controls.Add(this.newFieldButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddArticleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add article";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button newFieldButton;
        private System.Windows.Forms.Button deleteFieldButton;
    }
}