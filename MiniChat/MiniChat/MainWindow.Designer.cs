namespace MiniChat
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.chat_text = new System.Windows.Forms.TextBox();
            this.msg = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.list = new System.Windows.Forms.ListBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // chat_text
            // 
            this.chat_text.BackColor = System.Drawing.SystemColors.HotTrack;
            this.chat_text.ForeColor = System.Drawing.SystemColors.Window;
            this.chat_text.Location = new System.Drawing.Point(12, 12);
            this.chat_text.Multiline = true;
            this.chat_text.Name = "chat_text";
            this.chat_text.ReadOnly = true;
            this.chat_text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chat_text.Size = new System.Drawing.Size(391, 201);
            this.chat_text.TabIndex = 0;
            // 
            // msg
            // 
            this.msg.Location = new System.Drawing.Point(12, 219);
            this.msg.Multiline = true;
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(304, 34);
            this.msg.TabIndex = 1;
            this.msg.TextChanged += new System.EventHandler(this.msg_TextChanged);
            this.msg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.msg_KeyPress);
            this.msg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.msg_KeyUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(328, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(410, 12);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(120, 238);
            this.list.TabIndex = 3;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "MiniChat";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 265);
            this.Controls.Add(this.list);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.chat_text);
            this.Name = "MainWindow";
            this.Text = "MiniChat";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox chat_text;
        public System.Windows.Forms.TextBox msg;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.ListBox list;
        public System.Windows.Forms.NotifyIcon notifyIcon1;

    }
}

