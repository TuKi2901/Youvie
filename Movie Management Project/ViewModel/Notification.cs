using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serilog;
using System.Threading.Tasks;

namespace Movie_Management_Project.ViewModel
{
    public class ErrorHandling
    {
        public void ShowError(string errorMessage)
        {
            // Hiển thị thông báo lỗi cho người dùng, ví dụ: sử dụng MessageBox, Toast, Snackbar, AlertDialog, vv.
            // Tùy thuộc vào nền tảng và công nghệ bạn đang sử dụng
            // Ví dụ trong Console

            Shell.Current.DisplayAlert("Error!", $"{errorMessage}", "OK");
            
        }

        public void LogError(string errorMessage)
        {
            // Ghi log lỗi, ví dụ: sử dụng logging framework như Serilog, log4net, NLog, vv.
            // Hoặc có thể lưu vào một tệp log

            // Ví dụ in ra Console
            Console.WriteLine($"Error: {errorMessage}");

            // Ví dụ sử dụng Serilog
            //Log.Error(errorMessage);
        }

        public void HandleError(Exception ex)
        {
            // Xử lý một Exception cụ thể (nếu cần)
            // Ví dụ: nếu bạn muốn xử lý lỗi kiểu ArgumentException một cách đặc biệt

            if (ex is ArgumentException argEx)
            {
                // Xử lý lỗi kiểu ArgumentException
                // Ví dụ: ShowError(argEx.Message);
            }
            else
            {
                // Xử lý các loại lỗi khác
                // Ví dụ: LogError(ex.Message);
            }
        }
    }

}
