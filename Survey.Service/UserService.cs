using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Survey.Common;
using Survey.Data.Infrastructure;
using Survey.Data.Repositories;
using Survey.Model.Abstract;
using Survey.Model.Models;

namespace Survey.Service
{
    public interface IUserService : IBaseService
    {
        UpdateResult Register(User user);

        UpdateResult Login(string username,
                           string password);

        UpdateResult Update(User user);

        UpdateResult Delete(int userId);

        UpdateResult Delete(User user);

        UpdateResult ChangePassword(int userId,
                                    string password,
                                    string newPassword);

        IEnumerable<User> GetAll();

        IEnumerable<User> GetMulti(Expression<Func<User, bool>> predicate);

        IEnumerable<User> GetMultiPaging(Expression<Func<User, bool>> predicate,
                                         Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
                                         out int total,
                                         int index = 0,
                                         int size = 20);

        User GetById(int id);

        User GetByUsername(string username);
    }

    public class UserService : BaseService,
                               IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(ILogRepository logRepository,
                           IUnitOfWork unitOfWork,
                           IUserRepository userRepository) : base(logRepository,
                                                                  unitOfWork)
        {
            _userRepository = userRepository;
        }

        #region IUserService Members

        public UpdateResult Register(User user)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                user.Username = user.Username.Trim()
                                    .ToLower();

                User existUser = _userRepository.GetSingleByCondition(c => string.Equals(c.Username,
                                                                                         user.Username));
                if(existUser == null)
                {
                    string salt = StringHelpers.CreateSalt();
                    string password = StringHelpers.CreatePasswordHash(user.Password,
                                                                       salt);

                    user.PasswordSalt = salt;
                    user.Password = password;

                    _userRepository.Add(user);
                }
                else
                {
                    result.State = 4;
                    switch (existUser.Status)
                    {
                        case UserStatus.ACTIVE:
                            result.KeyLanguage = $"Tên đăng nhập {user.Username} đã được đăng ký. Vui lòng thử lại hoặc liên hệ quản trị viên";
                            break;
                        case UserStatus.DENIED:
                            result.KeyLanguage = $"Tên đăng nhập {user.Username} đã bị từ chối đăng ký. Vui lòng liên hệ quản trị viên";
                            break;
                        case UserStatus.LOCKED:
                            result.KeyLanguage = $"Tên đăng nhập {user.Username} đang tạm khóa. Vui lòng liên hệ quản trị viên";
                            break;
                        case UserStatus.PENDING:
                            result.KeyLanguage = $"Tên đăng nhập {user.Username} đang chờ duyệt. Vui lòng thử lại sau";
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            $"Error when register user {user.Username}");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }

            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public UpdateResult Login(string username,
                                  string password)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                username = username.Trim()
                                   .ToLower();
                User user = _userRepository.GetSingleByCondition(c => string.Equals(c.Username,
                                                                                    username));

                if(user != null)
                {
                    switch (user.Status)
                    {
                        case UserStatus.ACTIVE:
                            if(string.Equals(user.Password,
                                             StringHelpers.CreatePasswordHash(password,
                                                                              user.PasswordSalt)))
                            {
                                result.Value = user;
                                user.LastLoginDate = DateTime.Now;
                                _userRepository.Update(user);
                            }
                            else
                            {
                                result.State = 4;
                                result.KeyLanguage = "Mật khẩu không chính xác. Vui lòng thử lại";
                            }
                            break;
                        case UserStatus.DENIED:
                            result.State = 4;
                            result.KeyLanguage = $"Tên đăng nhập {user.Username} đã bị từ chối đăng ký. Vui lòng liên hệ quản trị viên";
                            break;
                        case UserStatus.LOCKED:
                            result.State = 4;
                            result.KeyLanguage = $"Tên đăng nhập {user.Username} đang tạm khóa. Vui lòng liên hệ quản trị viên";
                            break;
                        case UserStatus.PENDING:
                            result.State = 4;
                            result.KeyLanguage = $"Tên đăng nhập {user.Username} đang chờ duyệt. Vui lòng thử lại sau";
                            break;
                    }
                }
                else
                {
                    result.State = 4;
                    result.KeyLanguage = $"Tên đăng nhập {username} không tồn tại trong hệ thống. Vui lòng đăng ký mới";
                }
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            $"Error when login user {username}");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public UpdateResult Update(User user)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                User existUser = _userRepository.GetSingleById(user.Id);
                if(existUser != null)
                {
                    existUser.Role = user.Role;
                    existUser.Name = user.Name;
                    existUser.Origanization = user.Origanization;
                    existUser.Specification = user.Specification;
                    existUser.Status = user.Status;
                    if(!string.IsNullOrEmpty(user.Password))
                    {
                        string salt = StringHelpers.CreateSalt();
                        string password = StringHelpers.CreatePasswordHash(user.Password,
                                                                           salt);
                        existUser.PasswordSalt = salt;
                        existUser.Password = password;
                    }

                    _userRepository.Update(existUser);
                }
                else
                {
                    result.State = 4;
                    result.KeyLanguage = UpdateResult.USER_NOT_FOUND;
                }
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            $"Error when update user {user.Username}");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public UpdateResult Delete(int userId)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                _userRepository.Delete(userId);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            $"Error when delete user id {userId}");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public UpdateResult Delete(User user)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                _userRepository.Delete(user);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            $"Error when delete user {user.Username}");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public UpdateResult ChangePassword(int userId,
                                           string password,
                                           string newPassword)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                User user = _userRepository.GetSingleById(userId);
                if(user != null)
                {
                    if(string.Equals(user.Password,
                                     StringHelpers.CreatePasswordHash(password,
                                                                      user.PasswordSalt)))
                    {
                        string salt = StringHelpers.CreateSalt();
                        string passwordSalt = StringHelpers.CreatePasswordHash(newPassword,
                                                                               salt);
                        user.PasswordSalt = salt;
                        user.Password = passwordSalt;

                        _userRepository.Update(user);
                    }
                    else
                    {
                        result.State = 4;
                        result.KeyLanguage = "Mật khẩu cũ không chính xác";
                    }
                }
                else
                {
                    result.State = 4;
                    result.KeyLanguage = UpdateResult.USER_NOT_FOUND;
                }
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            $"Error when update password user {userId}");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return _userRepository.GetAll();
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when get all users");
            }
            return null;
        }

        public IEnumerable<User> GetMulti(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return _userRepository.GetMulti(predicate);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when get multi users");
            }
            return null;
        }

        public IEnumerable<User> GetMultiPaging(Expression<Func<User, bool>> predicate,
                                                Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
                                                out int total,
                                                int index = 0,
                                                int size = 20)
        {
            try
            {
                return _userRepository.GetMultiPaging(predicate,
                                                      orderBy,
                                                      out total,
                                                      index,
                                                      size);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when get multi paging users");
            }
            total = 0;
            return null;
        }

        public User GetById(int id)
        {
            try
            {
                return _userRepository.GetSingleById(id);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when get user by id");
            }
            return null;
        }

        public User GetByUsername(string username)
        {
            username = username.Trim()
                               .ToLower();
            User user = _userRepository.GetSingleByCondition(c => string.Equals(c.Username.ToLower(),
                                                                                username));
            return user;
        }

        #endregion
    }
}
