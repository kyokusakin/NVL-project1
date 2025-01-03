﻿    namespace WindowsFormsApp1_2024_12_27
    {
        partial class Form1
        {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        public static System.Drawing.Color Transparent { get; }
        private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// 清除任何使用中的資源。
            /// </summary>
            /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Windows Form 設計工具產生的程式碼

            /// <summary>
            /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
            /// 這個方法的內容。
            /// </summary>
            private void InitializeComponent()
            {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel_Main = new System.Windows.Forms.Panel();
            this.Speaker = new System.Windows.Forms.Label();
            this.DialogueLabel = new System.Windows.Forms.Label();
            this.button_Reset = new System.Windows.Forms.Button();
            this.Character = new System.Windows.Forms.PictureBox();
            this.choice3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.choice2 = new System.Windows.Forms.Button();
            this.choice1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dialoguePanel = new System.Windows.Forms.Panel();
            this.nextbutton = new System.Windows.Forms.Button();
            this.Scene = new System.Windows.Forms.PictureBox();
            this.panel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Character)).BeginInit();
            this.dialoguePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Scene)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Main
            // 
            this.panel_Main.Controls.Add(this.DialogueLabel);
            this.panel_Main.Controls.Add(this.Speaker);
            this.panel_Main.Controls.Add(this.button_Reset);
            this.panel_Main.Controls.Add(this.Character);
            this.panel_Main.Controls.Add(this.choice3);
            this.panel_Main.Controls.Add(this.label1);
            this.panel_Main.Controls.Add(this.choice2);
            this.panel_Main.Controls.Add(this.choice1);
            this.panel_Main.Controls.Add(this.button1);
            this.panel_Main.Controls.Add(this.textBox1);
            this.panel_Main.Controls.Add(this.Scene);
            this.panel_Main.Controls.Add(this.dialoguePanel);
            this.panel_Main.Location = new System.Drawing.Point(-1, 2);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(822, 532);
            this.panel_Main.TabIndex = 0;
            // 
            // Speaker
            // 
            this.Speaker.AutoSize = true;
            this.Speaker.BackColor = System.Drawing.Color.Transparent;
            this.Speaker.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Speaker.Location = new System.Drawing.Point(18, 413);
            this.Speaker.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Speaker.Name = "Speaker";
            this.Speaker.Size = new System.Drawing.Size(70, 20);
            this.Speaker.TabIndex = 19;
            this.Speaker.Text = "Speaker";
            this.Speaker.Visible = false;
            // 
            // DialogueLabel
            // 
            this.DialogueLabel.AutoSize = true;
            this.DialogueLabel.BackColor = System.Drawing.Color.Transparent;
            this.DialogueLabel.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.DialogueLabel.Location = new System.Drawing.Point(18, 447);
            this.DialogueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DialogueLabel.Name = "DialogueLabel";
            this.DialogueLabel.Size = new System.Drawing.Size(109, 20);
            this.DialogueLabel.TabIndex = 12;
            this.DialogueLabel.Text = "DialogueText";
            this.DialogueLabel.Visible = false;
            // 
            // button_Reset
            // 
            this.button_Reset.BackColor = System.Drawing.Color.Transparent;
            this.button_Reset.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Reset.Location = new System.Drawing.Point(2, 2);
            this.button_Reset.Margin = new System.Windows.Forms.Padding(2);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(49, 43);
            this.button_Reset.TabIndex = 23;
            this.button_Reset.TabStop = false;
            this.button_Reset.Text = "🔙";
            this.button_Reset.UseVisualStyleBackColor = false;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // Character
            // 
            this.Character.BackColor = System.Drawing.Color.Transparent;
            this.Character.Image = ((System.Drawing.Image)(resources.GetObject("Character.Image")));
            this.Character.Location = new System.Drawing.Point(704, 249);
            this.Character.Margin = new System.Windows.Forms.Padding(2);
            this.Character.Name = "Character";
            this.Character.Size = new System.Drawing.Size(113, 119);
            this.Character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Character.TabIndex = 18;
            this.Character.TabStop = false;
            this.Character.Visible = false;
            // 
            // choice3
            // 
            this.choice3.BackColor = System.Drawing.Color.Transparent;
            this.choice3.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.choice3.Location = new System.Drawing.Point(90, 215);
            this.choice3.Margin = new System.Windows.Forms.Padding(2);
            this.choice3.Name = "choice3";
            this.choice3.Size = new System.Drawing.Size(650, 40);
            this.choice3.TabIndex = 22;
            this.choice3.Text = "button3";
            this.choice3.UseVisualStyleBackColor = false;
            this.choice3.Visible = false;
            this.choice3.Click += new System.EventHandler(this.choice_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 297);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "玩家名稱";
            // 
            // choice2
            // 
            this.choice2.BackColor = System.Drawing.Color.Transparent;
            this.choice2.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.choice2.Location = new System.Drawing.Point(90, 155);
            this.choice2.Margin = new System.Windows.Forms.Padding(2);
            this.choice2.Name = "choice2";
            this.choice2.Size = new System.Drawing.Size(650, 40);
            this.choice2.TabIndex = 16;
            this.choice2.Text = "button3";
            this.choice2.UseVisualStyleBackColor = false;
            this.choice2.Visible = false;
            this.choice2.Click += new System.EventHandler(this.choice_Click);
            // 
            // choice1
            // 
            this.choice1.BackColor = System.Drawing.Color.Transparent;
            this.choice1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.choice1.Location = new System.Drawing.Point(90, 95);
            this.choice1.Margin = new System.Windows.Forms.Padding(2);
            this.choice1.Name = "choice1";
            this.choice1.Size = new System.Drawing.Size(650, 40);
            this.choice1.TabIndex = 15;
            this.choice1.Text = "button2";
            this.choice1.UseVisualStyleBackColor = false;
            this.choice1.Visible = false;
            this.choice1.Click += new System.EventHandler(this.choice_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(385, 317);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 31);
            this.button1.TabIndex = 14;
            this.button1.Text = "確認";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.GameStart);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(290, 324);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(68, 22);
            this.textBox1.TabIndex = 13;
            // 
            // dialoguePanel
            // 
            this.dialoguePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dialoguePanel.Controls.Add(this.nextbutton);
            this.dialoguePanel.Location = new System.Drawing.Point(1, 368);
            this.dialoguePanel.Margin = new System.Windows.Forms.Padding(2);
            this.dialoguePanel.Name = "dialoguePanel";
            this.dialoguePanel.Size = new System.Drawing.Size(821, 164);
            this.dialoguePanel.TabIndex = 21;
            this.dialoguePanel.Click += new System.EventHandler(this.DialogueLabel_Click);
            // 
            // nextbutton
            // 
            this.nextbutton.Location = new System.Drawing.Point(789, 127);
            this.nextbutton.Margin = new System.Windows.Forms.Padding(2);
            this.nextbutton.Name = "nextbutton";
            this.nextbutton.Size = new System.Drawing.Size(27, 24);
            this.nextbutton.TabIndex = 11;
            this.nextbutton.Text = "▶";
            this.nextbutton.UseVisualStyleBackColor = true;
            this.nextbutton.Visible = false;
            this.nextbutton.Click += new System.EventHandler(this.nextbutton_Click);
            // 
            // Scene
            // 
            this.Scene.Location = new System.Drawing.Point(0, 0);
            this.Scene.Margin = new System.Windows.Forms.Padding(2);
            this.Scene.Name = "Scene";
            this.Scene.Size = new System.Drawing.Size(822, 368);
            this.Scene.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Scene.TabIndex = 17;
            this.Scene.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(820, 532);
            this.Controls.Add(this.panel_Main);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel_Main.ResumeLayout(false);
            this.panel_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Character)).EndInit();
            this.dialoguePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Scene)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.Button choice3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Speaker;
        private System.Windows.Forms.Button choice2;
        private System.Windows.Forms.Button choice1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label DialogueLabel;
        private System.Windows.Forms.Panel dialoguePanel;
        private System.Windows.Forms.Button nextbutton;
        private System.Windows.Forms.PictureBox Character;
        private System.Windows.Forms.PictureBox Scene;
        private System.Windows.Forms.Button button_Reset;
    }
    }

