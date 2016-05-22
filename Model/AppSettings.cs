using System;
using System.Linq;
using Model;
using Model.Tables;


namespace Model
{
    public class AppSettings
    {
        private readonly DataBase _data;

        public AppSettings(DataBase data)
        {
            _data = data;
        }

        public decimal DefaultCosts
        {
            get
            {
                var value = "0";
                GetValue("DC", "Default Costs", ref value);
                return Convert.ToDecimal(value);
            }
            set
            {
                SetValue("DC", "Default Costs", value.ToString());
            }
        }

        public decimal DefaultCostsSingle
        {
            get
            {
                var value = "0";
                GetValue("DCS", "Default Costs Single", ref value);
                return Convert.ToDecimal(value);
            }
            set
            {
                SetValue("DCS", "Default Costs Single", value.ToString());
            }
        }

 
        private void GetValue(string code, string description, ref string value)
        {
            var setting = _data.Settings.FirstOrDefault(c => c.Code == code);
            if (setting != null)
            {
                if (setting.Value != null)
                {
                    value = setting.Value;
                    return;
                }
                setting.Value = value;
                _data.SubmitChanges();
                return;
            }
            _data.Settings.Add(new Settings
            {
                Code = code,
                Description = description,
                Value = value,
                ModifiedDate = DateTime.Now,
                CreateDate = DateTime.Now
            });
            _data.SubmitChanges();
        }

        private void SetValue(string code, string description, string value)
        {
            var setting = _data.Settings.FirstOrDefault(c => c.Code == code);
            if (setting != null)
                setting.Value = value;
            else
                _data.Settings.Add(new Settings
                {
                    Code = code,
                    Description = description,
                    Value = value,
                    ModifiedDate = DateTime.Now
                });
            _data.SubmitChanges();
        }

    }
}