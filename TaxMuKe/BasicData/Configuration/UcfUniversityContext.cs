using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using BasicFramework.Component;
using System.Reflection;
using BasicData.Domain;
using BasicData.Domain.Course;

namespace BasicData
{
    public partial class UcfUniversityContext : DbContext
    {
        static UcfUniversityContext()
        {
            Database.SetInitializer<UcfUniversityContext>(null);
        }

        public UcfUniversityContext()
            : base("Name=UcfUniversityContext")
        {
//            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
//            var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
//            mappingCollection.GenerateViews(new List<EdmSchemaError>());
            Database.Initialize(false);
        }
        #region DBSet  用于数据的增删改

        public virtual DbSet<CourseViewRecord> CourseViewRecords { get; set; }
        public virtual DbSet<MuKeBanner> MuKeBanners { get; set; }

        public virtual DbSet<Interfacecalllog> Interfacecalllog { get; set; }
        public virtual DbSet<Settings> Setting { get; set; }
        public virtual DbSet<VersionManage> VersionManage { get; set; }

//        public virtual DbSet<Contents> Contents { get; set; }
//        public virtual DbSet<ContentMeta> ContentMetas { get; set; }

        public virtual DbSet<MuKeCourse> MuKeCourses { get; set; }
        //public virtual DbSet<MuKeCourseCapter> MuKeCourseCapter { get; set; }
        public virtual DbSet<MuKeRecordCourse> MuKeRecordCourses { get; set; }
        public virtual DbSet<MuKeRecordCourseCapter> MuKeRecordCourseCapters { get; set; }
        public virtual DbSet<MuKeCourseCapter> MuKeCourseCapters { get; set; }
        public virtual DbSet<MuKeCourseCapterComment> MuKeCourseCapterComments { get; set; }
        public virtual DbSet<MuKeCourseSearchKeyWord> MuKeCourseSearchKeyWords { get; set; }

        public virtual DbSet<MuKeCourseCapterTestPagperManager> MuKeCourseCapterTestPagperManagers { get; set; }
        public virtual DbSet<MuKeCourseCapterTestPagper> MuKeCourseCapterTestPagpers { get; set; }
        public virtual DbSet<MuKeCourseTestRecord> MuKeCourseTestRecords { get; set; }

        public virtual DbSet<MuKeMaterial> MuKeMaterials { get; set; }

      

        public virtual DbSet<TaxonomyRelationships> TaxonomyRelationships { get; set; }
        public virtual DbSet<Taxonomy> Taxonomys { get; set; }

        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<PermissionResource> PermissionResources { get; set; }
        public virtual DbSet<PermissionRole> Roles { get; set; }
        public virtual DbSet<UserHistory> UserHistorys { get; set; }
        public virtual DbSet<MuKeUserInteractiveValidation> MuKeUserInteractiveValidations { get; set; }
        public virtual DbSet<UserMeta> UserMetas { get; set; }
        public virtual DbSet<MuKeUsers> MuKeUsers { get; set; }

        public virtual DbSet<MuKeSmsChannel> MuKeSmsChannels { get; set; }

        public virtual DbSet<SmsRequestQueue> SmsRequestQueues { get; set; }

        public virtual DbSet<MuKeSmsSendRecord> MuKeSmsSendRecords { get; set; }

        public virtual DbSet<MuKeGlobalSet> MuKeGlobalSets { get; set; }
     
        public virtual DbSet<MuKeHomeBanner> MuKeHomeBanners { get; set; }
        public virtual DbSet<MuKeCourseBanner> MuKeCourseBanners { get; set; }
    

        public virtual DbSet<MuKeUserSet> MuKeUserSets { get; set; }
        public virtual DbSet<MuKeDirtyWord> MuKeDirtyWords { get; set; }
        public virtual DbSet<KaHao> KaHaos { get; set; }
        public virtual DbSet<MuKeShangJia> MuKeShangJias { get; set; }
       
        public virtual DbSet<AlipayAsyncNotifyLog> AlipayAsyncNotifys { get; set; }
        public virtual DbSet<MuKeHomeCourseSet> MuKeHomeCourseSets { get; set; }
        public virtual DbSet<AppStoreSet> AppStoreSets { get; set; }

        #endregion

        #region IQueryable 用于数据的查询
        public IQueryable<PermissionResource> QueryablePermissionResources { get { return PermissionResources.AsPowerful(); } }
        public IQueryable<PermissionRole> QueryableRoles { get { return Roles.AsPowerful(); } }
        public IQueryable<Permissions> QueryablePermissions { get { return Permissions.AsPowerful(); } }
        public IQueryable<MuKeUsers> QueryableUsers { get { return MuKeUsers.AsPowerful(); } }

        public IQueryable<UserMeta> QueryableUserMetas { get { return UserMetas.AsPowerful(); } }
        public IQueryable<MuKeBanner> QueryableBanners { get { return MuKeBanners.AsPowerful(); } }

        public IQueryable<MuKeMaterial> QueryableMaterials { get { return MuKeMaterials.AsPowerful(); } }
        public IQueryable<MuKeCourse> QueryableCourse { get { return MuKeCourses.AsPowerful(); } }
        public IQueryable<MuKeCourseCapter> QueryableCourseItem { get { return MuKeCourseCapters.AsPowerful(); } }

        public IQueryable<TaxonomyRelationships> QueryableTaxonomyRelationships { get { return TaxonomyRelationships.AsPowerful(); } }
        public IQueryable<Taxonomy> QueryableTaxonomys { get { return Taxonomys.AsPowerful(); } }


        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //ef 6新特性，自动加载配置
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// 强制提交（高并发时候使用，应对瞬态故障）
        /// </summary>
        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;
            do
            {
                try
                {
                    base.SaveChanges();
                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }
        public void RollbackChanges()
        {
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }
        public void DetachedAllEntitis()
        {
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Detached);
        }
    }
}
