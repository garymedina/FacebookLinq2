﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Facebook;
using System.Reflection;
using facebook.Tables;

namespace Facebook.Linq
{
	internal class FacebookDataReader : IDataReader
	{
		public FacebookDataReader(Facebook.JsonArray json)
		{
			doc = json;
		}


		public int Count
		{
			get
			{
				return doc.Count;
			}
		}

		Facebook.JsonArray doc;
		IEnumerator<object> CurrentNode;

		#region IDataReader Members

		public void Close()
		{
			CurrentNode = null;
			doc = null;
		}

		public int Depth
		{
			get { throw new NotImplementedException(); }
		}

		public DataTable GetSchemaTable()
		{
			throw new NotImplementedException();
		}

		public bool IsClosed
		{
			get { return doc == null; }
		}

		public bool NextResult()
		{
			if (CurrentNode == null)
			{
				if (doc != null)
				{
					CurrentNode = doc.GetEnumerator();
					return CurrentNode.MoveNext();
				}
			}
			else
			{
				return CurrentNode.MoveNext();
			}
			return false;
		}

		public bool Read()
		{
			return NextResult();
		}

		public int RecordsAffected
		{
			get { return 0; }
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			CurrentNode = null;
			doc = null;
		}

		#endregion

		#region IDataRecord Members

		public int FieldCount
		{
			get { return 0; }
		}

		public bool GetBoolean(int i)
		{
			throw new NotImplementedException();
		}

