namespace naru.math
{
    /// <summary>
    /// Use this class to display the linear units in a dropdownlist
    /// </summary>
    /// <remarks></remarks>
    public class LinearUnitClass
    {
        private NumberFormatting.LinearUnits m_eLinearUnit;

        private string m_sDisplayName;
        public LinearUnitClass(string sDisplayName, NumberFormatting.LinearUnits eLinearUnit)
        {
            m_sDisplayName = sDisplayName;
            m_eLinearUnit = eLinearUnit;
        }

        public LinearUnitClass(NumberFormatting.LinearUnits eLinearUnit)
        {
            m_sDisplayName = NumberFormatting.GetUnitsAsString(eLinearUnit);
            m_eLinearUnit = eLinearUnit;
        }

        public NumberFormatting.LinearUnits LinearUnit
        {
            get { return m_eLinearUnit; }
        }

        public NumberFormatting.AreaUnits AreaUnit
        {
            get { return NumberFormatting.GetAreaUnitsRaw(m_eLinearUnit); }
        }

        public NumberFormatting.VolumeUnits VolumeUnit
        {
            get { return NumberFormatting.GetVolumeUnitsRaw(m_eLinearUnit); }
        }

        public override string ToString()
        {
            return m_sDisplayName;
        }

        public object GetUnitsAsString(bool bParentheses = false)
        {
            return NumberFormatting.GetUnitsAsString(m_eLinearUnit, bParentheses);
        }
    }
}