using System.Linq;
using Microsoft.EntityFrameworkCore;
using NursingModel.Form;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using NursingCommon;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace NursingModel.TaskModels
{
    public partial class TaskDbContext : DbContext
    {
        public TaskDbContext()
        {
        }

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }

        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {
            new DebugLoggerProvider()
        });

        public virtual DbSet<DrugExecuteConfig> DrugExecuteConfig { get; set; }
        public virtual DbSet<EventTime> EventTime { get; set; }
        public virtual DbSet<OrderExecuteRemind> OrderExecuteRemind { get; set; }
        public virtual DbSet<OrderExecuteTaskConfig> OrderExecuteTaskConfig { get; set; }
        public virtual DbSet<OrderTaskFormConfig> OrderTaskFormConfig { get; set; }
        public virtual DbSet<PatNursingTask> PatNursingTask { get; set; }
        public virtual DbSet<PatNursingTaskFinish> PatNursingTaskFinish { get; set; }
        public virtual DbSet<SignTaskConfig> SignTaskConfig { get; set; }
        public virtual DbSet<SupdevTaskConfig> SupdevTaskConfig { get; set; }
        public virtual DbSet<TaskEvent> TaskEvent { get; set; }
        public virtual DbSet<TaskFormConfig> TaskFormConfig { get; set; }
        public virtual DbSet<TaskFormTiming> TaskFormTiming { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<TimingList> TimingList { get; set; }
        public virtual DbSet<WardOrderTaskForm> WardOrderTaskForm { get; set; }
        public virtual DbSet<WardSignTask> WardSignTask { get; set; }
        public virtual DbSet<WardTaskFormConfig> WardTaskFormConfig { get; set; }

        public virtual DbSet<NursingRecForm> NursingRecForm { get; set; }
        public virtual DbSet<NursingRecFormItem> NursingRecFormItem { get; set; }
        //public virtual DbSet<NursingRecFormItemListValues> NursingRecFormItemListValues { get; set; }
        public virtual DbSet<FormList> FormList { get; set; }
        public virtual DbSet<FormItem> FormItems { get; set; }
        public virtual DbSet<FormItemListValue> FormItemListValues { get; set; }
        public virtual DbSet<FormScore> FormScores { get; set; }
        public virtual DbSet<FormNiList> FormNiLists { get; set; }

        public virtual DbSet<EFPatObservRec> EFPatObservRec { get; set; }
        public virtual DbSet<PatScaleFormRec> PatScaleFormRec { get; set; }
        public virtual DbSet<PatScaleFormRecDetail> PatScaleFormRecDetail { get; set; }
        public virtual DbSet<PatScaleFormComboItemDetail> PatScaleFormComboItemDetail { get; set; }
        public virtual DbSet<PatFormNiRecDetail> PatFormNiRecDetail { get; set; }

        public virtual DbSet<PatAsFormRec> PatAsFormRec { get; set; }
        public virtual DbSet<PatAsFormRecDetail> PatAsFormRecDetail { get; set; }
        public virtual DbSet<PatAsFormComboItemDetail> PatAsFormComboItemDetail { get; set; }
        public virtual DbSet<PatNurseDataRec> PatNurseDataRec { get; set; }

        public virtual DbSet<PatNurseDataRecLayout> PatNurseDataRecLayout { get; set; }

        public virtual DbSet<PatSupdevList> PatSupdevList { get; set; }
        public virtual DbSet<PatSupdevOperating> PatSupdevOperating { get; set; }

        // Unable to generate entity type for table 'ZLNTASK.WARD_TIMING_LIST'. Please see the warning messages.
        // Unable to generate entity type for table 'ZLNTASK.PAT_NURSING_TASK_PROCESS'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DataConnect dataConnect = SiteConfig.DataConnectList.Where(o => o.name == "zlapex").FirstOrDefault();
                string conn = string.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3};Password={4}", dataConnect.HOST, dataConnect.Port, dataConnect.Service, dataConnect.User, dataConnect.Password);
                optionsBuilder.UseOracle(conn);
                optionsBuilder.UseLoggerFactory(MyLoggerFactory).UseOracle(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");
            //.HasAnnotation("Relational:DefaultSchema", "ZLNTASK");


            //});
            //评分表项目
            modelBuilder.Entity<FormList>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.FormVersion });
                entity.HasMany(e => e.FormItems).WithOne().HasForeignKey(e => new { e.FormId, e.FormVersion });
                entity.HasMany(e => e.FormScores).WithOne().HasForeignKey(e => new { e.FormId, e.FormVersion });
                entity.HasMany(e => e.FormNiLists).WithOne().HasForeignKey(e => new { e.FormId, e.FormVersion });
            });

            //表单项目选项
            //评分表项目,配置对应表单的外键
            modelBuilder.Entity<FormItem>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.FormVersion, e.ItemId });
                entity.HasMany(e => e.FormItemListValues).WithOne().HasForeignKey(e => new { e.FormId, e.FormVersion, e.ItemId });
            });
            //表单值域
            modelBuilder.Entity<FormItemListValue>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.FormVersion, e.ItemId, e.Display });
            });

            //表单分数
            modelBuilder.Entity<FormScore>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.FormVersion, e.ResultObservItemId });
            });

            //表单护理措施
            modelBuilder.Entity<FormNiList>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.FormVersion, e.NiListId });
            });


            //护理记录单
            modelBuilder.Entity<NursingRecForm>(entity =>
            {
                entity.HasKey(e => new { e.FormId });
            });

            //护理记录项目
            modelBuilder.Entity<NursingRecFormItem>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.ItemId });
            });

            ////护理记录项目选项
            //modelBuilder.Entity<NursingRecFormItemListValues>(entity =>
            //{
            //    entity.HasKey(e => new { e.FormId, e.ObservItemId });
            //});

            //病人所见项
            modelBuilder.Entity<EFPatObservRec>(entity =>
            {
                entity.HasKey(e => e.ObservIndexId);
            });

            //病人评分表记录
            modelBuilder.Entity<PatScaleFormRec>(entity =>
            {
                entity.HasKey(e => e.ScaleRecId);
                entity.HasMany(e => e.PatScaleFormRecDetails).WithOne().HasForeignKey(e => e.ScaleRecId);
                entity.HasMany(e => e.PatFormNiLists).WithOne().HasForeignKey(e => e.ScaleRecId);
            });

            //评分表护理措施
            modelBuilder.Entity<PatFormNiRecDetail>(entity =>
            {
                entity.HasKey(o => o.PatFormNiRecId);

            });
            //病人评分表明细记录
            modelBuilder.Entity<PatScaleFormRecDetail>(entity =>
            {
                entity.HasKey(e => e.ScaleRecDetailId);
                entity.HasKey(c => new { c.ScaleRecId, c.ScaleItemId });
                entity.HasMany(e => e.PatScaleFormComboItemDetail).WithOne().HasForeignKey(j => new { j.ScaleRecId, j.ScaleItemId });
            });

            //病人评分表组合项目的子项记录
            modelBuilder.Entity<PatScaleFormComboItemDetail>(entity =>
            {
                entity.HasKey(e => new { e.ScaleRecId, e.ScaleItemId, e.DetailItemId });
            });

            //病人评估表记录
            modelBuilder.Entity<PatAsFormRec>(entity =>
            {
                entity.HasKey(e => e.AsFormRecId);
                entity.HasMany(e => e.PatAsFormRecDetail).WithOne().HasForeignKey(o => new { o.AsFormRecId });
            });

            //病人评估表明细记录
            modelBuilder.Entity<PatAsFormRecDetail>(entity =>
            {
                entity.HasKey(e => e.AsFormRecDetailId);
                entity.HasKey(o => new { o.AsFormRecId, o.AsFormItemId });
                entity.HasMany(e => e.PatAsFormComboItemDetail).WithOne().HasForeignKey(o => new { o.AsFormRecId, o.AsFormItemId });
            });

            //病人评估表明细记录
            modelBuilder.Entity<PatAsFormComboItemDetail>(entity =>
            {
                entity.HasKey(e => new { e.AsFormRecId, e.AsFormItemId, e.DetailItemId });
            });

            //病人护理记录数据单记录
            modelBuilder.Entity<PatNurseDataRec>(entity =>
            {
                entity.HasKey(e => e.NurseDataId);
            });

            //病人护理记录数据单记录
            modelBuilder.Entity<PatNurseDataRecLayout>(entity =>
            {
                entity.HasKey(e => e.NurseDataId);
            });

            //病人的管道
            modelBuilder.Entity<PatSupdevList>(entity =>
            {
                entity.HasKey(o => o.PatSupdevId);
                //entity.HasMany(e => e.PatSupdevOperating).WithOne().HasForeignKey(o => o.PatSupdevOperId);
            });

            //病人的操作
            modelBuilder.Entity<PatSupdevOperating>(entity =>
            {
                entity.HasKey(o => o.PatSupdevOperId);
            });

            modelBuilder.Entity<DrugExecuteConfig>(entity =>
            {
                entity.ToTable("DRUG_EXECUTE_CONFIG");

                entity.HasIndex(e => e.Id)
                    .HasName("DRUG_EXECUTE_CONFIG_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("NUMBER(18)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaseRoleFunc)
                    .HasColumnName("BASE_ROLE_FUNC")
                    .HasColumnType("CLOB");

                entity.Property(e => e.DrugAttribute)
                    .HasColumnName("DRUG_ATTRIBUTE")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.DrugBaseCode)
                    .HasColumnName("DRUG_BASE_CODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.DrugName)
                    .HasColumnName("DRUG_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.ExecuteType)
                    .HasColumnName("EXECUTE_TYPE")
                    .HasColumnType("VARCHAR2(1)")
                    .HasDefaultValueSql("'e'");

                entity.Property(e => e.LastModifier)
                    .IsRequired()
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.TaskDescript)
                    .HasColumnName("TASK_DESCRIPT")
                    .HasColumnType("VARCHAR2(4000)");

                entity.Property(e => e.TimingListCode)
                    .IsRequired()
                    .HasColumnName("TIMING_LIST_CODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.UserRoleFunc)
                    .HasColumnName("USER_ROLE_FUNC")
                    .HasColumnType("CLOB");

                entity.HasOne(d => d.TimingListCodeNavigation)
                    .WithMany(p => p.DrugExecuteConfig)
                    .HasForeignKey(d => d.TimingListCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DRUG_EXECUTE_CONFIG_TIMING_LIST_FK");
            });

            modelBuilder.Entity<EventTime>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("EVENT_TIME_PK");

                entity.ToTable("EVENT_TIME");

                entity.HasIndex(e => e.Code)
                    .HasName("EVENT_TIME_PK")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasColumnName("CODE")
                    .HasColumnType("VARCHAR2(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Type).HasColumnName("TYPE");
            });

            modelBuilder.Entity<OrderExecuteRemind>(entity =>
            {
                entity.ToTable("ORDER_EXECUTE_REMIND");

                entity.HasIndex(e => e.Id)
                    .HasName("ORDER_EXECUTE_REMIND_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("VARCHAR2(36)")
                    .ValueGeneratedNever();

                entity.Property(e => e.LastModifier)
                    .IsRequired()
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.OrderItem)
                    .IsRequired()
                    .HasColumnName("ORDER_ITEM")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.OrderItemId)
                    .IsRequired()
                    .HasColumnName("ORDER_ITEM_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.RemindLabel)
                    .IsRequired()
                    .HasColumnName("REMIND_LABEL")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.TimeScope).HasColumnName("TIME_SCOPE");
            });

            modelBuilder.Entity<OrderExecuteTaskConfig>(entity =>
            {
                entity.ToTable("ORDER_EXECUTE_TASK_CONFIG");

                entity.HasIndex(e => e.Id)
                    .HasName("ORDER_EXECUTE_CONFIG_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("VARCHAR2(36)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaseRoleFunc)
                    .HasColumnName("BASE_ROLE_FUNC")
                    .HasColumnType("CLOB");

                entity.Property(e => e.LastModifier)
                    .IsRequired()
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.OrderItem)
                    .HasColumnName("ORDER_ITEM")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.OrderItemId)
                    .HasColumnName("ORDER_ITEM_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.TaskDescript)
                    .IsRequired()
                    .HasColumnName("TASK_DESCRIPT")
                    .HasColumnType("VARCHAR2(4000)");

                entity.Property(e => e.TaskTypeCode)
                    .HasColumnName("TASK_TYPE_CODE")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.TimingListCode)
                    .IsRequired()
                    .HasColumnName("TIMING_LIST_CODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.TimingType)
                    .HasColumnName("TIMING_TYPE")
                    .HasColumnType("VARCHAR2(1)")
                    .HasDefaultValueSql("'e'");

                entity.Property(e => e.UserRoleFunc)
                    .HasColumnName("USER_ROLE_FUNC")
                    .HasColumnType("CLOB");

                entity.Property(e => e.WardId)
                    .HasColumnName("WARD_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.WardName)
                    .HasColumnName("WARD_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.HasOne(d => d.TimingListCodeNavigation)
                    .WithMany(p => p.OrderExecuteTaskConfig)
                    .HasForeignKey(d => d.TimingListCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ORDER_EXECUTE_CONFIG_TIMING_LIST_FK");
            });

            modelBuilder.Entity<OrderTaskFormConfig>(entity =>
            {
                entity.ToTable("ORDER_TASK_FORM_CONFIG");

                entity.HasIndex(e => e.Id)
                    .HasName("ORDER_TASK_FORM_CONFIG_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("VARCHAR2(36)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaseRoleFunc)
                    .HasColumnName("BASE_ROLE_FUNC")
                    .HasColumnType("CLOB");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.DrugAttribute)
                    .HasColumnName("DRUG_ATTRIBUTE")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.FormId)
                    .HasColumnName("FORM_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.LastModifier)
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.OrderItem)
                    .HasColumnName("ORDER_ITEM")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.OrderItemId)
                    .IsRequired()
                    .HasColumnName("ORDER_ITEM_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.Statu).HasColumnName("STATU");

                entity.Property(e => e.UserRoleFunc)
                    .HasColumnName("USER_ROLE_FUNC")
                    .HasColumnType("CLOB");
            });

            modelBuilder.Entity<PatNursingTask>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("NURSING_TASK_PK");

                entity.ToTable("PAT_NURSING_TASK");

                entity.HasIndex(e => e.TaskId)
                    .HasName("NURSING_TASK_PK")
                    .IsUnique();

                entity.Property(e => e.TaskId)
                    .HasColumnName("TASK_ID")
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BeginTime)
                    .HasColumnName("BEGIN_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("CREATE_TIME")
                    .HasColumnType("DATE")
                    .HasDefaultValueSql("sysdate  ");

                entity.Property(e => e.EventObjectId)
                    .IsRequired()
                    .HasColumnName("EVENT_OBJECT_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.ExectueDescript)
                    .HasColumnName("EXECTUE_DESCRIPT")
                    .HasColumnType("VARCHAR2(4000)");

                entity.Property(e => e.Pid)
                    .IsRequired()
                    .HasColumnName("PID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.Pvid)
                    .IsRequired()
                    .HasColumnName("PVID")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.RequestFinishTime)
                    .HasColumnName("REQUEST_FINISH_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.ResponsibleId)
                    .HasColumnName("RESPONSIBLE_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.TaskEventCode)
                    .IsRequired()
                    .HasColumnName("TASK_EVENT_CODE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.TaskObjectId)
                    .HasColumnName("TASK_OBJECT_ID")
                    .HasColumnType("VARCHAR2(4000)");

                entity.Property(e => e.Title)
                    .HasColumnName("TITLE")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.WardId)
                    .IsRequired()
                    .HasColumnName("WARD_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.TaskTypeCode)
                    .IsRequired()
                    .HasColumnName("TASK_TYPE_CODE")
                    .HasColumnType("VARCHAR2(10)");

                entity.HasOne(d => d.TaskEventCodeNavigation)
                    .WithMany(p => p.PatNursingTask)
                    .HasForeignKey(d => d.TaskEventCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NURSING_TASK_TASK_SOURCE_OPERATION_FK");

                entity.Property(e => e.Baby)
                    .HasColumnName("NB_SNO")
                    .HasColumnType("NUMBER");
            });

            modelBuilder.Entity<PatNursingTaskFinish>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("TASK_ID_PK");

                entity.ToTable("PAT_NURSING_TASK_FINISH");

                entity.HasIndex(e => e.TaskId)
                    .HasName("TASK_ID_PK")
                    .IsUnique();

                entity.Property(e => e.TaskId)
                    .HasColumnName("TASK_ID")
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BeginTime)
                    .HasColumnName("BEGIN_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("CREATE_TIME")
                    .HasColumnType("DATE")
                    .HasDefaultValueSql("sysdate  ");

                entity.Property(e => e.EventCode)
                    .HasColumnName("EVENT_CODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.EventObjectId)
                    .IsRequired()
                    .HasColumnName("EVENT_OBJECT_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.ExectueDescript)
                    .HasColumnName("EXECTUE_DESCRIPT")
                    .HasColumnType("VARCHAR2(4000)");

                entity.Property(e => e.ExecuteTime)
                    .HasColumnName("EXECUTE_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.Executor)
                    .HasColumnName("EXECUTOR")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.Pid)
                    .IsRequired()
                    .HasColumnName("PID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.Pvid)
                    .IsRequired()
                    .HasColumnName("PVID")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.RequestFinishTime)
                    .HasColumnName("REQUEST_FINISH_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.ResponsibleId)
                    .HasColumnName("RESPONSIBLE_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.TaskEventCode)
                    .IsRequired()
                    .HasColumnName("TASK_EVENT_CODE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.TaskObjectId)
                    .HasColumnName("TASK_OBJECT_ID")
                    .HasColumnType("VARCHAR2(4000)");

                entity.Property(e => e.TaskTypeCode)
                    .IsRequired()
                    .HasColumnName("TASK_TYPE_CODE")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.Title)
                    .HasColumnName("TITLE")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.WardId)
                    .IsRequired()
                    .HasColumnName("WARD_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.Baby)
                    .HasColumnName("NB_SNO")
                    .HasColumnType("NUMBER");
            });

            modelBuilder.Entity<SignTaskConfig>(entity =>
            {
                entity.ToTable("SIGN_TASK_CONFIG");

                entity.HasIndex(e => e.Id)
                    .HasName("SIGN_TASK_CONFIG_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("VARCHAR2(36)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaseRoleFunc)
                    .HasColumnName("BASE_ROLE_FUNC")
                    .HasColumnType("CLOB");

                entity.Property(e => e.Descript)
                    .HasColumnName("DESCRIPT")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.OperationCode)
                    .HasColumnName("OPERATION_CODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.Scope)
                    .HasColumnName("SCOPE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SignTaskConfigId)
                    .IsRequired()
                    .HasColumnName("SIGN_TASK_CONFIG_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasColumnType("VARCHAR2(1)");

                entity.Property(e => e.UserRoleFunc)
                    .HasColumnName("USER_ROLE_FUNC")
                    .HasColumnType("CLOB");

                entity.HasOne(d => d.OperationCodeNavigation)
                    .WithMany(p => p.SignTaskConfig)
                    .HasForeignKey(d => d.OperationCode)
                    .HasConstraintName("SIGN_TASK_CONFIG_TASK_SOURCE_OPERATION_FK");
            });

            modelBuilder.Entity<SupdevTaskConfig>(entity =>
            {
                entity.HasKey(e => new { e.SupdevTypeId, e.TaskEventCode })
                    .HasName("SUPDEV_TYPE_ID_PK");

                entity.ToTable("SUPDEV_TASK_CONFIG");

                entity.HasIndex(e => new { e.SupdevTypeId, e.TaskEventCode })
                    .HasName("SUPDEV_TYPE_ID_PK")
                    .IsUnique();

                entity.Property(e => e.SupdevTypeId)
                    .HasColumnName("SUPDEV_TYPE_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.TaskEventCode)
                    .HasColumnName("TASK_EVENT_CODE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.BaseRoleFunc)
                    .HasColumnName("BASE_ROLE_FUNC")
                    .HasColumnType("CLOB");

                entity.Property(e => e.LastModifier)
                    .IsRequired()
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.UserRoleFunc)
                    .HasColumnName("USER_ROLE_FUNC")
                    .HasColumnType("CLOB");

                entity.HasOne(d => d.TaskEventCodeNavigation)
                    .WithMany(p => p.SupdevTaskConfig)
                    .HasForeignKey(d => d.TaskEventCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SUPDEV_TASK_CONFIG_TASK_SOURCE_OPERATION_FK");
            });

            modelBuilder.Entity<TaskEvent>(entity =>
            {
                entity.HasKey(e => e.EventCode)
                    .HasName("TASK_SOURCE_OPERATION_PK");

                entity.ToTable("TASK_EVENT");

                entity.HasIndex(e => e.EventCode)
                    .HasName("TASK_SOURCE_OPERATION_PK")
                    .IsUnique();

                entity.Property(e => e.EventCode)
                    .HasColumnName("EVENT_CODE")
                    .HasColumnType("VARCHAR2(50)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("EVENT_NAME")
                    .HasColumnType("VARCHAR2(100)");
            });

            modelBuilder.Entity<TaskFormConfig>(entity =>
            {
                entity.ToTable("TASK_FORM_CONFIG");

                entity.HasIndex(e => e.Id)
                    .HasName("FORM_TASK_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("VARCHAR2(36)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.FormId)
                    .HasColumnName("FORM_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.FormTimingId)
                    .IsRequired()
                    .HasColumnName("FORM_TIMING_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.LastModifier)
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.Scope)
                    .HasColumnName("SCOPE")
                    .HasDefaultValueSql(@"0
");

                entity.Property(e => e.Statu).HasColumnName("STATU");

                entity.Property(e => e.TaskEventCode)
                    .HasColumnName("TASK_EVENT_CODE")
                    .HasColumnType("VARCHAR2(50)");

                entity.HasOne(d => d.FormTiming)
                    .WithMany(p => p.TaskFormConfig)
                    .HasForeignKey(d => d.FormTimingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FORM_TASK_CONFIG_TASK_FORM_TIMING_FK");

                entity.HasOne(d => d.TaskEventCodeNavigation)
                    .WithMany(p => p.TaskFormConfig)
                    .HasForeignKey(d => d.TaskEventCode)
                    .HasConstraintName("FORM_TASK_CONFIG_TASK_SOURCE_OPERATION_FK");
            });

            modelBuilder.Entity<TaskFormTiming>(entity =>
            {
                entity.HasKey(e => e.FormTimingId)
                    .HasName("TASK_FORM_TIMING_PK");

                entity.ToTable("TASK_FORM_TIMING");

                entity.HasIndex(e => e.FormTimingId)
                    .HasName("TASK_FORM_TIMING_PK")
                    .IsUnique();

                entity.Property(e => e.FormTimingId)
                    .HasColumnName("FORM_TIMING_ID")
                    .HasColumnType("VARCHAR2(36)")
                    .ValueGeneratedNever();

                entity.Property(e => e.HightOperator)
                    .HasColumnName("HIGHT_OPERATOR")
                    .HasColumnType("VARCHAR2(2)");

                entity.Property(e => e.HightScore)
                    .HasColumnName("HIGHT_SCORE")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.LowerOperator)
                    .HasColumnName("LOWER_OPERATOR")
                    .HasColumnType("VARCHAR2(2)");

                entity.Property(e => e.LowerScore)
                    .HasColumnName("LOWER_SCORE")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.PerDayCount)
                    .HasColumnName("PER_DAY_COUNT")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Timing)
                    .HasColumnName("TIMING")
                    .HasColumnType("VARCHAR2(4000)");

                entity.Property(e => e.TimingName)
                    .HasColumnName("TIMING_NAME")
                    .HasColumnType("VARCHAR2(50)");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("TASK_TYPE_PK");

                entity.ToTable("TASK_TYPE");

                entity.HasIndex(e => e.Code)
                    .HasName("TASK_TYPE_PK")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("TASK_TYPE__UN")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasColumnName("CODE")
                    .HasColumnType("VARCHAR2(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.LastModifier)
                    .IsRequired()
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.TaskWindow)
                    .HasColumnName("TASK_WINDOW")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasColumnType("VARCHAR2(10)");
            });

            modelBuilder.Entity<TimingList>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("TIMING_LIST_PK");

                entity.ToTable("TIMING_LIST");

                entity.HasIndex(e => e.Code)
                    .HasName("TIMING_LIST_PK")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasColumnName("CODE")
                    .HasColumnType("VARCHAR2(20)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.LastModifier)
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.Timing)
                    .HasColumnName("TIMING")
                    .HasColumnType("VARCHAR2(4000)");

                entity.Property(e => e.ZlhisId)
                    .HasColumnName("ZLHIS_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.ZlhisName)
                    .HasColumnName("ZLHIS_NAME")
                    .HasColumnType("VARCHAR2(36)");
            });

            modelBuilder.Entity<WardOrderTaskForm>(entity =>
            {
                entity.HasKey(e => new { e.OrderTaskFormConfigId, e.WardId })
                    .HasName("WARD_ORDER_TASK_FORM_PK");

                entity.ToTable("WARD_ORDER_TASK_FORM");

                entity.HasIndex(e => new { e.OrderTaskFormConfigId, e.WardId })
                    .HasName("WARD_ORDER_TASK_FORM_PK")
                    .IsUnique();

                entity.Property(e => e.OrderTaskFormConfigId)
                    .HasColumnName("ORDER_TASK_FORM_CONFIG_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.WardId)
                    .HasColumnName("WARD_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.LastModifier)
                    .IsRequired()
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.Statu).HasColumnName("STATU");

                entity.HasOne(d => d.OrderTaskFormConfig)
                    .WithMany(p => p.WardOrderTaskForm)
                    .HasForeignKey(d => d.OrderTaskFormConfigId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WARD_ORDER_TASK_FORM_ORDER_TASK_FORM_CONFIG_FK");
            });

            modelBuilder.Entity<WardSignTask>(entity =>
            {
                entity.HasKey(e => new { e.WardId, e.SignTaskConfigId })
                    .HasName("WARD_SIGN_TASK_PK");

                entity.ToTable("WARD_SIGN_TASK");

                entity.HasIndex(e => new { e.WardId, e.SignTaskConfigId })
                    .HasName("WARD_SIGN_TASK_PK")
                    .IsUnique();

                entity.Property(e => e.WardId)
                    .HasColumnName("WARD_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.SignTaskConfigId)
                    .HasColumnName("SIGN_TASK_CONFIG_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.LastModifier)
                    .IsRequired()
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.Statu).HasColumnName("STATU");

                entity.HasOne(d => d.SignTaskConfig)
                    .WithMany(p => p.WardSignTask)
                    .HasForeignKey(d => d.SignTaskConfigId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WARD_SIGN_TASK_SIGN_TASK_CONFIG_FK");
            });

            modelBuilder.Entity<WardTaskFormConfig>(entity =>
            {
                entity.HasKey(e => new { e.FormTaskConfigId, e.WardId })
                    .HasName("WARD_TASK_FORM_CONFIG_PK");

                entity.ToTable("WARD_TASK_FORM_CONFIG");

                entity.HasIndex(e => new { e.FormTaskConfigId, e.WardId })
                    .HasName("WARD_TASK_FORM_CONFIG_PK")
                    .IsUnique();

                entity.Property(e => e.FormTaskConfigId)
                    .HasColumnName("FORM_TASK_CONFIG_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.WardId)
                    .HasColumnName("WARD_ID")
                    .HasColumnType("VARCHAR2(36)");

                entity.Property(e => e.LastModifier)
                    .IsRequired()
                    .HasColumnName("LAST_MODIFIER")
                    .HasColumnType("VARCHAR2(80)");

                entity.Property(e => e.LastModifyTime)
                    .HasColumnName("LAST_MODIFY_TIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.Statu).HasColumnName("STATU");

                entity.HasOne(d => d.FormTaskConfig)
                    .WithMany(p => p.WardTaskFormConfig)
                    .HasForeignKey(d => d.FormTaskConfigId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WARD_FORM_TASK_FORM_TASK_CONFIG_FK");
            });
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.HasSequence("ISEQ$$_130735");

            modelBuilder.HasSequence("ISEQ$$_130738");

            modelBuilder.HasSequence("ISEQ$$_130742");

            modelBuilder.HasSequence("PAT_NURSING_TASK_FINISH_TASK_I");

            modelBuilder.HasSequence("PAT_NURSING_TASK_PROCESS_TASK_");

            modelBuilder.HasSequence("PAT_NURSING_TASK_TASK_ID_SEQ");

            modelBuilder.HasSequence("ZLNTASK_SEQ");
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
