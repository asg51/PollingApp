using FluentValidation.Results;
using PollingApp.BL.ValidationRules;
using PollingApp.Entities;
using PollingApp.Entities.P2PModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.BL.Contcat
{
    public class AdminManager
    {
        AdminValidation validations;
        public AdminManager()
        {
            validations = new AdminValidation();
        }
        public void Add(Admin admin, Poll poll)
        {
            ValidationResult result = validations.Validate(admin);
            if (result.IsValid)
            {
                if (poll.Admins.GetList().Where(x => x.Key == admin.Key).ToList().Count > 0)
                {
                    throw new Exception("Aynı anahtar kullanılamaz!");
                }
                poll.Admins.Add(admin);
                Managers.clientManager.AddAdmin(poll.Urls, poll.PollingName, poll.Admins.GetList(), admin);
            }
            else
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        public bool P2PAdd(PostAdminSetting adminSetting, string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            poll.Admins.Add(adminSetting.Admin);
            try
            {
                for (int i = 0; i < adminSetting.Admins.Count; i++)
                {
                    if (!AdminEquivocation(adminSetting.Admins[i], poll.Admins.Get(i)))
                    {
                        poll.Admins.Delete(adminSetting.Admin);
                        return false;
                    }
                }
                if (poll.Admins.GetList().Where(x => x.Key == adminSetting.Admin.Key).ToList().Count > 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool P2PDelete(PostAdminSetting adminSetting, string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            List<Admin> admins = new List<Admin>();
            admins.AddRange(poll.Admins.GetList());
            admins.Remove(admins.FirstOrDefault(x => x.Index == adminSetting.Admin.Index));
            try
            {
                for (int i = 0; i < adminSetting.Admins.Count; i++)
                {
                    if (!AdminEquivocation(adminSetting.Admins[i], admins[i]))
                    {
                        return false;
                    }
                }
                poll.Admins.Delete(poll.Admins.GetList().FirstOrDefault(x => x.Index == adminSetting.Admin.Index));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool P2PEdit(PostAdminSetting adminSetting, string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            List<Admin> admins = new List<Admin>();
            admins.AddRange(poll.Admins.GetList());
            Admin admin = admins.FirstOrDefault(x => x.Index == adminSetting.Admin.Index);
            SwapAdmin(admin, adminSetting.Admin);
            try
            {
                for (int i = 0; i < adminSetting.Admins.Count; i++)
                {
                    if (!AdminEquivocation(adminSetting.Admins[i], admins[i]))
                    {
                        return false;
                    }
                }
                SwapAdmin(admins.FirstOrDefault(x => x.Index == adminSetting.Admin.Index), admin);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void P2PRefreshList(IList<Admin> admins, string pollName)
        {
            PollingList.dbPoll.Search(pollName).Admins.SetList(admins);
        }
        public void Edit(Poll poll, Admin admin, Admin editAdmin)
        {
            ValidationResult result = validations.Validate(editAdmin);
            if (result.IsValid)
            {
                SwapAdmin(admin, editAdmin);
                Managers.clientManager.EditAdmin(poll.Urls, poll.PollingName, poll.Admins.GetList(), admin);
            }
            else
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        public void Delete(Admin admin, Poll poll)
        {
            try
            {
                poll.Admins.Delete(admin);
                Managers.clientManager.DeleteAdmin(poll.Urls, poll.PollingName, poll.Admins.GetList(), admin);
            }
            catch
            {
                throw new Exception("Silme başarısız!");
            }
        }
        public void Delete(int index, Poll poll)
        {
            try
            {
                Admin admin = poll.Admins.Get(index);
                poll.Admins.Delete(index);
                Managers.clientManager.DeleteAdmin(poll.Urls, poll.PollingName, poll.Admins.GetList(), admin);
            }
            catch
            {
                throw new Exception("Silme başarısız!");
            }
        }
        public int LastIndex(Poll poll)
        {
            if (poll.Admins.Count == 0)
                return 0;
            return poll.Admins.GetIndex(poll.Admins.Count - 1);
        }
        public void AdminControl(Admin admin)
        {
            ValidationResult result = validations.Validate(admin);
            if (!result.IsValid)
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        private void SwapAdmin(Admin admin1, Admin admin2)
        {
            admin1.Key = admin2.Key;
            admin1.Name = admin2.Name;
            admin1.Password = admin2.Password;
            admin1.Surname = admin2.Surname;
        }
        private bool AdminEquivocation(Admin admin1, Admin admin2)
        {
            try
            {
                return (admin1.Index == admin2.Index &&
                      admin1.Key == admin2.Key &&
                      admin1.Name == admin2.Name &&
                      admin1.Password == admin2.Password &&
                      admin1.Surname == admin2.Surname);
            }
            catch
            {
                return false;
            }
        }
    }
}
