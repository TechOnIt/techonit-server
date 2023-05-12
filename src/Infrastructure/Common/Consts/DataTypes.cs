﻿namespace TechOnIt.Infrastructure.Common.Consts;

public class DataTypes
{
	#region sql-data types
	public const string boolean = "bit"; // https://learn.microsoft.com/en-us/sql/t-sql/data-types/bit-transact-sql?view=sql-server-ver16
    public const string guid = "uniqueidentifier"; // https://learn.microsoft.com/en-us/sql/t-sql/data-types/uniqueidentifier-transact-sql?view=sql-server-ver16
    #endregion

    #region strings
    // https://learn.microsoft.com/en-us/sql/t-sql/data-types/nchar-and-nvarchar-transact-sql?view=sql-server-ver16
    public const string nvarchar15 = "nvarchar(15)";
    public const string nvarchar50 = "nvarchar(50)";
    public const string nvarchar100 = "nvarchar(100)";
    public const string nvarchar150 = "nvarchar(150)";
    public const string nvarchar500 = "nvarchar(500)";
    public const string nvarchar4000 = "nvarchar(4000)";
    public const string nvarcharMax = "nvarchar(max)";

    // https://learn.microsoft.com/en-us/sql/t-sql/data-types/char-and-varchar-transact-sql?view=sql-server-ver16
    public const string varchar10 = "varchar(10)";
    public const string varchar15 = "varchar(15)";
    public const string varchar20 = "varchar(20)";
    public const string varchar30 = "varchar(30)";
    public const string varchar50 = "varchar(50)";
    #endregion

    #region numerics
    public const string integer = "int"; // https://learn.microsoft.com/en-us/sql/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql?view=sql-server-ver16
    public const string decimals = "decimal"; // https://learn.microsoft.com/en-us/sql/t-sql/data-types/decimal-and-numeric-transact-sql?view=sql-server-ver16
    /// <summary>
    /// 0 - 255
    /// </summary>
    public const string tinyint = "tinyint"; // https://learn.microsoft.com/en-us/sql/t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql?view=sql-server-ver16
    public const string numerics = "numeric"; // https://learn.microsoft.com/en-us/sql/t-sql/data-types/decimal-and-numeric-transact-sql?view=sql-server-ver16
    /// <summary>
    /// double
    /// </summary>
    public const string bigint = "bigint";
    public const string forMoney = "money"; // https://learn.microsoft.com/en-us/sql/t-sql/data-types/money-and-smallmoney-transact-sql?view=sql-server-ver16
    #endregion

    #region date
    public const string datetime2 = "datetime2"; // https://learn.microsoft.com/en-us/sql/t-sql/data-types/datetime2-transact-sql?view=sql-server-ver16
    public const string rowVersion = "rowversion";
    #endregion
}
