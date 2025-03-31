using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTK.Results.Core
{
    public struct Error : IEquatable<Error>
    {
        private readonly Dictionary<string, object> _metadata;

        public static Error Create(string code, string message, string detail = default, string instance = default, string type = default, int statusCode = 400)
        {
            return new Error(code, message, detail, instance, type, statusCode);
            //ProblemDetails details = new ProblemDetails();
        }

        private Error(string code, string message, string detail = default, string instance = default, string type = default, int statusCode = 400)
        {
            Code = code;
            Message = message;
            Detail = detail;
            Instance = instance;
            Type = type;

            StatusCode = statusCode;
            _metadata = new Dictionary<string, object>(10)
            {
                [nameof(Code)] = code
            };
        }
        public string Message { get; private set; }
        public string Code { get; private set; }
        public string Type { get; private set; }
        public string Title => Message;
        public string Detail { get; private set; }
        public string Instance { get; private set; }
        public int StatusCode { get; private set; }

        public Dictionary<string, object> Metadata => new Dictionary<string, object>
        {
            [nameof(Metadata).ToLower()] = _metadata
        };

        public static Error None => new Error(string.Empty, string.Empty);
        public static Error Null => Error.Create("001", "Object of type {0} is null");

        public Error WithMetadata(string code, object data)
        {
            //_metadata.Add(code, data);
            AddMetadata(code, data);
            return this;
        }

        public Error WithMetadata(Dictionary<string, object> metadata)
        {
            //_metadata.Add(code, data);
            foreach (var meta in metadata)
            {
                AddMetadata(meta.Key, meta.Value);
            }
            return this;
        }

        public Error WithStatusCode(int status)
        {
            StatusCode = status;
            return this;
            //return this with { StatusCode = status };
        }

        public Error WithType(string type)
        {
            Type = type;
            return this;
            //return this with { Type = type };
        }

        public Error WithMessage(string message)
        {
            Message = message;
            return this;
            //return this with { Message = message };
        }

        public Error WithMessageTransform(Func<string, string> transform)
        {
            Message = transform(Message);
            return this;
            //return this with { Message = transform(Message) };
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Code);
            hashCode.Add(Message);
            hashCode.Add(Type);
            hashCode.Add(StatusCode);
            hashCode.Add(Instance);
            hashCode.Add(Detail);
            foreach (var item in _metadata)
            {
                hashCode.Add(item.Key);
                hashCode.Add(item.Value);
            }

            return hashCode.ToHashCode();
        }

        public bool Equals(Error other)
        {
            if (Code != other.Code || Message != other.Message || Type != other.Type || StatusCode != other.StatusCode || Detail != other.Detail || Instance != other.Instance)
            {
                return false;
            }


            if (Metadata is null) { return other.Metadata is null; }

            return other.Metadata != null && CompareMetadata(_metadata, (Dictionary<string, object>)other.Metadata[nameof(Metadata).ToLower()]);
        }

        private static bool CompareMetadata(Dictionary<string, object> metadata, Dictionary<string, object> otherMetadata)
        {
            if (ReferenceEquals(metadata, otherMetadata))
            {
                return true;
            }

            if (metadata.Count != otherMetadata.Count)
            {
                return false;
            }

            var sameKeys = metadata.Keys.OrderBy(k => k).SequenceEqual(otherMetadata.Keys.OrderBy(k => k));

            if (!sameKeys)
            {
                return false;
            }

            foreach (var keyValuePair in metadata)
            {
                //if (!otherMetadata.TryGetValue(keyValuePair.Key, out var otherValue) ||!keyValuePair.Value.Equals(otherValue))
                //{
                //    return false;
                //}
                var otherValue = otherMetadata[keyValuePair.Key];
                var oneValue = metadata[keyValuePair.Key];
                var isEquals = oneValue.Equals(otherValue);
                var isReferenceEquals = ReferenceEquals(otherValue, oneValue);

                if (isEquals is false /*|| isReferenceEquals is false*/)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator ==(Error one, Error other) => one.Equals(other);
        public static bool operator !=(Error one, Error other) => !one.Equals(other);

        public override bool Equals(object obj)
        {
            return obj is Error ? this.Equals((Error)obj) : false;
            //return base.Equals(obj);
        }


        private void AddMetadata(string key, object value)
        {
            if (_metadata.ContainsKey(key))
            {
                _metadata[key] = value;
            }
            else
            {
                _metadata.Add(key,value);
            }
        }
    }
}
