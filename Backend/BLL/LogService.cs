using AutoMapper;
using BOL;
using BOL.Dto;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LogService
    {
        public static bool SetLog(LogDto log)
        {
            if (log == null)
            {
                return false;
            }
            else
            {
                var _log = new Log()
                {
                    Country = log.Country,
                    Ip = log.Ip,
                    CreatedAt = DateTime.Now,
                    EndAt = DateTime.Now.AddMinutes(10),
                    Status = 0,
                    Token = Guid.NewGuid().ToString(),
                    FK_Users_Id = log.FK_Users_Id

                };
                DataAccessFactory.LogDataAccess().Add(_log);
                return true;
            }
        }
        public static IEnumerable<LogDto> GetAllLog()
        {
            return DataAccessFactory.LogDataAccess().Get().Select(Mapper.Map<BOL.Log, LogDto>);
        }
        public static string GetTokenByUsername(string _username)
        {
            var _user = UserService.GetUserByUsername(_username); 
            if(_user == null) { return null; }
            else
            {
                var temp =  DataAccessFactory.LogDataAccess().Get().LastOrDefault(x => x.FK_Users_Id == _user.Id);
                if(temp != null) { return temp.Token; }
                else { return null; }
            }
        }
        // Update the Log table status when anyone login with right credentials
        public static bool UpdateLogTimeAndStatus(int id)
        {
            var _log = DataAccessFactory.LogDataAccess().Get().LastOrDefault(x => x.FK_Users_Id == id);
            
            if (_log == null)
            {
                return true;
            }
            else
            {
                if (_log.EndAt < DateTime.Now)
                {
                    _log.Status = 1;
                    DataAccessFactory.LogDataAccess().Edit(_log);
                    return true;
                }
                else
                {
                    return false;
                }
            }
           
            
        }
    }
}
