using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInventory.ViewModels
{
   public  class LoginViwModel:Models.user,IDataErrorInfo
    {

        private string message;
        private string error;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChange("Message"); }
        }

        public string Error => throw new NotImplementedException();

        public CommandHandler LoginCommand { get; }
        public CommandHandler CloseCommand { get; }

        public string this[string columnName] => throw new NotImplementedException();

        public LoginViwModel()
        {
            LoginCommand = new CommandHandler { CanExecuteAction = x => string.IsNullOrEmpty(error), ExecuteAction = LoginAction };
            CloseCommand = new CommandHandler { CanExecuteAction = x =>true, ExecuteAction =x=>WindowClose()};
        }

        public bool LoginSuccess { get; private set; }
        public Action WindowClose { get; internal set; }

        private void LoginAction(object obj)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Users.Where(O => O.UserName == this.UserName).FirstOrDefault();
                    if (result == null)
                        throw new SystemException("Anda Tidak Memiliki Akses, Hubungi Administrator");
                    else
                    {
                        if (result.Password != this.Password)
                            throw new SystemException("Password Anda Salah");
                        else if(result.StatusAktif== StatusAktif.Tidak )
                            throw new SystemException("User Anda Telah Di Non Aktifkan");
                        else
                        {
                            this.UserId = result.UserId;
                            this.Nama = result.Nama;
                            this.LoginSuccess = true;
                            WindowClose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
        }
    }
}
