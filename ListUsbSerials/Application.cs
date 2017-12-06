using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Management;

using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;
using ListUsbSerials.Properties;

namespace ListUsbSerials
{
    public partial class Application : Form
    {
        void updateComList()
        {
            portList.Items.Clear();//TODO call this later only

            //First, get the active serialports
            RegistryKey serialRoot = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM");
            List<string> activeSerials = new List<string>();
            if (serialRoot != null)
            {
                foreach (string valueName in serialRoot.GetValueNames())
                {
                    activeSerials.Add(serialRoot.GetValue(valueName).ToString());
                }
                serialRoot.Close();
            }

            //then, get the USB devices
            string[] usbRoots = { "SYSTEM\\CurrentControlSet\\Enum\\USB", "SYSTEM\\CurrentControlSet\\Enum\\FTDIBUS" }; 
            foreach (string usbRootStr in usbRoots) {
                RegistryKey usbRoot = Registry.LocalMachine.OpenSubKey(usbRootStr);
                if (usbRoot != null)
                {
                    foreach (string usbDevStr in usbRoot.GetSubKeyNames())
                    {
                        if (usbDevStr.StartsWith("VID_")) //not a root hub
                        {
                            RegistryKey usbDev = usbRoot.OpenSubKey(usbDevStr);
                            if (usbDev != null)
                            {
                                foreach (string usbEntityStr in usbDev.GetSubKeyNames())
                                {
                                    RegistryKey usbEntity = usbDev.OpenSubKey(usbEntityStr);
                                    if (usbEntity != null)
                                    {
                                        RegistryKey devparms = usbEntity.OpenSubKey("Device Parameters");
                                        if (devparms != null) {
                                            object port = devparms.GetValue("PortName");
                                            if (port != null)
                                            {
                                                ListViewItem portData = new ListViewItem(port.ToString());
                                                portData.SubItems.Add(usbDevStr.Substring(usbDevStr.IndexOf("VID_") + 4, 4));
                                                portData.SubItems.Add(usbDevStr.Substring(usbDevStr.IndexOf("PID_") + 4, 4));

                                                string serial = "";
                                                if (usbRootStr.EndsWith("FTDIBUS"))
                                                {
                                                    serial = usbDevStr.Substring(usbDevStr.LastIndexOf("+") + 1);
                                                    //last charachter is A for first serial, B for second etc. However, the port numbering should be in order
                                                    serial = serial.Substring(0, serial.Length - 1);
                                                }
                                                else if (usbDevStr.Contains("MI_"))
                                                {
                                                    string rootdevice = usbDevStr.Substring(0, usbDevStr.LastIndexOf("&MI"));
                                                    foreach (string serialId in usbRoot.OpenSubKey(rootdevice).GetSubKeyNames())
                                                    {
                                                        object parentPrefix = usbRoot.OpenSubKey(rootdevice + "\\" + serialId).GetValue("ParentIdPrefix");
                                                        if (parentPrefix != null && usbEntityStr.StartsWith(parentPrefix.ToString()))
                                                            serial = serialId;
                                                    }
                                                }
                                                else
                                                {
                                                    serial = usbEntityStr;
                                                }
                                                portData.SubItems.Add(serial);

                                                object mfg = usbEntity.GetValue("Mfg");
                                                if (mfg != null)
                                                    portData.SubItems.Add(mfg.ToString().Substring(mfg.ToString().LastIndexOf(';') + 1));
                                                else
                                                    portData.SubItems.Add("");
                                                object desc = usbEntity.GetValue("DeviceDesc");
                                                if (desc != null)
                                                    portData.SubItems.Add(desc.ToString().Substring(desc.ToString().LastIndexOf(';') + 1));
                                                else
                                                    portData.SubItems.Add("");

                                                if (!activeSerials.Contains(port.ToString()))
                                                {
                                                    portData.ForeColor = SystemColors.GrayText;
                                                    portData.BackColor = SystemColors.InactiveBorder;
                                                    portData.Selected = false;
                                                }
                                                if (showDisconnected.Checked || portData.ForeColor != SystemColors.GrayText)
                                                {
                                                    portList.Items.Add(portData);
                                                }
                                            }
                                            devparms.Close();
                                        }
                                        usbEntity.Close();
                                    }

                                }
                                usbDev.Close();
                            }
                        }
                    }
                    usbRoot.Close();
                }
            }

            portList.Sort();
        }


