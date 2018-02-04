using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniChat
{
    public partial class Client
    {
        public static int Parse(byte[] bytes,int startIndex)
        {
            try
            {
                Packet p = new Packet(bytes,startIndex);
                switch (p.Opcode)
                {
                    case 2:
                        string name = p.ReadString();
                        string msg = p.ReadString();
                        Globals.Form.chat_text.AppendText(string.Format("<{0}> : {1}\n", name, msg));
                       /* Globals.Form.notifyIcon1.BalloonTipText = string.Format("<{0}> Sent a messege", name);
                        Globals.Form.notifyIcon1.ShowBalloonTip(20);*/
                        break;
                    case 7:
                        string nickname = p.ReadString();
                        Globals.Form.chat_text.AppendText("<" + nickname + "> is now online.\n");
                        if (!Globals.Form.list.Items.Contains(nickname))
                            Globals.Form.list.Items.Add(nickname);
                        /*Globals.Form.notifyIcon1.BalloonTipText = string.Format("<{0}> is online.",nickname);
                        Globals.Form.notifyIcon1.ShowBalloonTip(20);*/
                        break;
                    case 8:
                        string x = p.ReadString();
                        Globals.Form.chat_text.AppendText("<" + x + "> Disconnected\n");
                        Globals.Form.list.Items.Remove(x);
                        /*Globals.Form.notifyIcon1.BalloonTipText = string.Format("<{0}> is offline.",x);
                        Globals.Form.notifyIcon1.ShowBalloonTip(20);*/
                        break;
                    case 6:
                        if (p.ReadBYTE() == 0x01)
                        {
                            Globals.NameForm.Close();
                            Globals.Form.Text += " | " + Globals.Name;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("This name is already in use, choose another one.");
                            Globals.Name = "";
                        }
                        break;
                    case 3:
                        int count = (int)p.ReadWORD();
                        for (int i = 0; i < count; i++)
                        {
                            Globals.Form.list.Items.Add(p.ReadString());
                        }

                        break;
                    case 20:
                        System.Windows.Forms.MessageBox.Show("Sorry,server is full.");
                        Environment.Exit(1);
                        break;
                }
                return p.Size;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return -1;
        }
    }
}
