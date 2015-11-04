﻿namespace JSONViewer
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
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.panelInput = new System.Windows.Forms.Panel();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.treeViewOutput = new System.Windows.Forms.TreeView();
            this.btnJsonToTree = new System.Windows.Forms.Button();
            this.btnTreeToJson = new System.Windows.Forms.Button();
            this.btnFormat = new System.Windows.Forms.Button();
            this.btnRemoveWhiteSpace = new System.Windows.Forms.Button();
            this.btnExpand = new System.Windows.Forms.Button();
            this.btnCollapse = new System.Windows.Forms.Button();
            this.panelInput.SuspendLayout();
            this.panelOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(13, 60);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(578, 432);
            this.txtInput.TabIndex = 0;
            this.txtInput.Text = "";
            // 
            // panelInput
            // 
            this.panelInput.Controls.Add(this.btnRemoveWhiteSpace);
            this.panelInput.Controls.Add(this.btnFormat);
            this.panelInput.Location = new System.Drawing.Point(13, 13);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(578, 41);
            this.panelInput.TabIndex = 1;
            // 
            // panelOutput
            // 
            this.panelOutput.Controls.Add(this.btnCollapse);
            this.panelOutput.Controls.Add(this.btnExpand);
            this.panelOutput.Location = new System.Drawing.Point(670, 13);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(578, 41);
            this.panelOutput.TabIndex = 2;
            // 
            // treeViewOutput
            // 
            this.treeViewOutput.LabelEdit = true;
            this.treeViewOutput.Location = new System.Drawing.Point(670, 61);
            this.treeViewOutput.Name = "treeViewOutput";
            this.treeViewOutput.Size = new System.Drawing.Size(578, 431);
            this.treeViewOutput.TabIndex = 3;
            // 
            // btnJsonToTree
            // 
            this.btnJsonToTree.Location = new System.Drawing.Point(598, 61);
            this.btnJsonToTree.Name = "btnJsonToTree";
            this.btnJsonToTree.Size = new System.Drawing.Size(66, 42);
            this.btnJsonToTree.TabIndex = 4;
            this.btnJsonToTree.Text = ">";
            this.btnJsonToTree.UseVisualStyleBackColor = true;
            this.btnJsonToTree.Click += new System.EventHandler(this.btnJsonToTree_Click);
            // 
            // btnTreeToJson
            // 
            this.btnTreeToJson.Location = new System.Drawing.Point(598, 122);
            this.btnTreeToJson.Name = "btnTreeToJson";
            this.btnTreeToJson.Size = new System.Drawing.Size(66, 42);
            this.btnTreeToJson.TabIndex = 5;
            this.btnTreeToJson.Text = "<";
            this.btnTreeToJson.UseVisualStyleBackColor = true;
            this.btnTreeToJson.Click += new System.EventHandler(this.btnTreeToJson_Click);
            // 
            // btnFormat
            // 
            this.btnFormat.Location = new System.Drawing.Point(3, 3);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(130, 35);
            this.btnFormat.TabIndex = 6;
            this.btnFormat.Text = "Format";
            this.btnFormat.UseVisualStyleBackColor = true;
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // btnRemoveWhiteSpace
            // 
            this.btnRemoveWhiteSpace.Location = new System.Drawing.Point(139, 3);
            this.btnRemoveWhiteSpace.Name = "btnRemoveWhiteSpace";
            this.btnRemoveWhiteSpace.Size = new System.Drawing.Size(130, 35);
            this.btnRemoveWhiteSpace.TabIndex = 7;
            this.btnRemoveWhiteSpace.Text = "Remove White Space";
            this.btnRemoveWhiteSpace.UseVisualStyleBackColor = true;
            this.btnRemoveWhiteSpace.Click += new System.EventHandler(this.btnRemoveWhiteSpace_Click);
            // 
            // btnExpand
            // 
            this.btnExpand.Location = new System.Drawing.Point(3, 3);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(130, 35);
            this.btnExpand.TabIndex = 8;
            this.btnExpand.Text = "Expand";
            this.btnExpand.UseVisualStyleBackColor = true;
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // btnCollapse
            // 
            this.btnCollapse.Location = new System.Drawing.Point(139, 3);
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(130, 35);
            this.btnCollapse.TabIndex = 9;
            this.btnCollapse.Text = "Collapse";
            this.btnCollapse.UseVisualStyleBackColor = true;
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 504);
            this.Controls.Add(this.btnTreeToJson);
            this.Controls.Add(this.btnJsonToTree);
            this.Controls.Add(this.treeViewOutput);
            this.Controls.Add(this.panelOutput);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.txtInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "JSON Viewer (v1.0)";
            this.panelInput.ResumeLayout(false);
            this.panelOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtInput;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Button btnRemoveWhiteSpace;
        private System.Windows.Forms.Button btnFormat;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.Button btnCollapse;
        private System.Windows.Forms.Button btnExpand;
        private System.Windows.Forms.TreeView treeViewOutput;
        private System.Windows.Forms.Button btnJsonToTree;
        private System.Windows.Forms.Button btnTreeToJson;
    }
}

