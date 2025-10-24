﻿using System;
using System.Windows.Forms;

namespace ServerApp
{
    public partial class ServerForm : Form
    {
        private TcpServer server;

        public ServerForm()
        {
            InitializeComponent();
            server = new TcpServer();
            server.OnLog += LogMessage;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int port = (int)numPort.Value;
                server.Start(port);

                btnStart.Enabled = false;
                btnStop.Enabled = true;
                numPort.Enabled = false;

                lblStatus.Text = "Server Running";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting server: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                server.Stop();

                btnStart.Enabled = true;
                btnStop.Enabled = false;
                numPort.Enabled = true;

                lblStatus.Text = "Server Stopped";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping server: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void LogMessage(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(LogMessage), message);
                return;
            }

            txtLog.AppendText(message + Environment.NewLine);
            txtLog.ScrollToCaret();
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server.IsRunning)
            {
                var result = MessageBox.Show("Server is still running. Stop and exit?",
                    "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    server.Stop();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}