		public byte GetByte(int i)
		{
			throw new NotImplementedException();
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public char GetChar(int i)
		{
			throw new NotImplementedException();
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		public string GetDataTypeName(int i)
		{
			throw new NotImplementedException();
		}

		public DateTime GetDateTime(int i)
		{
			throw new NotImplementedException();
		}

		public decimal GetDecimal(int i)
		{
			throw new NotImplementedException();
		}

		public double GetDouble(int i)
		{
			throw new NotImplementedException();
		}

		public Type GetFieldType(int i)
		{
			throw new NotImplementedException();
		}

		public float GetFloat(int i)
		{
			throw new NotImplementedException();
		}

		public Guid GetGuid(int i)
		{
			throw new NotImplementedException();
		}

		public short GetInt16(int i)
		{
			throw new NotImplementedException();
		}

		public int GetInt32(int i)
		{
			throw new NotImplementedException();
		}

		public long GetInt64(int i)
		{
			throw new NotImplementedException();
		}

		public string GetName(int i)
		{
			throw new NotImplementedException();
		}

		public int GetOrdinal(string name)
		{
			throw new NotImplementedException();
		}

		public string GetString(int i)
		{
			throw new NotImplementedException();
		}

		public object GetValue(int i)
		{
			throw new NotImplementedException();
		}

		public int GetValues(object[] values)
		{
			throw new NotImplementedException();
		}

		public bool IsDBNull(int i)
		{
			throw new NotImplementedException();
		}

		public object this[string name]
		{
			get
			{
				return this[name, null];
			}
		}

		public ICollection<string> GetCurrentKeys()
		{
			Facebook.JsonObject asJsonObject = (Facebook.JsonObject)(CurrentNode.Current);
			return asJsonObject.Keys;

		}

		public object this[string name, Type type]
		{
			get
			{
				Facebook.JsonObject asJsonObject = (Facebook.JsonObject) (CurrentNode.Current);
				if(asJsonObject.ContainsKey(name))
				{
					if (type == null)
					{
						return asJsonObject[name]; 
					}
					return ConvertPropertyFromJson(name, asJsonObject[name], type);
				}
				else
				{
					return null;
				}
			}
		}

		public object this[int i]
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		object ConvertPropertyFromJson(string objectName, object value, Type propType)
		{
			if (value == null)
				return null;

			var uType = Nullable.GetUnderlyingType(propType);
			if (uType!=null && uType!=propType) //is nullable
			{
				propType = uType;
			}
			if (propType == typeof(Int64))
			{
				return (value);
			}
			else if (propType == typeof(Decimal))
			{
				return (value);
			}
			else if (propType == typeof(string))
			{
				return value;
			}
			else if (propType == typeof(bool))
			{
				return (value);
			}
			else if (propType == typeof(DateTime))
			{
				return ParseFacebookDateTime((long)value);
			}
			else if (propType == typeof(CoordsType))
			{
				return new CoordsType((JsonObject)value);
			}
			else if (propType == typeof(DeviceList))
			{
				return new DeviceList((JsonArray)value);
			}
			else if (propType == typeof(GenderList))
			{
				return new GenderList((JsonArray)value);
			}
			else if (propType == typeof(HometownLocationType))
			{
				return new HometownLocationType((JsonObject)value);
			}				
			else if (propType.IsEnum)
			{
				return ParseFacebookEnum(propType, value as string);
			}
			else if (value is Facebook.JsonObject)
			{
				return value;
			}
			else if (value is Facebook.JsonArray)
			{
				if ((value as Facebook.JsonArray).Count == 0)
				{
					return null;
				}
				if (propType == typeof(UidsList))
				{
					return new UidsList((JsonArray)value);
				}
				if (propType == typeof(UrlList))
				{
					return new UrlList((JsonArray)value);
				}				
				return value;
			}
			else if (propType == typeof(Uid))
			{
				long? val = FixValueForId(value);
				return new Uid(val);
			}
			else if (propType == typeof(AppId))
			{
				long? val = FixValueForId(value);
				return new AppId(val);
			}
			else if (propType == typeof(DomainId))
			{
				long? val = FixValueForId(value);
				return new DomainId(val);
			}
			else if (propType == typeof(StatusId))
			{
				long? val = FixValueForId(value);
				return new StatusId(val);
			}				
			else if (propType == typeof(RequestId))
			{
				long? val = FixValueForId(value);
				return new RequestId(val);
			}
			else if (propType == typeof(CheckinId))
			{
				long? val = FixValueForId(value);
				return new CheckinId(val);
			}
			else if (propType == typeof(PageId))
			{
				long? val = FixValueForId(value);
				return new PageId(val);
			}
			else if (propType == typeof(PostId))
			{
				long? val = FixValueForId(value);
				return new PostId(val);
			}
			else if (propType == typeof(EventId))
			{
				long? val = FixValueForId(value);
				return new EventId(val);
			}
			else if (propType == typeof(FriendListId))
			{
				long? val = FixValueForId(value);
				return new FriendListId(val);
			}
			else if (propType == typeof(LinkId))
			{
				long? val = FixValueForId(value);
				return new LinkId(val);
			}
			else if (propType == typeof(GroupId))
			{
				long? val = FixValueForId(value);
				return new GroupId(val);
			}
			else if (propType == typeof(ThreadId))
			{
				long? val = FixValueForId(value);
				return new ThreadId(val);
			}
			else if (propType == typeof(NotificationId))
			{
				long? val = FixValueForId(value);
				return new NotificationId(val);
			}
			else if (propType == typeof(OfferId))
			{
				long? val = FixValueForId(value);
				return new OfferId(val);
			}
			else if (propType == typeof(MilestoneId))
			{
				long? val = FixValueForId(value);
				return new MilestoneId(val);
			}
			else if (propType == typeof(PhotoId))
			{
				long? val = FixValueForId(value);
				return new PhotoId(val);
			}
			else if (propType == typeof(ReviewId))
			{
				long? val = FixValueForId(value);
				return new ReviewId(val);
			}
			else if (propType == typeof(MessageId))
			{
				long? val = FixValueForId(value);
				return new MessageId(val);
			}
			else if (propType == typeof(VideoId))
			{
				long? val = FixValueForId(value);
				return new VideoId(val);
			}
			else
				throw new NotImplementedException();
		}

		private long? FixValueForId(object value)
		{
			long? val;
			if (value is String)
			{
				val = long.Parse(value as String);
			}
			else
			{
				val = value as long?;
			}
			return val;
		}
		
		private object ParseFacebookEnum(Type propType, string value)
		{
			if (value.IsNullOrEmpty())
				return ReflectionHelper.GetDefaultValue(propType);
			value = value.Split('_').StringConcat("");
			return Enum.Parse(propType, value, true);
		}

		private static long ParseLong(string value)
		{
			if (value.IsNullOrEmpty())
				return 0;
			return Int64.Parse(value);
		}

		private static decimal ParseDecimal(string value)
		{
			if (value.IsNullOrEmpty())
				return 0;
			return Decimal.Parse(value);
		}

		private object ConvertNativePropertyFromJson(string objectName, string propertyName, string value)
		{
			switch (objectName)
			{
				case "friend_info":
					switch (propertyName)
					{
						case "uid1":
						case "uid2":
							return ParseLong(value);
					}
					break;
			}
			throw new NotImplementedException();
		}


		public static bool ParseBool(string value)
		{
			switch (value.ToLower())
			{
				case "true":
				case "yes":
					return true;
				case "false":
				case "no":
					return false;
				case "0":
					return false;
				default:
					{
						int n = 0;
						if (Int32.TryParse(value, out n))
							return n != 0;
						return false;
					}
			}
		}
		public static DateTime ParseFacebookDateTime(long i)
		{
			return DateHelper.ConvertDoubleToDate(Convert.ToDouble(i));
		}
	}
}
