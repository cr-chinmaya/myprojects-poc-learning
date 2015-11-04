namespace JSONViewer
{
    partial class NodeForm
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
            this.panelAddNode = new System.Windows.Forms.Panel();
            this.btnNewNodeSubmit = new System.Windows.Forms.Button();
            this.lblText = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtNodeText = new System.Windows.Forms.TextBox();
            this.txtNodeName = new System.Windows.Forms.TextBox();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.btnRemoveNode = new System.Windows.Forms.Button();
            this.panelAddNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAddNode
            // 
            this.panelAddNode.Controls.Add(this.btnNewNodeSubmit);
            this.panelAddNode.Controls.Add(this.lblText);
            this.panelAddNode.Controls.Add(this.lblName);
            this.panelAddNode.Controls.Add(this.txtNodeText);
            this.panelAddNode.Controls.Add(this.txtNodeName);
            this.panelAddNode.Location = new System.Drawing.Point(12, 65);
            this.panelAddNode.Name = "panelAddNode";
            this.panelAddNode.Size = new System.Drawing.Size(284, 184);
            this.panelAddNode.TabIndex = 7;
            this.panelAddNode.Visible = false;
            // 
            // btnNewNodeSubmit
            // 
            this.btnNewNodeSubmit.Location = new System.Drawing.Point(193, 146);
            this.btnNewNodeSubmit.Name = "btnNewNodeSubmit";
            this.btnNewNodeSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnNewNodeSubmit.TabIndex = 9;
            this.btnNewNodeSubmit.Text = "Submit";
            this.btnNewNodeSubmit.UseVisualStyleBackColor = true;
            this.btnNewNodeSubmit.Click += new System.EventHandler(this.btnNewNodeSubmit_Click);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(9, 55);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(31, 13);
            this.lblText.TabIndex = 8;
            this.lblText.Text = "Text:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(9, 24);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 13);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "Name: ";
            // 
            // txtNodeText
            // 
            this.txtNodeText.Location = new System.Drawing.Point(50, 55);
            this.txtNodeText.Multiline = true;
            this.txtNodeText.Name = "txtNodeText";
            this.txtNodeText.Size = new System.Drawing.Size(218, 85);
            this.txtNodeText.TabIndex = 6;
            // 
            // txtNodeName
            // 
            this.txtNodeName.Location = new System.Drawing.Point(50, 21);
            this.txtNodeName.Name = "txtNodeName";
            this.txtNodeName.Size = new System.Drawing.Size(217, 20);
            this.txtNodeName.TabIndex = 5;
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(74, 13);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(75, 23);
            this.btnAddNode.TabIndex = 8;
            this.btnAddNode.Text = "Add Node";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // btnRemoveNode
            // 
            this.btnRemoveNode.Location = new System.Drawing.Point(155, 13);
            this.btnRemoveNode.Name = "btnRemoveNode";
            this.btnRemoveNode.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveNode.TabIndex = 9;
            this.btnRemoveNode.Text = "Remove Node";
            this.btnRemoveNode.UseVisualStyleBackColor = true;
            this.btnRemoveNode.Click += new System.EventHandler(this.btnRemoveNode_Click);
            // 
            // NewNodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 261);
            this.Controls.Add(this.btnRemoveNode);
            this.Controls.Add(this.btnAddNode);
            this.Controls.Add(this.panelAddNode);
            this.Name = "NewNodeForm";
            this.Text = "NewNodeForm";
            this.panelAddNode.ResumeLayout(false);
            this.panelAddNode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelAddNode;
        private System.Windows.Forms.Button btnNewNodeSubmit;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtNodeText;
        private System.Windows.Forms.TextBox txtNodeName;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.Button btnRemoveNode;
    }
}