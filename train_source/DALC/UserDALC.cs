using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data.Users;
using NHibernate;
using NHibernate.Criterion;

namespace DALC
{
    /// <summary>
    /// Класс для работы с БД по пользователям
    /// </summary>
    public class UserDALC
    {
        //хэш пароля для admin "ALSr87c0H0K2TiohRZycEhjAe6GEyCCbfjavZ9y2tG4yR/hSlv7czY2TkK+Jn9gAmA=="}
        private UserDALC()
        {
        }

        private static UserDALC m_instance;
        private static object m_lock = new object();

        public static UserDALC Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_lock)
                    {
                        if (m_instance == null)
                            m_instance = new UserDALC();
                    }
                }
                return m_instance;
            }
        }

        #region Group

        /// <summary>
        /// Создать группу
        /// </summary>
        public void CreateGroup(Group _group)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(_group, _group.ID);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Изменить группу
        /// </summary>
        public void UpdateGroup(Group _group)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(_group);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Удалить группу
        /// </summary>
        public void DeleteGroup(Guid _id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var group = session.Get<Group>(_id);
                    session.Delete(group);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Получить все группы
        /// </summary>
        public List<Group> GetAllGroups()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var groups = session
                        .CreateCriteria<Group>()
                        .List<Group>()
                        .ToList();
                    transaction.Rollback();
                    return groups;
                }
            }
        }

        /// <summary>
        /// Получить группу по её ID
        /// </summary>
        public Group GetGroupById(Guid _groupId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var group = session.Get<Group>(_groupId);
                    transaction.Rollback();
                    return group;
                }
            }
        }

        /// <summary>
        /// Получить группы по преподавателю
        /// </summary>
        public List<Group> GetGroupsByTeacherId(Guid _teacherId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var groups = session
                        .CreateCriteria<Group>()
                        .Add(Restrictions.Eq("TeacherId", _teacherId))
                        .List<Group>()
                        .ToList();
                    transaction.Rollback();
                    return groups;
                }
            }
        }

        #endregion

        #region Teacher

        /// <summary>
        /// Создать преподавателя
        /// </summary>
        public void CreateTeacher(Teacher _teacher)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(_teacher, _teacher.ID);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Изменить преподавателя
        /// </summary>
        public void UpdateTeacher(Teacher _teacher)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(_teacher);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Удалить преподавателя
        /// </summary>
        public void DeleteTeacher(Guid _id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var teacher = session.Get<Teacher>(_id);
                    session.Delete(teacher);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Получить список всех преподавателей
        /// </summary>
        public List<Teacher> GetAllTeachers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var teachers = session
                        .CreateCriteria<Teacher>()
                        .List<Teacher>()
                        .ToList();
                    transaction.Rollback();
                    return teachers;
                }
            }
        }

        /// <summary>
        /// Получить преподавателя по его логину
        /// </summary>
        public Teacher GetTeacherByLogin(string _login)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var teacher = session
                        .CreateCriteria<Teacher>()
                        .Add(Restrictions.Eq("Login", _login))
                        .UniqueResult<Teacher>();
                    transaction.Rollback();
                    return teacher;
                }
            }
        }

        /// <summary>
        /// Получить преподавателя по его ID
        /// </summary>
        public Teacher GetTeacherById(Guid _teacherId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var teacher = session.Get<Teacher>(_teacherId);
                    transaction.Rollback();
                    return teacher;
                }
            }
        }

        /// <summary>
        /// Проставить признак IsOnline
        /// </summary>
        public void SetTeacherIsOnline(Guid _id, bool _isOnline)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var teacher = session.Get<Teacher>(_id);
                    teacher.IsOnline = _isOnline;
                    session.Update(teacher);
                    transaction.Commit();
                }
            }
        }

        #endregion

        #region Student

        /// <summary>
        /// Создать студента
        /// </summary>
        public void CreateStudent(Student _student)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(_student, _student.ID);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Изменить студента
        /// </summary>
        public void UpdateStudent(Student _student)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(_student);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Удалить студента
        /// </summary>
        public void DeleteStudent(Guid _id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var student = session.Get<Student>(_id);
                    session.Delete(student);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Получить список всех студентов
        /// </summary>
        public List<Student> GetAllStudents()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var students = session
                        .CreateCriteria<Student>()
                        .List<Student>()
                        .ToList();
                    transaction.Rollback();
                    return students;
                }
            }
        }

        /// <summary>
        /// Получить студентов группы
        /// </summary>
        public List<Student> GetStudentsByGroupId(Guid _groupId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var students = session
                        .CreateCriteria<Student>()
                        .Add(Restrictions.Eq("GroupId", _groupId))
                        .List<Student>()
                        .ToList();
                    transaction.Rollback();
                    return students;
                }
            }
        }

        /// <summary>
        /// Получить студента по его ID
        /// </summary>
        public Student GetStudentById(Guid _id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var student = session.Get<Student>(_id);
                    transaction.Rollback();
                    return student;
                }
            }
        }

        /// <summary>
        /// Проставить признак IsOnline
        /// </summary>
        public void SetStudentIsOnline(Guid _id, bool _isOnline)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var student = session.Get<Student>(_id);
                    student.IsOnline = _isOnline;
                    session.Update(student);
                    transaction.Commit();
                }
            }
        }

        #endregion   
    
        # region Admin

        /// <summary>
        /// Проставить администратора по идентификатору
        /// </summary>
        public Admin GetAdminById(Guid _id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var admin = session.Get<Admin>(_id);
                    transaction.Rollback();
                    return admin;
                }
            }
        }

        /// <summary>
        /// Проставить администратора по логину
        /// </summary>
        public Admin GetAdminByLogin(string _login)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var admin = session
                        .CreateCriteria<Admin>()
                        .Add(Restrictions.Eq("Login", _login))
                        .UniqueResult<Admin>();
                    transaction.Rollback();
                    return admin;
                }
            }
        }

        /// <summary>
        /// Проставить признак IsOnline
        /// </summary>
        public void SetAdminIsOnline(Guid _id, bool _isOnline)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var teacher = session.Get<Admin>(_id);
                    teacher.IsOnline = _isOnline;
                    session.Update(teacher);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Проставить всех администраторов
        /// </summary>
        public List<Admin> GetAllAdmins()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var admins = session
                        .CreateCriteria<Admin>()
                        .List<Admin>()
                        .ToList();
                    transaction.Rollback();
                    return admins;
                }
            }
        }

        #endregion
    }
}
