using ServerBox.Lib;
using ServerBox.Model;
using LiteDB;

namespace ServerBox.Service
{
    class UserService
    {
        private DatabaseHelper db;
        private LiteCollection<User> user;

        public UserService(DatabaseHelper db)
        {
            this.db = db;
            user = this.db.GetConnection().GetCollection<User>("user");
        }

        public bool HasPasscode()
        {
            var result = user.FindOne(x => x.Key.Equals("MAIN"));
            if (result == null)
            {
                InitializeUserTable();
                return false;
            } else if (result.Value == null || result.Value == "")
            {
                return false;
            } else
            {
                return true;
            }
        }

        public void SetPasscode(string passcode)
        {
            var result = user.FindOne(x => x.Key.Equals("MAIN"));
            if (result == null)
            {
                InitializeUserTable();
                result = user.FindOne(x => x.Key.Equals("MAIN"));
            }
            result.Value = passcode;
            user.Update(result);
        }

        private void InitializeUserTable()
        {
            User newData = new User{ Key="MAIN", Value="" };
            user.Insert(newData);
        }

        public bool CheckPasscode(string passcode)
        {
            var result = user.FindOne(x => x.Key.Equals("MAIN"));
            if (result != null && result.Value.Equals(passcode))
            {
                return true;
            }
            return false;
        }

    }
}
