using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace Bdv.ScanData.Services.Impl
{
    public class User32WindowService : IWindowService
    {
        private readonly ILogger logger;

        public User32WindowService(ILogger logger)
        {
            this.logger = logger;
        }

        public IEnumerable<string> GetWindows()
        {
            var result = new List<string>();
            EnumWindows((hwnd, param) =>
            {
                try
                {
                    var length = GetWindowTextLength(hwnd);
                    if (length > 0)
                    {
                        var sb = new StringBuilder(length + 1);
                        GetWindowText(hwnd, sb, length + 1);
                        if (sb.Length > 0)
                        {
                            result.Add(sb.ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Warn(e, $"Не удалось получить заголовок окна с hwnd = '{hwnd}'");
                }

                return true;
            }, new IntPtr(0));

            return result;
        }

        private string GetText(HWND hwnd, string windowTitle)
        {
            var length = GetWindowTextLength(hwnd);
            if (length > 0)
            {
                var sb = new StringBuilder(length + 1);
                try
                {
                    GetWindowText(hwnd, sb, length + 1);
                    return sb.ToString();
                }
                catch (Exception e)
                {
                    logger.Warn(e, $"Не удалось получить текст поля с hwnd = '{hwnd}', заголовок окна = '{windowTitle}'");
                }
            }
            return string.Empty;
        }

        public IEnumerable<string> GetControls(string windowTitle, string controlClass)
        {
            var pHwnd = FindWindow(null, windowTitle);
            var cHwnd = FindWindowEx(pHwnd, HWND.NULL, controlClass, null);
            while (!cHwnd.IsNull)
            {
                yield return GetText(cHwnd, windowTitle);
                cHwnd = FindWindowEx(pHwnd, cHwnd, controlClass, null);
            }
        }

        private HWND GetControl(string windowTitle, string controlClass, int controlIndex)
        {
            var pHwnd = FindWindow(null, windowTitle);
            var cHwnd = FindWindowEx(pHwnd, HWND.NULL, controlClass, null);
            var i = 0;
            while (!cHwnd.IsNull && i < controlIndex)
            {
                cHwnd = FindWindowEx(pHwnd, cHwnd, controlClass, null);
            }
            if (i != controlIndex)
            {
                logger.Debug($"Не найдено поле класса = '{controlClass}', индекс = '{controlIndex}', заголовок окна = '{windowTitle}'");
                return HWND.NULL;
            }

            return cHwnd;
        }

        public void SetText(string windowTitle, string controlClass, int controlIndex, string text)
        {
            var hwnd = GetControl(windowTitle, controlClass, controlIndex);
            if (hwnd.IsNull)
            {
                return;
            }
            int i = 0;
            try
            {
                SendMessage(hwnd, (uint)WindowMessage.WM_SETTEXT, ref i, new StringBuilder(text));
            }
            catch (Exception e)
            {
                logger.Error(e, $"Текст для поля с hwnd = '{hwnd}', класс = '{controlClass}', индекс = '{controlIndex}', заголовок окна = '{windowTitle}' не установлен, текст = '{text}'");
            }
        }

        public string GetText(string windowTitle, string controlClass, int controlIndex)
        {
            var hwnd = GetControl(windowTitle, controlClass, controlIndex);
            return hwnd.IsNull ? string.Empty : GetText(hwnd, windowTitle);
        }
    }
}
