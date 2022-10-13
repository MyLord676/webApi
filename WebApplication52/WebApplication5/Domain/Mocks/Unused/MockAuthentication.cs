//using WebApplication2.Data;
//using WebApplication2.Domain.Entities;
//using WebApplication5.Domain.Entities;
//using WebApplication5.Domain.Interfaces;

//namespace WebApplication5.Domain.Mocks
//{
//    public class MockAuthentication : IAuthentication
//    {
//        public User Login(UserAuth userAuth)
//        {
//            var values = Repository.GetAllValues<User, DataBaseContext>();
//            return values.Result.Where(user => user.NickName == userAuth.NickName).First();
//        }

//        public bool Logout(string token)
//        {
//            throw new NotImplementedException();
//        }

//        public string RefreshToken(string refreshToken)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
