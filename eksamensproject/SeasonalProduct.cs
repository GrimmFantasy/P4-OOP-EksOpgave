﻿namespace EksOP
{
    public class SeasonalProduct : Product
    {
        private bool _stat;
        public bool IsActive 
        { 
            get => _stat;
            set 
            {
                if (DateTime.Today == SeasonStartDate)
                {
                    value = true;
                }
                else if (DateTime.Today == SeasonEndDate) 
                { 
                    value= false;
                }
                _stat = value;
            } 
        }   
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }
        public SeasonalProduct(string name, decimal price, int state, DateTime end) : base(name, price, state)
        {
           SeasonEndDate = end;

        }
    }

}