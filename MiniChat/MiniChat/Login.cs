using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniChat
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Client.s.Connected)
                Client.Start("127.0.0.1", 1000);
            Packet p = new Packet(6);
            p.AddString(textBox1.Text);
            Client.s.Send(p.GetBytes());
            Globals.Name = textBox1.Text;
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
            {
                button1.PerformClick();
            }
        }
    }
}
