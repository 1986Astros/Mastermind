using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistryExtensions
{
    public class RegistryEx
    {
        const string WindowTopKeyname = "top";
        const string WindowLeftKeyname = "left";
        const string WindowHeightKeyname = "height";
        const string WindowWidthKeyname = "width";
        const string WindowMaximizedKeyname = "maximized";
        const string WindowTopmostKeyname = "topmost";

        public static char[] Separators = { '\\', '/' };
        public static string Separator = "\\";

        private RegistryKey RegistryRoot;
        private Dictionary<string, KeyInfo> RegistryKeys = new Dictionary<string, KeyInfo>();

        public enum HKey
        {
            LocalMachine = 0,
            CurrentUser = 1
        }

        private class KeyInfo
        {
            public RegistryKey regKey;
            public Form Form = null;
        }

        public RegistryEx(HKey hkey, string CompanyName, string AppName) : base()
        {
            if (hkey == HKey.LocalMachine)
            {
                RegistryRoot = Registry.LocalMachine.OpenSubKey("Software", true);
            }
            else
            {
                RegistryRoot = Registry.CurrentUser.OpenSubKey("Software", true);
            }

            RegistryRoot = RegistryRoot.CreateSubKey(CompanyName);
            foreach (string SubFolder in AppName.Split(Separators))
            {
                RegistryRoot = RegistryRoot.CreateSubKey(SubFolder);
            }
        }

        public RegistryKey AddGeneralKey(string KeyName)
        {
            string KeyNameUpper = KeyName.ToUpper();

            if (RegistryKeys.ContainsKey(KeyNameUpper))
            {
                return RegistryKeys[KeyNameUpper].regKey;
            }

            string[] KeyNames = KeyName.Split(Separators);
            RegistryKey rk = RegistryRoot;

            for (int i = 0; i < KeyNames.Length; i++)
            {
                rk = rk.CreateSubKey(KeyNames[i]);
            }

            RegistryKeys.Add(KeyNameUpper, new KeyInfo() { regKey = rk });

            return rk;
        }

        private KeyInfo GetKeyInfo(string KeyName)
        {
            return RegistryKeys[KeyName.ToUpper()];
        }

        public bool HasKey(string KeyName)
        {
            if (RegistryKeys.ContainsKey(KeyName.ToUpper()))
            {
                return true;
            }

            RegistryKey rk = RegistryRoot;

            foreach (string SubKeyName in KeyName.Split(Separators))
            {
                if (rk.SubKeyCount > 0)
                {
                    if (!rk.GetSubKeyNames().Any(s => s.Equals(SubKeyName, StringComparison.CurrentCultureIgnoreCase)))
                        return false;
                }
                else
                {
                    return false;
                }

                rk = rk.CreateSubKey(SubKeyName);
            }

            return true;
        }

        public void SetValue(string KeyName, string ValueName, object Value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(Value.GetType());
            if (tc.CanConvertFrom(typeof(string)) && tc.CanConvertTo(typeof(string)))
            {
                AddGeneralKey(KeyName).SetValue(ValueName, tc.ConvertToString(Value));
            }
            else
            {
                throw new Exception($"RegistryEx.SetValue({KeyName},{ValueName},{Value.ToString()}): Unable to set value for type {Value.GetType().Name}");
            }
        }
        public object GetValue(string KeyName, string ValueName, object Value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(Value.GetType());
            if (tc.CanConvertFrom(typeof(string)) && tc.CanConvertTo(typeof(string)))
            {
                return tc.ConvertFromString((string)(AddGeneralKey(KeyName).GetValue(ValueName, tc.ConvertToString(Value))));
            }
            else
            {
                throw new Exception($"RegistryEx.SetValue({KeyName},{ValueName},{Value.ToString()}): Unable to get value for type {Value.GetType().Name}");
            }
        }

        public IEnumerable<string> GetValueNames(string KeyName)
        {
            return AddGeneralKey(KeyName).GetValueNames();
        }

        public void DeleteKey(string KeyName)
        {
            try
            {
                List<string> KeyNames = new List<string>(KeyName.Split(Separators));
                bool RemoveSubKeys = false;

                if (KeyNames.Count() == 0)
                {
                    if (AddGeneralKey(KeyName).SubKeyCount > 0)
                    {
                        RegistryRoot.DeleteSubKeyTree(KeyName);
                        RemoveSubKeys = true;
                    }
                    else
                        RegistryRoot.DeleteSubKey(KeyName, false);
                }
                else
                {
                    //int LastName = KeyNames.GetUpperBound(0);
                    string LastKeyName = KeyNames.Last();
                    KeyNames.RemoveAt(LastKeyName.Count() - 1);
                    string ThisKeyName = String.Join(Separator, KeyNames);

                    if (AddGeneralKey(KeyName).SubKeyCount > 0)
                    {
                        AddGeneralKey(ThisKeyName).DeleteSubKeyTree(LastKeyName);
                        RemoveSubKeys = true;
                    }
                    else
                        AddGeneralKey(ThisKeyName).DeleteSubKey(LastKeyName, false);
                }

                if (RemoveSubKeys)
                {
                    List<string> KeysToRemove = new List<string>();
                    string KeyNameUpper = KeyName.ToUpper();

                    foreach (KeyValuePair<string, KeyInfo> kvp in RegistryKeys)
                    {
                        if (kvp.Key.ToString().StartsWith(KeyNameUpper))
                            KeysToRemove.Add(kvp.Key.ToString());
                    }
                    foreach (string KeyToRemove in KeysToRemove)
                        RegistryKeys.Remove(KeyToRemove);
                }
                RegistryRoot.Flush();
                RegistryKeys.Remove(KeyName.ToUpper());
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"RegistryEx.DeleteKey({KeyName})");
                Trace.Indent();
                Trace.WriteLine(ex.Message);
                Trace.Unindent();
            }
        }

        public void DeleteValue(string KeyName, string ValueName)
        {
            AddGeneralKey(KeyName).DeleteValue(ValueName, false);
        }

        public void MoveKey(string SourceKey, string DestKey)
        {
            CopyKey(SourceKey, DestKey, true);
        }

        public void CopyKey(string SourceKey, string DestKey, bool DeleteSource = false)
        {
            RegistryKey Source = AddGeneralKey(SourceKey);
            RegistryKey Dest = AddGeneralKey(DestKey);
            List<string> Names = new List<string>(Source.GetValueNames());
            object Default = Source.GetValue(null);     // root value of a tree

            if (Default != null)
                Dest.SetValue(null, Default);

            foreach (string ValueName in Names)
                Dest.SetValue(ValueName, Source.GetValue(ValueName));

            Names = new List<string>(Source.GetSubKeyNames());
            foreach (string SourceName in Names)
                CopyKey($"{SourceKey}{Separator}{SourceName}", $"{DestKey}{Separator}{SourceName}", DeleteSource);

            if (DeleteSource)
                DeleteKey(SourceKey);
        }

        #region "Forms"
        private static Dictionary<Form, RegistryKey> FormToRegKey = new Dictionary<Form, RegistryKey>();

        /// <summary>
        /// Tracks and saves information about the size and location of this Form and all its Splitters.
        /// </summary>
        /// <param name="Form">The Form to track.</param>
        /// <returns></returns>
        public RegistryKey AddFormKey(Form Form)
        {
            string KeyName = Form.Name;
            RegistryKey RegKey = AddGeneralKey(KeyName);
            KeyInfo ki = GetKeyInfo(KeyName);
            int FormTop, FormLeft, FormHeight, FormWidth;
            bool FormMaximized;

            ki.Form = Form;

            // get the saved position and size
            FormTop = (int)RegKey.GetValue(WindowTopKeyname, Form.Top);
            FormLeft = (int)RegKey.GetValue(WindowLeftKeyname, Form.Left);
            switch (Form.FormBorderStyle)
            {
                case FormBorderStyle.Sizable:
                case FormBorderStyle.SizableToolWindow:
                    FormHeight = (int)RegKey.GetValue(WindowHeightKeyname, Form.Height);
                    FormWidth = (int)RegKey.GetValue(WindowWidthKeyname, Form.Width);
                    break;
                default:
                    FormHeight = Form.Height;
                    FormWidth = Form.Width;
                    break;
            }
            FormMaximized = Convert.ToBoolean(RegKey.GetValue(WindowMaximizedKeyname, false));

            // make sure the entire window is visible
            bool IsVisible = false;
            foreach (Screen s in Screen.AllScreens)
            {
                if  (FormLeft >=  s.Bounds.Left && FormTop >=s. Bounds.Top && FormLeft < (s.Bounds.Right  - 20) && FormTop < (s.Bounds.Bottom - 20))
                {
                    IsVisible = true;
                    break;
                }
            }
            if (!IsVisible)
            {
                FormLeft = Screen.PrimaryScreen.Bounds.Left;
                FormTop = Screen.PrimaryScreen.Bounds.Top;
            }

            // restore window size and position
            Form.Width = FormWidth;
            Form.Height = FormHeight;
            if (FormMaximized)
            {
                Form.WindowState = FormWindowState.Maximized;
            }

            Form.Move += Form_Move;
            Form.Resize += Form_Resize;

            FormToRegKey.Add(Form, RegKey);
            PrepareSplitContainers(Form.Controls);

            return RegKey;
        }

        private void Form_Move(object sender, EventArgs e)
        {
            Form Form = (Form)sender;
            RegistryKey RegKey = AddGeneralKey(Form.Name);

            switch (Form.WindowState)
            {
                case FormWindowState.Normal:
                    RegKey.SetValue(WindowMaximizedKeyname, false);
                    RegKey.SetValue(WindowTopKeyname, Form.Top);
                    RegKey.SetValue(WindowLeftKeyname, Form.Left);
                    break;
                case FormWindowState.Maximized:
                    RegKey.SetValue(WindowMaximizedKeyname, true);
                    break;
                case FormWindowState.Minimized:
                    break;
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            Form Form = (Form)sender;
            RegistryKey RegKey = AddGeneralKey(Form.Name);

            switch (Form.WindowState)
            {
                case FormWindowState.Normal:
                    RegKey.SetValue(WindowMaximizedKeyname, false);
                    RegKey.SetValue(WindowTopKeyname, Form.Top);
                    RegKey.SetValue(WindowLeftKeyname, Form.Left);
                    RegKey.SetValue(WindowWidthKeyname, Form.Width);
                    RegKey.SetValue(WindowHeightKeyname, Form.Height);
                    break;
                case FormWindowState.Maximized:
                    RegKey.SetValue(WindowMaximizedKeyname, true);
                    break;
                case FormWindowState.Minimized:
                    break;
            }
        }

        private void PrepareSplitContainers(Control.ControlCollection Controls)
        {
            foreach (Control c in Controls)
            {
                if (c.GetType() == typeof(SplitContainer))
                {
                    SplitContainer sc = (SplitContainer)c;
                    sc.Panel1.VisibleChanged += SplitContainerPanel_VisibleChanged;
                    sc.Panel2.VisibleChanged += SplitContainerPanel_VisibleChanged;
                    sc.SplitterMoving += SplitContainer_SplitterMoving;
                }
                PrepareSplitContainers(c.Controls); // there may be other controls containing splitters
            }
        }
        private void SplitContainerPanel_VisibleChanged(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)((SplitterPanel)sender).Parent;
            if (sc != null)
            {
                int SplitterDistance = (int)FormToRegKey[sc.ParentForm].GetValue(sc.Name, sc.SplitterDistance);
                if ((sc.Orientation == Orientation.Horizontal && SplitterDistance != sc.Height) ||
                    (sc.Orientation == Orientation.Vertical && SplitterDistance != sc.Height))
                {
                    // give it a chance to finish layout in case the container will large enough
                    System.Windows.Forms.Timer SplitterTimer = new System.Windows.Forms.Timer() { Interval = 40, Tag = sc, Enabled = false };
                    SplitterTimer.Tick += SplitterTimer_Tick;
                    SplitterTimer.Enabled = true;
                }
                else
                    sc.SplitterDistance = (int)FormToRegKey[sc.ParentForm].GetValue(sc.Name, sc.SplitterDistance);
            }
        }
        private void SplitterTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer SplitterTimer = (System.Windows.Forms.Timer)sender;
            SplitterTimer.Enabled = false;
            SplitterTimer.Tick -= SplitterTimer_Tick;

            SplitContainer sc = (SplitContainer)SplitterTimer.Tag;
            sc.SplitterDistance = (int)FormToRegKey[sc.ParentForm].GetValue(sc.Name, sc.SplitterDistance);

            SplitterTimer.Dispose();
            SplitterTimer = null;
        }
        private void SplitContainer_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            SplitContainer sc = (SplitContainer)sender;
            int SplitterDistance = Math.Max(e.SplitX, e.SplitY);
            FormToRegKey[sc.ParentForm].SetValue(sc.Name, SplitterDistance);
        }
        #endregion
    }
}

