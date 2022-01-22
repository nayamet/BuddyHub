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
    public class RecoveryPasswordService
    {
        public static void Add(RecoveryPasswordDto rp)
        {
            DataAccessFactory.RecoveryPasswordDataAccess().Add(Mapper.Map<RecoveryPassword>(rp));
        }

        public static RecoveryPasswordDto GetRecoveryPasswordByUserId(int id)
        {
            var recopass = DataAccessFactory.RecoveryPasswordDataAccess().Get().Where(r => r.FK_Users_Id == id).FirstOrDefault();
            if(recopass == null)
            {
                return null;
            }
            var rp = Mapper.Map<RecoveryPassword, RecoveryPasswordDto>(recopass);
            return rp;
        }

        public static RecoveryPasswordDto GetRecoveryPasswordByUsername(string username)
        {
            var _User = DataAccessFactory.UserDataAccess().Get().Where(u => u.Username == username).FirstOrDefault();
            if (_User != null)
            {
                return GetRecoveryPasswordByUserId(_User.Id);
            }
            else
            {
                return null;
            }
        }

        public static bool EditRecoveryPassword(RecoveryPasswordDto rp)
        {
            var _rp = DataAccessFactory.RecoveryPasswordDataAccess().Get(rp.Id);

            if (_rp == null) { return false; }
            else
            {
                DataAccessFactory.RecoveryPasswordDataAccess().Edit(Mapper.Map<RecoveryPasswordDto, RecoveryPassword>(rp));
                return true;
            }
        }
    }
}
