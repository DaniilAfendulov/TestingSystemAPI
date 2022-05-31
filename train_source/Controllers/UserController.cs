using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceProxy;
using Common.Data.Users;
using System.Threading;

namespace Controllers
{
    /// <summary>
    /// Логика работы с пользователями
    /// </summary>
    public class UserController : IDisposable
    {
        /// <summary>
        /// Событие отсоединения от Сервиса
        /// </summary>
        public event Action OnDisconnect;

        /// <summary>
        /// Событие соединения с Сервисом
        /// </summary>
        public event Action OnConnect;

        /// <summary>
        /// прокси-клиент Сервиса
        /// </summary>
        private UserProxy m_userProxy;

        /// <summary>
        /// Текущий залогиненый студент
        /// </summary>
        public static Student CurrentStudent { get; set; }

        /// <summary>
        /// Текущий залогиненый преподаватель
        /// </summary>
        public static Teacher CurrentTeacher { get; set; }

        /// <summary>
        /// Наличие подключения к Сервис
        /// </summary>
        public static bool IsConnected { get; set; }

        /// <summary>
        /// Является ли текущий пользователь администратором
        /// </summary>
        public static bool IsAdmin { get; set; }

        internal UserController()
        {
            m_userProxy = UserProxy.Instance;

            m_userProxy.OnDisconnect += m_userProxy_OnDisconnect;
            m_userProxy.OnConnect += m_userProxy_OnConnect;

            IsAdmin = false;
        }

        private void m_userProxy_OnDisconnect()
        {
            IsConnected = false;
            if (OnDisconnect != null)
                OnDisconnect();
            new Thread(retrieveConnectionProc) { IsBackground = true }.Start();
        }

        /// <summary>
        /// Реакция на установку соединения
        /// </summary>
        private void m_userProxy_OnConnect()
        {
            IsConnected = true;
            if (OnConnect != null)
                OnConnect();
        }

        /// <summary>
        /// Функция восстановления соединения
        /// </summary>
        private void retrieveConnectionProc()
        {
            while (true)
            {
                try
                {
                    if (ReConnect())
                    {
                        if (CurrentTeacher != null)
                            CurrentTeacher = m_userProxy.LoginToMaster(CurrentTeacher.Login, CurrentTeacher.Password);
                        if (CurrentStudent != null)
                            m_userProxy.LoginToLector(CurrentStudent);
                        break;
                    }
                    Thread.Sleep(2000);
                }
                catch { }
            }
        }

        /// <summary>
        /// Получить список всех групп
        /// </summary>
        public List<Group> GetAllGroups()
        {
            return m_userProxy.GetAllGroups();
        }

        /// <summary>
        /// Получить список всех групп по текущему преподавателю
        /// </summary>
        public List<Group> GetGroupsByCurrentTeacher()
        {
            if (UserController.CurrentStudent != null)
                return new List<Group> { m_userProxy.GetAllGroups().First(x => x.ID == UserController.CurrentStudent.GroupId) };
            else
                return m_userProxy.GetGroupsByTeacherId(UserController.CurrentTeacher.ID);
        }

        /// <summary>
        /// Получить список всех преподавателей
        /// </summary>
        public List<Teacher> GetAllTeachers()
        {
            return m_userProxy.GetAllTeachers();
        }

        /// <summary>
        /// Получить список всех студентов группы
        /// </summary>
        public List<Student> GetStudentsByGroupId(Guid _groupId)
        {
            return m_userProxy.GetStudentsByGroupId(_groupId).OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Залогиниться в АРМ Студент
        /// </summary>
        public void LoginToLector(Student _student)
        {
            m_userProxy.LoginToLector(_student);
        }

        /// <summary>
        /// Залогиниться в АРМ Преподаватель
        /// </summary>
        public Teacher LoginToMaster(string _login, string _password)
        {
            var teacher = m_userProxy.LoginToMaster(_login, _password);
            UserController.IsAdmin = m_userProxy.IsAdmin(teacher.ID);
            return teacher;
        }

        /// <summary>
        /// Восстановить соединение
        /// </summary>
        public bool ReConnect()
        {
            bool isConnected = false;
            try
            {
                m_userProxy.ValidateConnect();
                isConnected = true;
            }
            catch
            {
                try
                {
                    m_userProxy.CloseConnection();
                }
                catch { }
            }

            return isConnected;
        }

        public void Dispose()
        {
            if (CurrentTeacher != null)
                m_userProxy.LogoutByTeacher();
            if (CurrentStudent != null)
                m_userProxy.LogoutByStudent();
            m_userProxy.CloseConnection();
        }

        /// <summary>
        /// Удалить преподавателя
        /// </summary>
        public void DeleteTeacher(Teacher _teacher)
        {
            m_userProxy.DeleteTeacher(_teacher.ID);
        }

        /// <summary>
        /// Удалить студента
        /// </summary>
        public void DeleteStudent(Student _student)
        {
            m_userProxy.DeleteStudent(_student.ID);
        }

        /// <summary>
        /// Проверить свободен ли логин
        /// </summary>
        public bool ValidateTeacherLogin(string _login)
        {
            return m_userProxy.ValidateTeacherLogin(_login);
        }

        /// <summary>
        /// Изменить ФИО преподавателя
        /// </summary>
        public void UpdateTeacherName(Guid _id, string _name)
        {
            m_userProxy.UpdateTeacherName(_id, _name);
        }

        /// <summary>
        /// Удалить студента
        /// </summary>
        public void CreateTeacher(Teacher _teacher)
        {
            m_userProxy.CreateTeacher(_teacher);
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        public void CreateGroup(Group _group)
        {
            m_userProxy.CreateGroup(_group);
        }

        /// <summary>
        /// Изменить группу
        /// </summary>
        public void UpdateGroup(Group _group)
        {
            m_userProxy.UpdateGroup(_group);
        }

        /// <summary>
        /// Удалить группу
        /// </summary>
        public void DeleteGroup(Group _group)
        {
            m_userProxy.DeleteGroup(_group.ID);
        }

        /// <summary>
        /// Создать студента
        /// </summary>
        public void CreateStudent(Student _student)
        {
            m_userProxy.CreateStudent(_student);
        }

        /// <summary>
        /// Изменить студента
        /// </summary>
        public void UpdateStudent(Student _student)
        {
            m_userProxy.UpdateStudent(_student);
        }
    }
}
