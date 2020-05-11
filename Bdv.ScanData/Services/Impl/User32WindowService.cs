using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //try
            //{
            //    var length = SendMessage(hwnd, (uint)WindowMessage.WM_GETTEXTLENGTH).ToInt32();
            //    var sb = new StringBuilder(length + 1);
            //    SendMessage(hwnd, (int)WindowMessage.WM_GETTEXT, sb.Capacity, sb);
            //    return sb.ToString();
            //}
            //catch (Exception e)
            //{
            //    return string.Empty;
            //}

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

        private IEnumerable<HWND> GetChildControls(HWND pHwnd, string controlClass)
        {
            var result = new List<HWND>();
            var cHwnd = FindWindowEx(pHwnd, HWND.NULL, null, null);
            while (!cHwnd.IsNull)
            {
                var sb = new StringBuilder(10);
                GetClassName(cHwnd, sb, sb.Capacity);
                if (sb.ToString().Equals(controlClass, StringComparison.InvariantCultureIgnoreCase))
                {
                    result.Add(cHwnd);
                }
                result.AddRange(GetChildControls(cHwnd, controlClass));
                cHwnd = FindWindowEx(pHwnd, cHwnd, null, null);
            }
            return result;
        }

        public IEnumerable<string> GetControls(string windowTitle, string controlClass)
        {
            var pHwnd = FindWindow(null, windowTitle);
            foreach (var cHwnd in GetChildControls(pHwnd, controlClass))
            {
                yield return GetText(cHwnd, windowTitle);
            }
        }

        private HWND GetControl(string windowTitle, string controlClass, int controlIndex)
        {
            var pHwnd = FindWindow(null, windowTitle);
            var childs = GetChildControls(pHwnd, controlClass).ToList();

            if (childs.Count < (controlIndex + 1))
            {
                logger.Debug($"Не найдено поле класса = '{controlClass}', индекс = '{controlIndex}', заголовок окна = '{windowTitle}'");
                return HWND.NULL;
            }

            return childs[controlIndex];
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
