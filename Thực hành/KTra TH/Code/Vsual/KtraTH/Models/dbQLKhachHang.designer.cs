﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KtraTH.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="KtraTH")]
	public partial class dbQLKhachHangDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertKhachhang(Khachhang instance);
    partial void UpdateKhachhang(Khachhang instance);
    partial void DeleteKhachhang(Khachhang instance);
    partial void InsertKhuvuc(Khuvuc instance);
    partial void UpdateKhuvuc(Khuvuc instance);
    partial void DeleteKhuvuc(Khuvuc instance);
    #endregion
		
		public dbQLKhachHangDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["KtraTHConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public dbQLKhachHangDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dbQLKhachHangDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dbQLKhachHangDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dbQLKhachHangDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Khachhang> Khachhangs
		{
			get
			{
				return this.GetTable<Khachhang>();
			}
		}
		
		public System.Data.Linq.Table<Khuvuc> Khuvucs
		{
			get
			{
				return this.GetTable<Khuvuc>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Khachhang")]
	public partial class Khachhang : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _MaKH;
		
		private int _MaKV;
		
		private string _TenKH;
		
		private System.Nullable<int> _Namsinh;
		
		private EntityRef<Khuvuc> _Khuvuc;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMaKHChanging(string value);
    partial void OnMaKHChanged();
    partial void OnMaKVChanging(int value);
    partial void OnMaKVChanged();
    partial void OnTenKHChanging(string value);
    partial void OnTenKHChanged();
    partial void OnNamsinhChanging(System.Nullable<int> value);
    partial void OnNamsinhChanged();
    #endregion
		
		public Khachhang()
		{
			this._Khuvuc = default(EntityRef<Khuvuc>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MaKH", DbType="Char(4) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string MaKH
		{
			get
			{
				return this._MaKH;
			}
			set
			{
				if ((this._MaKH != value))
				{
					this.OnMaKHChanging(value);
					this.SendPropertyChanging();
					this._MaKH = value;
					this.SendPropertyChanged("MaKH");
					this.OnMaKHChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MaKV", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int MaKV
		{
			get
			{
				return this._MaKV;
			}
			set
			{
				if ((this._MaKV != value))
				{
					if (this._Khuvuc.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMaKVChanging(value);
					this.SendPropertyChanging();
					this._MaKV = value;
					this.SendPropertyChanged("MaKV");
					this.OnMaKVChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TenKH", DbType="NChar(40)")]
		public string TenKH
		{
			get
			{
				return this._TenKH;
			}
			set
			{
				if ((this._TenKH != value))
				{
					this.OnTenKHChanging(value);
					this.SendPropertyChanging();
					this._TenKH = value;
					this.SendPropertyChanged("TenKH");
					this.OnTenKHChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Namsinh", DbType="Int")]
		public System.Nullable<int> Namsinh
		{
			get
			{
				return this._Namsinh;
			}
			set
			{
				if ((this._Namsinh != value))
				{
					this.OnNamsinhChanging(value);
					this.SendPropertyChanging();
					this._Namsinh = value;
					this.SendPropertyChanged("Namsinh");
					this.OnNamsinhChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Khuvuc_Khachhang", Storage="_Khuvuc", ThisKey="MaKV", OtherKey="MaKV", IsForeignKey=true)]
		public Khuvuc Khuvuc
		{
			get
			{
				return this._Khuvuc.Entity;
			}
			set
			{
				Khuvuc previousValue = this._Khuvuc.Entity;
				if (((previousValue != value) 
							|| (this._Khuvuc.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Khuvuc.Entity = null;
						previousValue.Khachhangs.Remove(this);
					}
					this._Khuvuc.Entity = value;
					if ((value != null))
					{
						value.Khachhangs.Add(this);
						this._MaKV = value.MaKV;
					}
					else
					{
						this._MaKV = default(int);
					}
					this.SendPropertyChanged("Khuvuc");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Khuvuc")]
	public partial class Khuvuc : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _MaKV;
		
		private string _TenKV;
		
		private EntitySet<Khachhang> _Khachhangs;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMaKVChanging(int value);
    partial void OnMaKVChanged();
    partial void OnTenKVChanging(string value);
    partial void OnTenKVChanged();
    #endregion
		
		public Khuvuc()
		{
			this._Khachhangs = new EntitySet<Khachhang>(new Action<Khachhang>(this.attach_Khachhangs), new Action<Khachhang>(this.detach_Khachhangs));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MaKV", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int MaKV
		{
			get
			{
				return this._MaKV;
			}
			set
			{
				if ((this._MaKV != value))
				{
					this.OnMaKVChanging(value);
					this.SendPropertyChanging();
					this._MaKV = value;
					this.SendPropertyChanged("MaKV");
					this.OnMaKVChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TenKV", DbType="NChar(30)")]
		public string TenKV
		{
			get
			{
				return this._TenKV;
			}
			set
			{
				if ((this._TenKV != value))
				{
					this.OnTenKVChanging(value);
					this.SendPropertyChanging();
					this._TenKV = value;
					this.SendPropertyChanged("TenKV");
					this.OnTenKVChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Khuvuc_Khachhang", Storage="_Khachhangs", ThisKey="MaKV", OtherKey="MaKV")]
		public EntitySet<Khachhang> Khachhangs
		{
			get
			{
				return this._Khachhangs;
			}
			set
			{
				this._Khachhangs.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Khachhangs(Khachhang entity)
		{
			this.SendPropertyChanging();
			entity.Khuvuc = this;
		}
		
		private void detach_Khachhangs(Khachhang entity)
		{
			this.SendPropertyChanging();
			entity.Khuvuc = null;
		}
	}
}
#pragma warning restore 1591
