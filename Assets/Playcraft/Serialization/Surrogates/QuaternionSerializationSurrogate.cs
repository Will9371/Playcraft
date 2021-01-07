// Credit: Game Dev Guide
// Source: https://www.youtube.com/watch?v=5roZtuqZyuw

using System.Runtime.Serialization;
using UnityEngine;

namespace Playcraft
{
    public class QuaternionSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var value = (Quaternion)obj;
            info.AddValue("x", value.x);
            info.AddValue("y", value.y);
            info.AddValue("z", value.z);
            info.AddValue("w", value.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var value = (Quaternion)obj;
            value.x = (float)info.GetValue("x", typeof(float));
            value.y = (float)info.GetValue("y", typeof(float));
            value.z = (float)info.GetValue("z", typeof(float));
            value.w = (float)info.GetValue("w", typeof(float));
            obj = value;
            return obj;
        }
    }
}