using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Users;
using System.Threading;

namespace ServiceProxy
{
    public class UserProxy
    {
        public event Action OnDisconnect;
        public event Action OnConnect;

        private Thread m_pingThread;

        private static UserProxy m_instance;
        private static object m_lock = new object();

        public static UserProxy Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_lock)
                    {
                        if (m_instance == null) 
                            m_instance = new UserProxy();
                    }
                }
                return m_instance;
            }
        }

        public Teacher LoginToMaster(string _login, string _password)
        {
            var teacher = TrainingClient.Client.LoginToMaster(_login, _password);

            m_pingThread = new Thread(pingProc);
            m_pingThread.IsBackground = true;
            m_pingThread.Start();

            return teacher;
        }

        private void pingProc()
        {
            try
            {
                while (true)
                {
                    PingServer();
                    Thread.Sleep(10000);
                }
            }
            catch (Exception)
            {
                if (OnDisconnect != null)
                    OnDisconnect();
            }
        }

        public void LoginToLector(Student _student)
        {
            TrainingClient.Client.LoginToLector(_student);

            m_pingThread = new Thread(pingProc);
            m_pingThread.IsBackground = true;
            m_pingThread.Start();
        }

        public List<Group> GetAllGroups()
        {
            return TrainingClient.Client.GetAllGroups();
        }

        public List<Student> GetStudentsByGroupId(Guid _groupId)
        {
            return TrainingClient.Client.GetStudentsByGroupId(_groupId);
        }

        public void PingServer()
        {
            TrainingClient.Client.PingServer();
        }

        public void ValidateConnect()
        {
            TrainingClient.Client.ValidateConnect();
            if (OnConnect != null)
                OnConnect();
        }

        public void CloseConnection()
        {
            TrainingClient.CloseConnection();
        }

        public void LogoutByTeacher()
        {
            TrainingClient.Client.LogoutByTeacher();
        }

        public void LogoutByStudent()
        {
            TrainingClient.Client.LogoutByStudent();
        }

        public List<Teacher> GetAllTeachers()
        {
            return TrainingClient.Client.GetAllTeachers();
        }

        public void DeleteTeacher(Guid _id)
        {
            TrainingClient.Client.DeleteTeacher(_id);
        }

        public void DeleteStudent(Guid _id)
        {
            TrainingClient.Client.DeleteStudent(_id);
        }

        public bool ValidateTeacherLogin(string _login)
        {
            return TrainingClient.Client.ValidateTeacherLogin(_login);
        }

        public void CreateTeacher(Teacher _teacher)
        {
            TrainingClient.Client.CreateTeacher(_teacher);
        }

        public void CreateStudent(Student _student)
        {
            TrainingClient.Client.CreateStudent(_student);
        }

        public void UpdateTeacherName(Guid _id, string _name)
        {
            TrainingClient.Client.UpdateTeacherName(_id, _name);
        }

        public void UpdateTeacherPassword(Guid _id, string _password)
        {
            TrainingClient.Client.UpdateTeacherPassword(_id, _password);
        }

        public void UpdateStudent(Student _student)
        {
            TrainingClient.Client.UpdateStudent(_student);
        }

        public void UpdateGroup(Group _group)
        {
            TrainingClient.Client.UpdateGroup(_group);
        }

        public void CreateGroup(Group _group)
        {
            TrainingClient.Client.CreateGroup(_group);
        }

        public void DeleteGroup(Guid _id)
        {
            TrainingClient.Client.DeleteGroup(_id);
        }

        public bool IsAdmin(Guid _id)
        {
            return TrainingClient.Client.IsAdmin(_id);
        }

        public List<Group> GetGroupsByTeacherId(Guid _teacherId)
        {
            return TrainingClient.Client.GetGroupsByTeacherId(_teacherId);
        }
    }
}
