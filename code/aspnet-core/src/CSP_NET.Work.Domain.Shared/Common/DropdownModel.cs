using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.Common
{
    public interface IDropdownAppService
    {
        Task<List<DropdownItemWork>> GetDropdownItems();
    }
    public interface IDropdownAppService<TId> where TId : struct
    {
        Task<List<DropdownItem<TId>>> GetDropdownItems();
    }
    public interface IDropdownValueAppService
    {
        Task<List<DropdownItemValue>> GetDropdownItemValues();
    }
    public interface IDropdownValueAppService<TId> where TId : struct
    {
        Task<List<DropdownItemValue<TId>>> GetDropdownItemValues();
    }
    public interface IDropdownValueAppService<TId, TVal> where TId : struct where TVal : struct
    {
        Task<List<DropdownItemValue<TId, TVal>>> GetDropdownItemValues();
    }

    public interface IDropdownItem
    {
        string Code { get; set; }
        string Name { get; set; }
        bool Selected { get; set; }
        bool Hidden { get; set; }
    }
    public class DropdownItemWork : IDropdownItem
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public bool Hidden { get; set; }

        public DropdownItemValue ToDropdownItemValue()
        {
            IDropdownItem t = (IDropdownItem)this;
            DropdownItemValue result = t as DropdownItemValue;
            result.Value = result.Id;
            return result;
        }
    }
    public class DropdownItem<TId> : IDropdownItem where TId : struct
    {
        public TId Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public bool Hidden { get; set; }
    }
    public class DropdownItemValue : IDropdownItem
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
        public bool Hidden { get; set; }
        public static object GetPropertyValue(DropdownItemValue item, string propertyName)
        {
            var propertyInfo = typeof(DropdownItemValue).GetProperty(propertyName);
            return propertyInfo?.GetValue(item);
        }
    }
    public class DropdownItemValue<TVal> : IDropdownItem where TVal : struct
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public TVal Value { get; set; }
        public bool Selected { get; set; }
        public bool Hidden { get; set; }
    }
    public class DropdownItemValue<TId, TVal> : IDropdownItem where TId : struct where TVal : struct
    {
        public TId Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public TVal Value { get; set; }
        public bool Selected { get; set; }
        public bool Hidden { get; set; }
    }
}
