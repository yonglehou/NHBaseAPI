using NHBaseThrift.Analyzing;
using NHBaseThrift.Attributes;
using NHBaseThrift.Enums;
using NHBaseThrift.Helpers;
using NHBaseThrift.Network;
using NHBaseThrift.Proxies;

namespace NHBaseThrift.TypeProcessors
{
    /// <summary>
    ///     Int64����ThriftЭ���ֶδ��������ṩ����صĻ�������
    /// </summary>
    public class Int64ThriftTypeProcessor : ThriftTypeProcessor
    {
        #region Constructor.

        /// <summary>
        ///     Int64����ThriftЭ���ֶδ��������ṩ����صĻ�������
        /// </summary>
        public Int64ThriftTypeProcessor()
        {
            _supportedType = typeof(long);
            _expectedDataSize = 8;
        }

        #endregion

        #region Overrides of IntellectTypeProcessor

        /// <summary>
        ///     �ӵ������ͻ�����ת��ΪԪ����
        /// </summary>
        /// <param name="proxy">�ڴ�Ƭ�δ�����</param>
        /// <param name="attribute">�ֶ�����</param>
        /// <param name="analyseResult">�������</param>
        /// <param name="target">Ŀ�����ʵ��</param>
        /// <param name="isArrayElement">��ǰд���ֵ�Ƿ�Ϊ����Ԫ�ر�ʾ</param>
        public override void Process(IMemorySegmentProxy proxy, ThriftPropertyAttribute attribute, ToBytesAnalyseResult analyseResult, object target, bool isArrayElement = false, bool isNullable = false)
        {
            long value = analyseResult.GetValue<long>(target);
            proxy.WriteSByte((sbyte)attribute.PropertyType);
            proxy.WriteInt16(((short)attribute.Id).ToBigEndian());
            proxy.WriteInt64(value.ToBigEndian());
        }

        /// <summary>
        ///     ��Ԫ����ת��Ϊ�������ͻ�����
        /// </summary>
        /// <param name="instance">Ŀ�����</param>
        /// <param name="result">�������</param>
        /// <param name="container">������������</param>
        public override GetObjectResultTypes Process(object instance, GetObjectAnalyseResult result, INetworkDataContainer container)
        {
            long value;
            if(!container.TryReadInt64(out value)) return GetObjectResultTypes.NotEnoughData;
            result.SetValue(instance, value.ToLittleEndian());
            return GetObjectResultTypes.Succeed;
        }

        #endregion
    }
}