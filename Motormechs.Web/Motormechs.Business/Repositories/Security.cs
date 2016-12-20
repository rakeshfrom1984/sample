using Motormechs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motormechs.Business.Repositories
{
    public class Security:IDisposable
    {
        #region Global Variable.............................

        static MotorMechsEntities obj;

        #endregion

        /// <summary>
        /// check if token present or not
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns>
        /// if user has the same token then return true
        /// if user has not same token then return false
        /// </returns>
        public static bool CheckToken(int userId, string token)
        {
            User eUser = new User();
            bool status = false;
            using (obj = new MotorMechsEntities())
            {
                eUser = obj.Users.Where(l => l.Id == userId).SingleOrDefault();
            }
            if (eUser != null)
            {
                status = eUser.Token == token ? true : false;
            }

            return status;
        }

        public void Dispose()
        {
            obj.Dispose();
            this.Dispose();
        }
    }
}
