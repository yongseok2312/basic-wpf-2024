namespace ex07_EmployeeMngApp.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public required string EmpName { get; set; }
        public required string Salary { get; set; }

        public int DeptName { get; set; }
        public int Addr { get; set; }

        public static readonly string SELECT_QUERY =  @"SELECT [Id]
                                                              ,[EmpName]
                                                              ,[Salary]
                                                              ,[DeptName]
                                                              ,[Addr]
                                                          FROM [Employees]";
        public static readonly string INSERT_QUERY = @"INSERT INTO [Employees]
                                                                       ([EmpName]
                                                                       ,[Salary]
                                                                       ,[DeptName]
                                                                       ,[Addr])
                                                                 VALUES
                                                                       (@EmpName
                                                                       ,@Salary
                                                                       ,@DeptName
                                                                       ,@Addr)";
        public static readonly string UPDATE_QUERY = @"UPDATE [Employees]
                                                                   SET [EmpName] = @EmpName
                                                                      ,[Salary] = @Salary
                                                                      ,[DeptName] = @DeptName
                                                                      ,[Addr] = @Addr
                                                                 WHERE Id = @Id";
        public static readonly string DELETE_QUERY = @"DELETE FROM [Employees]
                                                                 WHERE Id=@Id";
    }
}
