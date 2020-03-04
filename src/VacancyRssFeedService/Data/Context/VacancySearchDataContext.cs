using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Web;

namespace VacancyRssFeedService.Data.Context
{
    //[Database(Name = "NAVMS_MS")]
    public partial class VacancySearchDataContext : DataContext
    {
        private static MappingSource mappingSource = new AttributeMappingSource();

        partial void OnCreated();

        public VacancySearchDataContext(string connection) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public VacancySearchDataContext(System.Data.IDbConnection connection) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public VacancySearchDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public VacancySearchDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        [FunctionAttribute(Name = "dbo.uspGetVacanciesForRSS")]
        public ISingleResult<uspGetVacanciesForRSSResult> uspGetVacanciesForRSS([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FeedType", DbType = "Int")] System.Nullable<int> feedType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DateTimeRange", DbType = "Int")] System.Nullable<int> dateTimeRange, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FrameworkCode", DbType = "VarChar(3)")] string frameworkCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OccupationCode", DbType = "VarChar(3)")] string occupationCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CountyCode", DbType = "VarChar(MAX)")] string countyCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Town", DbType = "VarChar(MAX)")] string town, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RegionCode", DbType = "VarChar(MAX)")] string regionCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "VacancyReferenceNumber", DbType = "Int")] System.Nullable<int> vacancyReferenceNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FeedTitle", DbType = "NVarChar(300)")] ref string feedTitle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FeedDescription", DbType = "NVarChar(300)")] ref string feedDescription, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FeedImageUrl", DbType = "NVarChar(300)")] ref string feedImageUrl, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FeedCopyrightInfo", DbType = "NVarChar(300)")] ref string feedCopyrightInfo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AlternateLink", DbType = "NVarChar(300)")] ref string alternateLink)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), feedType, dateTimeRange, frameworkCode, occupationCode, countyCode, town, regionCode, vacancyReferenceNumber, feedTitle, feedDescription, feedImageUrl, feedCopyrightInfo, alternateLink);
            feedTitle = ((string)(result.GetParameterValue(8)));
            feedDescription = ((string)(result.GetParameterValue(9)));
            feedImageUrl = ((string)(result.GetParameterValue(10)));
            feedCopyrightInfo = ((string)(result.GetParameterValue(11)));
            alternateLink = ((string)(result.GetParameterValue(12)));
            return ((ISingleResult<uspGetVacanciesForRSSResult>)(result.ReturnValue));
        }
    }

    public partial class uspGetVacanciesForRSSResult
    {

        private int _VacancyId;

        private string _VacancyTitle;

        private string _EmployerTradingName;

        private string _ShortDescription;

        private System.Nullable<int> _VacancyType;

        private string _VacancyLocation;

        private string _JobRole;

        private System.Nullable<System.DateTime> _ClosingDate;

        private System.DateTime _PublishDate;

        private System.Nullable<int> _VacancyLocationTypeId;

        private System.Nullable<int> _VacancyReferenceNumber;

        private System.Nullable<decimal> _WeeklyWage;

        private string _WageText;

        public uspGetVacanciesForRSSResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VacancyId", DbType = "Int NOT NULL")]
        public int VacancyId
        {
            get
            {
                return this._VacancyId;
            }
            set
            {
                if ((this._VacancyId != value))
                {
                    this._VacancyId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VacancyTitle", DbType = "NVarChar(100)")]
        public string VacancyTitle
        {
            get
            {
                return this._VacancyTitle;
            }
            set
            {
                if ((this._VacancyTitle != value))
                {
                    this._VacancyTitle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EmployerTradingName", DbType = "NVarChar(255) NOT NULL", CanBeNull = false)]
        public string EmployerTradingName
        {
            get
            {
                return this._EmployerTradingName;
            }
            set
            {
                if ((this._EmployerTradingName != value))
                {
                    this._EmployerTradingName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ShortDescription", DbType = "NVarChar(256)")]
        public string ShortDescription
        {
            get
            {
                return this._ShortDescription;
            }
            set
            {
                if ((this._ShortDescription != value))
                {
                    this._ShortDescription = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VacancyType", DbType = "Int")]
        public System.Nullable<int> VacancyType
        {
            get
            {
                return this._VacancyType;
            }
            set
            {
                if ((this._VacancyType != value))
                {
                    this._VacancyType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VacancyLocation", DbType = "NVarChar(40)")]
        public string VacancyLocation
        {
            get
            {
                return this._VacancyLocation;
            }
            set
            {
                if ((this._VacancyLocation != value))
                {
                    this._VacancyLocation = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JobRole", DbType = "NVarChar(200) NOT NULL", CanBeNull = false)]
        public string JobRole
        {
            get
            {
                return this._JobRole;
            }
            set
            {
                if ((this._JobRole != value))
                {
                    this._JobRole = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ClosingDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ClosingDate
        {
            get
            {
                return this._ClosingDate;
            }
            set
            {
                if ((this._ClosingDate != value))
                {
                    this._ClosingDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PublishDate", DbType = "DateTime NOT NULL")]
        public System.DateTime PublishDate
        {
            get
            {
                return this._PublishDate;
            }
            set
            {
                if ((this._PublishDate != value))
                {
                    this._PublishDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VacancyLocationTypeId", DbType = "Int")]
        public System.Nullable<int> VacancyLocationTypeId
        {
            get
            {
                return this._VacancyLocationTypeId;
            }
            set
            {
                if ((this._VacancyLocationTypeId != value))
                {
                    this._VacancyLocationTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VacancyReferenceNumber", DbType = "Int")]
        public System.Nullable<int> VacancyReferenceNumber
        {
            get
            {
                return this._VacancyReferenceNumber;
            }
            set
            {
                if ((this._VacancyReferenceNumber != value))
                {
                    this._VacancyReferenceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_WeeklyWage", DbType = "Money")]
        public System.Nullable<decimal> WeeklyWage
        {
            get
            {
                return this._WeeklyWage;
            }
            set
            {
                if ((this._WeeklyWage != value))
                {
                    this._WeeklyWage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_WageText", DbType = "NVarChar(MAX) NULL", CanBeNull = true)]
        public string WageText
        {
            get
            {
                return this._WageText;
            }
            set
            {
                if ((this._WageText != value))
                {
                    this._WageText = value;
                }
            }
        }
    }
}