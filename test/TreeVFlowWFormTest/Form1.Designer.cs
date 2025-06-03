using System.Windows.Forms;

namespace TreeVFlowWFormTest
{
    partial class Form1
    {

        #region Windows Form Designer generated code
        private TreeVFlowControl.Imp.TreeVFlowControl _treeVFlowControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button addTreeNodeButton;
        private System.Windows.Forms.Button removeTreeNodeButton;
        private System.Windows.Forms.Button addContentButton;
        private System.Windows.Forms.Button removeContentButton;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._treeVFlowControl1 = new TreeVFlowControl.Imp.TreeVFlowControl();
            this.button11 = new System.Windows.Forms.Button();
            this.activeTreeNodeToggleVisibleButton = new System.Windows.Forms.Button();
            this.activeContentToggleVisibleButton = new System.Windows.Forms.Button();
            this.activeTreeNodeFooterToggleVisibleButton = new System.Windows.Forms.Button();
            this.activeTreeNodeHeaderToggleVisibleButton = new System.Windows.Forms.Button();
            this.showActiveContentButton = new System.Windows.Forms.Button();
            this.showActiveTreeNodeButton = new System.Windows.Forms.Button();
            this.lblContentNode = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTreeNode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.removeContentButton = new System.Windows.Forms.Button();
            this.addContentButton = new System.Windows.Forms.Button();
            this.removeTreeNodeButton = new System.Windows.Forms.Button();
            this.addTreeNodeButton = new System.Windows.Forms.Button();
            this.rootNodeActiveTreeNodeButton1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this._treeVFlowControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rootNodeActiveTreeNodeButton1);
            this.splitContainer1.Panel2.Controls.Add(this.button11);
            this.splitContainer1.Panel2.Controls.Add(this.activeTreeNodeToggleVisibleButton);
            this.splitContainer1.Panel2.Controls.Add(this.activeContentToggleVisibleButton);
            this.splitContainer1.Panel2.Controls.Add(this.activeTreeNodeFooterToggleVisibleButton);
            this.splitContainer1.Panel2.Controls.Add(this.activeTreeNodeHeaderToggleVisibleButton);
            this.splitContainer1.Panel2.Controls.Add(this.showActiveContentButton);
            this.splitContainer1.Panel2.Controls.Add(this.showActiveTreeNodeButton);
            this.splitContainer1.Panel2.Controls.Add(this.lblContentNode);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.lblTreeNode);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.removeContentButton);
            this.splitContainer1.Panel2.Controls.Add(this.addContentButton);
            this.splitContainer1.Panel2.Controls.Add(this.removeTreeNodeButton);
            this.splitContainer1.Panel2.Controls.Add(this.addTreeNodeButton);
            this.splitContainer1.Size = new System.Drawing.Size(1056, 736);
            this.splitContainer1.SplitterDistance = 341;
            this.splitContainer1.TabIndex = 1;
            // 
            // _treeVFlowControl1
            // 
            this._treeVFlowControl1.AutoScroll = true;
            this._treeVFlowControl1.AutoSize = true;
            this._treeVFlowControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._treeVFlowControl1.BackColor = System.Drawing.Color.Azure;
            this._treeVFlowControl1.Location = new System.Drawing.Point(0, 0);
            this._treeVFlowControl1.Name = "_treeVFlowControl1";
            this._treeVFlowControl1.Size = new System.Drawing.Size(344, 3);
            this._treeVFlowControl1.TabIndex = 0;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(61, 561);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(177, 44);
            this.button11.TabIndex = 14;
            this.button11.Text = "AddTreeNode";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // activeTreeNodeToggleVisibleButton
            // 
            this.activeTreeNodeToggleVisibleButton.Location = new System.Drawing.Point(61, 317);
            this.activeTreeNodeToggleVisibleButton.Name = "activeTreeNodeToggleVisibleButton";
            this.activeTreeNodeToggleVisibleButton.Size = new System.Drawing.Size(194, 44);
            this.activeTreeNodeToggleVisibleButton.TabIndex = 13;
            this.activeTreeNodeToggleVisibleButton.Text = "ActiveTreeNode Toggle Visible";
            this.activeTreeNodeToggleVisibleButton.UseVisualStyleBackColor = true;
            this.activeTreeNodeToggleVisibleButton.Click += new System.EventHandler(this.activeTreeNodeToggleVisibleButton_Click);
            // 
            // activeContentToggleVisibleButton
            // 
            this.activeContentToggleVisibleButton.Location = new System.Drawing.Point(254, 474);
            this.activeContentToggleVisibleButton.Name = "activeContentToggleVisibleButton";
            this.activeContentToggleVisibleButton.Size = new System.Drawing.Size(194, 44);
            this.activeContentToggleVisibleButton.TabIndex = 12;
            this.activeContentToggleVisibleButton.Text = "ActiveContent Toggle Visible";
            this.activeContentToggleVisibleButton.UseVisualStyleBackColor = true;
            this.activeContentToggleVisibleButton.Click += new System.EventHandler(this.activeContentVisibleToggleButton_Click);
            // 
            // activeTreeNodeFooterToggleVisibleButton
            // 
            this.activeTreeNodeFooterToggleVisibleButton.Location = new System.Drawing.Point(61, 417);
            this.activeTreeNodeFooterToggleVisibleButton.Name = "activeTreeNodeFooterToggleVisibleButton";
            this.activeTreeNodeFooterToggleVisibleButton.Size = new System.Drawing.Size(194, 44);
            this.activeTreeNodeFooterToggleVisibleButton.TabIndex = 11;
            this.activeTreeNodeFooterToggleVisibleButton.Text = "ActiveTreeNode Footer Toggle Visible";
            this.activeTreeNodeFooterToggleVisibleButton.UseVisualStyleBackColor = true;
            this.activeTreeNodeFooterToggleVisibleButton.Click += new System.EventHandler(this.activeTreeNodeFooterVisibleToggleButton_Click);
            // 
            // activeTreeNodeHeaderToggleVisibleButton
            // 
            this.activeTreeNodeHeaderToggleVisibleButton.Location = new System.Drawing.Point(61, 367);
            this.activeTreeNodeHeaderToggleVisibleButton.Name = "activeTreeNodeHeaderToggleVisibleButton";
            this.activeTreeNodeHeaderToggleVisibleButton.Size = new System.Drawing.Size(194, 44);
            this.activeTreeNodeHeaderToggleVisibleButton.TabIndex = 10;
            this.activeTreeNodeHeaderToggleVisibleButton.Text = "ActiveTreeNode Header Toggle Visible";
            this.activeTreeNodeHeaderToggleVisibleButton.UseVisualStyleBackColor = true;
            this.activeTreeNodeHeaderToggleVisibleButton.Click += new System.EventHandler(this.activeTreeNodeHeaderVisibleToggleButton_Click);
            // 
            // showActiveContentButton
            // 
            this.showActiveContentButton.Location = new System.Drawing.Point(254, 123);
            this.showActiveContentButton.Name = "showActiveContentButton";
            this.showActiveContentButton.Size = new System.Drawing.Size(194, 44);
            this.showActiveContentButton.TabIndex = 9;
            this.showActiveContentButton.Text = "Show Active Content\r\n";
            this.showActiveContentButton.UseVisualStyleBackColor = true;
            this.showActiveContentButton.Click += new System.EventHandler(this.showActiveContentButton_Click);
            // 
            // showActiveTreeNodeButton
            // 
            this.showActiveTreeNodeButton.Location = new System.Drawing.Point(254, 23);
            this.showActiveTreeNodeButton.Name = "showActiveTreeNodeButton";
            this.showActiveTreeNodeButton.Size = new System.Drawing.Size(194, 44);
            this.showActiveTreeNodeButton.TabIndex = 8;
            this.showActiveTreeNodeButton.Text = "Show ActiveTreeNode";
            this.showActiveTreeNodeButton.UseVisualStyleBackColor = true;
            this.showActiveTreeNodeButton.Click += new System.EventHandler(this.showActiveTreeNodeButton_Click);
            // 
            // lblContentNode
            // 
            this.lblContentNode.Location = new System.Drawing.Point(173, 278);
            this.lblContentNode.Name = "lblContentNode";
            this.lblContentNode.Size = new System.Drawing.Size(100, 23);
            this.lblContentNode.TabIndex = 7;
            this.lblContentNode.Text = "label3";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(22, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Active Content Node:";
            // 
            // lblTreeNode
            // 
            this.lblTreeNode.Location = new System.Drawing.Point(173, 255);
            this.lblTreeNode.Name = "lblTreeNode";
            this.lblTreeNode.Size = new System.Drawing.Size(100, 23);
            this.lblTreeNode.TabIndex = 5;
            this.lblTreeNode.Text = "label2";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Active TreeNode:";
            // 
            // removeContentButton
            // 
            this.removeContentButton.Location = new System.Drawing.Point(61, 173);
            this.removeContentButton.Name = "removeContentButton";
            this.removeContentButton.Size = new System.Drawing.Size(177, 44);
            this.removeContentButton.TabIndex = 3;
            this.removeContentButton.Text = "RemoveContent";
            this.removeContentButton.UseVisualStyleBackColor = true;
            this.removeContentButton.Click += new System.EventHandler(this.removeContentButton_Click);
            // 
            // addContentButton
            // 
            this.addContentButton.Location = new System.Drawing.Point(61, 123);
            this.addContentButton.Name = "addContentButton";
            this.addContentButton.Size = new System.Drawing.Size(177, 44);
            this.addContentButton.TabIndex = 2;
            this.addContentButton.Text = "AddContent";
            this.addContentButton.UseVisualStyleBackColor = true;
            this.addContentButton.Click += new System.EventHandler(this.addContentButton_Click);
            // 
            // removeTreeNodeButton
            // 
            this.removeTreeNodeButton.Location = new System.Drawing.Point(61, 73);
            this.removeTreeNodeButton.Name = "removeTreeNodeButton";
            this.removeTreeNodeButton.Size = new System.Drawing.Size(177, 44);
            this.removeTreeNodeButton.TabIndex = 1;
            this.removeTreeNodeButton.Text = "RemoveTreeNode";
            this.removeTreeNodeButton.UseVisualStyleBackColor = true;
            this.removeTreeNodeButton.Click += new System.EventHandler(this.removeTreeNodeButton_Click);
            // 
            // addTreeNodeButton
            // 
            this.addTreeNodeButton.Location = new System.Drawing.Point(61, 23);
            this.addTreeNodeButton.Name = "addTreeNodeButton";
            this.addTreeNodeButton.Size = new System.Drawing.Size(177, 44);
            this.addTreeNodeButton.TabIndex = 0;
            this.addTreeNodeButton.Text = "AddTreeNode";
            this.addTreeNodeButton.UseVisualStyleBackColor = true;
            this.addTreeNodeButton.Click += new System.EventHandler(this.addTreeNodeButton_Click);
            // 
            // rootNodeActiveTreeNodeButton1
            // 
            this.rootNodeActiveTreeNodeButton1.Location = new System.Drawing.Point(254, 255);
            this.rootNodeActiveTreeNodeButton1.Name = "rootNodeActiveTreeNodeButton1";
            this.rootNodeActiveTreeNodeButton1.Size = new System.Drawing.Size(194, 44);
            this.rootNodeActiveTreeNodeButton1.TabIndex = 15;
            this.rootNodeActiveTreeNodeButton1.Text = "Set RootNode ActiveTreeNode";
            this.rootNodeActiveTreeNodeButton1.UseVisualStyleBackColor = true;
            this.rootNodeActiveTreeNodeButton1.Click += new System.EventHandler(this.rootNodeActiveTreeNodeButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 736);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button rootNodeActiveTreeNodeButton1;

        private System.Windows.Forms.Button button11;

        private System.Windows.Forms.Button activeTreeNodeToggleVisibleButton;

        private System.Windows.Forms.Button activeTreeNodeHeaderToggleVisibleButton;
        private System.Windows.Forms.Button activeTreeNodeFooterToggleVisibleButton;
        private System.Windows.Forms.Button activeContentToggleVisibleButton;

        private System.Windows.Forms.Button showActiveTreeNodeButton;
        private System.Windows.Forms.Button showActiveContentButton;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTreeNode;
        private System.Windows.Forms.Label lblContentNode;
        private System.Windows.Forms.Label label4;

        #endregion


    }
}