        class ListViewItemComparer : IComparer
        {
            
            public int Compare(object x, object y)
            {
                int xItem = Int32.Parse(((ListViewItem)x).SubItems[0].Text.Substring(3));
                int yItem = Int32.Parse(((ListViewItem)y).SubItems[0].Text.Substring(3));

                return xItem-yItem;
            }
        }


        private ManagementEventWatcher watcher;



        public Application()
        {
            InitializeComponent();
            portList.ListViewItemSorter = new ListViewItemComparer();
            updateComList();
            watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2 OR EventType = 3");
            watcher.EventArrived += new EventArrivedEventHandler(InvokeRefresh);
            watcher.Query = query;
            watcher.Start();
        }


        private void RefreshNeeded(object sender, EventArgs e)
        {
            updateComList();
        }

        private void InvokeRefresh(object sender, EventArgs e)
        {
            MethodInvoker inv = delegate
            {
                updateComList();
            };
            this.Invoke(inv);
            
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            watcher.Stop();
        }

        private void PortList_DoubleClick(object sender, EventArgs e)
        {
            Point localPoint = portList.PointToClient(Cursor.Position);
            ListViewHitTestInfo hit = portList.HitTest(localPoint);

            Process terminal = new Process();
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                terminal.StartInfo.FileName = Settings.Default["terminalb"].ToString();
                terminal.StartInfo.Arguments = Settings.Default["teminalargsb"].ToString();
            } else if (ModifierKeys.HasFlag(Keys.Control))
            {
                terminal.StartInfo.FileName = Settings.Default["terminalc"].ToString();
                terminal.StartInfo.Arguments = Settings.Default["teminalargsc"].ToString();
            } else
            {
                terminal.StartInfo.FileName = Settings.Default["terminal"].ToString();
                terminal.StartInfo.Arguments = Settings.Default["teminalargs"].ToString();
            }
            terminal.StartInfo.Arguments = terminal.StartInfo.Arguments.Replace("{device}", hit.Item.Text);
            terminal.StartInfo.Arguments = terminal.StartInfo.Arguments.Replace("{devicenum}", hit.Item.Text.Substring(3));
            terminal.StartInfo.Arguments = terminal.StartInfo.Arguments.Replace("{vid}", hit.Item.SubItems[1].Text.ToString());
            terminal.StartInfo.Arguments = terminal.StartInfo.Arguments.Replace("{pid}", hit.Item.SubItems[2].Text.ToString());
            terminal.StartInfo.Arguments = terminal.StartInfo.Arguments.Replace("{serial}", hit.Item.SubItems[3].Text.ToString());
            terminal.StartInfo.Arguments = terminal.StartInfo.Arguments.Replace("{mfg}", hit.Item.SubItems[3].Text.ToString());
            terminal.StartInfo.Arguments = terminal.StartInfo.Arguments.Replace("{desc}", hit.Item.SubItems[3].Text.ToString());

            try
            {
                terminal.Start();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("The specifed program (" + terminal.StartInfo.FileName + ") cannot be started", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PortList_MouseClick(object sender, MouseEventArgs e)
        {
            if ( e.Button == MouseButtons.Right)
            {
                Point localPoint = portList.PointToClient(Cursor.Position);
                ListViewHitTestInfo hit = portList.HitTest(localPoint);
                Clipboard.SetText(hit.SubItem.Text);
            }
            
        }

        private void PortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //we don't want to have selection, but mouse events won't work otherwise
            portList.SelectedIndices.Clear();
        }

        private void TerminalSetup_Click(object sender, EventArgs e)
        {
            Form confDlg = new ConfigDialog();
            confDlg.ShowDialog(this);
        }
    }
}
