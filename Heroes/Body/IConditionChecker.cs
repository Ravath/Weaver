using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace Weaver.Heroes.Body
{
    public enum ConditionOperation
    {
        INF,EQ,SUP
    }

    public interface IConditionChecker<T> where T : Module
    {
        public bool Check(T toCheck);
        public string ConditionToString() ;

        /// <summary>
        /// Converts a string to a condition tree.
        /// </summary>
        /// <param name="formula">The condition macro.</param>
        /// <returns>A condition.</returns>
        public static IConditionChecker<T> Convert(string formula)
        {
            // currently do only 'AND'.
            // TODO : add 'OR' management.
            string[] cdts = formula.Split("&");
            if(cdts.Length == 1)
                return DefaultAttributeChecker<T>.Convert(formula);
            else
            {
                AndCondition<T> return_cdt = new();
                int index = 0;
                AndCondition<T> iter = return_cdt;

                iter.A = DefaultAttributeChecker<T>.Convert(cdts[index++]);
                iter.B = new AndCondition<T>();

                while(index < cdts.Length-1)
                {
                    iter = (AndCondition<T>)iter.B;
                    iter.A = DefaultAttributeChecker<T>.Convert(cdts[index++]);
                    iter.B = new AndCondition<T>();
                }
                iter.B = DefaultAttributeChecker<T>.Convert(cdts[index++]);
                
                return return_cdt;
            }
        }
    }

    public class AndCondition<T> : IConditionChecker<T> where T : Module
    {
        public IConditionChecker<T> A { get; set; }
        public IConditionChecker<T> B { get; set; }

        public bool Check(T toCheck)
        {
            return A.Check(toCheck) && B.Check(toCheck);
        }
        public string ConditionToString() {
            return string.Format("{0}&{1}", A.ConditionToString(), B.ConditionToString());
        }
    }

    public class DefaultAttributeChecker<T> : IConditionChecker<T> where T : Module
    {
        public string AttributePath { get; set; }
        public decimal AttributeThreshold { get; set; }
        public ConditionOperation Condition { get; set; }

        public bool Check(T toCheck)
        {
            // COMMENT because compilatin and Attribute not found.
            // Attribute a = (Attribute)toCheck.GetRegistered(AttributePath);

            // return Condition switch
            // {
            //     ConditionOperation.EQ  => a.BaseValue == AttributeThreshold,
            //     ConditionOperation.INF => a.BaseValue <= AttributeThreshold,
            //     ConditionOperation.SUP => a.BaseValue >= AttributeThreshold,
            //     _ => throw new NotImplementedException(),
            // };
            return true;
        }
        
        public string ConditionToString() {
            string co = Condition switch
            {
                ConditionOperation.EQ => "=",
                ConditionOperation.INF => "<",
                ConditionOperation.SUP => ">",
                _ => throw new NotImplementedException(),
            };
            return string.Format("{0}{1}{2}", AttributePath, co, AttributeThreshold);
        }

        public static DefaultAttributeChecker<T> Convert(string formula)
        {
            Contract.Assert(formula != null);

            // Use regex to get the structure
            Regex rg = new(@"[<>=]");
            MatchCollection matches = rg.Matches(formula);
            if(matches.Count!=1)
                throw new ArgumentException("Error in condition "+formula);
            
            // Get the different substrings
            ConditionOperation co = matches[0].Value switch
            {
                "=" => ConditionOperation.EQ,
                ">" => ConditionOperation.SUP,
                "<" => ConditionOperation.INF,
                _ => throw new NotImplementedException(),
            };
            string val = formula[(matches[0].Index+matches[0].Length)..];

            // Convert to actual checker
            return new DefaultAttributeChecker<T>()
            {
                AttributePath = formula[..matches[0].Index],
                AttributeThreshold = decimal.Parse(val),
                Condition = co
            };
        }
    }
}