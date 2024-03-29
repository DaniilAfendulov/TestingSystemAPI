﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.17929
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceProxy.TrainingServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TrainingServiceReference.IServiceInterface")]
    public interface IServiceInterface {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/LoginToMaster", ReplyAction="http://tempuri.org/IServiceInterface/LoginToMasterResponse")]
        Common.Data.Users.Teacher LoginToMaster(string _login, string _password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/PingServer", ReplyAction="http://tempuri.org/IServiceInterface/PingServerResponse")]
        void PingServer();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/LoginToLector", ReplyAction="http://tempuri.org/IServiceInterface/LoginToLectorResponse")]
        void LoginToLector(Common.Data.Users.Student _user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/LogoutByStudent", ReplyAction="http://tempuri.org/IServiceInterface/LogoutByStudentResponse")]
        void LogoutByStudent();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/LogoutByTeacher", ReplyAction="http://tempuri.org/IServiceInterface/LogoutByTeacherResponse")]
        void LogoutByTeacher();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/ValidateConnect", ReplyAction="http://tempuri.org/IServiceInterface/ValidateConnectResponse")]
        void ValidateConnect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/GetAllGroups", ReplyAction="http://tempuri.org/IServiceInterface/GetAllGroupsResponse")]
        System.Collections.Generic.List<Common.Data.Users.Group> GetAllGroups();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/GetAllTeachers", ReplyAction="http://tempuri.org/IServiceInterface/GetAllTeachersResponse")]
        System.Collections.Generic.List<Common.Data.Users.Teacher> GetAllTeachers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/GetStudentsByGroupId", ReplyAction="http://tempuri.org/IServiceInterface/GetStudentsByGroupIdResponse")]
        System.Collections.Generic.List<Common.Data.Users.Student> GetStudentsByGroupId(System.Guid _groupId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/GetGroupsByTeacherId", ReplyAction="http://tempuri.org/IServiceInterface/GetGroupsByTeacherIdResponse")]
        System.Collections.Generic.List<Common.Data.Users.Group> GetGroupsByTeacherId(System.Guid _teacherId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/DeleteTeacher", ReplyAction="http://tempuri.org/IServiceInterface/DeleteTeacherResponse")]
        void DeleteTeacher(System.Guid _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/DeleteStudent", ReplyAction="http://tempuri.org/IServiceInterface/DeleteStudentResponse")]
        void DeleteStudent(System.Guid _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/UpdateTeacherName", ReplyAction="http://tempuri.org/IServiceInterface/UpdateTeacherNameResponse")]
        void UpdateTeacherName(System.Guid _id, string _name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/UpdateTeacherPassword", ReplyAction="http://tempuri.org/IServiceInterface/UpdateTeacherPasswordResponse")]
        void UpdateTeacherPassword(System.Guid _id, string _password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/UpdateStudent", ReplyAction="http://tempuri.org/IServiceInterface/UpdateStudentResponse")]
        void UpdateStudent(Common.Data.Users.Student _student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/CreateTeacher", ReplyAction="http://tempuri.org/IServiceInterface/CreateTeacherResponse")]
        void CreateTeacher(Common.Data.Users.Teacher _teacher);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/CreateStudent", ReplyAction="http://tempuri.org/IServiceInterface/CreateStudentResponse")]
        void CreateStudent(Common.Data.Users.Student _student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/ValidateTeacherLogin", ReplyAction="http://tempuri.org/IServiceInterface/ValidateTeacherLoginResponse")]
        bool ValidateTeacherLogin(string _login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/CreateGroup", ReplyAction="http://tempuri.org/IServiceInterface/CreateGroupResponse")]
        void CreateGroup(Common.Data.Users.Group _group);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/UpdateGroup", ReplyAction="http://tempuri.org/IServiceInterface/UpdateGroupResponse")]
        void UpdateGroup(Common.Data.Users.Group _group);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/DeleteGroup", ReplyAction="http://tempuri.org/IServiceInterface/DeleteGroupResponse")]
        void DeleteGroup(System.Guid _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/CreateStatistic", ReplyAction="http://tempuri.org/IServiceInterface/CreateStatisticResponse")]
        void CreateStatistic(Common.Data.Statistics.TestStatistic _statistic);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/UpdateStatistic", ReplyAction="http://tempuri.org/IServiceInterface/UpdateStatisticResponse")]
        void UpdateStatistic(Common.Data.Statistics.TestStatistic _statistic);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/GetDifficultyStatistic", ReplyAction="http://tempuri.org/IServiceInterface/GetDifficultyStatisticResponse")]
        System.Collections.Generic.List<Common.Data.Statistics.DifficultyStatistic> GetDifficultyStatistic(System.Collections.Generic.List<System.Guid> _testIds);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/GetStudentModuleStatistic", ReplyAction="http://tempuri.org/IServiceInterface/GetStudentModuleStatisticResponse")]
        System.Collections.Generic.List<Common.Data.Statistics.StudentModuleStatistic> GetStudentModuleStatistic(System.Collections.Generic.List<Common.Data.Content.Lesson> _lessons, System.Collections.Generic.List<Common.Data.Content.TheoryTest> _theoryTests, System.Collections.Generic.List<Common.Data.Content.PracticeTest> _practiceTests, System.Guid _groupId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/IsAdmin", ReplyAction="http://tempuri.org/IServiceInterface/IsAdminResponse")]
        bool IsAdmin(System.Guid _id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceInterfaceChannel : ServiceProxy.TrainingServiceReference.IServiceInterface, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceInterfaceClient : System.ServiceModel.ClientBase<ServiceProxy.TrainingServiceReference.IServiceInterface>, ServiceProxy.TrainingServiceReference.IServiceInterface {
        
        public ServiceInterfaceClient() {
        }
        
        public ServiceInterfaceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceInterfaceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceInterfaceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceInterfaceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Common.Data.Users.Teacher LoginToMaster(string _login, string _password) {
            return base.Channel.LoginToMaster(_login, _password);
        }
        
        public void PingServer() {
            base.Channel.PingServer();
        }
        
        public void LoginToLector(Common.Data.Users.Student _user) {
            base.Channel.LoginToLector(_user);
        }
        
        public void LogoutByStudent() {
            base.Channel.LogoutByStudent();
        }
        
        public void LogoutByTeacher() {
            base.Channel.LogoutByTeacher();
        }
        
        public void ValidateConnect() {
            base.Channel.ValidateConnect();
        }
        
        public System.Collections.Generic.List<Common.Data.Users.Group> GetAllGroups() {
            return base.Channel.GetAllGroups();
        }
        
        public System.Collections.Generic.List<Common.Data.Users.Teacher> GetAllTeachers() {
            return base.Channel.GetAllTeachers();
        }
        
        public System.Collections.Generic.List<Common.Data.Users.Student> GetStudentsByGroupId(System.Guid _groupId) {
            return base.Channel.GetStudentsByGroupId(_groupId);
        }
        
        public System.Collections.Generic.List<Common.Data.Users.Group> GetGroupsByTeacherId(System.Guid _teacherId) {
            return base.Channel.GetGroupsByTeacherId(_teacherId);
        }
        
        public void DeleteTeacher(System.Guid _id) {
            base.Channel.DeleteTeacher(_id);
        }
        
        public void DeleteStudent(System.Guid _id) {
            base.Channel.DeleteStudent(_id);
        }
        
        public void UpdateTeacherName(System.Guid _id, string _name) {
            base.Channel.UpdateTeacherName(_id, _name);
        }
        
        public void UpdateTeacherPassword(System.Guid _id, string _password) {
            base.Channel.UpdateTeacherPassword(_id, _password);
        }
        
        public void UpdateStudent(Common.Data.Users.Student _student) {
            base.Channel.UpdateStudent(_student);
        }
        
        public void CreateTeacher(Common.Data.Users.Teacher _teacher) {
            base.Channel.CreateTeacher(_teacher);
        }
        
        public void CreateStudent(Common.Data.Users.Student _student) {
            base.Channel.CreateStudent(_student);
        }
        
        public bool ValidateTeacherLogin(string _login) {
            return base.Channel.ValidateTeacherLogin(_login);
        }
        
        public void CreateGroup(Common.Data.Users.Group _group) {
            base.Channel.CreateGroup(_group);
        }
        
        public void UpdateGroup(Common.Data.Users.Group _group) {
            base.Channel.UpdateGroup(_group);
        }
        
        public void DeleteGroup(System.Guid _id) {
            base.Channel.DeleteGroup(_id);
        }
        
        public void CreateStatistic(Common.Data.Statistics.TestStatistic _statistic) {
            base.Channel.CreateStatistic(_statistic);
        }
        
        public void UpdateStatistic(Common.Data.Statistics.TestStatistic _statistic) {
            base.Channel.UpdateStatistic(_statistic);
        }
        
        public System.Collections.Generic.List<Common.Data.Statistics.DifficultyStatistic> GetDifficultyStatistic(System.Collections.Generic.List<System.Guid> _testIds) {
            return base.Channel.GetDifficultyStatistic(_testIds);
        }
        
        public System.Collections.Generic.List<Common.Data.Statistics.StudentModuleStatistic> GetStudentModuleStatistic(System.Collections.Generic.List<Common.Data.Content.Lesson> _lessons, System.Collections.Generic.List<Common.Data.Content.TheoryTest> _theoryTests, System.Collections.Generic.List<Common.Data.Content.PracticeTest> _practiceTests, System.Guid _groupId) {
            return base.Channel.GetStudentModuleStatistic(_lessons, _theoryTests, _practiceTests, _groupId);
        }
        
        public bool IsAdmin(System.Guid _id) {
            return base.Channel.IsAdmin(_id);
        }
    }
}
