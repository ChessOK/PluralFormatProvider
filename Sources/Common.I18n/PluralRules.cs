namespace Common.I18n
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal sealed class PluralRules
    {
        class PluralRulesCollection : List<Predicate<object>>
        {
        }

        private readonly PluralRulesCollection _localeRules;

        public PluralRules(CultureInfo cultureInfo)
        {
            var languageCode = cultureInfo.TwoLetterISOLanguageName;
            if (!Global.ContainsKey(languageCode))
            {
                throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                    "Language with code {0} is not supported yet", languageCode));
            }

            _localeRules = Global[languageCode];
        }

        public int GetIndex(object arg)
        {
            for (int i = 0; i < _localeRules.Count; i++)
            {
                if (_localeRules[i](arg))
                {
                    return i;
                }
            }

            return _localeRules.Count;
        }

        public int RulesCount
        {
            get
            {
                return _localeRules.Count + 1;
            }
        }

        private static readonly Dictionary<string, PluralRulesCollection> _global 
            = new Dictionary<string, PluralRulesCollection>();

        private static Dictionary<string, PluralRulesCollection> Global { get { return _global; } }

        static PluralRules()
        {
            InitializeRules();
        }

        private static void AddRule(string languages, PluralRulesCollection rules)
        {
            var langArray = languages.Split(' ');
            foreach(var item in langArray)
            {
                _global.Add(item, rules);
            }
        }

        /// <summary>
        /// Инициализация таблицы правил.
        /// Правила взяты из http://www.unicode.org/repos/cldr/trunk/common/supplemental/plurals.xml
        /// </summary>
        private static void InitializeRules()
        {
            AddRule(
                "az bm bo dz fa id ig ii hu ja jv ka kde kea km kn ko lo ms my sah ses sg th to tr vi wo yo zh",
                new PluralRulesCollection());
            AddRule(
                "ar",
                new PluralRulesCollection
                    {
                        arg => IsN(arg, 0),
                        arg => IsN(arg, 1),
                        arg => IsN(arg, 2),
                        arg => Nmod100InAb(arg, 3, 10),
                        arg => Nmod100InAb(arg, 11, 99) });
            AddRule(
                "asa af bem bez bg bn brx ca cgg chr da de dv ee el en eo es et eu fi fo fur fy gl gsw gu ha haw he is it jmc kaj kcg kk kl ksb ku lb lg mas ml mn mr nah nb nd ne nl nn no nr ny nyn om or pa pap ps pt rof rm rwk saq seh sn so sq ss ssy st sv sw syr ta te teo tig tk tn ts ur wae ve vun xh xog zu",
                new PluralRulesCollection { Is1 });
            AddRule(
                "ak am bh fil tl guw hi ln mg nso ti wa",
                new PluralRulesCollection { arg => Is1(arg) || IsN(arg, 0) });
            AddRule(
                "ff fr kab",
                new PluralRulesCollection { arg => WithinAb(arg, 0, 2) && !IsN(arg, 2) });
            AddRule(
                "lv",
                new PluralRulesCollection
                    {
                        arg => IsN(arg, 0),
                        arg => Nmod10IsP(arg, 1) && !Nmod100IsP(arg, 11) });
            AddRule(
                "iu kw naq se sma smi smj smn sms",
                new PluralRulesCollection
                    {
                        arg => IsN(arg, 1),
                        arg => IsN(arg, 2) });
            AddRule(
                "ga",
                new PluralRulesCollection
                    {
                        arg => IsN(arg, 1),
                        arg => IsN(arg, 2),
                        arg => IsN(arg, 3) || IsN(arg, 4) || IsN(arg, 5) || IsN(arg, 6),
                        arg => IsN(arg, 7) || IsN(arg, 8) || IsN(arg, 9) || IsN(arg, 10) });
            AddRule(
                "ro mo",
                new PluralRulesCollection
                    {
                        Is1,
                        arg => IsN(arg, 0) || !IsN(arg, 1) && Nmod100InAb(arg, 1, 19) });
            AddRule(
                "lt",
                new PluralRulesCollection
                    {
                        arg => Nmod10IsP(arg, 1) && !Nmod100InAb(arg, 11, 19),
                        arg => Nmod10InAb(arg, 2, 9) && !Nmod100InAb(arg, 11, 19) });
            AddRule(
                "be bs hr ru sh sr uk",
                new PluralRulesCollection
                    {
                        arg => Nmod10IsP(arg, 1) && !Nmod100IsP(arg, 11),
                        arg => Nmod10InAb(arg, 2, 4) && !Nmod100InAb(arg, 12, 14),
                        arg => Nmod10IsP(arg, 0) || Nmod10InAb(arg, 5, 9) || Nmod100InAb(arg, 11, 14) });
            AddRule(
                "cs sk",
                new PluralRulesCollection
                    {
                        Is1,
                        arg => IsN(arg, 2) || IsN(arg, 3) || IsN(arg, 4) });
            AddRule(
                "pl",
                new PluralRulesCollection
                    {
                        Is1,
                        arg => Nmod10InAb(arg, 2, 4) && !Nmod100InAb(arg, 12, 14),
                        arg => !Is1(arg) && Nmod10InAb(arg, 0, 1) || Nmod10InAb(arg, 5, 9) || Nmod100InAb(arg, 12, 14),
                    });
            AddRule(
                "sl",
                new PluralRulesCollection
                    {
                        arg => Nmod100IsP(arg, 1),
                        arg => Nmod100IsP(arg, 2),
                        arg => Nmod100InAb(arg, 3, 4) });
            AddRule(
                "mt",
                new PluralRulesCollection
                    {
                        Is1,
                        arg => IsN(arg, 0) || Nmod100InAb(arg, 2, 10),
                        arg => Nmod100InAb(arg, 11, 19) });
            AddRule("mk", 
                new PluralRulesCollection { arg => Nmod10IsP(arg, 1) && !IsN(arg, 11) });
            AddRule(
                "cy",
                new PluralRulesCollection
                    {
                        arg => IsN(arg, 0),
                        arg => IsN(arg, 1),
                        arg => IsN(arg, 2),
                        arg => IsN(arg, 3),
                        arg => IsN(arg, 6),
                    });
            AddRule(
                "gv",
                new PluralRulesCollection { arg => Nmod10InAb(arg, 1, 2) || NmodMisP(arg, 20, 0) });
        }

        #region Helper methods

        public static bool WithinAb(object value, uint a, uint b)
        {
            return NWithinAb(value, a, b);
        }

        public static bool Is1(object value)
        {
            return IsN(value, 1);
        }

        public static bool Nmod10IsP(object value, uint p)
        {
            return NmodMisP(value, 10, p);
        }

        public static bool Nmod100IsP(object value, uint p)
        {
            return NmodMisP(value, 100, p);
        }

        public static bool NmodMisP(object value, uint m, uint p)
        {
            return NmodMisInAb(value, m, p, p);
        }

        public static bool Nmod10InAb(object value, uint a, uint b)
        {
            return NmodMisInAb(value, 10, a, b);
        }

        public static bool Nmod100InAb(object value, uint a, uint b)
        {
            return NmodMisInAb(value, 100, a, b);
        }

        public static bool IsN(object value, uint n)
        {
            var typeCode = Type.GetTypeCode(value.GetType());
            switch (typeCode)
            {
                case TypeCode.SByte: 
                case TypeCode.Int16: 
                case TypeCode.Int32:
                    return Math.Abs((Int32)value) == n;

                case TypeCode.Int64: 
                    return Math.Abs((Int64)value) == n;

                case TypeCode.Byte: 
                case TypeCode.UInt16: 
                case TypeCode.UInt32: 
                    return (UInt32)value == n;

                case TypeCode.UInt64: 
                    return (UInt64)value == n;

                case TypeCode.Single: 
                case TypeCode.Double: 
                    return Math.Abs((Double)value) == n;

                case TypeCode.Decimal: 
                    return Math.Abs((Decimal)value) == n;

                default:
                    return false;
            }
        }

        public static bool NmodMisInAb(object value, uint m, uint a, uint b)
        {
            var typeCode = Type.GetTypeCode(value.GetType());
            switch (typeCode)
            {
                case TypeCode.SByte: 
                case TypeCode.Int16: 
                case TypeCode.Int32:
                    var int32Value = Math.Abs((Int32)value);
                    return ((int32Value % m) >= a) && ((int32Value % m) <= b);

                case TypeCode.Int64:
                    var int64Value = Math.Abs((Int64)value);
                    return ((int64Value % m) >= a) && ((int64Value % m) <= b);

                case TypeCode.Byte: 
                case TypeCode.UInt16: 
                case TypeCode.UInt32: 
                    return (((UInt32)value % m) >= a) && (((UInt32)value % m) <= b);

                case TypeCode.UInt64: 
                    return (((UInt64)value % m) >= a) && (((UInt64)value % m) <= b);

                case TypeCode.Single: 
                case TypeCode.Double:
                    var doubleValue = Math.Abs((Double)value);
                    return ((doubleValue % m) >= a) && ((doubleValue % m) <= b) && Math.Truncate(doubleValue) == doubleValue;

                case TypeCode.Decimal:
                    var decimalValue = Math.Abs((Decimal)value);
                    return ((decimalValue % m) >= a) && ((decimalValue % m) <= b) && Math.Truncate(decimalValue) == decimalValue;

                default:
                    return false;
            }
        }

        public static bool NWithinAb(object value, uint a, uint b)
        {
            var typeCode = Type.GetTypeCode(value.GetType());
            switch (typeCode)
            {
                case TypeCode.SByte: 
                case TypeCode.Int16: 
                case TypeCode.Int32:
                    var int32Value = Math.Abs((Int32)value);
                    return (int32Value >= a) && (int32Value <= b);

                case TypeCode.Int64:
                    var int64Value = Math.Abs((Int64)value);
                    return (int64Value >= a) && (int64Value <= b);

                case TypeCode.Byte: 
                case TypeCode.UInt16: 
                case TypeCode.UInt32: 
                    return ((UInt32)value >= a) && ((UInt32)value <= b);

                case TypeCode.UInt64: 
                    return ((UInt64)value >= a) && ((UInt64)value <= b);

                case TypeCode.Single: 
                case TypeCode.Double:
                    var doubleValue = Math.Abs((Double)value);
                    return (doubleValue >= a) && (doubleValue <= b);

                case TypeCode.Decimal:
                    var decimalValue = Math.Abs((Decimal)value);
                    return (decimalValue >= a) && (decimalValue <= b);

                default:
                    return false;
            }
        }

        #endregion
    }
}